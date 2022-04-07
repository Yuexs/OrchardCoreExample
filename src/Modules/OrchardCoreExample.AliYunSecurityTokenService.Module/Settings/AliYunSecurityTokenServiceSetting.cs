namespace OrchardCoreExample.AliYunSecurityTokenService.Module.Settings
{
    public class AliYunSecurityTokenServiceSetting
    {
        /// <summary>
        ///     存储空间名称
        /// </summary>
        public string BucketName { get; set; }

        /// <summary>
        ///     地域名称
        /// </summary>
        public string RegionName { get; set; }

        /// <summary>
        ///     AccessKeyId
        /// </summary>
        public string AccessKeyId { get; set; }

        /// <summary>
        ///     AccessKeySecret
        /// </summary>
        public string AccessKeySecret { get; set; }

        /// <summary>
        /// RoleArn
        /// </summary>
        public string RoleArn { get; set; }

        /// <summary>
        /// Policy
        /// </summary>
        public string Policy { get; set; }
    }
}