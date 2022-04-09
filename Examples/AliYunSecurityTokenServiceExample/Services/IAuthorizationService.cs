using System.Threading.Tasks;
using AliYunSecurityTokenServiceExample.Models;

namespace AliYunSecurityTokenServiceExample.Services
{
    public interface IAuthorizationService
    {
        /// <summary>
        /// AccessToken
        /// </summary>
        string AccessToken { get; }

        /// <summary>
        /// 获取 GetAuthorization
        /// </summary>
        /// <returns></returns>
        Task<AuthorizationModel> GetAuthorization();
    }
}
