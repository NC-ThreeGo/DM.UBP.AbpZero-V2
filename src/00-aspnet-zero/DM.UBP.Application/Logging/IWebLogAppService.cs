using Abp.Application.Services;
using DM.UBP.Dto;
using DM.UBP.Logging.Dto;

namespace DM.UBP.Logging
{
    public interface IWebLogAppService : IApplicationService
    {
        GetLatestWebLogsOutput GetLatestWebLogs();

        FileDto DownloadWebLogs();
    }
}
