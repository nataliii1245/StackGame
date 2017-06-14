using System;

namespace StackGame.Loggers
{
    public class ConsoleLogger : ILogger
    {
       #region Методы

        /// <summary>
        /// Записать событие в консоль
        /// </summary>
		public void Log(string message)
		{
            Console.WriteLine(message);
		}
        #endregion
    }
}
