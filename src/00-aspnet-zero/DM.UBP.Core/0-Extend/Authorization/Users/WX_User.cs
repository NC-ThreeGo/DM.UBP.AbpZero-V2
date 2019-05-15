using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DM.UBP.Authorization.Users
{
    [Table("AbpUsers")]
    public class WX_User: User
    {
        public string WeiXinUserId { get; set; } 
    }
}
