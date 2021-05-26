using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interactivity;

namespace HomeWork_10.TelegramBot
{
    /// <summary>
    /// Проба использования Behavior.
    /// Сколько не читал но не понимаю как его использовать, да и зачем он нужен.
    /// </summary>
    //public class BehaviorButtonSend : Behavior<Button>
    //{
    //    /*
    //    // попробовал сделать Attached свойство для TextBox. Но все равно проще сделать binding
    //    public TextBox AnswerTextBox
    //    {
    //        get { return (TextBox)GetValue(MyPropertyProperty); }
    //        set { SetValue(MyPropertyProperty, value); }
    //    }

    //    // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
    //    public static readonly DependencyProperty MyPropertyProperty =
    //        DependencyProperty.Register("AnswerTextBox", typeof(int), typeof(BehaviorButtonSend), new PropertyMetadata(""));*/



    //    protected override void OnAttached()
    //    {
    //        base.OnAttached();
    //        AssociatedObject.Click += Click;
    //    }

    //    protected override void OnDetaching()
    //    {
    //        base.OnDetaching();
    //        AssociatedObject.Click -= Click;
    //    }


    //    //Метод для события нажатия.
    //    void Click(Object s, RoutedEventArgs e)
    //    {
    //        BotCient.SendMessage(AnswerTextBox);         //Что бы не копипастить просто вызываем метод.
    //    }
    //}
}
