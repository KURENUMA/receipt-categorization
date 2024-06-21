using C1.Win.C1FlexGrid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using ReceiptClient.Models;
using ReceiptClient.Shared;
using ReceiptClient.ViewModels;

namespace ReceiptClient.UserControls
{
    public partial class NotificationGrid : UserControl
    {
        private NotificationGridManager _manager;
        private GridOrderManager _orderManager;
        public NotificationGrid(
            //ProjectGridManager manager = null, 
            //GridOrderManager orderManager=null
            NotificationGridManager manager = null
            )
        {
            _manager = manager ?? new NotificationGridManager();
            //_orderManager = orderManager;
            InitializeComponent();

            c1FlexGrid1.DataSource = _manager.Dt; 
            //pagination1.TotalPages = _manager.TotalPages;

            //_manager.OnDataSourceChange += (object sender, EventArgs args) =>
            //{
            //    IndexRowNumbers();
            //    this.pagination1.TotalPages = _manager.TotalPages;
            //};
            //this.pagination1.onPageChange += (object sender, int page) =>
            //{
            //    _manager.SetCurrentPage(page);
            //    _manager.Reload();
            //};

            //c1FlexGrid1.AfterDragColumn += GdProjectList_AfterDragColumn;
            ////if(_orderManager != null )
            //InitializeColumns();
        }

        private EventHandler<Notification> OnUpdateNotification()
        {
            return (sender, notif) => {
                _manager.UpdateNotification(notif);
                _manager.SaveToJson();
                _manager.Reload();
            };
        }

        private void NotificationGrid_Load(object sender, EventArgs e)
        {
            _manager.Reload();
        }


        //private void InitializeColumns()
        //{
        //    // ヘッダの設定
        //    c1FlexGrid1.Cols[0].Caption = "";
        //    c1FlexGrid1.Cols[0].Width = 30;
        //    List<ColumnSetting> configs = _orderManager.GetColumnSettingsForSelectedPattern();

        //    c1FlexGrid1.Cols.Count = configs.Count + 1;

        //    int columnIndexOffset = 1;
        //    for (int i = 0; i < configs.Count; i++)
        //    {
        //        var config = ProjectDatumHelpers.ColumnConfigs.First(x => x.Caption == configs[i].ColumnName);
        //        c1FlexGrid1.Cols[i + columnIndexOffset].Caption = config.Caption;
        //        c1FlexGrid1.Cols[i + columnIndexOffset].Width = config.Width;
        //        c1FlexGrid1.Cols[i + columnIndexOffset].StyleNew.TextAlign = config.TextAlign;
        //        c1FlexGrid1.Cols[i + columnIndexOffset].StyleNew.Font = config.Font;
        //    }
        //}

        //private void IndexRowNumbers()
        //{
        //    //すべての行についてループを実行
        //    for (int rowIndex = 1; rowIndex < c1FlexGrid1.Rows.Count; rowIndex++)
        //    {
        //        c1FlexGrid1[rowIndex, 0] = rowIndex; // 行のインデックスをCols[0]に設定
        //    }
        //}

        ///// <summary>
        ///// 列をドラッグで入れ替えた際に表示順をJSONファイルに保存する
        ///// </summary>
        //private void GdProjectList_AfterDragColumn(object sender, DragRowColEventArgs e)
        //{

        //    // 現在の列順序を取得
        //    List<string> currentColumnOrder = new List<string>();
        //    for (int i = 1; i < c1FlexGrid1.Cols.Count; i++)
        //    {
        //        // 1列目はヘッダ列のため、1からスタート
        //        currentColumnOrder.Add(c1FlexGrid1.Cols[i].Name);
        //    }

        //    // JSONを更新
        //    _orderManager.UpdateColumnOrderForPattern(_orderManager.SelectedPattern, currentColumnOrder.ToArray());

        //    // 更新されたJSONデータをファイルに保存
        //    _orderManager.SaveToFile();
        //}

        //public void ApplyColumnOrder(string patternName)
        //{
        //    // 列の順序と設定を取得
        //    var columnOrder = _orderManager.GetColumnOrderForPattern(patternName);

        //    // グリッドの列順序を更新
        //    for (int i = 0; i < columnOrder.Length; i++)
        //    {
        //        var col = c1FlexGrid1.Cols[columnOrder[i]];

        //        // 最初の列（行ヘッダ用の列）を移動しないようにする
        //        if (col.Index == 0) continue;

        //        // 移動先のインデックスも0を避けるようにする
        //        int targetIndex = i + 1; // iが0から始まるため、最初の移動先インデックスは1

        //        c1FlexGrid1.Cols.Move(col.Index, targetIndex);
        //    }
        //}

        //public void ApplyColumnSettings(string patternName)
        //{
        //    var columnSettings = _orderManager.GetColumnSettingsForPattern(patternName);
        //    foreach (var setting in columnSettings)
        //    {
        //        var col = c1FlexGrid1.Cols[setting.ColumnName];
        //        col.Width = setting.Width;
        //        col.Visible = setting.Visible;
        //    }
        //}
    }
}
