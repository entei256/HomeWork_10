using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace HomeWork_10.TelegramBot
{
    class ChatUser
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


        
    }
}
