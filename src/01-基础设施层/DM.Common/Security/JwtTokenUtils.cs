using DM.Common.Extensions;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DM.Common.Security
{
    public class JwtTokenUtils
    {
        /// <summary>
        /// 根据用户名、角色、到期时间、Token接收者生成JwtToken
        /// </summary>
        /// <param name="username"></param>
        /// <param name="role"></param>
        /// <param name="audience">Token接收者</param>
        /// <returns></returns>
        public string GenerateJwtToken(string username, string role, string audience)
        {
            string keyDir = AppDomain.CurrentDomain.BaseDirectory;
            DateTime expire = DateTime.Now.AddMinutes(Convert.ToDouble(ConfigurationManager.AppSettings["Jwt_ExpireMinutes"] ?? "30"));
            RsaSecurityKey key;

            if (RSAUtils.TryGetKeyParameters(keyDir, true, out RSAParameters keyparams) == false)
            {
                //key = default(RsaSecurityKey);
                throw new Exception("读取RSA密钥文件失败！");
            }
            else
            {
                key = new RsaSecurityKey(keyparams);
            }

            string jti = audience + username + expire.GetMilliseconds();
            jti = jti.ToMd5Hash(); // Jwt 的一个参数，用来标识 Token

            var claims = new[]
            {
                new Claim(ClaimTypes.Role, role ?? string.Empty), // 添加角色信息
                new Claim(ClaimTypes.NameIdentifier, username), // 用户名称
                new Claim("jti",jti,ClaimValueTypes.String) // jti，用来标识 token
            };

            JwtSecurityToken jwtToken = new JwtSecurityToken
            (
                issuer: ConfigurationManager.AppSettings["Jwt_Issuer"] ?? "DM.UBP",
                audience: audience,
                claims: claims,
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.RsaSha256Signature),
                expires: expire
            );

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            string tokenString = tokenHandler.WriteToken(jwtToken);

            return tokenString;
        }


        /// <summary>
        /// 根据Token接收者验证JwtToken
        /// </summary>
        /// <param name="TokenString"></param>
        /// <param name="audience"></param>
        /// <returns>用户名</returns>
        public string ValidateJwtToken(string tokenString, string audience)
        {
            string username = null;

            try
            {
                SecurityToken securityToken;
                JwtSecurityTokenHandler securityTokenHandler = new JwtSecurityTokenHandler();

                string keyDir = AppDomain.CurrentDomain.BaseDirectory;
                RsaSecurityKey key;
                if (RSAUtils.TryGetKeyParameters(keyDir, false, out RSAParameters keyparams) == false)
                {
                    //return null;
                    throw new Exception("读取RSA密钥文件失败！");
                }
                else
                {
                    key = new RsaSecurityKey(keyparams);
                }

                TokenValidationParameters validationParameters = new TokenValidationParameters()
                {
                    ValidIssuer = ConfigurationManager.AppSettings["Jwt_Issuer"] ?? "DM.UBP",
                    ValidAudience = audience,
                    IssuerSigningKey = key,
                    ValidateLifetime = true,
                    LifetimeValidator = LifetimeValidator,
                };

                ClaimsPrincipal claims = securityTokenHandler.ValidateToken(tokenString, validationParameters, out securityToken);
                var claim = claims.FindFirst(ClaimTypes.NameIdentifier);
                if (claim != null)
                    username = claim.Value;
            }
            catch (Exception ex)
            {
                username = null;
            }

            return username;
        }

        private bool LifetimeValidator(DateTime? notBefore, DateTime? expires, SecurityToken token, TokenValidationParameters @params)
        {
            if (expires != null)
            {
                return expires > DateTime.UtcNow;
            }
            return false;
        }
    }
}
