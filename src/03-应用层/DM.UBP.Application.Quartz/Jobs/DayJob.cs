using Abp.Dependency;
using Abp.Quartz.Quartz;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DM.UBP.Application.Quartz.Jobs
{
    public class DayJob : JobBase, ITransientDependency
    {
        public override void Execute(IJobExecutionContext context)
        {
            Logger.Info("Test day work:)");
        }
    }
}
