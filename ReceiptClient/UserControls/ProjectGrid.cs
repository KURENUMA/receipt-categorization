using C1.Win.C1FlexGrid;
using ClosedXML.Excel;
using System;
using System.IO;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using ReceiptClient.Models;
using ReceiptClient.Shared;

namespace ReceiptClient.UserControls
{
    public partial class ProjectGrid : UserControl
    {
        private ProjectGridManager _manager;
        private GridOrderManager _orderManager;
        private GridColorManager _gridColorManager;
        public event EventHandler GridDoubleClick;
        public event EventHandler GridDataBindingComplete;

        public ProjectGrid(ProjectGridManager manager = null, GridOrderManager orderManager=null, GridColorManager gridColorManager=null)
        {
            _gridColorManager = gridColorManager;
            _manager = manager;
            _orderManager = orderManager;
            InitializeComponent();
           
            c1FlexGrid1.DataSource = _manager.Dt;
            pagination1.TotalPages = _manager.TotalPages;

            _manager.OnDataSourceChange += (object sender, EventArgs args) =>
            {
                IndexRowNumbers( 1 );
                this.pagination1.TotalPages = _manager.TotalPages;
            };
            this.pagination1.onPageChange += (object sender, int page) =>
            {
                _manager.SetCurrentPage(page);
                _manager.SyncPage();
                IndexRowNumbers(page);
                SetGridColors();
            };
           
            c1FlexGrid1.AfterDragColumn += GdProjectList_AfterDragColumn;
            c1FlexGrid1.DoubleClick += new EventHandler(c1FlexGrid1_DoubleClick);
            c1FlexGrid1.DataBindingComplete += new EventHandler(c1FlexGrid1_DataBindingComplete);
            InitializeColumns();

            c1FlexGrid1.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            c1FlexGrid1.AllowEditing = false;
        }

        private void ProjectGrid_Load(object sender, EventArgs e)
        {
            _manager.Reload();
        }

        /// <summary>
        /// ダブルクリックイベントの発行
        /// </summary>
        private void c1FlexGrid1_DoubleClick(object sender, EventArgs e)
        {
            GridDoubleClick?.Invoke(this, e);
        }

        /// <summary>
        /// バインド完了イベントの発行（仮）
        /// </summary>
        private void c1FlexGrid1_DataBindingComplete(object sender, EventArgs e)
        {
            GridDataBindingComplete?.Invoke(this, e);
        }

        private void InitializeColumns()
        {
            // ヘッダの設定
            c1FlexGrid1.Cols[0].Caption = "";
            c1FlexGrid1.Cols[0].Width = 30;
        }

        private void IndexRowNumbers(int page )
        {
            int pageSize = _manager.pageSize;

            //すべての行についてループを実行
            for (int rowIndex = 1; rowIndex < c1FlexGrid1.Rows.Count; rowIndex++)
            {
                c1FlexGrid1[rowIndex, 0] = rowIndex + ( pageSize * (page - 1));
            }
        }

        /// <summary>
        /// 列をドラッグで入れ替えた際に表示順をJSONファイルに保存する
        /// </summary>
        private void GdProjectList_AfterDragColumn(object sender, DragRowColEventArgs e)
        {

            // 現在の列順序を取得
            List<string> currentColumnOrder = new List<string>();
            for (int i = 1; i < c1FlexGrid1.Cols.Count; i++)
            {
                // 1列目はヘッダ列のため、1からスタート
                currentColumnOrder.Add(c1FlexGrid1.Cols[i].Name);
            }

            // JSONを更新
            _orderManager.UpdateColumnOrderForPattern(_orderManager.SelectedPattern, currentColumnOrder.ToArray());

            // 更新されたJSONデータをファイルに保存
            _orderManager.SaveToFile();
        }

        public string getEpcoNumber()
        {
            // 選択されている行のインデックスを取得
            int rowIndex = c1FlexGrid1.Row;

            string epcoNumber = "";

            // 行が有効な範囲内かどうか確認
            if (rowIndex >= 0 && rowIndex < c1FlexGrid1.Rows.Count)
            {
                // "エプコ管理番号" 列の値を取得
                var cellValue = c1FlexGrid1[rowIndex, "Aoba管理番号"];
                if (cellValue != null)
                {
                    epcoNumber = cellValue.ToString();
                }
            }

            return epcoNumber;
        }

        public void ApplyColumnSettings(string patternName)
        {
            var columnSettings = _orderManager.GetColumnSettingsForPattern(patternName);
            foreach (var setting in columnSettings)
            {
                var col = c1FlexGrid1.Cols[setting.ColumnName];
                col.Width = setting.Width;
                col.Visible = setting.Visible;
            }
        }

        public int GetProjectCount()
        {
            return _manager.Total;
        }

        public string GetFilterOptionStr()
        {
            return _manager.FilterOptionStr;
        }

        /// <summary>
        /// C1FlexGridの表示状態を設定
        /// true：C1FlexGridを表示
        /// false：非表示
        /// </summary>
        /// <param name="isVisible">ページネーションコントロールを表示する場合は true、非表示にする場合は false。</param>
        public void SetFlexGridVisibility(bool isVisible)
        {
            c1FlexGrid1.Visible = isVisible;
        }

        /// <summary>
        /// ページネーションコントロールの表示状態を設定
        /// true：ページネーションコントロールを表示
        /// false：非表示
        /// </summary>
        /// <param name="isVisible">ページネーションコントロールを表示する場合は true、非表示にする場合は false。</param>
        public void SetPaginationVisibility(bool isVisible)
        {
            pagination1.Visible = isVisible;
        }

        /// <summary>
        /// C1FlexGridの背景色と文字色を行単位で設定する
        /// </summary>
        public void SetGridColors()
        {
            // WebAPIからデータを取得
            var processDetails = _gridColorManager.GetAllUserColorSettings("default");

            // "作業工程名" 列が存在するか確認
            int colIndex = c1FlexGrid1.Cols.IndexOf("作業工程名");
            if (colIndex == -1)
            {
                // 列が存在しない場合、終了
                Console.WriteLine("列 '作業工程名' が見つかりません。");
                return;
            }

            // 行ごとにループして色を設定
            for (int row = 1; row < c1FlexGrid1.Rows.Count; row++)
            {
                string processName = c1FlexGrid1[row, "作業工程名"].ToString(); // 列の値を取得

                // ProcessDetailを検索
                if (processDetails.ContainsKey(processName))
                {
                    var matchingDetail = processDetails[processName];
                    if (matchingDetail.isValid)
                    {
                        // 背景色と文字色を設定
                        c1FlexGrid1.Rows[row].StyleNew.BackColor = ColorTranslator.FromHtml(matchingDetail.Background);
                        c1FlexGrid1.Rows[row].StyleNew.ForeColor = ColorTranslator.FromHtml(matchingDetail.Foreground);
                    }
                }
            }
            c1FlexGrid1.Invalidate();
        }
    }
}
