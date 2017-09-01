using System;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using DM.UBP.Authorization.Users;
using DM.UBP.Friendships;

namespace DM.UBP.Chat.Dto
{
    [AutoMapFrom(typeof(User))]
    public class ChatUserDto : EntityDto<long>
    {
        public int? TenantId { get; set; }

        public Guid? ProfilePictureId { get; set; }

        public string UserName { get; set; }

        public string TenancyName { get; set; }

        public int UnreadMessageCount { get; set; }

        public bool IsOnline { get; set; }

        public FriendshipState State { get; set; }
    }
}