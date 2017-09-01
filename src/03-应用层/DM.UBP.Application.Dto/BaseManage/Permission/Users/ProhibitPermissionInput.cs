using System.ComponentModel.DataAnnotations;

namespace DM.UBP.Application.Dto.BaseManage.Permission.Users
{
    public class ProhibitPermissionInput
    {
        [Range(1, long.MaxValue)]
        public int UserId { get; set; }

        [Required]
        public string PermissionName { get; set; }
    }
}