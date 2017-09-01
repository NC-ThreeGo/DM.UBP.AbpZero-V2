using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace DM.UBP.CodeBuilder
{
    /// <summary>
    /// 数据库类型
    /// </summary>
    public enum DbType
    {
        SqlServer = 0,
        DevartOracle = 1,
        Oracle = 2,
        MySql = 3,
    }

    public static class Utils
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="field"></param>
        /// <returns></returns>
        public static string GetCSType(DbType dbType, Field field)
        {
            string type = "";
            switch (dbType)
            {
                case DbType.DevartOracle:
                case DbType.Oracle:
                    switch (field.Type.ToLower())
                    {
                        case "varchar2":
                        case "char2":
                            type = "string";
                            break;
                        case "number":
                            if (field.Scale > 0)
                                type = "decimal";
                            else
                            {
                                if (field.Length == 1)
                                {
                                    type = "bool";
                                }
                                if ((field.Length >= 2) && (field.Length <= 18))
                                {
                                    type = "int";
                                }
                                if (field.Length > 18) 
                                {
                                    type = "long";
                                }
                            }
                            break;
                        case "date":
                            type = "DateTime";
                            break;
                    }
                    break;
                case DbType.SqlServer:
                    break;
                case DbType.MySql:
                    break;
            }

            return type;
        }

        //public static string ToPascalCase(this string s)
        //{
        //    var m = Regex.Match(s, "^(?<word>^[a-z]+|[A-Z]+|[A-Z][a-z]+)+$");
        //    var g = m.Groups["word"];

        //    // Take each word and convert individually to TitleCase
        //    // to generate the final output.  Note the use of ToLower
        //    // before ToTitleCase because all caps is treated as an abbreviation.
        //    var t = Thread.CurrentThread.CurrentCulture.TextInfo;
        //    var sb = new StringBuilder();
        //    foreach (var c in g.Captures.Cast<Capture>())
        //        sb.Append(t.ToTitleCase(c.Value.ToLower()));
        //    return sb.ToString();
        //}
    }
}
