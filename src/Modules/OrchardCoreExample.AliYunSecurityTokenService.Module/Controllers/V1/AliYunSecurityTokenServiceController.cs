using System.Threading.Tasks;
using OrchardCoreExample.AliYunSecurityTokenService.Module.Permissions;
using OrchardCoreExample.AliYunSecurityTokenService.Module.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace OrchardCoreExample.AliYunSecurityTokenService.Module.Controllers.V1
{
    [Route("api/aliyunsecuritytokenservice/v1/[action]")]
    [Authorize(AuthenticationSchemes = "Api"), IgnoreAntiforgeryToken, AllowAnonymous]
    [ApiController]
    public class AliYunSecurityTokenServiceController: Controller
    {
        #region 变量

        private readonly IAuthorizationService _authorizationService;
        private readonly IAliYunSecurityTokenService _aliYunSecurityTokenService;

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="authorizationService"></param>
        /// <param name="aliYunSecurityTokenService"></param>
        public AliYunSecurityTokenServiceController(IAuthorizationService authorizationService, IAliYunSecurityTokenService aliYunSecurityTokenService)
        {
            _authorizationService = authorizationService;
            _aliYunSecurityTokenService = aliYunSecurityTokenService;
        }

        #endregion

        #region 方法

        /// <summary>
        /// 获取安全令牌
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> GetSecurityTokenService([FromBody] JObject param)
        {
            if (!await _authorizationService.AuthorizeAsync(User, AliYunSecurityTokenServicePermission.AliYunSecurityTokenServiceAccess))
            {
                return Unauthorized();
            }
            
            var securityToken = _aliYunSecurityTokenService.GetSecurityTokenService(param["directory"].Value<string>(), param["roleSessionName"].Value<string>());
            return Json(securityToken);
        }

        #endregion
    }
}
