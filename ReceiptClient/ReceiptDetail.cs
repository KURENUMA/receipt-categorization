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
using ReceiptClient.Common;

namespace ReceiptClient
{
    public partial class ReceiptDetail : Form
    {
        private static readonly HttpClient httpClient = new HttpClient();

        private const string GPT4V_KEY = ""; // Azure APIキー
        private const string GPT4V_ENDPOINT = "";
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
            // ウェイトカーソルを表示
            Application.UseWaitCursor = true;

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
                    c1PictureBox1.Image.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Png);
                    memoryStream.Seek(0, SeekOrigin.Begin);
                    imageBytes = memoryStream.ToArray();
                }

                // 画像をBase64エンコード
                string base64Image = Convert.ToBase64String(imageBytes);

                // Azure OpenAI APIにリクエストを送信
                var result = await SendImageToOpenAIAsync(base64Image, "Please extract the text from this receipt.");

                // 結果を表示
                MessageBox.Show($"APIレスポンス: {result}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"エラーが発生しました: {ex.Message}");
            }
            finally
            {
                // ウェイトカーソルを非表示
                Application.UseWaitCursor = false;
            }
        }

        private async Task<string> SendImageToOpenAIAsync(string base64Image, string question)
        {
            using (var httpClient = new HttpClient())
            {
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
                                url = $"data:image/jpeg;base64,{base64Image}"
                            }
                        },
                        new {
                            type = "text",
                            text = question
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
                    return responseData.ToString();
                }
                else
                {
                    return $"Error: {response.StatusCode}, {response.ReasonPhrase}";
                }
            }
        }

        private void btnPaste_Click(object sender, EventArgs e)
        {
            PasteClipboardData();
        }
    }
}
