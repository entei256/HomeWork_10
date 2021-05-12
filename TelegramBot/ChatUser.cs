using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace HomeWork_10.TelegramBot
{
    public class ChatUser
    {
        public long UserID { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public ObservableCollection<UserMessege> Messeges { get; set; } = new ObservableCollection<UserMessege>();
    }
}
