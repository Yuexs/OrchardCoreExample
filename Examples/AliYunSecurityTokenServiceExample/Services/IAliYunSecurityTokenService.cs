using System.Threading.Tasks;
using AliYunSecurityTokenServiceExample.Models;

namespace AliYunSecurityTokenServiceExample.Services
{
    public interface IAliYunSecurityTokenService
    {
        /// <summary>
        /// 获取阿里云SecurityToken
        /// </summary>
        /// <param name="directory">授权目录</param>
        /// <param name="roleSessionName"></param>
        /// <returns></returns>
        Task<SecurityTokenModel> GetAliYunSecurityToken(string directory, string roleSessionName = "aliyunOSSSTSSession");
    }
}
