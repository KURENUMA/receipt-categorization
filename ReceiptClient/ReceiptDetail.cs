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
                    using (var bitmap = new Bitmap(c1PictureBox1.Image))
                    {
                        bitmap.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Png);
                    }
                    imageBytes = memoryStream.ToArray();
                }

                // 画像をBase64エンコード
                string base64Image = Convert.ToBase64String(imageBytes);

                // チャットセッションを開始して最初のメッセージを送信
                var initialPayload = new
                {
                    model = "gpt-4-vision-preview",
                    messages = new[]
                    {
                new { role = "system", content = "あなたは画像解析の専門家です。これから送信される領収書の画像データを解析し、内容を抽出してください。" },
                new { role = "user", content = "これから数回にわたって領収書の画像データを送信します。最後にすべてのデータをまとめて処理してください。" }
            }
                };

                var initialResponse = await SendChatMessageAsync(initialPayload);
                if (initialResponse == null)
                {
                    MessageBox.Show("初期メッセージの送信に失敗しました。");
                    return;
                }

                // メッセージを分割して送信
                int chunkSize = 5000; // 適切なサイズに調整
                int offset = 0;
                bool isFirstChunk = true;

                while (offset < base64Image.Length)
                {
                    int size = Math.Min(chunkSize, base64Image.Length - offset);
                    string base64Chunk = base64Image.Substring(offset, size);
                    offset += size;

                    // 最初のチャンクにプレフィックスを付ける
                    if (isFirstChunk)
                    {
                        base64Chunk = $"data:image/jpeg;base64,{base64Chunk}";
                        isFirstChunk = false;
                    }

                    var chunkPayload = new
                    {
                        model = "gpt-4-vision-preview",
                        messages = new[]
                        {
                    new { role = "user", content = $"これが領収書の画像データの一部です：\n\n{base64Chunk}" }
                }
                    };

                    var chunkResponse = await SendChatMessageAsync(chunkPayload);
                    if (chunkResponse == null)
                    {
                        MessageBox.Show("データチャンクの送信に失敗しました。");
                        return;
                    }
                }

                // 最後のメッセージを送信して処理を依頼
                var finalPayload = new
                {
                    model = "gpt-4-vision-preview",
                    messages = new[]
                    {
                new { role = "user", content = "以上でデータの送信は終了です。これらのデータをまとめて処理し、領収書の内容をExcel形式に変換してください。" }
            }
                };

                var finalResponse = await SendChatMessageAsync(finalPayload);
                if (finalResponse == null)
                {
                    MessageBox.Show("最終メッセージの送信に失敗しました。");
                    return;
                }

                // 最終レスポンスからデータを取得してExcelに保存
                string base64Excel = finalResponse;
                byte[] excelBytes = Convert.FromBase64String(base64Excel);
                SaveToExcel(excelBytes);

                MessageBox.Show("処理が完了しました。");
            }
            catch (Exception ex)
            {
                MessageBox.Show("エラーが発生しました: " + ex.Message);
            }
            finally
            {
                // ウェイトカーソルを非表示
                Application.UseWaitCursor = false;
            }
        }

        private async Task<string> SendChatMessageAsync(object payload)
        {
            int retryCount = 0;
            int maxRetries = 5;
            int delay = 2000; // 2秒待機

            while (retryCount < maxRetries)
            {
                using (var content = new StringContent(JsonConvert.SerializeObject(payload), Encoding.UTF8, "application/json"))
                {
                    // OpenAIのAPIキーを設定
                    string apiKey = "";
                    if (string.IsNullOrEmpty(apiKey))
                    {
                        MessageBox.Show("APIキーが設定されていません。");
                        return null;
                    }

                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);

                    // 正しいAPIエンドポイントを使用
                    var response = await httpClient.PostAsync("https://api.openai.com/v1/chat/completions", content);

                    if ((int)response.StatusCode == 429) // 429 Too Many Requests
                    {
                        retryCount++;
                        if (retryCount == maxRetries)
                        {
                            MessageBox.Show("リクエストが過剰です。後でもう一度お試しください。");
                            return null;
                        }
                        await Task.Delay(delay); // 一定時間待機
                        continue;
                    }

                    if (!response.IsSuccessStatusCode)
                    {
                        // レスポンスが成功しなかった場合のデバッグ情報
                        string errorContent = await response.Content.ReadAsStringAsync();
                        MessageBox.Show($"リクエスト失敗: ステータスコード: {(int)response.StatusCode}, 内容: {errorContent}");
                        return null;
                    }

                    var responseContent = await response.Content.ReadAsStringAsync();
                    dynamic result = JsonConvert.DeserializeObject(responseContent);

                    // レスポンスのデバッグ情報を表示
                    MessageBox.Show($"レスポンス内容: {responseContent}");

                    // レスポンスから必要なデータを抽出
                    string contentText = result.choices[0].message.content.ToString();
                    return contentText;
                }
            }

            return null; // 最大リトライ回数を超えた場合
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
