using Abp.Runtime.Caching;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DM.UBP.Application.Service.WeiXinManager
{
    public class WeiXinApi
    {
        private string corpId;

        private string secret;

        private string agentId;

        private readonly ICache cache;

        /// <summary>
        /// 构造 传入corpId和对应的应用secret
        /// 获得对应的access_token
        /// </summary>
        /// <param name="corpId"></param>
        /// <param name="secret"></param>
        public WeiXinApi(ICache _cache, string _corpId, string _secret,string _agentId)
        {
            cache = _cache;
            agentId = _agentId;
            corpId = _corpId;
            secret = _secret;
        }

        /// <summary>
        /// 当前AccessToken
        /// </summary>
        private string accessToken
        {
            get { return getAccessTokenByCache(); }
        }

        /// <summary>
        /// 从缓存中获取AccessToken
        /// </summary>
        /// <returns></returns>
        private string getAccessTokenByCache()
        {
            string accessToken = cache.Get("accessToken", () => getAccessToken());
            return accessToken;
        }

        /// <summary>
        /// 获取AccessToken
        /// </summary>
        /// <returns></returns>
        public string getAccessToken()
        {
            string accessTokenUrl = string.Format("https://qyapi.weixin.qq.com/cgi-bin/gettoken?corpid={0}&corpsecret={1}", corpId, secret);
            JObject joAccessToken = WeiXinApiHelper.GetJson(accessTokenUrl);
            string accessToken = joAccessToken["access_token"].ToString();
            //有效期
            string expiresIn = joAccessToken["expires_in"].ToString();
            int cacheTime = (Convert.ToInt32(expiresIn) / 60) - 1;
            cache.DefaultSlidingExpireTime = TimeSpan.FromMinutes(cacheTime);

            return accessToken;
        }


        /// <summary>
        /// 获取部门列表
        /// </summary>
        /// <param name="id">部门id。获取指定部门及其下的子部门。 如果不填，默认获取全量组织架构</param>
        public JObject GetDepartment(string id = null)
        {
            string url = string.Format("https://qyapi.weixin.qq.com/cgi-bin/department/list?access_token={0}&id={1}", accessToken, id);
            JObject joDepartment = WeiXinApiHelper.GetJson(url);
            return joDepartment;
        }

        /// <summary>
        /// 获取部门成员详情
        /// </summary>
        /// <param name="id">部门id。获取指定部门及其下的子部门。 如果不填，默认获取全量组织架构</param>
        public JObject GetUserInfoList(string id,int child = 0)
        {
            string url = string.Format("https://qyapi.weixin.qq.com/cgi-bin/user/list?access_token={0}&department_id={1}&fetch_child={2}", accessToken, id, child.ToString());
            JObject joDepartment = WeiXinApiHelper.GetJson(url);
            return joDepartment;
        }

        /// <summary>
        /// 获取部门成员信息
        /// </summary>
        /// <param name="id">部门id。获取指定部门及其下的子部门。 如果不填，默认获取全量组织架构</param>
        public JObject GetUserInfoSimpleList(string id, int child = 0)
        {
            string url = string.Format("https://qyapi.weixin.qq.com/cgi-bin/user/simplelist?access_token={0}&department_id={1}&fetch_child={2}", accessToken, id, child.ToString());
            JObject joDepartment = WeiXinApiHelper.GetJson(url);
            return joDepartment;
        }

        /// <summary>
        /// 创建部门
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public JObject CreateDepartment(string name,string parentid)
        {
            string url = string.Format("https://qyapi.weixin.qq.com/cgi-bin/department/create?access_token={0}", accessToken);
            var dep = new { name, parentid };
            JObject result = WeiXinApiHelper.PostJson(url, dep);
            return result;
        }

        /// <summary>
        /// 创建用户
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="name"></param>
        /// <param name="department"></param>
        /// <param name="mobile"></param>
        /// <param name="email"></param>
        /// <returns></returns>

        public JObject CreateUser(string userid, string name, string department, string email)
        {
            string url = string.Format("https://qyapi.weixin.qq.com/cgi-bin/user/create?access_token={0}", accessToken);
            var user = new { userid, name, department, email };
            JObject result = WeiXinApiHelper.PostJson(url, user);
            return result;
        }

        /// <summary>
        /// 给全员发送文本消息
        /// </summary>
        /// <param name="msgInfo"></param>
        /// <returns></returns>
        public JObject SendTextMsgToAll(string msg)
        {
            string url = string.Format("https://qyapi.weixin.qq.com/cgi-bin/message/send?access_token={0}", accessToken);
            var text = new { content = msg };
            var info = new { touser = "@all", msgtype = "text", text };
            JObject result = WeiXinApiHelper.PostJson(url, info);
            return result;
        }
    }
}
