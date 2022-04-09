using System;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using AliYunSecurityTokenServiceExample.Extensions;
using AliYunSecurityTokenServiceExample.Models;
using Newtonsoft.Json;

namespace AliYunSecurityTokenServiceExample.Services
{
    public class AliYunSecurityTokenService : IAliYunSecurityTokenService
    {
        #region 变量定义

        private readonly IAuthorizationService _authorizationService;

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="authorizationService"></param>
        public AliYunSecurityTokenService(IAuthorizationService authorizationService)
        {
            _authorizationService = authorizationService;
        }

        #endregion

        #region 获取阿里云SecurityToken

        /// <summary>
        /// 获取阿里云SecurityToken
        /// </summary>
        /// <param name="directory">授权目录</param>
        /// <param name="roleSessionName"></param>
        /// <returns></returns>
        public async Task<SecurityTokenModel> GetAliYunSecurityToken(string directory,
            string roleSessionName = "aliyunOSSSTSSession")
        {
            var _client = new HttpClient();
            var urlFormat = Configs.HostBase + "/api/aliyunoss/v1/GetSecurityToken";
            var dataFormat = new
            {
                directory,
                roleSessionName
            };
            try
            {
                var response =
                    await _client.PostJsonAsync(urlFormat, JsonConvert.SerializeObject(dataFormat), _authorizationService.AccessToken);

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception(response.StatusCode + " " + await response.Content.ReadAsStringAsync());
                }

                var jsonData = await response.Content.ReadAsStringAsync();
                var securityToken = JsonConvert.DeserializeObject<SecurityTokenModel>(jsonData);
                // UTC时间转本地时间
                if (securityToken != null && securityToken.Expiration < DateTime.Now)
                    securityToken.Expiration =
                        TimeZoneInfo.ConvertTimeFromUtc(securityToken.Expiration, TimeZoneInfo.Local);
                return securityToken;
            }
            catch (Exception ex)
            {
                Console.WriteLine(
                    $"{MethodBase.GetCurrentMethod()!.ReflectedType!.Name}.{MethodBase.GetCurrentMethod()!.Name}=>{ex.Message}");
                return new SecurityTokenModel();
            }
        }

        #endregion
    }
}
