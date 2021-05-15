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
        private Action<object> execute;               //Сама логика комманды
        private Func<object, bool> canExecute;        //Признак возможности выполнить комманду

        //Конструктор.
        public BotClientCommand(Action<object> execute, Func<object, bool> canExecute = null)
        {
            this.execute = execute;
            this.canExecute = canExecute;
        }

        #region Реализация интерфейса ICommand
        //Метод реагирующий на изменения комманд
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            return this.canExecute == null || this.canExecute(parameter); //Если canExecute null то возвращаем null. Если нет то возвращает canExecute(parameter)
        }

        public void Execute(object parameter)
        {
            this.execute(parameter);        //Выполняем то что было передано.
        }
        #endregion
    }
}
