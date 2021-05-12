using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;

namespace HomeWork_10.TelegramBot
{
    /// <summary>
    /// Основной клас бота
    /// </summary>
    public class BotCient
    {
        /// <summary>
        /// Начальный прослушиватель.
        /// </summary>
        /// <param name="sernder"></param>
        /// <param name="message"></param>
        public static void BotHomeListner(object sernder, Telegram.Bot.Args.MessageEventArgs message)
        {
            if (message.Message.Type != Telegram.Bot.Types.Enums.MessageType.Text)           //Проверяем что пришел текст
                return;

            if(MainWindow.ChatUsers.Count <= 0)           //Если коллекция пользователей пуста , то создаем нового пользователя.
            {
                MainWindow.ChatUsers.Add(new TelegramBot.ChatUser
                {
                    Messeges = new ObservableCollection<UserMessege>(),
                    Name = message.Message.From.FirstName + " " + message.Message.From.LastName,
                    UserID = message.Message.From.Id,
                    UserName = message.Message.From.Username
                });
            } 

            var u = message.Message.From;       //Просто посмотреть

            foreach(var user in MainWindow.ChatUsers)
            {
                if(user.UserID == message.Message.From.Id)
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
