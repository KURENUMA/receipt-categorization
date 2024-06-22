using System;
using System.Collections.Generic;
using ReceiptClient.Models;

namespace ReceiptClient
{
    public static class ReceiptInfoTestData
    {
        public static List<ReceiptInfo> GetTestData()
        {
            var testData = new List<ReceiptInfo>();

            for (int i = 1; i <= 30; i++)
            {
                testData.Add(new ReceiptInfo
                {
                    領収書ID = i,
                    テナントID = i % 5 + 1,
                    クライアントID = i % 10 + 1,
                    税理士ID = i % 3 + 1,
                    取引日時 = DateTime.Now.AddDays(-i),
                    取引内容 = $"取引内容サンプル{i}",
                    金額 = 1000.00m * i,
                    内消費税 = 100.00m * i,
                    処理ステータス = (short)(i % 2),
                    画像データパス = $"https://drive.google.com/uc?id=sample_file_id_{i}", // 仮のGoogle DriveのファイルIDを使用
                    作成日時 = DateTime.Now.AddDays(-i),
                    作成者ID = i % 4 + 1,
                    更新日時 = DateTime.Now.AddDays(-(i / 2)),
                    更新者ID = i % 4 + 1,
                    削除フラグ = i % 5 == 0,
                    削除日時 = i % 5 == 0 ? DateTime.Now.AddDays(-i / 2) : (DateTime?)null
                });
            }

            return testData;
        }
    }
}