using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DM.UBP.Authorization.Users.Dto
{
    public class WX_CreateOrUpdateUserInput: CreateOrUpdateUserInput
    {
        [Required]
        public new WX_UserEditDto User { get; set; }
    }
}
