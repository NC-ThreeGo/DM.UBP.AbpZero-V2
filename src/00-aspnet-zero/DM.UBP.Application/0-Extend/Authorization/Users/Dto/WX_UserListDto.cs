using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DM.UBP.Authorization.Users.Dto
{
    public class WX_UserListDto : UserListDto
    {
        public string WeiXinUserId { set; get; }
    }
}
