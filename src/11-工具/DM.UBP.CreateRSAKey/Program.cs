using DM.Common.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DM.UBP.CreateRSAKey
{
    class Program
    {
        static void Main(string[] args)
        {
            string keyDir = AppDomain.CurrentDomain.BaseDirectory;
            if (RSAUtils.TryGetKeyParameters(keyDir, true, out RSAParameters keyParams) == false)
            {
                Console.WriteLine("按任意键开始生产RSAKey文件。");
                Console.Read();
                keyParams = RSAUtils.GenerateAndSaveKey(keyDir);
                Console.WriteLine("RSAKey文件生存成功！");
            }
            else
            {
                //Console.WriteLine("RSAKey文件已经存在！");

                Console.WriteLine("生成jwtToken");
                JwtTokenUtils jwtTokenUtils = new JwtTokenUtils();
                string jwtToken = jwtTokenUtils.GenerateJwtToken("zhuqp", "", "pbirs");
                Console.WriteLine(jwtToken);

                Console.WriteLine("验证jwtToken");
                string username = jwtTokenUtils.ValidateJwtToken(jwtToken, "pbirs");
                Console.WriteLine(username);

                Console.Read();
            }
        }
    }
}
