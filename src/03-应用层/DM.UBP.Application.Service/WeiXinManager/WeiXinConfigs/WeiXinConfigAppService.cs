//------------------------------------------------------------
// All Rights Reserved , Copyright (C)  
// 版本：1.0
/// <author>
///		<name></name>
///		<date>0001/1/1 0:00:00</date>
/// </author>
//------------------------------------------------------------

using System.Threading.Tasks;
using System.Collections.Generic;
using Abp.Auditing;
using Abp.AutoMapper;
using Abp.Authorization;
using Abp.Application.Services.Dto;
using DM.UBP.Domain.Entity.WeiXinManager;
using DM.UBP.Domain.Service.WeiXinManager.WeiXinConfigs;
using DM.UBP.Domain.Service.WeiXinManager;
using DM.UBP.Application.Dto.WeiXinManager.WeiXinConfigs;
using System.Linq;
using System.Linq.Dynamic;
using DM.UBP.Dto;
using Abp.Runtime.Caching;
using Newtonsoft.Json.Linq;
using DM.UBP.Organizations;
using DM.UBP.Authorization.Users;
using DM.UBP.Authorization.Users.Dto;
using System.Data;
using DM.UBP.Organizations.Dto;

namespace DM.UBP.Application.Service.WeiXinManager.WeiXinConfigs
{
    /// <summary>
    /// 的Application.Service
    /// <summary>
    [AbpAuthorize(AppPermissions_WeiXinManager.Pages_WeiXinManager_WeiXinConfigs)]
    public class WeiXinConfigAppService : IWeiXinConfigAppService
    {
        private readonly IWeiXinConfigManager _WeiXinConfigManager;
        private readonly IOrganizationUnitAppService _organizationUnitAppService;
        private readonly IUserAppService _userAppService;
        private readonly ICacheManager _cacheManager;

        public WeiXinConfigAppService(
           IWeiXinConfigManager weixinconfigmanager,
           IOrganizationUnitAppService organizationUnitAppService,
           IUserAppService userAppService,
            ICacheManager cacheManager
           
           )
        {
            _WeiXinConfigManager = weixinconfigmanager;
            _organizationUnitAppService = organizationUnitAppService;
            _cacheManager = cacheManager;
            _userAppService = userAppService;
        }

        public async Task<PagedResultDto<WeiXinConfigOutputDto>> GetWeiXinConfigs()
        {
            var entities = await _WeiXinConfigManager.GetAllWeiXinConfigsAsync();
            var listDto = entities.MapTo<List<WeiXinConfigOutputDto>>();

            return new PagedResultDto<WeiXinConfigOutputDto>(
            listDto.Count,
            listDto
            );
        }
        public async Task<PagedResultDto<WeiXinConfigOutputDto>> GetWeiXinConfigs(PagedAndSortedInputDto input)
        {
            var entities = await _WeiXinConfigManager.GetAllWeiXinConfigsAsync();
            if (string.IsNullOrEmpty(input.Sorting))
                input.Sorting = "Id";
            var orderEntities = await Task.FromResult(entities.OrderBy(input.Sorting));
            var pageEntities = await Task.FromResult(orderEntities.Skip(input.SkipCount).Take(input.MaxResultCount));
            var listDto = pageEntities.MapTo<List<WeiXinConfigOutputDto>>();

            return new PagedResultDto<WeiXinConfigOutputDto>(
            entities.Count,
            listDto
            );
        }
        public async Task<WeiXinConfigOutputDto> GetWeiXinConfigById(long id)
        {
            var entity = await _WeiXinConfigManager.GetWeiXinConfigByIdAsync(id);
            return entity.MapTo<WeiXinConfigOutputDto>();
        }
        [AbpAuthorize(AppPermissions_WeiXinManager.Pages_WeiXinManager_WeiXinConfigs_Create)]
        public async Task<bool> CreateWeiXinConfig(WeiXinConfigInputDto input)
        {
            var entity = input.MapTo<WeiXinConfig>();
            return await _WeiXinConfigManager.CreateWeiXinConfigAsync(entity);
        }
        [AbpAuthorize(AppPermissions_WeiXinManager.Pages_WeiXinManager_WeiXinConfigs_Edit)]
        public async Task<bool> UpdateWeiXinConfig(WeiXinConfigInputDto input)
        {
            var entity = await _WeiXinConfigManager.GetWeiXinConfigByIdAsync(input.Id);
            input.MapTo(entity);
            return await _WeiXinConfigManager.UpdateWeiXinConfigAsync(entity);
        }
        [AbpAuthorize(AppPermissions_WeiXinManager.Pages_WeiXinManager_WeiXinConfigs_Delete)]
        public async Task DeleteWeiXinConfig(EntityDto input)
        {
            var entity = await _WeiXinConfigManager.GetWeiXinConfigByIdAsync(input.Id);
            await _WeiXinConfigManager.DeleteWeiXinConfigAsync(entity);
        }


       

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AbpAuthorize(AppPermissions_WeiXinManager.Pages_WeiXinManager_WeiXinConfigs_Edit)]
        public async Task<bool> SendMsg(WeiXinConfigSendMsgDto input)
        {
            var entity = await _WeiXinConfigManager.GetWeiXinConfigByIdAsync(input.Id);

            WeiXinApi api = new WeiXinApi(_cacheManager.GetCache("WeiXinApi"), entity.CorpId, entity.TXL_Secret, "1");

            api.SendTextMsgToAll(input.MsgInfo);
            //获取部门列表
            //JObject joDepInfo = api.GetDepartment();

            //SynchroDepartment(api, joDepInfo["department"]);
            return true;
        }

        /// <summary>
        /// 获取微信里面的部门信息 用于下载的时候显示用
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public DataTable GetWeiXinDepartmentInfo(WeiXinConfigOutputDto input)
        {
            WeiXinApi api = new WeiXinApi(_cacheManager.GetCache("WeiXinApi"), input.CorpId, input.TXL_Secret, "1");

            //获取部门信息
            JObject joDepInfo = api.GetDepartment();
            DataTable dt = new DataTable();
            dt.Columns.Add("id");
            dt.Columns.Add("name");
            dt.Columns.Add("parentid");
            dt.Columns.Add("userNum");

            var joDeps = joDepInfo["department"];

            for (int i = 0; i < joDeps.Count(); i++)
            {
                DataRow row = dt.NewRow();
                row["id"] = joDeps[i]["id"].ToString();
                row["name"] = joDeps[i]["name"].ToString();
                row["parentid"] = joDeps[i]["parentid"].ToString();

                JObject joUsers = api.GetUserInfoSimpleList(row["id"].ToString());
                row["userNum"] = joUsers["userlist"].Count().ToString();

                dt.Rows.Add(row);
            }

            return dt;
        }

        /// <summary>
        /// 获取UBP种所有部门，用户上传的时候显示用
        /// </summary>
        /// <returns></returns>
        public DataTable GetOrganizationUnitInfo()
        {
            var orgList = _organizationUnitAppService.GetOrganizationUnits();
            DataTable dt = new DataTable();
            dt.Columns.Add("id");
            dt.Columns.Add("name");
            dt.Columns.Add("parentid");
            dt.Columns.Add("userNum");

            foreach (var org in orgList.Result.Items)
            {
                DataRow row = dt.NewRow();
                row["id"] = org.Id;
                row["name"] = org.DisplayName;
                row["parentid"] = org.ParentId;
                row["userNum"] = org.MemberCount;

                dt.Rows.Add(row);
            }

            return dt;
        }

        /// <summary>
        /// 下载通讯录
        /// </summary>
        /// <param name="input"></param>
        /// <param name="txlIds"></param>
        /// <returns></returns>
        public async Task<bool> DownTXL(WeiXinConfigDownTXL input)
        {
            var entity = await _WeiXinConfigManager.GetWeiXinConfigByIdAsync(input.Id);
            WeiXinApi api = new WeiXinApi(_cacheManager.GetCache("WeiXinApi"), entity.CorpId, entity.TXL_Secret, "1");
            //获取部门列表
            JObject joDepInfo = api.GetDepartment();
            var joDeps = joDepInfo["department"];
            for (int i = 0; i < joDeps.Count(); i++)
            {
                var departName = joDeps[i]["name"].ToString();
                var parentid = joDeps[i]["parentid"].ToString();
                var id = joDeps[i]["id"].ToString();

                if (input.DepIds.Contains(id))
                {
                    var orgList = await _organizationUnitAppService.GetOrganizationUnits();
                    if (orgList.Items.Count(item => item.DisplayName == departName) == 0)
                    {//系统没有就创建
                        var parentDep = joDeps.SingleOrDefault(d => d["id"].ToString() == parentid);//找出父级信息，如果没有就是顶级
                        string parentName = parentDep?["name"]?.ToString();
                        CreateOrganizationUnit(departName, parentName);
                    }

                    JObject joUsers = api.GetUserInfoList(id);
                    var userList = await _userAppService.GetAllUsers();//获取全部用户
                    for (int u = 0; u < joUsers["userlist"].Count(); u++)
                    {
                        //对应用户账号
                        var userid = joUsers["userlist"][u]["userid"].ToString();
                        //用户姓名
                        var username = joUsers["userlist"][u]["name"].ToString();
                        var email = joUsers["userlist"][u]["email"].ToString();
                        var mobile = joUsers["userlist"][u]["mobile"].ToString();

                        if (userList.Count(item => item.UserName == userid) == 0)
                        {//没有就创建账号
                            CreateUser(username, email, mobile, userid, "123456");
                        }
                        AddUserToOrganizationUnit(userid, departName);
                    }
                }
            }

            return true;
        }

        /// <summary>
        /// 上传通讯录
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<bool> UploadTXL(WeiXinConfigDownTXL input)
        {
            var entity = await _WeiXinConfigManager.GetWeiXinConfigByIdAsync(input.Id);
            WeiXinApi api = new WeiXinApi(_cacheManager.GetCache("WeiXinApi"), entity.CorpId, entity.TXL_Secret, "1");
            //获取部门列表
            var joDeps = api.GetDepartment()["department"];
            //获取微信所有用户
            var joUsers = api.GetUserInfoList("1", 1)["userlist"];

            var orgList = await _organizationUnitAppService.GetOrganizationUnits();
            var listO = orgList.Items.Where(o => input.DepIds.Contains(o.Id.ToString())).ToList();

            //var userList = _userAppService.GetAllUsers().Result.ToList();

            UploadTXL(api, listO, joDeps, joUsers, 0);


            return true;
        }

        /// <summary>
        /// 按部门上传通讯录
        /// </summary>
        /// <param name="api"></param>
        /// <param name="listOrg"></param>
        /// <param name="userList"></param>
        /// <param name="joDeps"></param>
        /// <param name="parentId"></param>
        private void UploadTXL(WeiXinApi api,
            List<OrganizationUnitDto> listOrg,
            JToken joDeps,
            JToken joUsers,
            long parentId)
        {
            var depParentId = "-1";
            //查找出上级名称 然通过名称 找出对应微信部门的ID
            var parentOrg = listOrg.SingleOrDefault(o => o.Id == parentId);
            if (parentOrg != null)
            {
                var parentDep = joDeps.SingleOrDefault(d => d["name"].ToString() == parentOrg.DisplayName);
                if (parentDep != null)
                    depParentId = parentDep["id"].ToString();
            }
            var listO = listOrg.Where(a => (a.ParentId ?? 0) == parentId).ToList();
            foreach (var item in listO)
            {
                if (depParentId != "-1" && joDeps.Count(d => d["name"].ToString() == item.DisplayName) == 0)
                {//在微信中找到了父级ID，并且没有目标信息就创建部门
                    var dep = api.CreateDepartment(item.DisplayName, depParentId);

                    var userList = _organizationUnitAppService.GetOrganizationUnitAllUsers(item.Id);
                    foreach (var user in userList.Result)
                    {
                        if (joUsers.Count(u => u["userid"].ToString() == user.UserName) == 0)
                        {//创建用户
                            api.CreateUser(user.UserName, user.Surname + user.Name, dep["id"].ToString(), user.EmailAddress);
                        }
                    }
                    
                }
                
                if (joDeps.Count(d => d["name"].ToString() == item.DisplayName) > 0)
                {//如果没有找到父级ID，但是在微信中已经存在的就直接创建用户

                    var dep = joDeps.SingleOrDefault(d => d["name"].ToString() == item.DisplayName);
                    var userList = _organizationUnitAppService.GetOrganizationUnitAllUsers(item.Id);
                    foreach (var user in userList.Result)
                    {
                        if (joUsers.Count(u => u["userid"].ToString() == user.UserName) == 0)
                        {//创建用户
                            api.CreateUser(user.UserName, user.Surname + user.Name, dep["id"].ToString(), user.EmailAddress);
                        }
                    }

                }
                UploadTXL(api, listOrg, joDeps, joUsers, item.Id);

            }
        }

        /// <summary>
        /// 创建部门
        /// </summary>
        /// <param name="oName"></param>
        /// <param name="pName"></param>
        private async void CreateOrganizationUnit(string oName, string pName)
        {
            var orgList = await _organizationUnitAppService.GetOrganizationUnits();
            if (orgList.Items.Count(item => item.DisplayName == pName) == 0)
            {
                var input = new Organizations.Dto.CreateOrganizationUnitInput();
                input.DisplayName = oName;
                await _organizationUnitAppService.CreateOrganizationUnit(input);
            }
            else
            {
                var input = new Organizations.Dto.CreateOrganizationUnitInput();
                input.DisplayName = oName;
                input.ParentId = orgList.Items.First(item => item.DisplayName == pName).Id;
                await _organizationUnitAppService.CreateOrganizationUnit(input);
            }
        }

        /// <summary>
        /// 创建用户
        /// </summary>
        /// <param name="name">姓名</param>
        /// <param name="email"></param>
        /// <param name="mobile"></param>
        /// <param name="username">用户名</param>
        /// <param name="pwd">密码</param>
        private async void CreateUser(string name, string email, string mobile, string username, string pwd)
        {
            var user = new CreateOrUpdateUserInput();
            user.SendActivationEmail = false;
            user.SetRandomPassword = false;
            user.User = new UserEditDto();
            user.User.Name = name;
            user.User.Surname = name;
            if (string.IsNullOrEmpty(email))
                user.User.EmailAddress = username + "@jiangxi-isuzu.cn";
            else
                user.User.EmailAddress = email;
            user.User.PhoneNumber = mobile;
            user.User.UserName = username;
            user.User.Password = pwd;
            user.User.IsActive = true;
            user.User.ShouldChangePasswordOnNextLogin = false;
            user.User.IsTwoFactorEnabled = false;
            user.User.IsLockoutEnabled = true;

            user.AssignedRoleNames = new string[] { "User" };
            user.SendActivationEmail = false;
            user.SetRandomPassword = false;

            await _userAppService.CreateOrUpdateUser(user);
        }

        /// <summary>
        /// 将用户添加到部门中
        /// </summary>
        /// <param name="username"></param>
        /// <param name="depName"></param>
        private async void AddUserToOrganizationUnit(string username, string depName)
        {
            var userList = await _userAppService.GetAllUsers();//获取全部用户
            var user = userList.FirstOrDefault(u => u.UserName == username);
            if (user == null)
                return;

            var orgList = await _organizationUnitAppService.GetOrganizationUnits();
            var org = orgList.Items.FirstOrDefault(o => o.DisplayName == depName);
            if (org == null)
                return;

            var userToOrg = new UserToOrganizationUnitInput();
            userToOrg.OrganizationUnitId = org.Id;
            userToOrg.UserId = user.Id;
            await _organizationUnitAppService.AddUserToOrganizationUnit(userToOrg);
        }
    }
}
