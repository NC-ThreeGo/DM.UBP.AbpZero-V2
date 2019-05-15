using Abp.Authorization.Roles;
using Abp.Authorization.Users;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.Notifications;
using Abp.Runtime.Session;
using DM.UBP.Authorization.Roles;
using DM.UBP.Authorization.Users.Dto;
using DM.UBP.Authorization.Users.Exporting;
using DM.UBP.Notifications;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DM.UBP.Authorization.Users
{
    public class WX_UserAppService: UserAppService, WX_IUserAppService
    {
        private readonly RoleManager _roleManager;
        private readonly IUserEmailer _userEmailer;
        private readonly IUserListExcelExporter _userListExcelExporter;
        private readonly INotificationSubscriptionManager _notificationSubscriptionManager;
        private readonly IAppNotifier _appNotifier;
        private readonly IRepository<RolePermissionSetting, long> _rolePermissionRepository;
        private readonly IRepository<UserPermissionSetting, long> _userPermissionRepository;
        private readonly IRepository<UserRole, long> _userRoleRepository;
        private readonly IUserPolicy _userPolicy;

        public WX_UserAppService(RoleManager roleManager, IUserEmailer userEmailer, IUserListExcelExporter userListExcelExporter, INotificationSubscriptionManager notificationSubscriptionManager, IAppNotifier appNotifier, IRepository<RolePermissionSetting, long> rolePermissionRepository, IRepository<UserPermissionSetting, long> userPermissionRepository, IRepository<UserRole, long> userRoleRepository, IUserPolicy userPolicy) 
            : base(roleManager, userEmailer, userListExcelExporter, notificationSubscriptionManager, appNotifier, rolePermissionRepository, userPermissionRepository, userRoleRepository, userPolicy)
        {
            _roleManager = roleManager;
            _userEmailer = userEmailer;
            _userListExcelExporter = userListExcelExporter;
            _notificationSubscriptionManager = notificationSubscriptionManager;
            _appNotifier = appNotifier;
            _rolePermissionRepository = rolePermissionRepository;
            _userPermissionRepository = userPermissionRepository;
            _userRoleRepository = userRoleRepository;
            _userPolicy = userPolicy;
        }

        /// <summary>
        /// 获取所有用户
        /// </summary>
        /// <returns></returns>

        public async Task<List<WX_UserListDto>> GetAllUsers()
        {
            var query = UserManager.Users
               .Include(u => u.Roles);

            var users = await query.ToListAsync();

            var userListDtos = users.MapTo<List<WX_UserListDto>>();
            //await FillRoleNames(userListDtos);

            return userListDtos;
        }

        public async Task<long> CreateOrUpdateUser(WX_CreateOrUpdateUserInput input)
        {
            if (input.User.Id.HasValue)
            {
                return await UpdateUserAsync(input);
            }
            else
            {
                return await CreateUserAsync(input);
            }
        }

        protected virtual async Task<long> UpdateUserAsync(WX_CreateOrUpdateUserInput input)
        {
            Debug.Assert(input.User.Id != null, "input.User.Id should be set.");

            var user = await UserManager.FindByIdAsync(input.User.Id.Value);

            //Update user properties
            input.User.MapTo(user); //Passwords is not mapped (see mapping configuration)

            if (input.SetRandomPassword)
            {
                input.User.Password = User.CreateRandomPassword();
            }

            if (!input.User.Password.IsNullOrEmpty())
            {
                CheckErrors(await UserManager.ChangePasswordAsync(user, input.User.Password));
            }

            CheckErrors(await UserManager.UpdateAsync(user));

            //Update roles
            CheckErrors(await UserManager.SetRoles(user, input.AssignedRoleNames));

            if (input.SendActivationEmail)
            {
                user.SetNewEmailConfirmationCode();
                await _userEmailer.SendEmailActivationLinkAsync(user, input.User.Password);
            }

            return input.User.Id.Value;
        }

        protected virtual async Task<long> CreateUserAsync(WX_CreateOrUpdateUserInput input)
        {
            if (AbpSession.TenantId.HasValue)
            {
                await _userPolicy.CheckMaxUserCountAsync(AbpSession.GetTenantId());
            }

            var user = input.User.MapTo<WX_User>(); //Passwords is not mapped (see mapping configuration)
            user.TenantId = AbpSession.TenantId;
            
            //Set password
            if (!input.User.Password.IsNullOrEmpty())
            {
                CheckErrors(await UserManager.PasswordValidator.ValidateAsync(input.User.Password));
            }
            else
            {
                input.User.Password = User.CreateRandomPassword();
            }

            user.Password = new PasswordHasher().HashPassword(input.User.Password);
            user.ShouldChangePasswordOnNextLogin = input.User.ShouldChangePasswordOnNextLogin;

            //Assign roles
            user.Roles = new Collection<UserRole>();
            foreach (var roleName in input.AssignedRoleNames)
            {
                var role = await _roleManager.GetRoleByNameAsync(roleName);
                user.Roles.Add(new UserRole(AbpSession.TenantId, user.Id, role.Id));
            }

            var identityResult = await UserManager.CreateAsync(user);
            CheckErrors(identityResult);

            await CurrentUnitOfWork.SaveChangesAsync(); //To get new user's Id.

            //Notifications
            await _notificationSubscriptionManager.SubscribeToAllAvailableNotificationsAsync(user.ToUserIdentifier());
            await _appNotifier.WelcomeToTheApplicationAsync(user);

            //Send activation email
            if (input.SendActivationEmail)
            {
                user.SetNewEmailConfirmationCode();
                await _userEmailer.SendEmailActivationLinkAsync(user, input.User.Password);
            }

            return identityResult.MapTo<long>();
        }
    }
}
