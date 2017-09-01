using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using DM.UBP.Authorization.Users;

namespace DM.UBP.Configuration.Host.Dto
{
    public class SendTestEmailInput
    {
        [Required]
        [MaxLength(User.MaxEmailAddressLength)]
        public string EmailAddress { get; set; }
    }
}