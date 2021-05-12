using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Telegram.Bot;

namespace HomeWork_10.TelegramBot
{
    /// <summary>
    /// Некое подобие сингтона.
    /// </summary>
    public static class TelegramBots
    {
        private static TelegramBotClient tlgBot;

        public static TelegramBotClient GetBot()
        {
            if (tlgBot == null)           //Если нету экземпляра бота, то создаем.
            {
                tlgBot = new TelegramBotClient(File.ReadAllText("D:\\Учеба\\BotTlgToken.txt"));
                tlgBot.OnMessage += BotHomeListner;
            }
            
            return tlgBot;          //Возвращаем записанный в приватное поле экземпляр бота.
        }


        /// <summary>
        /// Начальный прослушиватель.
        /// </summary>
        /// <param name="sernder"></param>
        /// <param name="message"></param>
        public static void BotHomeListner(object sernder, Telegram.Bot.Args.MessageEventArgs message)
        {
            if (message.Message.Type != Telegram.Bot.Types.Enums.MessageType.Text)           //Проверяем что пришел текст
                return;

            var vmodel = BotVM.GetBotVM();
            vmodel.NewMessage(message);



            vmodel.ChatUsers.Add(new ChatUser { Name = "fig"});
        }
    }
}
