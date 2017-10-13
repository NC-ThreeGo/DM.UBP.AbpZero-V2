using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DM.UBP.Application.Dto.ReportManager
{
    public static class ReportDefine
    {
        public static Dictionary<string, int> CommandTypes = new Dictionary<string, int>
        {
            { "SQL语句",1 },
            { "存储过程",2}
        };


    }
}
