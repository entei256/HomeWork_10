using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace HomeWork_10.TelegramBot
{
    public class ChatUser : INotifyPropertyChanged
    {
        private ObservableCollection<UserMessege> messeges;
        public long UserID { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        //В тиории у каждого экземпляра класса должны быть свои связки с сообщениями. Попробуем сделать синглтон.
        public ObservableCollection<UserMessege> Messeges 
        {
            get 
            {
                if (this.messeges == null)
                    this.messeges = new ObservableCollection<UserMessege>();
                return this.messeges;
            } 
            set
            {
                if (this.messeges == null)
                    this.messeges = this.Messeges;      //Вызываем сами себя что бы уйти в get.
                this.messeges.Clear();
                foreach(var item in value)
                {
                    messeges.Add(item);
                }
            }
        }
        public DateTime LastMessageDate { 
            get {
                if (Messeges.Count <= 0)
                    return DateTime.MinValue;

                return Messeges[Messeges.Count - 1].Date;
            } 
        }
        public bool IsNewMessage { get; set; }


        #region Реалзация INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion

        public void NewMessage()
        {
            NotifyPropertyChanged("LastMessageDate");  //Уведомляем что в ChatUser обновилось свойство LastMessageDate, т.к. получили новое сообщение
            IsNewMessage = true;
        }
    }
}
