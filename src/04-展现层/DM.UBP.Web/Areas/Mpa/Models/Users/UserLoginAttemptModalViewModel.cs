using System.Collections.Generic;
using DM.UBP.Authorization.Users.Dto;

namespace DM.UBP.Web.Areas.Mpa.Models.Users
{
    public class UserLoginAttemptModalViewModel
    {
        public List<UserLoginAttemptDto> LoginAttempts { get; set; }
    }
}