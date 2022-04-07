using Aliyun.Acs.Core;
using Aliyun.Acs.Core.Auth.Sts;
using Aliyun.Acs.Core.Http;
using Aliyun.Acs.Core.Profile;
using OrchardCoreExample.AliYunSecurityTokenService.Module.Settings;
using OrchardCore.Entities;
using OrchardCore.Settings;

namespace OrchardCoreExample.AliYunSecurityTokenService.Module.Services
{
    public class AliYunSecurityTokenService : IAliYunSecurityTokenService
    {
        private readonly AliYunSecurityTokenServiceSetting _aliYunSecurityTokenServiceSetting;

        public AliYunSecurityTokenService(ISiteService siteService)
        {
            _aliYunSecurityTokenServiceSetting = siteService.GetSiteSettingsAsync().Result.As<AliYunSecurityTokenServiceSetting>();
        }

        /// <summary>
        /// 获取安全令牌
        /// </summary>
        /// <param name="directory"></param>
        /// <param name="roleSessionName"></param>
        /// <returns></returns>
        public AssumeRoleResponse.AssumeRole_Credentials GetSecurityTokenService(string directory, string roleSessionName)
        {
            IClientProfile profile =
                DefaultProfile.GetProfile(_aliYunSecurityTokenServiceSetting.RegionName, _aliYunSecurityTokenServiceSetting.AccessKeyId, _aliYunSecurityTokenServiceSetting.AccessKeySecret);
            var client = new DefaultAcsClient(profile);
            var policy = _aliYunSecurityTokenServiceSetting.Policy.Replace("BucketName", _aliYunSecurityTokenServiceSetting.BucketName)
                .Replace("Directory", directory);
            var request = new AssumeRoleRequest
            {
                AcceptFormat = FormatType.JSON,
                //指定角色ARN
                RoleArn = _aliYunSecurityTokenServiceSetting.RoleArn,
                RoleSessionName = roleSessionName,
                DurationSeconds = 3600,
                Policy = policy
            };

            var response = client.GetAcsResponse(request);

            return response.Credentials;
        }
    }
}
