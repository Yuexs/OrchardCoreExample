using Aliyun.Acs.Core.Auth.Sts;

namespace OrchardCoreExample.AliYunSecurityTokenService.Module.Services
{
    public interface IAliYunSecurityTokenService
    {
        /// <summary>
        /// 获取安全令牌
        /// </summary>
        /// <param name="directory"></param>
        /// <param name="roleSessionName"></param>
        /// <returns></returns>
        AssumeRoleResponse.AssumeRole_Credentials GetSecurityTokenService(string directory, string roleSessionName);
    }
}