using Abp.Notifications;
using DM.UBP.Dto;

namespace DM.UBP.Notifications.Dto
{
    public class GetUserNotificationsInput : PagedInputDto
    {
        public UserNotificationState? State { get; set; }
    }
}