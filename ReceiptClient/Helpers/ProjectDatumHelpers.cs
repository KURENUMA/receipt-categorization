using C1.Win.C1FlexGrid;
using ReceiptClient.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using ReceiptClient.Shared;

namespace ReceiptClient
{
    public static class ProjectDatumHelpers
    {
        /// <summary>
        /// データマッピングを設定
        /// 表示したいデータは本個所で設定する
        /// 列名 | 幅 | データの型 | 文字位置 | フォント | 設定するデータ
        /// ラムダ式を使用して設定→ラムダ式：簡潔な関数（メソッド）の表現方法
        /// 例：data => ((ReceiptInfo)data).Idはdataオブジェクトを受け取り、ReceiptInfoにキャストしてIdプロパティの値を返す動作となる
        /// </summary>
        public static readonly List<ColumnMappingConfig> ColumnConfigs = new List<ColumnMappingConfig>
        {
            new ColumnMappingConfig("ID", 50, typeof(int), TextAlignEnum.LeftCenter, new Font("Arial", 10),
                new LambdaMappingStrategy(data => ((ReceiptInfo)data).Id), "Id", false),

            new ColumnMappingConfig("領収書番号", 50, typeof(string), TextAlignEnum.LeftCenter, new Font("Arial", 10),
                new LambdaMappingStrategy(data => ((ReceiptInfo)data).領収書番号), "領収書番号", false),

            new ColumnMappingConfig("金額", 50, typeof(decimal), TextAlignEnum.RightCenter, new Font("Arial", 10),
                new LambdaMappingStrategy(data => ((ReceiptInfo)data).金額), "金額", false),

            new ColumnMappingConfig("日付", 100, typeof(DateTime), TextAlignEnum.CenterCenter, new Font("Arial", 10),
                new LambdaMappingStrategy(data => ((ReceiptInfo)data).日付), "日付", false),

            new ColumnMappingConfig("説明", 150, typeof(string), TextAlignEnum.LeftCenter, new Font("Arial", 10),
                new LambdaMappingStrategy(data => ((ReceiptInfo)data).説明), "説明", false),

            new ColumnMappingConfig("ベンダー名", 150, typeof(string), TextAlignEnum.LeftCenter, new Font("Arial", 10),
                new LambdaMappingStrategy(data => ((ReceiptInfo)data).ベンダー名), "ベンダー名", false),

            new ColumnMappingConfig("支払い方法", 120, typeof(string), TextAlignEnum.LeftCenter, new Font("Arial", 10),
                new LambdaMappingStrategy(data => ((ReceiptInfo)data).支払い方法), "支払い方法", false),

            new ColumnMappingConfig("備考", 200, typeof(string), TextAlignEnum.LeftCenter, new Font("Arial", 10),
                new LambdaMappingStrategy(data => ((ReceiptInfo)data).備考), "備考", false)
        };
    }
}
