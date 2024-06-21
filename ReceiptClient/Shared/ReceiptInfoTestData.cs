using System;
using System.Collections.Generic;
using ReceiptClient.Models;

namespace ReceiptClient.TestData
{
    public static class ReceiptInfoTestData
    {
        public static List<ReceiptInfo> GetTestData()
        {
            return new List<ReceiptInfo>
            {
                new ReceiptInfo
                {
                    Id = 1,
                    領収書番号 = "R001",
                    金額 = 5000,
                    日付 = DateTime.Now,
                    説明 = "文房具",
                    ベンダー名 = "文房具店",
                    支払い方法 = "クレジットカード",
                    備考 = "備考1"
                },
                new ReceiptInfo
                {
                    Id = 2,
                    領収書番号 = "R002",
                    金額 = 15000,
                    日付 = DateTime.Now.AddDays(-1),
                    説明 = "ビジネスランチ",
                    ベンダー名 = "レストラン",
                    支払い方法 = "現金",
                    備考 = "備考2"
                },
                new ReceiptInfo
                {
                    Id = 3,
                    領収書番号 = "R003",
                    金額 = 7000,
                    日付 = DateTime.Now.AddDays(-2),
                    説明 = "タクシー代",
                    ベンダー名 = "タクシー会社",
                    支払い方法 = "クレジットカード",
                    備考 = "備考3"
                },
                new ReceiptInfo
                {
                    Id = 4,
                    領収書番号 = "R004",
                    金額 = 3000,
                    日付 = DateTime.Now.AddDays(-3),
                    説明 = "飲み物代",
                    ベンダー名 = "カフェ",
                    支払い方法 = "現金",
                    備考 = "備考4"
                },
                new ReceiptInfo
                {
                    Id = 5,
                    領収書番号 = "R005",
                    金額 = 12000,
                    日付 = DateTime.Now.AddDays(-4),
                    説明 = "ホテル宿泊費",
                    ベンダー名 = "ホテル",
                    支払い方法 = "クレジットカード",
                    備考 = "備考5"
                },
                new ReceiptInfo
                {
                    Id = 6,
                    領収書番号 = "R006",
                    金額 = 8000,
                    日付 = DateTime.Now.AddDays(-5),
                    説明 = "会議室レンタル料",
                    ベンダー名 = "レンタルスペース",
                    支払い方法 = "現金",
                    備考 = "備考6"
                },
                new ReceiptInfo
                {
                    Id = 7,
                    領収書番号 = "R007",
                    金額 = 4500,
                    日付 = DateTime.Now.AddDays(-6),
                    説明 = "お菓子代",
                    ベンダー名 = "コンビニ",
                    支払い方法 = "クレジットカード",
                    備考 = "備考7"
                },
                new ReceiptInfo
                {
                    Id = 8,
                    領収書番号 = "R008",
                    金額 = 6500,
                    日付 = DateTime.Now.AddDays(-7),
                    説明 = "駐車料金",
                    ベンダー名 = "駐車場",
                    支払い方法 = "現金",
                    備考 = "備考8"
                },
                new ReceiptInfo
                {
                    Id = 9,
                    領収書番号 = "R009",
                    金額 = 2000,
                    日付 = DateTime.Now.AddDays(-8),
                    説明 = "コーヒー代",
                    ベンダー名 = "カフェ",
                    支払い方法 = "クレジットカード",
                    備考 = "備考9"
                },
                new ReceiptInfo
                {
                    Id = 10,
                    領収書番号 = "R010",
                    金額 = 11000,
                    日付 = DateTime.Now.AddDays(-9),
                    説明 = "ディナー代",
                    ベンダー名 = "レストラン",
                    支払い方法 = "現金",
                    備考 = "備考10"
                },
                new ReceiptInfo
                {
                    Id = 11,
                    領収書番号 = "R011",
                    金額 = 25000,
                    日付 = DateTime.Now.AddDays(-10),
                    説明 = "パソコン購入",
                    ベンダー名 = "家電量販店",
                    支払い方法 = "クレジットカード",
                    備考 = "備考11"
                },
                new ReceiptInfo
                {
                    Id = 12,
                    領収書番号 = "R012",
                    金額 = 1000,
                    日付 = DateTime.Now.AddDays(-11),
                    説明 = "文房具",
                    ベンダー名 = "文房具店",
                    支払い方法 = "現金",
                    備考 = "備考12"
                },
                new ReceiptInfo
                {
                    Id = 13,
                    領収書番号 = "R013",
                    金額 = 500,
                    日付 = DateTime.Now.AddDays(-12),
                    説明 = "飲み物代",
                    ベンダー名 = "自動販売機",
                    支払い方法 = "現金",
                    備考 = "備考13"
                },
                new ReceiptInfo
                {
                    Id = 14,
                    領収書番号 = "R014",
                    金額 = 800,
                    日付 = DateTime.Now.AddDays(-13),
                    説明 = "タクシー代",
                    ベンダー名 = "タクシー会社",
                    支払い方法 = "現金",
                    備考 = "備考14"
                },
                new ReceiptInfo
                {
                    Id = 15,
                    領収書番号 = "R015",
                    金額 = 6000,
                    日付 = DateTime.Now.AddDays(-14),
                    説明 = "昼食代",
                    ベンダー名 = "レストラン",
                    支払い方法 = "クレジットカード",
                    備考 = "備考15"
                },
                new ReceiptInfo
                {
                    Id = 16,
                    領収書番号 = "R016",
                    金額 = 700,
                    日付 = DateTime.Now.AddDays(-15),
                    説明 = "コーヒー代",
                    ベンダー名 = "カフェ",
                    支払い方法 = "現金",
                    備考 = "備考16"
                },
                new ReceiptInfo
                {
                    Id = 17,
                    領収書番号 = "R017",
                    金額 = 1500,
                    日付 = DateTime.Now.AddDays(-16),
                    説明 = "駐車料金",
                    ベンダー名 = "駐車場",
                    支払い方法 = "クレジットカード",
                    備考 = "備考17"
                },
                new ReceiptInfo
                {
                    Id = 18,
                    領収書番号 = "R018",
                    金額 = 2000,
                    日付 = DateTime.Now.AddDays(-17),
                    説明 = "お菓子代",
                    ベンダー名 = "コンビニ",
                    支払い方法 = "現金",
                    備考 = "備考18"
                },
                new ReceiptInfo
                {
                    Id = 19,
                    領収書番号 = "R019",
                    金額 = 3000,
                    日付 = DateTime.Now.AddDays(-18),
                    説明 = "文房具",
                    ベンダー名 = "文房具店",
                    支払い方法 = "クレジットカード",
                    備考 = "備考19"
                },
                new ReceiptInfo
                {
                    Id = 20,
                    領収書番号 = "R020",
                    金額 = 4000,
                    日付 = DateTime.Now.AddDays(-19),
                    説明 = "飲み物代",
                    ベンダー名 = "カフェ",
                    支払い方法 = "現金",
                    備考 = "備考20"
                },
                new ReceiptInfo
                {
                    Id = 21,
                    領収書番号 = "R021",
                    金額 = 5000,
                    日付 = DateTime.Now.AddDays(-20),
                    説明 = "タクシー代",
                    ベンダー名 = "タクシー会社",
                    支払い方法 = "クレジットカード",
                    備考 = "備考21"
                },
                new ReceiptInfo
                {
                    Id = 22,
                    領収書番号 = "R022",
                    金額 = 6000,
                    日付 = DateTime.Now.AddDays(-21),
                    説明 = "ビジネスランチ",
                    ベンダー名 = "レストラン",
                    支払い方法 = "現金",
                    備考 = "備考22"
                },
                new ReceiptInfo
                {
                    Id = 23,
                    領収書番号 = "R023",
                    金額 = 7000,
                    日付 = DateTime.Now.AddDays(-22),
                    説明 = "駐車料金",
                    ベンダー名 = "駐車場",
                    支払い方法 = "クレジットカード",
                    備考 = "備考23"
                },
                new ReceiptInfo
                {
                    Id = 24,
                    領収書番号 = "R024",
                    金額 = 8000,
                    日付 = DateTime.Now.AddDays(-23),
                    説明 = "昼食代",
                    ベンダー名 = "レストラン",
                    支払い方法 = "現金",
                    備考 = "備考24"
                },
                new ReceiptInfo
                {
                    Id = 25,
                    領収書番号 = "R025",
                    金額 = 9000,
                    日付 = DateTime.Now.AddDays(-24),
                    説明 = "文房具",
                    ベンダー名 = "文房具店",
                    支払い方法 = "クレジットカード",
                    備考 = "備考25"
                },
                new ReceiptInfo
                {
                    Id = 26,
                    領収書番号 = "R026",
                    金額 = 10000,
                    日付 = DateTime.Now.AddDays(-25),
                    説明 = "飲み物代",
                    ベンダー名 = "カフェ",
                    支払い方法 = "現金",
                    備考 = "備考26"
                },
                new ReceiptInfo
                {
                    Id = 27,
                    領収書番号 = "R027",
                    金額 = 11000,
                    日付 = DateTime.Now.AddDays(-26),
                    説明 = "タクシー代",
                    ベンダー名 = "タクシー会社",
                    支払い方法 = "クレジットカード",
                    備考 = "備考27"
                },
                new ReceiptInfo
                {
                    Id = 28,
                    領収書番号 = "R028",
                    金額 = 12000,
                    日付 = DateTime.Now.AddDays(-27),
                    説明 = "ビジネスランチ",
                    ベンダー名 = "レストラン",
                    支払い方法 = "現金",
                    備考 = "備考28"
                },
                new ReceiptInfo
                {
                    Id = 29,
                    領収書番号 = "R029",
                    金額 = 13000,
                    日付 = DateTime.Now.AddDays(-28),
                    説明 = "駐車料金",
                    ベンダー名 = "駐車場",
                    支払い方法 = "クレジットカード",
                    備考 = "備考29"
                },
                new ReceiptInfo
                {
                    Id = 30,
                    領収書番号 = "R030",
                    金額 = 14000,
                    日付 = DateTime.Now.AddDays(-29),
                    説明 = "昼食代",
                    ベンダー名 = "レストラン",
                    支払い方法 = "現金",
                    備考 = "備考30"
                }
            };
        }
    }
}
