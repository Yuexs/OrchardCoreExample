using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Threading.Tasks;
using AliYunSecurityTokenServiceExample.Extensions;
using AliYunSecurityTokenServiceExample.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace AliYunSecurityTokenServiceExample.Services
{
    public class AuthorizationService : IAuthorizationService
    {
        #region 变量定义

        private readonly string _clientId;
        private readonly string _clientSecret;
        private string _token = string.Empty;
        private long _expiresInTimeStamp;

        #endregion

        #region 构造函数

        /// <summary>
        ///     构造函数
        /// </summary>
        /// <param name="clientId"></param>
        /// <param name="clientSecret"></param>
        public AuthorizationService(string clientId, string clientSecret)
        {
            _clientId = clientId;
            _clientSecret = clientSecret;
        }

        #endregion

        #region AccessToken

        /// <summary>
        /// AccessToken
        /// </summary>
        public string AccessToken
        {
            get
            {
                if (_expiresInTimeStamp > DateTime.Now.ToTimeStamp() && !string.IsNullOrEmpty(_token)) return _token;

                var result = GetAuthorization().Result;
                if (string.IsNullOrEmpty(result.AccessToken)) return "";

                _token = result.AccessToken;
                _expiresInTimeStamp = DateTime.Now.AddSeconds(result.ExpiresIn).ToTimeStamp();
                return _token;
            }
        }

        #endregion

        #region 获取 GetAuthorization

        /// <summary>
        ///     获取 GetAuthorization
        /// </summary>
        /// <returns></returns>
        public async Task<AuthorizationModel> GetAuthorization()
        {
            var client = new HttpClient();
            var urlFormat = Configs.HostBase + "/connect/token";
            try
            {
                HttpContent content = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("grant_type", "client_credentials"),
                    new KeyValuePair<string, string>("client_id", _clientId),
                    new KeyValuePair<string, string>("client_secret", _clientSecret)
                });

                content.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");
                ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
                var response = await client.PostAsync(urlFormat, content);
                if (!response.IsSuccessStatusCode) return new AuthorizationModel();
                var jsonData= await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<AuthorizationModel>(jsonData);
            }
            catch (Exception ex)
            {
                Console.WriteLine(
                    $"{MethodBase.GetCurrentMethod()!.ReflectedType!.Name}.{MethodBase.GetCurrentMethod()!.Name}=>{ex.Message}");
                return new AuthorizationModel();
            }
        }

        #endregion
    }
}
