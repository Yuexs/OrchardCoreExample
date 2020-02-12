using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace OrchardCoreExample.WebApi.Module.Controllers
{
    [Route("api/demo/[action]")]
    [Authorize(AuthenticationSchemes = "Api"), IgnoreAntiforgeryToken, AllowAnonymous]
    [ApiController]
    public class WebApiController: Controller
    {
        private readonly IAuthorizationService _authorizationService;
        public WebApiController(IAuthorizationService authorizationService)
        {
            _authorizationService = authorizationService;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> test1(int x, int y)
        {
            if (!await _authorizationService.AuthorizeAsync(User, Permissions.WebApiPermission.WebApiPermissionAccess))
            {
                return Unauthorized(new { result = "没有权限。" });
            }
            return Json(new { result = "有权限。",x,y });
        }

        [HttpGet]
        public IActionResult test2()
        {
            return Json(new { result = "无需授权。" });
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> test3(int x,int y)
        {
            if (!await _authorizationService.AuthorizeAsync(User, Permissions.WebApiPermission.WebApiPermissionAccess))
            {
                return Unauthorized(new { result = "没有权限。" });
            }
            return Json(new { result = "有权限。",x,y });
        }
    }
}
