using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Windows.Forms;
using System.Drawing;

namespace ReceiptClient.Shared
{
    public class ColorSettings
    {
        public bool isValid { get; set; } = true;
        public string Background { get; set; }
        public string Foreground { get; set; }
    }

    public class UserSettings
    {
        public string Username { get; set; }
        public Dictionary<string, ColorSettings> Colors { get; set; }
    }

    public class Settings
    {
        public List<UserSettings> Users { get; set; }
    }
    public class GridColorManager
    {
        private string _jsonFilePath;
        private Settings _settings;
        public event EventHandler OnChangeSettings;
        public GridColorManager(string jsonFileName)
        {
            _jsonFilePath = Path.Combine(Application.LocalUserAppDataPath, jsonFileName);
            LoadOrCreateSettings();
        }

        private void LoadOrCreateSettings()
        {
            if (File.Exists(_jsonFilePath))
            {
                string json = File.ReadAllText(_jsonFilePath);
                _settings = JsonConvert.DeserializeObject<Settings>(json);
            }
            else
            {
                /*
                _settings = new Settings { Users = new List<UserSettings>() };
                _settings.Users.Add(LoadColorsFromDatabaseAsync().GetAwaiter().GetResult()); // DBから色情報を読み込む
                */
                SaveSettings(); // 新規ファイルの作成
            }
        }
        private UserSettings defaultInitColorsSettings()
        {
          
            return new UserSettings
            {
                Username = "default",
                Colors = new Dictionary<string, ColorSettings>
                    {   
                        { "受付", new ColorSettings { Background = ColorToHex(16777088), Foreground = ColorToHex(255) } },
                        { "依頼", new ColorSettings { Background = ColorToHex(12320767), Foreground = ColorToHex(0) } },
                        { "申請済み", new ColorSettings { Background = ColorToHex(14671839), Foreground = ColorToHex(16711680) } },
                        { "納品", new ColorSettings { Background = ColorToHex(12632256), Foreground = ColorToHex(16777215) } },
                        { "キャンセル", new ColorSettings { Background = ColorToHex(8421504), Foreground = ColorToHex(0) } }
                    }
            };
        }

        public async Task<UserSettings> LoadColorsFromDatabaseAsync()
        {
           
            
            UserSettings userSettings = defaultInitColorsSettings();

            // データベースからの色設定のダミーデータ
            try
            {
                var processDetails = await State.GetProcessDetails().ConfigureAwait(false);

                // C1FlexGrid の各行をループ
                foreach (var gridProcessColor in userSettings.Colors)
                {
                    var matchingDetail = processDetails.FirstOrDefault(detail => detail.工程名 == gridProcessColor.Key);

                    if (matchingDetail != null)
                    {
                        gridProcessColor.Value.Background = matchingDetail.背景色;
                        gridProcessColor.Value.Foreground = matchingDetail.文字色;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"エラーが発生しました: {ex.Message}");
            }
           return userSettings;

        }

        private string ColorToHex(int color)
        {
            Color clr = Color.FromArgb(color);
            return $"#{clr.R:X2}{clr.G:X2}{clr.B:X2}";
        }


        public ColorSettings GetUserColorSettings(string username, string status)
        {
            var userSetting = _settings.Users.Find(u => u.Username == username);
            if (userSetting != null && userSetting.Colors.ContainsKey(status))
            {
                return userSetting.Colors[status];
            }

            return null; // 設定が見つからない場合はnullを返す
        }

        public Dictionary<string, ColorSettings> GetAllUserColorSettings(string username)
        {
            var userSetting = _settings.Users.Find(u => u.Username == username);
            return userSetting?.Colors;
        }
        public UserSettings GetUserSettings(string username)
        {
            try { 
            return _settings.Users.Find(u => u.Username == username); ;
            }
            catch
            {
                return defaultInitColorsSettings();
            }
        }

        public void SaveSettings()
        {
            string json = JsonConvert.SerializeObject(_settings, Formatting.Indented);
            File.WriteAllText(_jsonFilePath, json);
            if (OnChangeSettings != null)
                OnChangeSettings(_settings.Users.FirstOrDefault(u=> u.Username == "default"),null);
        }

        public void UpdateUserColor(string username, string status, string backgroundColor, string foregroundColor)
        {
            var userSetting = _settings.Users.Find(u => u.Username == username);
            if (userSetting == null)
            {
                userSetting = new UserSettings
                {
                    Username = username,
                    Colors = new Dictionary<string, ColorSettings>()
                };
                _settings.Users.Add(userSetting);
            }

            if (userSetting.Colors.ContainsKey(status))
            {
                userSetting.Colors[status].Background = backgroundColor;
                userSetting.Colors[status].Foreground = foregroundColor;
            }
            else
            {
                userSetting.Colors.Add(status, new ColorSettings
                {
                    Background = backgroundColor,
                    Foreground = foregroundColor
                });
            }

            SaveSettings();
        }
        public void UpdateUserIsValid(string username, string status, bool isValid)
        {
            var userSetting = _settings.Users.Find(u => u.Username == username);
            if (userSetting == null)
            {
                userSetting = new UserSettings
                {
                    Username = username,
                    Colors = new Dictionary<string, ColorSettings>()
                };
                _settings.Users.Add(userSetting);
            }

            if (userSetting.Colors.ContainsKey(status))
            {
                userSetting.Colors[status].isValid = isValid;

            }

            SaveSettings();
        }
        public void UpdateUserSettings(string username, UserSettings userSetting)
        {
            try { 
            var index = _settings.Users.FindIndex(u => u.Username == username);
                _settings.Users[index] = userSetting;
            }
            catch
            {
                _settings.Users.Add(userSetting);
            }

            SaveSettings();
        }
        public void UpdateUserBackgroundColor(string username, string status, string backgroundColor)
        {
            var userSetting = _settings.Users.Find(u => u.Username == username);
            if (userSetting == null)
            {
                userSetting = new UserSettings
                {
                    Username = username,
                    Colors = new Dictionary<string, ColorSettings>()
                };
                _settings.Users.Add(userSetting);
            }

            if (userSetting.Colors.ContainsKey(status))
            {
                userSetting.Colors[status].Background = backgroundColor;
               
            }
            else
            {
                userSetting.Colors.Add(status, new ColorSettings
                {
                    Background = backgroundColor,
                    Foreground = ColorTranslator.ToHtml(Color.Black),
                });
            }

            SaveSettings();
        }
        public void UpdateUserForegroundColor(string username, string status,  string foregroundColor)
        {
            var userSetting = _settings.Users.Find(u => u.Username == username);
            if (userSetting == null)
            {
                userSetting = new UserSettings
                {
                    Username = username,
                    Colors = new Dictionary<string, ColorSettings>()
                };
                _settings.Users.Add(userSetting);
            }

            if (userSetting.Colors.ContainsKey(status))
            {
                
                userSetting.Colors[status].Foreground = foregroundColor;
            }
            else
            {
                userSetting.Colors.Add(status, new ColorSettings
                {
                    Background = ColorTranslator.ToHtml(Color.White),
                    Foreground = foregroundColor
                });
            }

            SaveSettings();
        }
    }

}
