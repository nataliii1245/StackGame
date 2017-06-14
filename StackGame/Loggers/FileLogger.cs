using System;
using System.IO;
using System.Text;
namespace StackGame.Loggers
{
    public class FileLogger : ILogger
    {
		#region Свойства
		/// <summary>
		/// Название файла, в который будем записывать историю приложения
		/// </summary>
		private readonly string fileName;

		/// <summary>
		/// Полный путь к файлу, в который будем записывать историю приложения
		/// </summary>
		private readonly string fullPath;

		#endregion

        #region Инициализация

        public FileLogger(string fileName)
		{
			this.fileName = fileName;

			var pathToDesktop = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
			fullPath = Path.Combine(pathToDesktop, fileName);

			using (StreamWriter streamWriter = new StreamWriter(fullPath, false, Encoding.Default))
			{
				streamWriter.WriteLine(string.Empty);
			}
		}

        #endregion

        #region Методы

		/// <summary>
		/// Записать событие в log- файл
		/// </summary>
		public void Log(string message)
		{
			using (StreamWriter streamWriter = new StreamWriter(fullPath, true, Encoding.Default))
			{
				streamWriter.WriteLine(message);
			}
		}
		#endregion
	}
}
