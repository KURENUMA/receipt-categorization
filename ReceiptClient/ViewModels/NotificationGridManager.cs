using Dma.DatasourceLoader;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using ReceiptClient.Models;
using ReceiptClient.UserControls;

namespace ReceiptClient.ViewModels
{
    public class NotificationGridManager : IGridManager
    {
        private List<Notification> notifications = new List<Notification>();
        private string jsonFilePath = Path.Combine(Application.LocalUserAppDataPath, "notifications.json");

        private readonly DataTable dt = new DataTable();
        public DataTable Dt => dt;
        private List<FilterCriteria> filters = new List<FilterCriteria>();

        private int currentPage = 1;
        private int pageSize = 50;

        public List<FilterCriteria> Filters => filters;

        public NotificationGridManager()
        {
            InitializeDataTable();
            LoadNotifications();
        }

        private void LoadNotifications()
        {
            var exists = File.Exists(jsonFilePath);
            if (!exists)
            {
                SaveToJson();
            }
            var json = File.ReadAllText(jsonFilePath);
            notifications = JsonConvert.DeserializeObject<List<Notification>>(json);

        }

        public void SaveToJson()
        {
            var jsonContent = JsonConvert.SerializeObject(notifications);
            File.WriteAllText(jsonFilePath, jsonContent);
        }

        public void AddNotification(Notification notif) {
            notifications.Add(notif);
        }

        private void InitializeDataTable()
        {
            foreach (var config in NotificationHelpers.ColumnConfigs)
            {
                dt.Columns.Add(config.Caption, config.DataType);
            }
        }

        public int Total => throw new NotImplementedException();

        public int TotalPages => throw new NotImplementedException();

        public event System.EventHandler OnDataSourceChange;

        public void Reload()
        {
            PopulateDataToDataTable(notifications, true);
            OnDataSourceChange?.Invoke(this, EventArgs.Empty);
        }

        private void PopulateDataToDataTable(List<Notification> list, bool shouldReset = false)
        {
            if (shouldReset)
            {
                // データテーブルを初期化する
                dt.Clear();
            }

            // APIから非同期にデータを取得
            //List<Models.TManagementItem> list = await State.GetTManagementItemsDataAsync();

            // 一時的にdtをDtProjectに変更する
            foreach (var data in list)
            {
                DataRow newRow = dt.NewRow();

                foreach (var config in NotificationHelpers.ColumnConfigs)
                {
                    // ColumnConfig のマッピングストラテジーを使用してデータを変換する
                    object mappedValue = config.MapData(data);
                    if (mappedValue == null)
                    {
                        mappedValue = DBNull.Value; // すべてのデータ型に対して DBNull.Value をセット
                    }
                    newRow[config.Caption] = mappedValue;
                }

                dt.Rows.Add(newRow);
            }
        }
        public void Reload(List<FilterCriteria> filters)
        {
            throw new NotImplementedException();
        }

        public void SetCurrentPage(int currentPage) { this.currentPage = currentPage; }
        public void SetPageSize(int pageSize) { this.pageSize = pageSize; }
        public void SetFilters(List<FilterCriteria> filters) { this.filters = filters; }

        public int GetNextId()
        {
            return notifications.Count() + 1;
        }

        public Notification GetNotificationById(int id)
        {
            return notifications.First(x => x.Id == id);
        }

        public void UpdateNotification(Notification notif)
        {
            var n = notifications.First(x => x.Id == notif.Id);
            n.DisplayStartDate = notif.DisplayStartDate;
            n.DisplayEndDate = notif.DisplayEndDate;
            n.Body = notif.Body;
            n.Title = notif.Title;
            n.Placement = notif.Placement;
        }
    }
}
