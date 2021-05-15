using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using System.Windows.Threading;

namespace HomeWork_10.TelegramBot
{
    /// <summary>
    /// Основной клас бота
    /// </summary>
    public class BotCient
    {
        //Получал ошибку, в ходе гугления выяснил что надо делать через диспатчер , т.к. мешает поток UI. но до конца(Или вообще) не понял почему так.
        static Dispatcher dispatcher = Dispatcher.CurrentDispatcher;         

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

        #region Выбранные элементы пользователей и сообщений
        private static UserMessege selectedMessage;
        private static ChatUser selectedUser;
        public static ChatUser SelectedUser 
        {
            get { return selectedUser; } 
            set //нуждно запускать простовление флага IsNew для сообщений пользователя
            {
                if (value == selectedUser)                  
                    return;
                changedIsNewFlag(user:selectedUser);
                selectedUser = value;
            }
        }
        public static UserMessege SelectedMessage 
        { 
            get 
            {
                return selectedMessage;
            } 
            set 
            {
                if (selectedMessage == value)
                    return;
                changedIsNewFlag(user:SelectedUser,message:selectedMessage);
                selectedMessage = value;
            } 
        }
        #endregion

        #region Комманды для UI
        private BotClientCommand sendCommand;
        public BotClientCommand SendCommand {
            get {
                return sendCommand ?? (sendCommand = new BotClientCommand(obj => SendMessage(obj)));      //Немного изменил вариант с Митанита. Больше всего понравился.
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
            bool IsNewUser = true;      //Флаг на то что новый USer написал боту.
            if (message.Message.Type != Telegram.Bot.Types.Enums.MessageType.Text)           //Проверяем что пришел текст
                return;

            if (ChatUsers.Count <= 0)           //Если коллекция пользователей пуста , то создаем нового пользователя.
            {
                dispatcher.Invoke(() =>
                ChatUsers.Add(new TelegramBot.ChatUser
                {
                    Messeges = new ObservableCollection<UserMessege>(),
                    Name = message.Message.From.FirstName + " " + message.Message.From.LastName,
                    UserID = message.Message.From.Id,
                    UserName = message.Message.From.Username
                }));
            }
            
            foreach(var user in ChatUsers)
            {
                if(user.UserID == message.Message.From.Id)       //Если UserID уже есть в коллекции , то ему добовляем новое сообщение.
                {
                    dispatcher.Invoke(() =>
                    user.Messeges.Add(new UserMessege
                    {
                        ChatID = message.Message.Chat.Id,
                        Date = DateTime.Now,
                        Text = message.Message.Text,
                        IsNew = true,
                        MessageId = message.Message.MessageId
                    })) ;
                    user.NewMessage();          //Костыль что бы время в UI обновлялось.
                    IsNewUser = false;           //Раз пользователь найде то меняем флаг.
                    return;                        //Так как нашли пользователя которому надо добавить сообщение, то дальнейшие проверки бесмыслены
                }
            }

            if (IsNewUser)           //Если все же новый пользователь.
            {
                dispatcher.Invoke(() =>
                            ChatUsers.Add(new TelegramBot.ChatUser
                            {
                                Messeges = new ObservableCollection<UserMessege>(),
                                Name = message.Message.From.FirstName + " " + message.Message.From.LastName,
                                UserID = message.Message.From.Id,
                                UserName = message.Message.From.Username
                            }));
                dispatcher.Invoke(() =>
                ChatUsers[ChatUsers.Count-1].Messeges.Add(new UserMessege
                {
                    ChatID = message.Message.Chat.Id,
                    Date = DateTime.Now,
                    Text = message.Message.Text
                })); //Добовляем ему сразу сообщение.
                ChatUsers[ChatUsers.Count - 1].NewMessage(); //Костыль что бы время в UI обновлялось.
            }
        }


        //Метод отправки сообщения
        public static void SendMessage(object TextToSend)
        {
            var bot = TelegramBots.getBot();  //Получаем ссылку на бота.
            var text = TextToSend as string; //Приводим полученный обьект к тексту

            foreach(var user in ChatUsers)
            {
                if (user.UserID == SelectedUser.UserID)
                {
                    user.Messeges.Add(new UserMessege
                    {
                        ChatID = user.UserID,
                        Date = DateTime.Now,
                        Text = text
                    });

                    bot.SendTextMessageAsync(user.UserID,text);
                }
            }


        }


        //Меняем флаг IsNew у сообщений.
        private static void changedIsNewFlag(ChatUser user, UserMessege message = null)
        {
            if (user == null)
                return;
            foreach(var curmessage in user.Messeges)
            {
                if (message == null || message.Equals(curmessage))
                    curmessage.IsNew = false;
            }
        }
    }
}
