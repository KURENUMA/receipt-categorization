using Newtonsoft.Json;
using System;

namespace ReceiptClient.Models
{
    public class NotificationPlacement : IEquatable<NotificationPlacement>
    {
        public static readonly NotificationPlacement Login = new NotificationPlacement("login");
        public static readonly NotificationPlacement Top = new NotificationPlacement("top");

        private string _value;
        public string Value => _value;

        [JsonConstructor]
        private NotificationPlacement(string value) {
            _value = value;
        }
       


        public bool Equals(NotificationPlacement other)
        {
            return other?.Value == _value;
        }

        public static implicit operator string(NotificationPlacement op) => op.Value;
        public static implicit operator NotificationPlacement(string value) => new NotificationPlacement(value);
    }
}
