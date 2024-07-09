using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using C1.Win.C1FlexGrid;
using C1.Win.C1Input;
using ClosedXML.Excel;
using Newtonsoft.Json;
using ReceiptClient.Common;

namespace ReceiptClient
{
    public partial class ReceiptDetail : Form
    {
        private static readonly HttpClient httpClient = new HttpClient();

        private const string QUESTION = "json形式(配列)で日付、内訳、単価、数量、金額、消費税額、合計金額を出力してください。json以外は不要。";
        private const string GPT4V_ENDPOINT = "https://openai-dis-div-east-us.openai.azure.com/openai/deployments/deployments-gpt4o-dis-div/chat/completions?api-version=2024-02-15-preview";

        public ReceiptDetail()
        {
            InitializeComponent();
            InitializeCustomComponents();
        }

        private void InitializeCustomComponents()
        {
            // c1PictureBox1にリソース画像を表示
            LoadInitialImage();

            // c1FlexGrid1の設定
            c1FlexGrid1.AllowEditing = true;
            c1FlexGrid1.AllowDragging = AllowDraggingEnum.None;
            c1FlexGrid1.Cols.Count = 12;
            c1FlexGrid1.Rows.Count = 50;
            c1FlexGrid1.ClipSeparators = "\t\n";

            // グリッドにデータを貼り付けるイベントハンドラ
            c1FlexGrid1.KeyDown += C1FlexGrid1_KeyDown;
        }

        private void LoadInitialImage()
        {
            var assembly = Assembly.GetExecutingAssembly();
            using (Stream stream = assembly.GetManifestResourceStream("ReceiptClient.Resources.InitialImage.jpeg"))
            {
                if (stream != null)
                {
                    // 画像をビットマップにコピーしてから使用
                    using (var bitmap = new Bitmap(stream))
                    {
                        c1PictureBox1.Image = new Bitmap(bitmap);
                    }
                    c1PictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
                }
                else
                {
                    MessageBox.Show("リソースが見つかりませんでした。");
                }
            }
        }

        private void C1FlexGrid1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.V)
            {
                PasteClipboardData();
                e.Handled = true;
            }
        }

        private void PasteClipboardData()
        {
            try
            {
                string clipboardText = Clipboard.GetText();
                if (string.IsNullOrEmpty(clipboardText)) return;

                string[] lines = clipboardText.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.None);
                int currentRow = c1FlexGrid1.Row;
                int currentCol = c1FlexGrid1.Col;

                foreach (string line in lines)
                {
                    if (string.IsNullOrEmpty(line.Trim())) continue; // 空行を無視
                    string[] cells = line.Split('\t');

                    for (int i = 0; i < cells.Length; i++)
                    {
                        if (currentRow < c1FlexGrid1.Rows.Count && (currentCol + i) < c1FlexGrid1.Cols.Count)
                        {
                            c1FlexGrid1[currentRow, currentCol + i] = cells[i];
                        }
                    }
                    currentRow++;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("データの貼り付け中にエラーが発生しました: " + ex.Message);
            }
        }

        private async void btnAI_Click(object sender, EventArgs e)
        {
            if (c1PictureBox1.Image != null)
            {
                string outputFileName = string.Format("領収書_{0:yyyyMMddHHmmss}.xlsx", DateTime.Now);
                using (var progressForm = new SimpleProgressForm())
                {
                    // この using ブロック内には、画面にアクセス(参照/設定)するコードを記述しないでください
                    progressForm.Start(this);
                    string data = await Task.Run(() => Gpt());

                    using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                    {
                        saveFileDialog.Filter = "Excel Files|*.xlsx";
                        saveFileDialog.Title = "Save an Excel File";
                        saveFileDialog.FileName = outputFileName;

                        if (saveFileDialog.ShowDialog() == DialogResult.OK)
                        {
                            outputFileName = saveFileDialog.FileName;
                            CreateExcel(outputFileName, data);
                            OpenExcel(outputFileName);
                        }
                    }
                }
            }
        }

        public async Task<string> Gpt()
        {
            string result = "";
            var encodedImage = ImageToBase64(c1PictureBox1.Image, ImageFormat.Jpeg);
            try
            {
                using (var httpClient = new HttpClient())
                {
                    string GPT4V_KEY = Environment.GetEnvironmentVariable("GPT4V_KEY");
                    httpClient.DefaultRequestHeaders.Add("api-key", GPT4V_KEY);
                    var payload = new
                    {
                        messages = new object[]
                        {
                            new {
                                role = "system",
                                content = new object[] {
                                    new {
                                        type = "text",
                                        text = "You are an AI assistant that helps people find information."
                                    }
                                }
                            },
                            new {
                                role = "user",
                                content = new object[] {
                                    new {
                                        type = "image_url",
                                        image_url = new {
                                            url = $"data:image/jpeg;base64,{encodedImage}"
                                        }
                                    },
                                    new {
                                        type = "text",
                                        text = QUESTION
                                    }
                                }
                            }
                        },
                        temperature = 0.7,
                        top_p = 0.95,
                        max_tokens = 800,
                        stream = false
                    };

                    var response = await httpClient.PostAsync(GPT4V_ENDPOINT, new StringContent(JsonConvert.SerializeObject(payload), Encoding.UTF8, "application/json"));

                    if (response.IsSuccessStatusCode)
                    {
                        var responseData = JsonConvert.DeserializeObject<dynamic>(await response.Content.ReadAsStringAsync());
                        Console.WriteLine(responseData);
                        result = responseData["choices"][0]["message"]["content"].ToString();
                    }
                    else
                    {
                        Console.WriteLine($"Error: {response.StatusCode}, {response.ReasonPhrase}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"エラーが発生しました: {ex.Message}");
            }
            return result;
        }

        private void CreateExcel(string outputFileName, string data)
        {
            try
            {
                if (string.IsNullOrEmpty(data))
                {
                    MessageBox.Show("データが空です。");
                    return;
                }

                using (var wb = new XLWorkbook()) // 型を明示的に指定
                {
                    wb.Worksheets.Add();
                    IXLWorksheet ws = wb.Worksheet("Sheet1");
                    data = data.Replace("```json", ""); // gptからの回答で不要な文字列を削除
                    data = data.Replace("```", "");

                    var List = JsonConvert.DeserializeObject<List<Dictionary<string, string>>>(data);

                    if (List == null)
                    {
                        MessageBox.Show("デシリアライズに失敗しました。データ形式を確認してください。");
                        return;
                    }

                    bool header = true;
                    int col = 1;
                    int row = 2;
                    foreach (var dic in List)
                    {
                        foreach (var kvp in dic)
                        {
                            if (header)
                            {
                                ws.Cell(1, col).Value = kvp.Key;
                            }

                            ws.Cell(row, col).Value = kvp.Value;
                            col++;
                        }
                        col = 1;
                        row++;
                        header = false;
                    }

                    wb.SaveAs(outputFileName);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Excelファイルの作成中にエラーが発生しました: {ex.Message}");
            }
        }

        public string ImageToBase64(Image image, ImageFormat format)
        {
            try
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    // 画像のコピーを作成して保存
                    using (var tempImage = new Bitmap(image))
                    {
                        tempImage.Save(ms, format);
                    }

                    byte[] imageBytes = ms.ToArray();

                    // バイト配列をBase64文字列に変換
                    string base64String = Convert.ToBase64String(imageBytes);
                    return base64String;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"画像の変換中にエラーが発生しました: {ex.Message}");
                return null;
            }
        }

        private void btnPaste_Click(object sender, EventArgs e)
        {
            PasteClipboardData();
        }

        private void btnImageDownload_Click(object sender, EventArgs e)
        {
            try
            {
                // c1PictureBox1に表示されている画像を取得
                if (c1PictureBox1.Image == null)
                {
                    MessageBox.Show("表示されている画像がありません。");
                    return;
                }

                // 画像をビットマップとして取得
                Bitmap bitmap;
                using (var originalImage = new Bitmap(c1PictureBox1.Image))
                {
                    bitmap = new Bitmap(originalImage);
                }

                // 保存ダイアログを表示
                using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                {
                    saveFileDialog.Filter = "PNG Image|*.png";
                    saveFileDialog.Title = "Save an Image File";
                    saveFileDialog.FileName = "image.png";

                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        // ユーザーがファイルを保存する場所を選択した場合
                        string filePath = saveFileDialog.FileName;

                        // 画像を選択された場所に保存
                        bitmap.Save(filePath, System.Drawing.Imaging.ImageFormat.Png);
                        MessageBox.Show("画像が保存されました。");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"エラーが発生しました: {ex.Message}");
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void OpenExcel(string filePath)
        {
            try
            {
                Process.Start(new ProcessStartInfo(filePath) { UseShellExecute = true });
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Excelファイルを開く際にエラーが発生しました: {ex.Message}");
            }
        }
    }
}
