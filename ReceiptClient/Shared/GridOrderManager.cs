using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using C1.Win.C1FlexGrid;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ReceiptClient.Models;

namespace ReceiptClient.Shared
{
    /// <summary>
    /// グリッド情報を管理するクラス
    /// JSONを参照してグリッドの項目順、表示非表示、幅を取得できる
    /// JSONにキーを増やす場合、本クラスにAPIを追加して参照することを想定
    /// </summary>
    public class GridOrderManager
    {
        private readonly string _jsonFilePath;
        public string FilePath => _jsonFilePath;
        private readonly List<ColumnMappingConfig> _initialColumnConfigs;
        private UserConfig _userPreference;
        public event System.EventHandler OnPatternChange;
        public string SelectedPattern => _userPreference.SelectedPattern;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="jsonFileName">JSONファイルの名前</param>
        /// <param name="initialColumnConfigs">初期の列設定</param>
        public GridOrderManager(string jsonFileName, List<ColumnMappingConfig> initialColumnConfigs = null)
        {
            _jsonFilePath = Path.Combine(Application.LocalUserAppDataPath, jsonFileName);
            _initialColumnConfigs = initialColumnConfigs;

            if (File.Exists(_jsonFilePath))
            {
                _userPreference = ParseFromFile();
            }
            else
            {
                if (initialColumnConfigs != null)
                {
                    _userPreference = GenerateNewJsonData(initialColumnConfigs);
                    SaveToFile  ();
                }
                else
                {
                    throw new Exception(jsonFileName + "ファイルがありません");
                }
            }
        }

        private UserConfig ParseFromFile()
        {
            return JsonConvert.DeserializeObject<UserConfig>(File.ReadAllText(_jsonFilePath));
        }

        /// <summary>
        /// 指定されたパターン名が存在するかどうかを判定する
        /// </summary>
        /// <param name="patternName">チェックするパターン名</param>
        /// <returns>存在する場合はtrue、そうでない場合はfalse</returns>
        public bool DoesPatternExist(string patternName)
        {
           return _userPreference.patterns.ContainsKey(patternName);
        }

        public string[] GetColumnOrderForSelectedPattern()
        {
            var patternName = _userPreference.SelectedPattern;
            if (!DoesPatternExist(patternName))
            {
                throw new InvalidOperationException($"JSONファイルに'{patternName}'というパターンは存在しません。");
            }
            return _userPreference.patterns[patternName].ColumnOrder.ToArray();
        }

        /// <summary>
        /// 指定されたパターンに基づいて列の順序を取得
        /// </summary>
        /// <param name="patternName">列の順序を取得するパターン名</param>
        /// <returns>列の順序</returns>
        /// 
        public string[] GetColumnOrderForPattern(string patternName)
        {
            if (!DoesPatternExist(patternName))
            {
                throw new InvalidOperationException($"JSONファイルに'{patternName}'というパターンは存在しません。");
            }

            return _userPreference.patterns[patternName].ColumnOrder.ToArray();
        }

        /// <summary>
        /// 指定されたパターンに基づいて列の設定を取得
        /// </summary>
        /// <param name="patternName">列の設定を取得するパターン名</param>
        /// <returns>列の設定のリスト</returns>
        public List<ColumnSetting> GetColumnSettingsForPattern(string patternName)
        {
            if (!DoesPatternExist(patternName))
            {
                throw new InvalidOperationException($"JSONファイルに'{patternName}'というパターンは存在しません。");
            }

            return ParseFromFile().patterns[patternName].ColumnSettings;
        }

        public List<ColumnSetting> GetColumnSettingsForSelectedPattern()
        {
            string patternName = _userPreference.SelectedPattern;
            if (!DoesPatternExist(patternName))
            {
                throw new InvalidOperationException($"JSONファイルに'{patternName}'というパターンは存在しません。");
            }
            return ParseFromFile().patterns[patternName].ColumnSettings;

        }

        /// <summary>
        /// 表示・非表示状態の取得
        /// </summary>
        public bool GetVisibilityForColumn(string patternName, string columnName)
        {
            if (_userPreference.patterns.TryGetValue(patternName, out PatternConfig pattern))
            {
                return pattern.ColumnSettings.First(x => x.ColumnName == columnName).Visible;
            }
            return true;
        }

        /// <summary>
        /// 指定された設定に基づいてグリッドの列設定を適用
        /// </summary>
        /// <param name="grid">列設定を適用するグリッド</param>
        /// <param name="settings">列設定のリスト</param>
        public void ApplyColumnSettingsToGrid(C1FlexGrid grid, List<ColumnSetting> settings)
        {
            foreach (var setting in settings)
            {
                var col = grid.Cols[setting.ColumnName];
                col.Width = setting.Width;
                col.Visible = setting.Visible;
            }
        }

       

        /// <summary>
        /// 指定されたパターンのcolumnOrderを更新する
        /// </summary>
        /// <param name="patternName">更新対象のパターン名</param>
        /// <param name="newOrder">新しいcolumnOrderの配列</param>
        public void UpdateColumnOrderForPattern(string patternName, string[] newOrder)
        {
            if (_userPreference.patterns.TryGetValue(patternName, out PatternConfig pattern))
            {
                pattern.ColumnOrder = newOrder.ToList();
            }
        }

        /// <summary>
        /// 指定されたパターンのcolumnSettingsの表示・非表示情報を更新する
        /// </summary>
        /// <param name="patternName">更新対象のパターン名</param>
        /// <param name="visibilityInfo">表示・非表示情報の辞書</param>
        public void UpdateColumnVisibilityForPattern(string patternName, Dictionary<string, bool> visibilityInfo)
        {
            if (_userPreference.patterns.TryGetValue(patternName, out PatternConfig pattern))
            {
                foreach (var item in pattern.ColumnSettings)
                {
                    var columnName = item.ColumnName;
                    if (visibilityInfo.ContainsKey(columnName))
                    {
                        item.Visible = visibilityInfo[columnName];
                    }
                }
            }
        }


        /// <summary>
        /// ColumnMappingConfigのリストを基に新しいJSONデータを生成する
        /// </summary>
        /// <param name="columnConfigs">ColumnMappingConfigのリスト</param>
        /// <returns>生成されたJObjectオブジェクト</returns>
        private UserConfig GenerateNewJsonData(List<ColumnMappingConfig> columnConfigs)
        {
            var patternNames = new[] { "PatternA", "PatternB", "PatternC" };
            var _conf = new UserConfig
            {
                UserId = "00001",
                Version = "1.0",
                SelectedPattern = patternNames[0],
                patterns = new Dictionary<string, PatternConfig>()

            };

            foreach (var patternName in patternNames)
            {
                _conf.patterns.Add(patternName, new PatternConfig
                {
                    ColumnOrder = columnConfigs.Select(config => config.Caption).ToList(),
                    ColumnSettings = columnConfigs.Select(config => new Models.ColumnSetting
                    {
                        ColumnName = config.Caption,
                        Width = config.Width,
                        Visible = true
                    }).ToList()
                });
            }

            return _conf;
        }

        /// <summary>
        /// 現在のJSONデータをファイルに保存する
        /// </summary>
        public void SaveToFile()
        {
            File.WriteAllText(_jsonFilePath, JsonConvert.SerializeObject(_userPreference));
        }

        /// <summary>
        /// JSONファイルを初期化する
        /// </summary>
        public void InitializeJsonFile()
        {
            _userPreference = GenerateNewJsonData(_initialColumnConfigs);
            SaveToFile();
        }

        public List<ColumnMappingConfig> GetOrderedColumnConfigsForPattern(string selectedPattern)
        {
            List<ColumnMappingConfig> orderedConfigs = new List<ColumnMappingConfig>();
            List<ColumnMappingConfig> configs = _initialColumnConfigs;

            var orders = GetColumnOrderForPattern(selectedPattern);
            var settings = GetColumnSettingsForPattern(selectedPattern);
          
            foreach(var order in orders)
            {
                if(settings.Any(o => o.ColumnName == order && o.Visible))
                {
                    var conf = configs.First(c => c.Caption == order);
                    orderedConfigs.Add(conf);
                }
            }

            return orderedConfigs;
        }

        public void SetSelectedPattern(string selectedPattern)
        {
            _userPreference.SelectedPattern = selectedPattern;
            OnPatternChange?.Invoke(this, EventArgs.Empty);
        }
    }

   
}
