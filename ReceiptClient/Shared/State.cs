using ReceiptClient.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ReceiptClient.Shared
{
    public class ClientVersionException : Exception
    {
        public ClientVersionException(String version): base(version) { }
    }
    public class SearchParam
    {
        public String Cond { get; set; } = "";
        public int Limit { get; set; } = 30;
    }

    internal class State
    {

        public static async Task initApp()
        {
            Newtonsoft.Json.Linq.JToken token = await Comm.Get1(Constants.API[key: "init"]);
        }

        /// <summary>
        /// 色設定を取得する
        /// </summary>
        public static async Task<List<ProcessDetail>> GetProcessDetails()
        {
            string url = Constants.API["get_process_details"];
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(url).ConfigureAwait(false);
                if (response.IsSuccessStatusCode)
                {
                    string jsonResponse = await response.Content.ReadAsStringAsync();
                    List<ProcessDetail> list = JsonConvert.DeserializeObject<List<ProcessDetail>>(jsonResponse);

                    // 色の変換
                    foreach (var detail in list)
                    {
                        detail.背景色 = DecimalToHex(detail.背景色);
                        detail.文字色 = DecimalToHex(detail.文字色);
                    }

                    return list;
                }
                else
                {
                    throw new Exception($"Error {response.StatusCode}: {response.ReasonPhrase}");
                }
            }
        }

        /// <summary>
        /// 10進数のRGB値を16進数の色コードに変換する
        /// </summary>
        public static string DecimalToHex(string decimalColorString)
        {
            int decimalColor = int.Parse(decimalColorString);
            return $"#{decimalColor:X6}";
        }


        /// <summary>
        /// ログイン認証
        /// </summary>
        /// 
        public class ApiResp<T>
        {
            public string Message { get; set; } = null;
            public int Code { get; set; } = 0;
            public T Data { get; set; }
        }
        public class LoginReq
        {
            public string LoginId { get; set; } = string.Empty;
            public string Password { get; set; } = string.Empty;
        }

        public static async Task<ApiResp<UserAuth>> DoLogin(string loginId, string password)
        {
            string url = Constants.API["login"];
            var loginData = new LoginReq
            {
                LoginId = loginId,
                Password = password
            };


            using (HttpClient client = new HttpClient())
            {
                string jsonRequest = JsonConvert.SerializeObject(loginData);
                StringContent content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync(url, content);

                if (response.IsSuccessStatusCode)
                {
                    string jsonResponse = await response.Content.ReadAsStringAsync();
                    ApiResp<UserAuth> result = JsonConvert.DeserializeObject<ApiResp<UserAuth>>(jsonResponse);
                    return result;
                }
                else
                {
                    throw new Exception($"Error {response.StatusCode}: {response.ReasonPhrase}");
                }
            }
        }

        /// <summary>
        /// LOGIN USER
        /// </summary>
        //ログイン時の処理を追加予定
        public static Models.UserAuth LoginUser { get; set; } = null;
        public static void setLoginUser(Models.UserAuth loginUser)
        {
            LoginUser = loginUser;
        }
    }
}
