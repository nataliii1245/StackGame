using System;
using System.IO;
using System.Text;
namespace StackGame.Observers
{
    public class FileObserver : IObserver
    {
        #region Свойства

        private readonly string fileName = "DeadLog.txt";

        private readonly string fullPath;
		#endregion



		#region Инициализация

        public FileObserver() 
        {
            var pathToDesktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            fullPath = Path.Combine(pathToDesktop, fileName);

            using (StreamWriter streamWriter = new StreamWriter(fullPath, false, Encoding.Default))
			{
				streamWriter.WriteLine(string.Empty);
			}
        }

		#endregion



		#region Методы

        /// <summary>
        /// Метод, вызывает обновление наблюдателя
        /// </summary>
        /// <returns>The update.</returns>
        /// <param name="object">Object.</param>
        public void Update(object @object)
        {
            if( @object is string message)
            {
                using (StreamWriter streamWriter = new StreamWriter(fullPath, true, Encoding.Default))
				{
                    streamWriter.WriteLine(message);
				}
            }
        }

		#endregion
	}
}
