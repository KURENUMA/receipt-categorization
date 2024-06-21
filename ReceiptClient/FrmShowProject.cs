using C1.Win.C1FlexGrid;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using ReceiptClient.Models;
using ReceiptClient.Shared;
using ReceiptClient.ViewModels;
using Dma.DatasourceLoader;
using System.Threading;
using ReceiptClient.UserControls;
using ClosedXML.Excel;
using ReceiptClient.Controls;
using System.IO;
using C1.Framework.Text.Lexicons;
using System.ComponentModel;
using ReceiptClient.Common;
using C1.Win.C1Input;
using static C1.Util.Win.Win32;
using System.Reflection;

namespace ReceiptClient
{
    public partial class FrmShowProject : Form
    {
        private ProjectGridManager projectGridManager;
        // データテーブルレイアウト
        private GridOrderManager gridManager;
        private GridColorManager colorManager;

        // 検索用の変数
        private List<Label> labelList;
        private List<TextBox> textBoxList;
        public ProjectGrid projectGrid1;

        /// <summary>
        /// 表示パターンを作成するモデルクラス名
        /// </summary>
        private const string TARGET_MODEL = "ReceiptClient.Models.ReceiptInfo";
        private const string DUMMY_USER_ID = "0";

        private PatternRepo patternRepo;

        public FrmShowProject()
        {
            patternRepo = new PatternRepo(State.LoginUser.ユーザーID, TARGET_MODEL);
            PatternInfo pattern = patternRepo.LoadPatterns().FirstOrDefault();
            if (pattern == null)
            {
                pattern = new PatternInfo { ClassName = TARGET_MODEL, Pattern = "デフォルト" };
            }

            projectGridManager = new ProjectGridManager(pattern);
            projectGrid1 = new ProjectGrid(projectGridManager);

            InitializeComponent();

            c1ComboBox1.SelectedItem = "100";
            BtnUpdate.Enabled = true;

            // 固定の画像を読み込む
            LoadInitialImage();

            // パターンUIを初期化
            initPatternUI();
        }

        private void LoadInitialImage()
        {
            // アセンブリ内のリソース名を取得して確認する
            var assembly = Assembly.GetExecutingAssembly();
            var resourceNames = assembly.GetManifestResourceNames();
            foreach (var resourceName in resourceNames)
            {
                Console.WriteLine(resourceName);
            }

            // リソース名が正しいか確認してから使用する
            using (Stream stream = assembly.GetManifestResourceStream("ReceiptClient.Resources.InitialImage.jpeg"))
            {
                if (stream != null)
                {
                    c1PictureBox1.Image = Image.FromStream(stream);
                }
                else
                {
                    MessageBox.Show("リソースが見つかりませんでした。");
                }
            }
        }

        /// <summary>
        /// データテーブル更新処理
        /// データテーブルの表示更新をしたい場合に本関数をCallする
        /// 更新ボタンやデータの変更通知を受け取り、更新したい場合に使用することを想定
        /// </summary>
        private void updateDataTable()
        {
            PatternInfo pattern = (PatternInfo)cmbPattern.SelectedItem;
            if (pattern == null) { return; }

            string selectedText = c1ComboBox1.SelectedItem.ToString();
            if (int.TryParse(selectedText, out int result))
            {
                projectGridManager.SetPageSize(result);
            }

            using (var progressForm = new SimpleProgressForm())
            {
                progressForm.Start(this);

                // 非同期でデータ取得
                projectGridManager.Reload(pattern, new List<FilterCriteria>());
                GdProjectList_RowColChange(this, EventArgs.Empty);
                InitializeColumns();
                projectGrid1.Visible = true;
            }
        }

        /// <summary>
        /// フォームロード（案件一覧画面）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmShowProject_Load(object sender, EventArgs e)
        {
            var userId = State.LoginUser.ユーザーID;
            gridManager = new GridOrderManager($"{userId}-orderSetting.json", ProjectDatumHelpers.ColumnConfigs);
            colorManager = new GridColorManager($"{userId}-colorSetting.json");
            projectGrid1 = new ProjectGrid(projectGridManager, gridManager, colorManager);
            projectGrid1.Dock = DockStyle.Fill;

            this.panel1.Controls.Add(projectGrid1);

            projectGridManager.OnDataSourceChange += GdProjectList_RowColChange;
            projectGrid1.GridDoubleClick += new System.EventHandler(c1FlexGrid1_DoubleClick);
            //以下は行変更時のイベントハンドラとする予定
            //projectGrid1.c1FlexGrid1.AfterRowChange += c1FlexGrid1_AfterRowChange;

            InitializeColumns();

            // ボタンを有効にする
            BtnSetting.Enabled = true;
            BtnEdit.Enabled = true;
            BtnUpdate.Enabled = true;
            btnOutputExcel.Enabled = true;
            btnSearch.Enabled = true;

            // 全件表示は未対応
            toggleButton.Visible = false;
        }

        private void InitializeColumns()
        {
            var pattern = (PatternInfo)cmbPattern.SelectedItem;
            if (pattern == null)
            {
                return;
            }

            // ヘッダの設定
            projectGrid1.c1FlexGrid1.Clear();
            BindingList<ColumnInfo> configs = pattern.Columns;

            projectGrid1.c1FlexGrid1.Cols.Count = configs.Count + 1;
            projectGrid1.c1FlexGrid1.Cols[0].Caption = "";
            projectGrid1.c1FlexGrid1.Cols[0].Width = 30;

            int columnIndexOffset = 1;
            for (int i = 0; i < configs.Count; i++)
            {
                var config = configs[i];
                var col = projectGrid1.c1FlexGrid1.Cols[i + columnIndexOffset];

                col.Caption = config.Label;
                col.Width = config.Width;
                col.StyleNew.TextAlign = (TextAlignEnum)config.TextAlignment;
                col.Name = config.VarName;
                col.AllowFiltering = AllowFiltering.None;
            }
        }

        /// <summary>
        /// DoubleClickイベント
        /// </summary>
        private void c1FlexGrid1_DoubleClick(object sender, EventArgs e)
        {
            BtnEdit_Click(sender, e);
        }

        /// <summary>
        /// Reload後の処理
        /// </summary>
        private void GdProjectList_RowColChange(object sender, EventArgs e)
        {
            // 件数出力
            var rows = projectGridManager.Dt.Rows.Count;
            if (projectGrid1.GetProjectCount() != -1)
            {
                this.lblProjectAllCount.Text = "検索結果：" + projectGrid1.GetProjectCount().ToString() + "件";
            }
            else
            {
                this.lblProjectAllCount.Text = "検索結果：";
            }

            this.textFilterStr.Text = projectGrid1.GetFilterOptionStr();

            InitializeColumns();

            // Gridの色を設定する
            projectGrid1.SetGridColors();

            /*
            // 行が変更された時に画像を更新する予定
            var fileId = projectGrid1.c1FlexGrid1[projectGrid1.c1FlexGrid1.Row, "FileId"]?.ToString();
            if (!string.IsNullOrEmpty(fileId))
            {
                using (var stream = GoogleDriveHelper.GetFileStream(fileId))
                {
                    c1PictureBox1.Image = Image.FromStream(stream);
                }
            }
            */
        }

        private void Combo_Delete_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Delete)
            {
                ((ComboBox)sender).SelectedIndex = -1;
            }
        }

        /// <summary>
        /// フォームクローズ時処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmShowProject_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        /// <summary>
        /// データを更新する
        /// </summary>
        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            updateDataTable();
        }

        /// <summary>
        /// 検索ボタン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSearch_Click(object sender, EventArgs e)
        {
            var pattern = (PatternInfo)cmbPattern.SelectedItem;
            if (pattern == null) return;
            var searchFrm = new FrmAdvancedSearch(projectGridManager.Filters, pattern);
            searchFrm.StartPosition = FormStartPosition.CenterParent;

            searchFrm.OnSearch += (sender, e) =>
            {
                projectGridManager.Reload(searchFrm.FilterCriterias);
            };
            searchFrm.OnSearchAndSave += (sender, e) =>
            {
                projectGridManager.SetFilters(searchFrm.FilterCriterias);
                projectGridManager.Reload(searchFrm.FilterCriterias);
            };
            searchFrm.OnReset += (sender, e) =>
            {
                projectGridManager.SetFilters(new List<FilterCriteria>());
            };
            // クローズイベントと接続	
            searchFrm.FormClosed += searchFrm_FormClosed;
            searchFrm.ShowDialog();
        }

        private void searchFrm_FormClosed(object sender, FormClosedEventArgs e)
        {
            GdProjectList_RowColChange(this, EventArgs.Empty);
        }

        /// <summary>
        /// 編集ボタン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnEdit_Click(object sender, EventArgs e)
        {
            using (var progressForm = new SimpleProgressForm())
            {
            }
        }

        /// <summary>
        /// Excel出力ボタン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOutputExcel_Click(object sender, EventArgs e)
        {
            string fName = Path.Combine(System.Windows.Forms.Application.LocalUserAppDataPath, DateTime.Now.ToString("yyyyMMdd_HHmmss_fff") + ".xlsx");
            projectGrid1.c1FlexGrid1.SaveExcel(fName, FileFlags.IncludeFixedCells);

            using (var process = new System.Diagnostics.Process())
            {
                process.StartInfo.FileName = fName;
                process.StartInfo.UseShellExecute = true;
                process.Start();
            }
        }

        /// <summary>
        /// "ページング表示" : "全件表示"を切り替えて、更新を行う
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toggleButton_Click(object sender, EventArgs e)
        {
            bool newShowAllState = projectGridManager.ToggleShowAll();
            toggleButton.Text = newShowAllState ? "ページング表示" : "全件表示";
            projectGrid1.SetPaginationVisibility(!newShowAllState);

            updateDataTable();
        }

        private void PgProject_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 色設定ボタン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnColorSet_Click(object sender, EventArgs e)
        {
            var FrmColorSet = new FrmColorSet(colorManager);
            FrmColorSet.Owner = this;
            if (FrmColorSet.ShowDialog() == DialogResult.OK)
            {
                updateDataTable();
            }
        }

        private void PatternSaveHandler(object sender, PatternInfo patternInfo)
        {
            patternRepo.SavePattern(patternInfo);
            initPatternUI();
        }

        private void ShowPatternSettingDialog(PatternInfo patternInfo)
        {
            var dlg = new FrmSubSetListCol(patternRepo, patternInfo);

            dlg.OnSavePattern += new EventHandler<PatternInfo>(PatternSaveHandler);

            dlg.ShowDialog();
        }

        private void initPatternUI()
        {
            cmbPattern.BeginUpdate();
            cmbPattern.Items.Clear();

            var patterns = patternRepo.LoadPatterns().ToArray();
            cmbPattern.DisplayMember = "Pattern";
            cmbPattern.Items.AddRange(patterns);

            if (patterns.Length > 0)
            {
                cmbPattern.SelectedIndex = 0;
                btnEditPattern.Enabled = true;
                btnCopyPattern.Enabled = true;
                btnNewPattern.Enabled = true;
                btnDeletePattern.Enabled = true;
            }
            else
            {
                cmbPattern.SelectedIndex = -1;
                cmbPattern.Text = string.Empty;
                btnEditPattern.Enabled = false;
                btnCopyPattern.Enabled = false;
                btnNewPattern.Enabled = true;
                btnDeletePattern.Enabled = false;
            }
            cmbPattern.EndUpdate();
        }

        private void BtnSetting_Click(object sender, EventArgs e)
        {
            var pattern = (PatternInfo)cmbPattern.SelectedItem;
            if (pattern != null)
            {
                patternRepo.SavePattern(pattern);
                updateDataTable();
                FrmShowProject mainFormInstance = this;
            }
        }

        private void btnNewPattern_Click(object sender, EventArgs e)
        {
            var pattern = new PatternInfo
            {
                Pattern = "新規パターン",
                ClassName = TARGET_MODEL,
            };
            ShowPatternSettingDialog(pattern);
        }

        private void btnEditPattern_Click(object sender, EventArgs e)
        {
            var pattern = (PatternInfo)cmbPattern.SelectedItem;
            if (pattern != null)
            {
                ShowPatternSettingDialog(pattern);
            }
        }

        private void btnCopyPattern_Click(object sender, EventArgs e)
        {
            var pattern = (PatternInfo)cmbPattern.SelectedItem;
            if (pattern != null)
            {
                var copied = PatternInfo.copy(pattern);
                ShowPatternSettingDialog(copied);
            }
        }

        private void btnDeletePattern_Click(object sender, EventArgs e)
        {
            var pattern = (PatternInfo)cmbPattern.SelectedItem;
            if (pattern != null)
            {
                patternRepo.DeletePattern(pattern);
                initPatternUI();
            }
        }

        private void c1ComboBox1_SelectedItemChanged(object sender, EventArgs e)
        {

        }

        private void c1FlexGrid1_AfterRowChange(object sender, RangeEventArgs e)
        {
            /* ★行変更時の画像変更処理を記載する予定
            var grid = (C1FlexGrid)sender;
            var fileId = grid[e.Row, "FileId"]?.ToString();

            if (!string.IsNullOrEmpty(fileId))
            {
                using (var stream = GoogleDriveHelper.GetFileStream(fileId))
                {
                    c1PictureBox1.Image = Image.FromStream(stream);
                }
            }
            */
        }
    }
}
