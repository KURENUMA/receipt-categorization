using System;
using System.Drawing;
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

namespace ReceiptClient
{
    public partial class ReceiptDetail : Form
    {
        private static readonly HttpClient httpClient = new HttpClient();

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
            c1FlexGrid1.Cols.Count = 10;
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
                    c1PictureBox1.Image = Image.FromStream(stream);
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
                    if (string.IsNullOrEmpty(line)) continue;
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
            try
            {
                // c1PictureBox1に表示されている画像を取得
                if (c1PictureBox1.Image == null)
                {
                    MessageBox.Show("表示されている画像がありません。");
                    return;
                }

                // 画像をメモリーストリームに保存
                byte[] imageBytes;
                using (var memoryStream = new MemoryStream())
                {
                    using (var bitmap = new Bitmap(c1PictureBox1.Image))
                    {
                        bitmap.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Png);
                    }
                    imageBytes = memoryStream.ToArray();
                }

                // APIリクエストを作成
                using (var content = new MultipartFormDataContent())
                {
                    // 画像ファイルを追加
                    var imageContent = new ByteArrayContent(imageBytes);
                    imageContent.Headers.ContentType = MediaTypeHeaderValue.Parse("image/png");
                    content.Add(imageContent, "file", "image.png");

                    // メッセージを追加
                    var messageContent = new StringContent("{\"role\": \"user\", \"content\": \"これは領収書の画像です。これをExcelの表にしてください。\"}", Encoding.UTF8, "application/json");
                    content.Add(messageContent, "payload_json");

                    // OpenAIのAPIキーを設定
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "");

                    // 正しいAPIエンドポイントを使用
                    var response = await httpClient.PostAsync("https://api.openai.com/v1/chat/completions", content);
                    response.EnsureSuccessStatusCode();

                    var responseContent = await response.Content.ReadAsStringAsync();
                    dynamic result = JsonConvert.DeserializeObject(responseContent);

                    // APIレスポンスからExcelのBase64データを取得
                    string base64Excel = result.choices[0].message.content.ToString();

                    // Base64データをバイト配列に変換
                    byte[] excelBytes = Convert.FromBase64String(base64Excel);

                    // 保存ダイアログを表示してファイルを保存
                    SaveToExcel(excelBytes);
                }
            }
            catch (HttpRequestException ex)
            {
                MessageBox.Show("HTTPリクエストエラー: " + ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("エラーが発生しました: " + ex.Message);
            }
        }

        private void SaveToExcel(byte[] excelBytes)
        {
            try
            {
                var saveFileDialog = new SaveFileDialog
                {
                    Filter = "Excel Files|*.xlsx",
                    Title = "Save an Excel File"
                };

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    File.WriteAllBytes(saveFileDialog.FileName, excelBytes);
                    MessageBox.Show("Excelファイルが保存されました。");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Excelファイルの保存中にエラーが発生しました: " + ex.Message);
            }
        }

        private void btnPaste_Click(object sender, EventArgs e)
        {
            PasteClipboardData();
        }
    }
}
