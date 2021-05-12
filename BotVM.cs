using HomeWork_10.TelegramBot;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace HomeWork_10
{
    class BotVM
    {
        private static BotVM bvm = new BotVM();

        public static BotVM GetBotVM()
        {
            if (bvm == null)
                bvm = new BotVM();
            return bvm;
        }



        #region Основная колекция пользователей
        public ObservableCollection<ChatUser> ChatUsers { get; set; } = new ObservableCollection<ChatUser>();
        #endregion

        public BotVM()
        {
            ChatUsers.Add(new ChatUser { Name = "aaa"});
            ChatUsers.Add(new ChatUser { Name = "bbb" });
            ChatUsers.Add(new ChatUser { Name = "ccc" });
            ChatUsers.Add(new ChatUser { Name = "ddd" });
            ChatUsers.Add(new ChatUser { Name = "eee" });
            ChatUsers.Add(new ChatUser { Name = "fff" });
        }

        public void NewMessage(Telegram.Bot.Args.MessageEventArgs message)
        {
            bool IsNewUser = true;
            if (ChatUsers.Count > 0)           //Если коллекция пользователей пуста , то создаем нового пользователя.
            {
                foreach (var user in ChatUsers)
                {
                    if (user.UserID == message.Message.From.Id)
                        IsNewUser = false;
                }
            }
            if(IsNewUser)
            {
                ChatUsers.Add(new TelegramBot.ChatUser
                {
                    Messeges = new ObservableCollection<UserMessege>(),
                    Name = message.Message.From.FirstName + " " + message.Message.From.LastName,
                    UserID = message.Message.From.Id,
                    UserName = message.Message.From.Username
                });
            }

            foreach (var user in ChatUsers)
            {
                if (user.UserID == message.Message.From.Id)
                {
                    user.Messeges.Add(new UserMessege
                    {
                        ChatID = message.Message.Chat.Id,
                        Date = DateTime.Now,
                        Text = message.Message.Text
                    });
                }
            }
        }
    }
}
