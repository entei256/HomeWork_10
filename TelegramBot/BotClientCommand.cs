using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace HomeWork_10.TelegramBot
{
    /// <summary>
    /// Класс реализации комманд
    /// </summary>
    public class BotClientCommand : ICommand
    {
        private Action execute;               //Сама логика комманды
        private Action<object> executePram;               //Сама логика комманды с параметрами
        private Func<object, bool> canExecute;        //Признак возможности выполнить комманду

        //Конструктор для создания комманды с параметрами.
        public BotClientCommand(Action<object> execute, Func<object, bool> canExecute = null)
        {
            this.executePram = execute;
            this.canExecute = canExecute;
        }

        public BotClientCommand(Action action, Func<object, bool> canExecute = null)
        {
            //  Set the action.
            this.execute = action;
            this.canExecute = canExecute;
        }

        #region Реализация интерфейса ICommand
        //Метод реагирующий на изменения комманд
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter = null)
        {
            return this.canExecute == null || this.canExecute(parameter); //Если canExecute null то возвращаем null. Если нет то возвращает canExecute(parameter)
        }


        public void Execute(object parameter = null)
        {
            if (parameter == null)
                this.execute();
            else
                this.executePram(parameter);        //Выполняем то что было передано.
        }
        #endregion
    }
}
