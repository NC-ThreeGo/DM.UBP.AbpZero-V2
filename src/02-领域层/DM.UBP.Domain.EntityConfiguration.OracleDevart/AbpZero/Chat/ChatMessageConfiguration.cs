using DM.UBP.Chat;
using DM.UBP.EF;

namespace DM.UBP.Domain.EntityConfiguration.OracleDevart.AbpZero.Chat
{
    public class ChatMessageConfiguration : EntityConfigurationBase<ChatMessage, long>
    {
        public ChatMessageConfiguration()
        {
            Property(j => j.Message)
                .HasMaxLength(4000);
        }
    }
}
