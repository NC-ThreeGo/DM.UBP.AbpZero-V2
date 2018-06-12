using Abp;
using Abp.Modules;
using Castle.Core.Logging;
using DM.UBP.Application.Quartz;
using DM.UBP.Application.Quartz.Servers;
using DM.UBP.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DM.UBP.Console
{
    [DependsOn(typeof(UbpEFModule), typeof(UbpAppQuartzModule))]
    public class UBPConsoleModule : AbpModule
    {
        public override void PreInitialize()
        {
            
        }

        public override void Initialize()
        {
            //IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }

        public override void PostInitialize()
        {

        }
    }
}
