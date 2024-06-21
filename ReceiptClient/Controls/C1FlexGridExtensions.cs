using C1.Win.C1FlexGrid;
using ReceiptClient.Shared;
using Newtonsoft.Json.Linq;
using System;
using System.Data;
using System.Drawing;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ReceiptClient.Controls
{
    public static class C1FlexGridExtensions
    {
        /// <summary>
        /// GridにgridDataAPIから取得したテーブルデータをバインド
        /// </summary>
        public static async Task InitializeGridForDetail(this C1FlexGrid flexGrid, SearchGridPrams searchGridPrams, string showfrom = null)
        {
            //searchAPIとGPTをもとに作成DataTabelを取得してバインドこの時手前のIDとNameは非表示にする
            string url = Constants.API["gridData"];
            Newtonsoft.Json.Linq.JArray list = (Newtonsoft.Json.Linq.JArray)(await Comm.Post(url, searchGridPrams));

            // 結果を格納するためのDataTableを作成
            DataTable dataTable = new DataTable();

            if (list is JArray jsonArray)
            {
                // 最初のJObjectからカラム情報を取得
                if (jsonArray.Count > 0 && jsonArray[0] is JObject firstObject)
                {
                    foreach (var property in firstObject.Properties())
                    {
                        dataTable.Columns.Add(property.Name, typeof(object));
                    }

                    // JArrayをループしてDataTableにデータを追加
                    foreach (var item in jsonArray)
                    {
                        if (item is JObject jObject)
                        {
                            var row = dataTable.NewRow();
                            foreach (var property in jObject.Properties())
                            {
                                row[property.Name] = (property.Value.Type != JTokenType.Object) ? property.Value : "";
                            }
                            dataTable.Rows.Add(row);
                        }
                    }
                }
            }
            ConfigureFlexGrid(flexGrid, dataTable);
            flexGrid.Visible = true;
            if (showfrom != null)
            {
                //Tagプロパティにshowfromを保持
                flexGrid.Tag = showfrom;
                //ダブルクリックでShowFromを出すイベントを追加
                flexGrid.MouseDoubleClick += FlexGrid_MouseDoubleClick;
            }

        }
        /// <summary>
        /// グリッドのダブルクリックイベント詳細画面を表示する
        /// </summary>
        /// <param name="grid">設定対象のFlexGrid</param>
        /// <param name="dt">データソースとして設定するDataTable</param>
        private static void FlexGrid_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            const string nameSpace = "CallCenterCustomer.";

            if (sender is C1FlexGrid flexGrid)
            {
                // クリックされたセルの座標を取得
                Point pt = flexGrid.PointToClient(Control.MousePosition);

                // クリックされた座標から行インデックスを取得
                int rowIndex = flexGrid.HitTest(pt.X, pt.Y).Row;

                // ヘッダー行をクリックイベントの対象から除外
                if (rowIndex > 0)
                {
                    // アセンブリから指定したフォーム名に対応する型を取得
                    Type formType = Assembly.GetExecutingAssembly().GetType(nameSpace + flexGrid.Tag.ToString());

                    if (formType != null && formType.IsSubclassOf(typeof(Form)))
                    {
                        // 指定したフォーム名に対応するフォームを作成
                        Form form = (Form)Activator.CreateInstance(formType);

                        // フォームを表示
                        form.Show();
                    }
                    else
                    {
                        MessageBox.Show("指定したフォームは存在しません。");
                    }
                }
            }
        }

        /// <summary>
        /// 指定されたFlexGridを設定し、DataTableをデータソースとして設定する
        /// </summary>
        /// <param name="grid">設定対象のFlexGrid</param>
        /// <param name="dt">データソースとして設定するDataTable</param>
        private static void ConfigureFlexGrid(C1FlexGrid grid, DataTable dt)
        {
            grid.SuspendLayout();


            // ヘッダの設定
            grid.Cols[0].Caption = "";
            //grid.Cols[0].Width = 30;

            grid.DataSource = dt;
            grid.Cols.Count = dt.Columns.Count + 1; ;
            grid.AllowFiltering = true;
            //grid.AutoResize = true;

            int columnIndexOffset = 1;
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                var config = ProjectDatumHelpers.ColumnConfigs[i];
                grid.Cols[i + columnIndexOffset].Caption = dt.Columns[i].ColumnName;
                grid.Cols[i + columnIndexOffset].Width = 250;
                grid.Cols[i + columnIndexOffset].StyleNew.TextAlign = config.TextAlign;
                grid.Cols[i + columnIndexOffset].StyleNew.Font = config.Font;
                //1番目と2番目の列を非表示にする(id,nameカラム)
                //if (i == 0)//|| i == 1)
                //{
                //    grid.Cols[i + columnIndexOffset].Visible = false;
                //}
            }
            grid.ResumeLayout();
        }
    }
}
