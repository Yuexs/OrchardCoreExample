using Newtonsoft.Json;

namespace AliYunSecurityTokenServiceExample.Models
{
    public class AuthorizationModel
    {
        [JsonProperty("token_type")]
        public string TokenType { get; set; }

        [JsonProperty("access_token")]
        public string AccessToken { get; set; }

        [JsonProperty("expires_in")]
        public long ExpiresIn { get; set; }
    }
}
