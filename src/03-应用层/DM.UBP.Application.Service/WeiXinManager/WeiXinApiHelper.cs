using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DM.UBP.Application.Service.WeiXinManager
{
    public static class WeiXinApiHelper
    {
        /// <summary>
        /// 传入Url接口  返回Json对象
        /// </summary>
        /// <param name="url">目标连接地址</param>
        /// <returns></returns>
        public static JObject GetJson(string url)
        {
            WebClient wc = new WebClient();
            wc.Credentials = CredentialCache.DefaultCredentials;
            wc.Encoding = Encoding.UTF8;
            string resultJson = wc.DownloadString(url);
            JObject jo = (JObject)JsonConvert.DeserializeObject(resultJson);
            if (jo["errcode"].ToString() != "0")
            {
                throw new Exception("请求异常errcode：" + jo["errcode"].ToString() + " 请按错误代码查询对应信息！");
            }
            return jo;
        }

        /// <summary>
        /// 发送Post请求传入json 返回json对象
        /// </summary>
        /// <param name="url"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static JObject PostJson(string url, object obj)
        {
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
            req.Method = "POST";
            req.ContentType = "application/json";
            string postStr = JsonConvert.SerializeObject(obj);
            byte[] data = Encoding.UTF8.GetBytes(postStr);
            req.ContentLength = data.Length;
            using (Stream reqStream = req.GetRequestStream())
            {
                reqStream.Write(data, 0, data.Length);
                reqStream.Close();
            }
            HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
            Stream stream = resp.GetResponseStream();

            string resultJson = string.Empty;
            //获取响应内容
            using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
            {
                resultJson = reader.ReadToEnd();
            }
            JObject jo = (JObject)JsonConvert.DeserializeObject(resultJson);
            if (jo["errcode"].ToString() != "0")
            {
                throw new Exception("请求异常errcode：" + jo["errcode"].ToString() + " 请按错误代码查询对应信息！");
            }
            return jo;
        }

    }
}
