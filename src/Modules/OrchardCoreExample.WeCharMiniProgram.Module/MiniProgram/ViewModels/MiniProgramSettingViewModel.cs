using System.ComponentModel.DataAnnotations;

namespace OrchardCoreExample.WeCharMiniProgram.Module.MiniProgram.ViewModels
{
    public class MiniProgramSettingViewModel
    {
        /// <summary>
        /// 小程序 appId
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "appId 不允许为空")]
        public string WxOpenAppId { get; set; }

        /// <summary>
        /// 小程序 appSecret
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "appSecret 不允许为空")]
        public string WxOpenAppSecret { get; set; }

        /// <summary>
        /// 小程序 token
        /// </summary
        public string WxOpenToken { get; set; }

        /// <summary>
        /// 小程序 encodingAesKey
        /// </summary>
        public string WxOpenEncodingAesKey { get; set; }
    }
}
