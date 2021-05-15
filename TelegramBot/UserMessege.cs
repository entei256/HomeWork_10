using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace HomeWork_10.TelegramBot
{
    public class UserMessege
    {
        public long ChatID { get; set; }
        public DateTime Date { get; set; }
        public string Text { get; set; }
        public int MessageId { get; set; }
        public bool IsNew { get; set; } = true;
        //public ChatUser CurUser { get; set; }
    }
}
