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
            }
            return tlgBot;          //Возвращаем записанный в приватное поле экземпляр бота.
        }
    }
}
