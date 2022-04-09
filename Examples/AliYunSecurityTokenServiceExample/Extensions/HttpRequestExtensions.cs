using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace AliYunSecurityTokenServiceExample.Extensions
{
    public static class HttpRequestExtensions
    {
        private static readonly JsonSerializerSettings JsonSettings = new JsonSerializerSettings()
        {
            NullValueHandling = NullValueHandling.Ignore
        };

        /// <summary>
        /// PostAsJsonAsync
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="client"></param>
        /// <param name="requestUri"></param>
        /// <param name="value"></param>
        /// <param name="token"></param>
        /// <param name="settings"></param>
        /// <returns></returns>
        public static Task<HttpResponseMessage> PostAsJsonAsync<T>(
            this HttpClient client,
            string requestUri,
            T value, string token = null,
            JsonSerializerSettings settings = null)
        {
            var content = new StringContent(
                JsonConvert.SerializeObject(value, settings ?? JsonSettings),
                Encoding.UTF8,
                "application/json");

            var request = new HttpRequestMessage(HttpMethod.Post, requestUri) { Content = content };

            request.Headers
                .Accept
                .Add(new MediaTypeWithQualityHeaderValue("application/json"));
            if (!string.IsNullOrEmpty(token))
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            return client.SendAsync(request);
        }

        /// <summary>
        /// PostJsonAsync
        /// </summary>
        /// <param name="client"></param>
        /// <param name="requestUri"></param>
        /// <param name="json"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public static Task<HttpResponseMessage> PostJsonAsync(
            this HttpClient client,
            string requestUri,
            string json, string token = null)
        {
            var content = new StringContent(
                json,
                Encoding.UTF8,
                "application/json");

            var request = new HttpRequestMessage(HttpMethod.Post, requestUri)
            {
                Content = content
            };

            request.Headers
                .Accept
                .Add(new MediaTypeWithQualityHeaderValue("application/json"));
            if (!string.IsNullOrEmpty(token))
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            client.DefaultRequestHeaders.Add("Accept", "application/json, text/javascript, */*; q=0.01");
            client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_15_7) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/90.0.4430.93 Safari/537.36 Edg/90.0.818.56");

            return client.SendAsync(request);
        }
    }
}
