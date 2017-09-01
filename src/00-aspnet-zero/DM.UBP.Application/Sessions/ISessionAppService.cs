using System.Threading.Tasks;
using Abp.Application.Services;
using DM.UBP.Sessions.Dto;

namespace DM.UBP.Sessions
{
    public interface ISessionAppService : IApplicationService
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();
    }
}
