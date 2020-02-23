using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using OrchardCore.Entities;
using OrchardCore.Settings;
using OrchardCoreExample.WeCharMiniProgram.Module.MiniProgram.Models;
using OrchardCoreExample.WeCharMiniProgram.Module.MiniProgram.Permissions;
using Senparc.Weixin;
using Senparc.Weixin.MP;
using Senparc.Weixin.WxOpen.AdvancedAPIs.Sns;
using Senparc.Weixin.WxOpen.Containers;
using Senparc.Weixin.WxOpen.Entities;
using Senparc.Weixin.WxOpen.Entities.Request;
using Senparc.Weixin.WxOpen.Helpers;
using System;
using System.Threading.Tasks;

namespace OrchardCoreExample.WeCharMiniProgram.Module.Controllers
{
    [Route("api/miniprogram/[action]")]
    [Authorize(AuthenticationSchemes = "Api"), IgnoreAntiforgeryToken, AllowAnonymous]
    [ApiController]
    public class MiniProgramController : Controller
    {
        private readonly ISiteService _siteService;
        private readonly IAuthorizationService _authorizationService;
        private readonly MiniProgramSettingModel _miniProgramSetting;

        public MiniProgramController(IAuthorizationService authorizationService, ISiteService siteService)
        {
            _authorizationService = authorizationService;
            _siteService = siteService;
            _miniProgramSetting = _siteService.GetSiteSettingsAsync().Result.As<MiniProgramSettingModel>();
        }

        /// <summary>
        /// GET请求用于处理微信小程序后台的URL验证
        /// </summary>
        /// <param name="postModel"></param>
        /// <param name="echostr"></param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("Index")]
        public ActionResult Get([FromForm]PostModel postModel, string echostr)
        {
            if (CheckSignature.Check(postModel.Signature, postModel.Timestamp, postModel.Nonce, _miniProgramSetting.WxOpenToken))
            {
                return Content(echostr); //返回随机字符串则表示验证通过
            }
            else
            {
                return Content(
                    $"failed:{postModel.Signature},{CheckSignature.GetSignature(postModel.Timestamp, postModel.Nonce, _miniProgramSetting.WxOpenToken)}。如果你在浏览器中看到这句话，说明此地址可以被作为微信小程序后台的Url，请注意保持Token一致。");
            }

        }

        /// <summary>
        /// wx.login登陆成功之后发送的请求
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> OnLogin([FromBody] JObject param)
        {
            // 不设置权限访问可以不做以下验证
            if (!await _authorizationService.AuthorizeAsync(User, MiniProgramPermission.WeCharMiniProgramAccess))
            {
                return Unauthorized(new { success = false, msg = "未授权访问" });
            }
            try
            {
                var jsonResult = await SnsApi.JsCode2JsonAsync(_miniProgramSetting.WxOpenAppId, _miniProgramSetting.WxOpenAppSecret, param["code"].Value<string>());
                if (jsonResult.errcode == ReturnCode.请求成功)
                {
                    var unionId = "";
                    // 定义sessionId为openid
                    var sessionBag = await SessionContainer.UpdateSessionAsync(jsonResult.openid, jsonResult.openid, jsonResult.session_key, unionId);
                    // 返回 sessionId
                    return Json(new { success = true, msg = "OK", sessionId = sessionBag.Key });
                }
                else
                {
                    return Json(new { success = false, msg = jsonResult.errmsg });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, msg = ex.Message });
            }
        }

        /// <summary>
        /// 签名验证
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CheckWxOpenSignature([FromBody] JObject param)
        {
            if (!await _authorizationService.AuthorizeAsync(User, MiniProgramPermission.WeCharMiniProgramAccess))
            {
                return Unauthorized(new { success = false, msg = "未授权访问" });
            }
            try
            {
                var sessionBag = await SessionContainer.GetSessionAsync(param["sessionId"].Value<string>());
                if (sessionBag == null)
                {
                    return Json(new { success = false, msg = "请先登录！" });
                }
                var checkSuccess = EncryptHelper.CheckSignature(sessionBag.Key, param["rawData"].Value<string>(), param["signature"].Value<string>());
                return Json(new { success = checkSuccess, msg = checkSuccess ? "签名校验成功" : "签名校验失败" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, msg = ex.Message });
            }
        }

        /// <summary>
        /// 解密用户消息
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> DecodeEncryptedData([FromBody] JObject param)
        {
            if (!await _authorizationService.AuthorizeAsync(User, MiniProgramPermission.WeCharMiniProgramAccess))
            {
                return Unauthorized(new { success = false, msg = "未授权访问" });
            }
            DecodeEntityBase decodedEntity = null;

            try
            {
                switch (param["type"].Value<string>().ToUpper())
                {
                    case "USERINFO": //wx.getUserInfo()
                        decodedEntity = EncryptHelper.DecodeUserInfoBySessionId(param["sessionId"].Value<string>(),
                            param["encryptedData"].Value<string>(), param["iv"].Value<string>());
                        break;
                    default:
                        break;
                }
            }
            catch
            {

            }

            //检验水印
            var checkWatermark = false;
            var openId = "";
            if (decodedEntity != null)
            {
                checkWatermark = decodedEntity.CheckWatermark(_miniProgramSetting.WxOpenAppId);

                //保存用户信息（可选）
                if (checkWatermark && decodedEntity is DecodedUserInfo decodedUserInfo)
                {
                    var sessionBag = await SessionContainer.GetSessionAsync(param["sessionId"].Value<string>()).ConfigureAwait(true);
                    if (sessionBag != null)
                    {

                        await SessionContainer.AddDecodedUserInfoAsync(sessionBag, decodedUserInfo).ConfigureAwait(true);
                    }
                }

                var userInfo = (DecodedUserInfo)decodedEntity;
                openId = userInfo.openId;
                // 下面可以做持久化将小程序用户创建或者更新到数据库
            }

            //注意：此处仅为演示，敏感信息请勿传递到客户端！
            return Json(new
            {
                success = checkWatermark,
                decodedEntity = decodedEntity,
                msg = $"水印验证：{(checkWatermark ? "通过" : "不通过")}",
                openId = openId
            });
        }

        /// <summary>
        /// 解密电话号码
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> DecryptPhoneNumber([FromBody] JObject param)
        {
            if (!await _authorizationService.AuthorizeAsync(User, MiniProgramPermission.WeCharMiniProgramAccess))
            {
                return Unauthorized(new { success = false, msg = "未授权访问" });
            }
            var sessionBag = await SessionContainer.GetSessionAsync(param["sessionId"].Value<string>());
            if (sessionBag == null)
            {
                return Json(new { success = false, msg = "请先登录！" });
            }
            try
            {
                var phoneNumber = EncryptHelper.DecryptPhoneNumber(sessionBag.Key, param["encryptedData"].ToString(), param["iv"].ToString());

                //throw new WeixinException("解密PhoneNumber异常测试");//启用这一句，查看客户端返回的异常信息

                return Json(new { success = true, phoneNumber = phoneNumber });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, msg = ex.Message, param });
            }
        }
    }
}
