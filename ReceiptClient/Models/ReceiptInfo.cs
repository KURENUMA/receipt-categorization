using System;
using System.Collections.Generic;

namespace ReceiptClient.Models
{
    public class ReceiptInfo
    {
        // 領収書の識別子
        public int Id { get; set; }

        // 領収書番号
        public string 領収書番号 { get; set; } = null!;

        // 金額
        public decimal 金額 { get; set; }

        // 日付
        public DateTime 日付 { get; set; }

        // 説明
        public string 説明 { get; set; } = null!;

        // ベンダー名
        public string ベンダー名 { get; set; } = null!;

        // 支払い方法
        public string 支払い方法 { get; set; } = null!;

        // 備考
        public string? 備考 { get; set; }
    }
}
