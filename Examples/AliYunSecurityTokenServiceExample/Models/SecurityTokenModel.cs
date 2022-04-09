using System;
using Newtonsoft.Json;

namespace AliYunSecurityTokenServiceExample.Models
{
    /// <summary>
    /// SecurityTokenModel
    /// </summary>
    public class SecurityTokenModel
    {
        /// <summary>
        /// AccessKeyId
        /// </summary>
        [JsonProperty("accessKeyId")]
        public string AccessKeyId { get; set; } = string.Empty;

        /// <summary>
        /// AccessKeySecret
        /// </summary>
        [JsonProperty("accessKeySecret")]
        public string AccessKeySecret { get; set; } = string.Empty;

        /// <summary>
        /// SecurityToken
        /// </summary>
        [JsonProperty("securityToken")]
        public string SecurityToken { get; set; } = string.Empty;
        /// <summary>
        /// Expiration
        /// </summary>
        [JsonProperty("expiration")]
        public DateTime Expiration { get; set; }
    }
}
