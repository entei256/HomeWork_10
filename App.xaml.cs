using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace HomeWork_10
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var startUpBot = TelegramBot.TelegramBots.getBot();         //Получаем ссылку на бота
            startUpBot.OnMessage += TelegramBot.BotCient.BotHomeListner; //Подписываемся на послушиватель при старте.
            startUpBot.StartReceiving(); //Запускаем бота

        }
    }
}
