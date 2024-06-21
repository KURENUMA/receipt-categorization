using C1.Win.C1FlexGrid;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ReceiptClient.Models;
using ReceiptClient.Shared;

namespace ReceiptClient
{
    public partial class FrmColorSet : Form
    {
        private ColorDialog colorDialog;
        private GridColorManager colorManager;
        private UserSettings userSettings;
        private const int ColumnIndexSettingColor = 1;
        private const int ColumnIndexBackgroundColor = 2;
        private const int ColumnIndexTextColor = 3;

        
        public FrmColorSet(GridColorManager colorManager)
        {
            this.colorManager = colorManager;
            userSettings =  colorManager.GetUserSettings("default");
            InitializeComponent();

            //　初期設定
            this.Text = "色設定";
            c1FlexGrid1.Cols[ColumnIndexBackgroundColor].AllowEditing = false;
            c1FlexGrid1.Cols[ColumnIndexTextColor].AllowEditing = false;
            c1FlexGrid1.Cols.Count = 4;
            c1FlexGrid1.Rows.Count = 6;

            ComboBox comboBox = new ComboBox();
            comboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox.Items.Add("設定なし");
            comboBox.Items.Add("設定あり");


            // 一列目にコンボボックスエディターを割り当て
            c1FlexGrid1.Cols[ColumnIndexSettingColor].Editor = comboBox;

           

            // 各列の幅
            c1FlexGrid1.Cols[0].Width = 80;
            c1FlexGrid1.Cols[1].Width = 100;
            c1FlexGrid1.Cols[2].Width = 100;
            c1FlexGrid1.Cols[3].Width = 100;

            // 列名の設定
            c1FlexGrid1.Cols[1].Caption = "設定";
            c1FlexGrid1.Cols[2].Caption = "背景色";
            c1FlexGrid1.Cols[3].Caption = "文字色";
 
            // 行名の設定
            c1FlexGrid1[0, 0] = "作業工程";
          
            c1FlexGrid1[1, 0] = "受付";
            c1FlexGrid1[2, 0] = "依頼";
            c1FlexGrid1[3, 0] = "申請済み";
            c1FlexGrid1[4, 0] = "納品";
            c1FlexGrid1[5, 0] = "キャンセル";

            // イベントハンドラの設定
            c1FlexGrid1.MouseDoubleClick += c1FlexGrid1_MouseDoubleClick;
            colorDialog = new ColorDialog();
            LoadColorsToGrid();
        }

        private void C1FlexGrid1_CellChanged(object sender, RowColEventArgs e)
        {
            string gridProcessName = c1FlexGrid1[e.Row, 0].ToString();
            string cell= c1FlexGrid1[e.Row, e.Col].ToString();
            userSettings.Colors[gridProcessName].isValid = cell == "設定あり";
            
        }

        private  void LoadColorsToGrid()
        {
            c1FlexGrid1.CellChanged -= C1FlexGrid1_CellChanged;
            try
            {

                var prorcessColors = userSettings.Colors;
              
                // C1FlexGrid の各行をループ
                for (int row = 1; row < c1FlexGrid1.Rows.Count; row++)
                {
                    string gridProcessName = c1FlexGrid1[row, 0].ToString(); // 一列目のセルの値

                    // ProcessDetail のリストを検索
                    if (prorcessColors.Keys.Contains(gridProcessName)) { 
                        var matchingDetail = prorcessColors[gridProcessName];

                        // グリッドの各行にデフォルト値を設定
                        c1FlexGrid1[row, ColumnIndexSettingColor] = null;
                        c1FlexGrid1[row, ColumnIndexSettingColor] = matchingDetail.isValid? "設定あり": "設定なし";

                        // 色情報を反映
                        CellStyle cellStyleBackground = c1FlexGrid1.GetCellStyle(row, ColumnIndexBackgroundColor) ?? c1FlexGrid1.Styles.Add($"StyleBackground{row}");
                        CellStyle cellStyleText = c1FlexGrid1.GetCellStyle(row, ColumnIndexTextColor) ?? c1FlexGrid1.Styles.Add($"StyleText{row}");

                        cellStyleBackground.BackColor = ColorTranslator.FromHtml(matchingDetail.Background);
                        c1FlexGrid1.SetCellStyle(row, ColumnIndexBackgroundColor, cellStyleBackground);

                        cellStyleText.BackColor = ColorTranslator.FromHtml(matchingDetail.Foreground);
                        c1FlexGrid1.SetCellStyle(row, ColumnIndexTextColor, cellStyleText);
                    }

                }

            }
            catch (Exception ex)
            {
                // エラー処理
                MessageBox.Show($"エラーが発生しました: {ex.Message}");
            }
            c1FlexGrid1.CellChanged += C1FlexGrid1_CellChanged;
        }

        private void c1FlexGrid1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            var hti = c1FlexGrid1.HitTest(e.X, e.Y);
            if (hti.Type == HitTestTypeEnum.Cell)
            {
                if (hti.Column == ColumnIndexBackgroundColor || hti.Column == ColumnIndexTextColor)
                {
                    // ColorDialogを表示
                    if (colorDialog.ShowDialog() == DialogResult.OK)
                    {

                        CellStyle cellStyle = c1FlexGrid1.GetCellStyle(hti.Row, hti.Column);
                        if (cellStyle == null)
                        {
                            cellStyle = c1FlexGrid1.Styles.Add("CustomStyle" + hti.Row + hti.Column);
                            c1FlexGrid1.SetCellStyle(hti.Row, hti.Column, cellStyle);
                        }

                        // 列インデックスに応じて背景色を設定
                        if (hti.Column == ColumnIndexBackgroundColor)
                        {
                            cellStyle.BackColor = colorDialog.Color;
                            string gridProcessName = c1FlexGrid1[hti.Row, 0].ToString();
                            userSettings.Colors[gridProcessName].Background = ColorTranslator.ToHtml(colorDialog.Color);
                            

                        }
                        else if (hti.Column == ColumnIndexTextColor)
                        {
                            cellStyle.BackColor = colorDialog.Color;
                            string gridProcessName = c1FlexGrid1[hti.Row, 0].ToString();
                            userSettings.Colors[gridProcessName].Foreground = ColorTranslator.ToHtml(colorDialog.Color);
                        }
                    }
                }
            }
        }

        private void FrmColorSet_Load(object sender, EventArgs e)
        {
            
        }

        private void BtnDefined_Click(object sender, EventArgs e)
        {
            colorManager.UpdateUserSettings("default", userSettings);
            DialogResult = DialogResult.OK;
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
           DialogResult = DialogResult.Cancel;
        }

        private void RestoreDefault_Click(object sender, EventArgs e)
        {
            AsyncLoadingForm loadingForm = new AsyncLoadingForm(callback: ()=>userSettings =  colorManager.LoadColorsFromDatabaseAsync().GetAwaiter().GetResult());
            loadingForm.ShowDialog();

            
            LoadColorsToGrid();
            c1FlexGrid1.Invalidate();
            c1FlexGrid1.Refresh();
        }
       
    }
}
