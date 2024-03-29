﻿using Castle.Core.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DM.UBP.Console
{
    class Program
    {
        static void Main(string[] args)
        {

            UBPConsoleApplication<UBPConsoleModule> application = new UBPConsoleApplication<UBPConsoleModule>();
            try
            {
                System.Console.WriteLine("Ubp Quartz 后台任务正在启动! ");
                application.Application_Start();
                System.Console.WriteLine("Ubp Quartz 后台任务启动成功! ");
                System.Console.ReadKey();

            }
            finally
            {
                System.Console.WriteLine("Ubp Quartz 后台任务结束! ");

                application.Application_End();
            }
        }
    }
}
