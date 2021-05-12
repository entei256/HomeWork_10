using HomeWork_10.TelegramBot;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace HomeWork_10
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
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
            set
            {
                chatusers.Clear();
                foreach (var item in value)
                {
                    chatusers.Add(item);
                }
            }
        }
        #endregion
        public MainWindow()
        {
            InitializeComponent();

            UserView.ItemsSource = ChatUsers;
        }
    }
}
