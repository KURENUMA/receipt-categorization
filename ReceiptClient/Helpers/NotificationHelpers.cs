using C1.Win.C1FlexGrid;
using System;
using System.Collections.Generic;
using System.Drawing;
using ReceiptClient.Models;
using ReceiptClient.Shared;

namespace ReceiptClient
{
    public static class NotificationHelpers
    {

        /// <summary>
        /// データマッピングを設定
        /// 表示したいデータは本個所で設定する
        /// 列名 | 幅 | データの型 | 文字位置 | フォント | 設定するデータ
        /// ラムダ式を使用して設定→ラムダ式：簡潔な関数（メソッド）の表現方法
        /// 例：data => ((ProjectDatum)data).noはdataオブジェクトを受け取り、ProjectDatumにキャストしてnoプロパティの値を返す動作となる
        /// </summary>
       
        public static readonly List<ColumnMappingConfig> ColumnConfigs = new List<ColumnMappingConfig>
        {
                new ColumnMappingConfig("id", 80, typeof(int), TextAlignEnum.CenterCenter, new Font("Arial", 12),
                new LambdaMappingStrategy(data => ((Notification)data).Id), "Id", false),
            new ColumnMappingConfig("掲載開始日", 80, typeof(DateTime), TextAlignEnum.CenterCenter, new Font("Arial", 12),
                new LambdaMappingStrategy(data => ((Notification)data).DisplayStartDate), "DisplayStartDate", false),
            new ColumnMappingConfig("掲載終了日", 80, typeof(DateTime), TextAlignEnum.CenterCenter, new Font("Arial", 12),
                new LambdaMappingStrategy(data => ((Notification)data).DisplayEndDate), "DisplayEndDate", false),

            new ColumnMappingConfig("掲載場所", 80, typeof(string), TextAlignEnum.CenterCenter, new Font("Arial", 12),
                new LambdaMappingStrategy(data => ((Notification)data).Placement.Value), "Placement", false),

            new ColumnMappingConfig("タイトル", 100, typeof(string), TextAlignEnum.CenterCenter, new Font("Arial", 12),
                new LambdaMappingStrategy(data => ((Notification)data).Title), "Title", false),

            new ColumnMappingConfig("内容", 100, typeof(string), TextAlignEnum.CenterCenter, new Font("Arial", 12),
                new LambdaMappingStrategy(data => ((Notification)data).Body), "Body", false),

        
        };
    }
}