using System.Collections.Generic;

namespace ReceiptClient.Shared
{
    public enum ErrorCode
    {
        NO_ERROR = 0,
        NO_LOGIN = -1,
        NO_USER = -2,
        NO_MAIL = -3,
        WPNO_EXISTS = -4,
        BAD_PASSCODE = -5,
        INVALID_USER_ID = -6,
    }

    internal class Constants
    {
        // 以下にWebAPIのリンクを記載する
        public const string API_DOMAIN = "https://localhost:7020";
        //public const string API_DOMAIN = "https://xxxxxx.azurewebsites.net";

        public const string orderSettingsFileName = "orderSetting.json";
        public static readonly Dictionary<string, string> API = new Dictionary<string, string>
        {
            //ログイン
            { "login", API_DOMAIN + "/api/login" },

            { "projectData", API_DOMAIN + "/api/project" },
            { "search", API_DOMAIN + "/api/project/search" },
            { "init", API_DOMAIN + "/api/init" },
            { "gridData", API_DOMAIN + "/api/gridData"},

            //色設定
            { "get_process_details", API_DOMAIN + "/api/get_process_details" }
        };
    }
}
