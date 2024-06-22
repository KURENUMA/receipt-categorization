using System;

namespace ReceiptClient.Models
{
    public class ReceiptInfo
    {
        // 領収書ID
        public int 領収書ID { get; set; }

        // テナントID
        public int テナントID { get; set; }

        // クライアントID
        public int クライアントID { get; set; }

        // 税理士ID
        public int 税理士ID { get; set; }

        // 取引日時
        public DateTime 取引日時 { get; set; }

        // 取引内容
        public string 取引内容 { get; set; } = null!;

        // 金額
        public decimal 金額 { get; set; }

        // 内消費税
        public decimal 内消費税 { get; set; }

        // 処理ステータス
        public short 処理ステータス { get; set; }

        // 画像データパス
        public string 画像データパス { get; set; } = null!;

        // 作成日時
        public DateTime 作成日時 { get; set; }

        // 作成者ID
        public int 作成者ID { get; set; }

        // 更新日時
        public DateTime 更新日時 { get; set; }

        // 更新者ID
        public int 更新者ID { get; set; }

        // 削除フラグ
        public bool 削除フラグ { get; set; }

        // 削除日時
        public DateTime? 削除日時 { get; set; }
    }
}
