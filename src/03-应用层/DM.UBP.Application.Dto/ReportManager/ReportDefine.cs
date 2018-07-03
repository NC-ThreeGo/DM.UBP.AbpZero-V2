using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DM.UBP.Application.Dto.ReportManager
{
    public static class ReportDefine
    {
        public enum CommandTypes : int
        {
            SQL语句 = 1,
            存储过程 = 2
        }

        public enum ParamterTypes : int
        {
            字符型 = 1,
            整型 = 2,
            浮点型 = 3,
            日期型 = 4,
            布尔型 = 5,
            Guid型 = 6
        }

        public enum UiTypes : int
        {
            文本框 = 1,
            多行文本 = 2,
            整数型 = 3,
            小数型 = 4,
            日期型 = 5,
            日期时间型 = 6,
            下拉框 = 7,
            多选下拉框 = 8
            //自动搜素下拉框 = 9,
            //自动多选搜索下拉框 = 10
        }
    }
}
