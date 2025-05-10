using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.Domain.Entities
{
    public class Notification : BaseEntity
    {
        public Notification(string value)
        {
            Value = value;
            Read = false;
        }
        public Notification(string value, string redirectUrl) : this(value)
        {
            RedirectUrl = redirectUrl;
        }
        public string Value { get; private set; }
        public int UserId { get; private set; }
        public bool Read { get; private set; } = false;
        public DateTime? ReadAt { get; private set; }
        public string RedirectUrl { get; private set; } = string.Empty;
        public virtual User? User { get; private set; }
        public void MarkAsRead()
        {
            this.Read = true;
            this.ReadAt = DateTime.UtcNow;
        }
    }


}
