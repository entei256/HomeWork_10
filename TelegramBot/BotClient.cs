using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace HomeWork_10.TelegramBot
{
    /// <summary>
    /// Основной клас бота
    /// </summary>
    class BotCient
    {
        #region Основная колекция пользователей
        private static ObservableCollection<ChatUser> chatusers;
        public static ObservableCollection<ChatUser> ChatUsers 
        {
            //Подобие сингтона. Все время существует 1 экземпляр колекции.
            get 
            { 
                if (chatusers == null)
                    chatusers = new ObservableCollection<ChatUser>();
                return chatusers;      
            }

            //Если будет присваивать значения другой колекции то просто очищаем и подом добовляем в текущую колекцию элементы новой колекции.
            //Надо что бы не потерять ссылку на колекцию.
            set {
                chatusers.Clear();
                foreach(var item in value)
                {
                    chatusers.Add(item);
                }
            }
        }
        #endregion

        /// <summary>
        /// Начальный прослушиватель.
        /// </summary>
        /// <param name="sernder"></param>
        /// <param name="message"></param>
        public static void BotHomeListner(object sernder, Telegram.Bot.Args.MessageEventArgs message)
        {
            if (message.Message.Type != Telegram.Bot.Types.Enums.MessageType.Text)           //Проверяем что пришел текст
                return;

            if(ChatUsers.Count <= 0)           //Если коллекция пользователей пуста , то создаем нового пользователя.
            {
                ChatUsers.Add(new TelegramBot.ChatUser
                {
                    Messeges = new ObservableCollection<UserMessege>(),
                    Name = message.Message.From.FirstName + " " + message.Message.From.LastName,
                    UserID = message.Message.From.Id,
                    UserName = message.Message.From.Username
                });
            } 

            var u = message.Message.From;       //Просто посмотреть

            foreach(var user in ChatUsers)
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
