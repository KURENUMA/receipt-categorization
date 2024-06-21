using log4net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace ReceiptClient.Shared
{
    public class ApiException : Exception
    {
        public ErrorCode ErrorCode { get; set; }
        public ApiException(ErrorCode code, string message) : base(message) { ErrorCode = code; }
    }

    internal class Comm
    {
        private static readonly ILog logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public static X509Certificate2 Certificate { get; set; }

        private static async Task<string> InternalCallApi(HttpMethod method, string url, object param)
        {
            using (var req = new HttpRequestMessage(method, url))
            {
                if (param != null)
                {
                    req.Content = new StringContent(JsonConvert.SerializeObject(param), Encoding.UTF8, "application/json");
                    logger.Debug(url);
                    logger.Debug(JsonConvert.SerializeObject(param));
                    logger.Debug(await req.Content.ReadAsStringAsync());
                }

                /*
                req.Headers.Authorization = new AuthenticationHeaderValue(Properties.Settings.Default.Authorization);

                if (State.LoginUser != null)
                {
                    req.Headers.Add("type", "0");
                    req.Headers.Add("id", State.LoginUser.Id.ToString());
                    req.Headers.Add("eigyoushoId", State.LoginUser.EigyoushoId.ToString());
                }
                */

                using (var client = new HttpClient())
                {
                    var resp = await client.SendAsync(req);
                    return await resp.Content.ReadAsStringAsync();
                }
            }
        }

        public static async Task<JToken> CallApi(HttpMethod method, string url, object param)
        {
            var resp = await InternalCallApi(method, url, param);

            JToken token = JsonConvert.DeserializeObject<JToken>(resp);
            ErrorCode code = (ErrorCode)(int)token["code"];
            if (code != ErrorCode.NO_ERROR)
            {
                throw (new ApiException(code, (string)token["message"]));
            }
            return token["data"];
        }

        public static async Task<JArray> GetAll(string url)
        {
            var resp = await InternalCallApi(HttpMethod.Get, url, null);
            JToken token = JsonConvert.DeserializeObject<JToken>(resp);

            return (JArray)token["data"];
        }

        public static async Task<JToken> Get1(string url)
        {
            return await CallApi(HttpMethod.Get, url, null);
        }

        public static async Task<JToken> Post(string url, object param)
        {
            return await CallApi(HttpMethod.Post, url, param);
        }

        public static async Task<JToken> Put(string url, object param)
        {
            return await CallApi(HttpMethod.Put, url, param);
        }

        public static async Task<JToken> Delete(string url, object param)
        {
            return await CallApi(HttpMethod.Delete, url, param);
        }

        private static HttpClientHandler CreateRequestHandler()
        {
            HttpClientHandler handler = new HttpClientHandler();
            handler.ClientCertificates.Add(Certificate);
            return handler;
        }

        public static async Task<string> GetAsync(string url)
        {
            using (HttpClient client = new HttpClient())
            {
                client.Timeout = TimeSpan.FromSeconds(10);
                HttpResponseMessage response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsStringAsync();
                }
                else
                {
                    throw new Exception($"Error {response.StatusCode}: {response.ReasonPhrase}");
                }
            }
        }
    }
}
