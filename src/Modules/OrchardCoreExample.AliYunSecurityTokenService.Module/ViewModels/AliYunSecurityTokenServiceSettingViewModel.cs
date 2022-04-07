using System.ComponentModel.DataAnnotations;

namespace OrchardCoreExample.AliYunSecurityTokenService.Module.ViewModels
{
    public class AliYunSecurityTokenServiceSettingViewModel
    {
        /// <summary>
        ///     存储空间名称
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "存储空间名称 不允许为空")]
        public string BucketName { get; set; }

        /// <summary>
        ///     地域名称
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "地域名称 不允许为空")]
        public string RegionName { get; set; }

        /// <summary>
        ///     AccessKeyId
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "AccessKeyId 不允许为空")]
        public string AccessKeyId { get; set; }

        /// <summary>
        ///     AccessKeySecret
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "AccessKeySecret 不允许为空")]
        public string AccessKeySecret { get; set; }

        /// <summary>
        /// RoleArn
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "RoleArn 不允许为空")]
        public string RoleArn { get; set; }

        /// <summary>
        /// Policy
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "Policy 不允许为空")]
        public string Policy { get; set; }
    }
}
