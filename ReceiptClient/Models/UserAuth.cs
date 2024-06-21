
namespace ReceiptClient.Models
{
    public class UserAuth
    {
        public int ID { get; set; }
        public string ユーザーID { get; set; }
        public string パスワードハッシュ { get; set; }
        public int アクセスレベル { get; set; }
    }
}
