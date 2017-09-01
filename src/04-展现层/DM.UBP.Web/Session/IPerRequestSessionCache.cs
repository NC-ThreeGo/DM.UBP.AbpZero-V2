using System.Threading.Tasks;
using DM.UBP.Sessions.Dto;

namespace DM.UBP.Web.Session
{
    public interface IPerRequestSessionCache
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformationsAsync();
    }
}
