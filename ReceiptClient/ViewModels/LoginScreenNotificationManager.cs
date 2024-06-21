using C1.Win.C1FlexGrid;
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
    public class LoginScreenNotificationManager : IGridManager
    {
        public int Total => throw new NotImplementedException();

        public int TotalPages => throw new NotImplementedException();

        private List<Notification> notifications = new List<Notification>();

        public List<Notification> Notifications => notifications;

        private string jsonFilePath = Path.Combine(Application.LocalUserAppDataPath, "notifications.json");

        private DataTable dt = new DataTable();
        public DataTable Dt => dt;
        private List<FilterCriteria> filters = new List<FilterCriteria>();

        private int currentPage = 1;
        private int pageSize = 50;

        public LoginScreenNotificationManager()
        {
            dt.Columns.Add("date", typeof(DateTime));
            
            dt.Columns.Add("content", typeof(string));
            LoadNotifications();

            foreach(var n in notifications
                .Where(n=>n.DisplayEndDate >= DateTime.Now)
                .Where(n=>n.Placement == NotificationPlacement.Login)
                )
            {
                var newRow = dt.NewRow();
                newRow["date"] = n.DisplayStartDate;
                var content = string.Format(@"{0}
{1}", n.Title, n.Body);
                newRow["content"] = content;
                dt.Rows.Add(newRow);
            }
           
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
        public List<FilterCriteria> Filters => filters;

        public event System.EventHandler OnDataSourceChange;

        public void Reload()
        {
            throw new NotImplementedException();
        }

        public void Reload(List<FilterCriteria> filters)
        {
            throw new NotImplementedException();
        }

        public void SetCurrentPage(int currentPage)
        {
            throw new NotImplementedException();
        }

        public void SetFilters(List<FilterCriteria> filters)
        {
            throw new NotImplementedException();
        }

        public void SetPageSize(int pageSize)
        {
            throw new NotImplementedException();
        }
    }
}
