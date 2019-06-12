using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DM.UBP.Authorization.Users
{
    public class WX_User: User
    {
        public virtual string WeiXinUserId { get; set; } 
    }
}
