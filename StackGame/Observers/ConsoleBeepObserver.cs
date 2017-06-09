using System;
namespace StackGame.Observers
{
    public class ConsoleBeepObserver : IObserver
    {
        #region Методы

        public void Update( object @object)
        {
            Console.Beep();
        }
        #endregion
    }
}
