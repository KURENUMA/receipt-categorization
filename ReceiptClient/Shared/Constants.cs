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
        //public const string API_DOMAIN = "https://localhost:7262";
        public const string API_DOMAIN = "https://shinseiapp.azurewebsites.net";

        public const string orderSettingsFileName = "orderSetting.json";
        public static readonly Dictionary<string, string> API = new Dictionary<string, string>
        {
            // ロック情報
            { "lockOrUnlock", API_DOMAIN + "/api/lock_or_unlock" },
            { "unlock", API_DOMAIN + "/api/unlock" },

            // 情報取得、チェック
            { "getAllManagementAsync", API_DOMAIN + "/api/GetAllManagement" },
            { "view_all_data", API_DOMAIN + "/api/view_all_data" },
            { "getTWorkProcessMaster", API_DOMAIN + "/api/getTWorkProcessMaster" },
            { "getTPrefectureMaster", API_DOMAIN + "/api/getTPrefectureMaster" },
            { "getTUsageMaster", API_DOMAIN + "/api/getTUsageMaster" },
            { "getTManagementItemMaster", API_DOMAIN + "/api/getTManagementItemMaster" },
            { "get_view_data", API_DOMAIN + "/api/get_view_data" },
            { "getTManagementItem", API_DOMAIN + "/api/getTManagementItem" },
            { "getTCityMaster", API_DOMAIN + "/api/getTCityMaster" },
            { "getTCityMasterByPrefectureId", API_DOMAIN + "/api/getTCityMasterByPrefectureId" },
            { "insertOrUpdateTManagementItem", API_DOMAIN + "/api/insertOrUpdateTManagementItem" },
            { "getViewPaymentRequests", API_DOMAIN + "/api/getViewPaymentRequests" },
            { "existsTManagementItem", API_DOMAIN + "/api/existsTManagementItem" },

            // 検索
            { "search_view_date", API_DOMAIN + "/api/Search/search_view_date" },
            
            //マスタメンテナンス系
            { "get_order_recipient_master", API_DOMAIN + "/api/get_order_recipient_master" },
            { "get_construction_companies", API_DOMAIN + "/api/get_construction_companies" },
            { "get_drawing_subcontractors", API_DOMAIN + "/api/get_drawing_subcontractors" },
            { "get_construction_subcontractors", API_DOMAIN + "/api/get_construction_subcontractors" },
            { "put_order_recipient_master", API_DOMAIN + "/api/put_order_recipient_master" },
            { "put_construction_companies", API_DOMAIN + "/api/put_construction_companies" },
            { "put_drawing_subcontractors", API_DOMAIN + "/api/put_drawing_subcontractors" },
            { "put_construction_subcontractors", API_DOMAIN + "/api/put_construction_subcontractors" },

            //ロック情報
            { "get_lock_info", API_DOMAIN + "/api/get_lock_info" },
            { "put_lock_info", API_DOMAIN + "/api/put_lock_info" },

            //色設定
            { "get_process_details", API_DOMAIN + "/api/get_process_details" },

            //ログイン
            { "login", API_DOMAIN + "/api/login" },

            { "projectData", API_DOMAIN + "/api/project" },
            { "search", API_DOMAIN + "/api/project/search" },
            { "init", API_DOMAIN + "/api/init" },
            { "gridData", API_DOMAIN + "/api/gridData"}

        };
    }
}
