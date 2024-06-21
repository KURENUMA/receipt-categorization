using log4net;
using System;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using ReceiptClient.Models;
using static ReceiptClient.Shared.State;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using ReceiptClient.Shared;
using ReceiptClient.Common;
using System.Threading.Tasks;

namespace ReceiptClient
{
    /// <summary>
    /// ログインフォームの処理を実装したクラス
    /// </summary>
    public partial class FrmLogin : Form
    {
        private const string AES_IV = @"pf69DL6GrWFyZcMK";
        private const string AES_Key = @"9Fix4L4HB4PKeKWY";

        private static readonly ILog logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// FrmLoginの新しいインスタンスを初期化する
        /// </summary>
        public FrmLogin()
        {
            InitializeComponent();
        }

        /// <summary>
        /// フォームがロードされる際の処理
        /// フォームの位置を親フォームの中央に配置し、エラーメッセージを初期化する
        /// </summary>
        private void FrmLogin_Load(object sender, EventArgs e)
        {
            var loggedInUser = new UserAuth
            {
                ID = 1,
                ユーザーID = "",
                パスワードハッシュ = "",
                アクセスレベル = 0
            };

            setLoginUser(loggedInUser);

            // エラーメッセージを表示するラベルを初期化（空に設定）
            labelMsg.Text = String.Empty;

            this.chkLogin.Checked = false;

            if (Properties.Settings.Default.login_flg)
            {
                this.tbLoginId.Text = Decrypt(Properties.Settings.Default.login_id, AES_IV, AES_Key);
                this.tbPassword.Text = Decrypt(Properties.Settings.Default.login_pass, AES_IV, AES_Key);
                this.chkLogin.Checked = true;
                this.ActiveControl = this.btnLogin;
            }

            // フォームをディスプレイの中央に配置
            CenterToScreen();
        }

        private void SetSettingData(UserAuth loginResult)
        {
            if (this.chkLogin.Checked)
            {
                Properties.Settings.Default.login_id = Encrypt(this.tbLoginId.Text, AES_IV, AES_Key);
                Properties.Settings.Default.login_pass = Encrypt(this.tbPassword.Text, AES_IV, AES_Key);
                Properties.Settings.Default.login_user = loginResult.ユーザーID;
                Properties.Settings.Default.login_flg = true;
            }
            else
            {
                Properties.Settings.Default.login_id = @"";
                Properties.Settings.Default.login_pass = @"";
                Properties.Settings.Default.login_user = @"";
                Properties.Settings.Default.login_flg = false;
            }
            Properties.Settings.Default.Save();
        }

        /// <summary>
        /// ユーザーIDのテキストボックスの内容が変わったときの処理
        /// ログインボタンの有効/無効を切り替える
        /// </summary>
        private void tbLoginId_TextChanged(object sender, EventArgs e)
        {
            // ユーザーIDとパスワードが入力されていればログインボタンを有効化、そうでなければ無効化
            btnLogin.Enabled = tbLoginId.Text.Length > 0 && tbPassword.Text.Length > 0;
        }

        /// <summary>
        /// フォームがアクティブになったときの処理
        /// ユーザーIDのテキストボックスにフォーカスを移動する
        /// </summary>
        private void FrmLogin_Activated(object sender, EventArgs e)
        {
            // ユーザーIDのテキストボックスにフォーカスを設定する
            tbLoginId.Focus();
        }

        /// <summary>
        /// ログインボタンがクリックされたときの処理
        /// ログイン処理およびエラーハンドリングを行う
        /// </summary>
        private async void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                using (var progressForm = new SimpleProgressForm())
                {
                    progressForm.Start(this);

                    // ウェイトカーソルを表示
                    Application.UseWaitCursor = true;

                    // 以下のAPIコール部分をコメントアウト
                    // ApiResp<UserAuth> response = await DoLogin(tbLoginId.Text, tbPassword.Text);
                    // UserAuth loggedInUser = response.Data;

                    // if (loggedInUser == null)
                    // {
                    //     throw new InvalidOperationException(response.Message);
                    // }
                    // setLoginUser(loggedInUser);

                    // 固定のユーザー認証データを使用
                    var loggedInUser = new UserAuth
                    {
                        ID = 1,
                        ユーザーID = tbLoginId.Text,
                        パスワードハッシュ = tbPassword.Text, // パスワードは適切なハッシュ関数でハッシュ化するべきです
                        アクセスレベル = 1
                    };

                    setLoginUser(loggedInUser);
                    SetSettingData(loggedInUser);
                }

                logger.Info("ログイン成功");
                // ウェイトカーソルを非表示
                Application.UseWaitCursor = false;

                // 新しいフォームを表示する前にログインフォームを閉じる
                this.Hide(); // フォームを閉じずに非表示にする
                FrmShowProject showProjectForm = new FrmShowProject();
                showProjectForm.FormClosed += (s, args) => this.Close(); // 新しいフォームが閉じられたときにログインフォームを閉じる
                showProjectForm.Show();
            }
            catch (Exception ex)
            {
                logger.Error("ログイン失敗：" + ex);
                // ウェイトカーソルを非表示
                Application.UseWaitCursor = false;

                // エラーメッセージを表示
                labelMsg.Text = ex.Message;
            }
        }

        private void chkLogin_CheckedChanged(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 対称鍵暗号を使って文字列を暗号化する
        /// </summary>
        /// <param name="text">暗号化する文字列</param>
        /// <param name="iv">対称アルゴリズムの初期ベクター</param>
        /// <param name="key">対称アルゴリズムの共有鍵</param>
        /// <returns>暗号化された文字列</returns>
        public static string Encrypt(string text, string iv, string key)
        {
            string strRet = "";
            using (RijndaelManaged rijndael = new RijndaelManaged())
            {
                rijndael.BlockSize = 128;
                rijndael.KeySize = 128;
                rijndael.Mode = CipherMode.CBC;
                rijndael.Padding = PaddingMode.PKCS7;

                rijndael.IV = Encoding.UTF8.GetBytes(iv);
                rijndael.Key = Encoding.UTF8.GetBytes(key);

                ICryptoTransform encryptor = rijndael.CreateEncryptor(rijndael.Key, rijndael.IV);

                byte[] encrypted;
                using (MemoryStream mStream = new MemoryStream())
                {
                    using (CryptoStream ctStream = new CryptoStream(mStream, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter sw = new StreamWriter(ctStream))
                        {
                            sw.Write(text);
                        }
                        encrypted = mStream.ToArray();
                    }
                }
                strRet = System.Convert.ToBase64String(encrypted);
            }
            return strRet;
        }

        /// <summary>
        /// 対称鍵暗号を使って暗号文を復号する
        /// </summary>
        /// <param name="cipher">暗号化された文字列</param>
        /// <param name="iv">対称アルゴリズムの初期ベクター</param>
        /// <param name="key">対称アルゴリズムの共有鍵</param>
        /// <returns>復号された文字列</returns>
        public static string Decrypt(string cipher, string iv, string key)
        {
            string plain = string.Empty;
            using (RijndaelManaged rijndael = new RijndaelManaged())
            {
                rijndael.BlockSize = 128;
                rijndael.KeySize = 128;
                rijndael.Mode = CipherMode.CBC;
                rijndael.Padding = PaddingMode.PKCS7;

                rijndael.IV = Encoding.UTF8.GetBytes(iv);
                rijndael.Key = Encoding.UTF8.GetBytes(key);

                ICryptoTransform decryptor = rijndael.CreateDecryptor(rijndael.Key, rijndael.IV);

                using (MemoryStream mStream = new MemoryStream(System.Convert.FromBase64String(cipher)))
                {
                    using (CryptoStream ctStream = new CryptoStream(mStream, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader sr = new StreamReader(ctStream))
                        {
                            plain = sr.ReadLine();
                        }
                    }
                }
            }
            return plain;
        }
    }
}
