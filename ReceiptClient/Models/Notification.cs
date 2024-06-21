using System;

namespace ReceiptClient.Models
{
    public class Notification
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public DateTime DisplayStartDate { get; set; } = DateTime.Now;
        public DateTime DisplayEndDate { get; set; } = DateTime.Now;
        public NotificationPlacement Placement { get; set; } = NotificationPlacement.Login;
        public DateTime CreatedDate { get; set; }
        public int CreatedUser { get; set; }
    }
}
