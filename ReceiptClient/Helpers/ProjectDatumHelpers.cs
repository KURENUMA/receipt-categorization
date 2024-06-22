using System;
using System.Collections.Generic;
using System.Drawing;
using C1.Win.C1FlexGrid;
using ReceiptClient.Models;
using ReceiptClient.Shared;

namespace ReceiptClient
{
    public static class ProjectDatumHelpers
    {
        public static readonly List<ColumnMappingConfig> ColumnConfigs = new List<ColumnMappingConfig>
        {
            new ColumnMappingConfig("領収書ID", 100, typeof(int), TextAlignEnum.LeftCenter, new Font("Arial", 10),
                new LambdaMappingStrategy(data => ((ReceiptInfo)data).領収書ID), "領収書ID", false),

            new ColumnMappingConfig("テナントID", 100, typeof(int), TextAlignEnum.LeftCenter, new Font("Arial", 10),
                new LambdaMappingStrategy(data => ((ReceiptInfo)data).テナントID), "テナントID", false),

            new ColumnMappingConfig("クライアントID", 100, typeof(int), TextAlignEnum.LeftCenter, new Font("Arial", 10),
                new LambdaMappingStrategy(data => ((ReceiptInfo)data).クライアントID), "クライアントID", false),

            new ColumnMappingConfig("税理士ID", 100, typeof(int), TextAlignEnum.LeftCenter, new Font("Arial", 10),
                new LambdaMappingStrategy(data => ((ReceiptInfo)data).税理士ID), "税理士ID", false),

            new ColumnMappingConfig("取引日時", 120, typeof(DateTime), TextAlignEnum.LeftCenter, new Font("Arial", 10),
                new LambdaMappingStrategy(data => ((ReceiptInfo)data).取引日時), "取引日時", false),

            new ColumnMappingConfig("取引内容", 200, typeof(string), TextAlignEnum.LeftCenter, new Font("Arial", 10),
                new LambdaMappingStrategy(data => ((ReceiptInfo)data).取引内容), "取引内容", false),

            new ColumnMappingConfig("金額", 100, typeof(decimal), TextAlignEnum.RightCenter, new Font("Arial", 10),
                new LambdaMappingStrategy(data => ((ReceiptInfo)data).金額), "金額", false),

            new ColumnMappingConfig("内消費税", 100, typeof(decimal), TextAlignEnum.RightCenter, new Font("Arial", 10),
                new LambdaMappingStrategy(data => ((ReceiptInfo)data).内消費税), "内消費税", false),

            new ColumnMappingConfig("処理ステータス", 100, typeof(short), TextAlignEnum.CenterCenter, new Font("Arial", 10),
                new LambdaMappingStrategy(data => ((ReceiptInfo)data).処理ステータス), "処理ステータス", false),

            new ColumnMappingConfig("画像データパス", 200, typeof(string), TextAlignEnum.LeftCenter, new Font("Arial", 10),
                new LambdaMappingStrategy(data => ((ReceiptInfo)data).画像データパス), "画像データパス", false),

            new ColumnMappingConfig("作成日時", 120, typeof(DateTime), TextAlignEnum.LeftCenter, new Font("Arial", 10),
                new LambdaMappingStrategy(data => ((ReceiptInfo)data).作成日時), "作成日時", false),

            new ColumnMappingConfig("作成者ID", 100, typeof(int), TextAlignEnum.LeftCenter, new Font("Arial", 10),
                new LambdaMappingStrategy(data => ((ReceiptInfo)data).作成者ID), "作成者ID", false),

            new ColumnMappingConfig("更新日時", 120, typeof(DateTime), TextAlignEnum.LeftCenter, new Font("Arial", 10),
                new LambdaMappingStrategy(data => ((ReceiptInfo)data).更新日時), "更新日時", false),

            new ColumnMappingConfig("更新者ID", 100, typeof(int), TextAlignEnum.LeftCenter, new Font("Arial", 10),
                new LambdaMappingStrategy(data => ((ReceiptInfo)data).更新者ID), "更新者ID", false),

            new ColumnMappingConfig("削除フラグ", 80, typeof(bool), TextAlignEnum.CenterCenter, new Font("Arial", 10),
                new LambdaMappingStrategy(data => ((ReceiptInfo)data).削除フラグ), "削除フラグ", false),

            new ColumnMappingConfig("削除日時", 120, typeof(DateTime?), TextAlignEnum.LeftCenter, new Font("Arial", 10),
                new LambdaMappingStrategy(data => ((ReceiptInfo)data).削除日時), "削除日時", false)
        };
    }
}
