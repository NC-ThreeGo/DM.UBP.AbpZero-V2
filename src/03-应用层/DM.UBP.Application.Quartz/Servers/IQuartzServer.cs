using Abp.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DM.UBP.Application.Quartz.Servers
{
    public interface IQuartzServer: IApplicationService
    {
        /// <summary>
        /// 开启自动调度
        /// </summary>
        void StartUp();

        /// <summary>
        /// 获取任务在未来周期内哪些时间会运行
        /// </summary>
        /// <param name="CronExpressionString">Cron表达式</param>
        /// <param name="numTimes">运行次数</param>
        /// <returns>运行时间段</returns>
        List<string> GetTaskFireTime(string CronExpressionString, int numTimes);

        Task ExecJob(long groupId, long id);
    }
}
