namespace StackGame.Loggers
{
    public interface ILogger
    {
		#region Методы

		/// <summary>
		/// Залогировать событие
		/// </summary>
		void Log(string message);

		#endregion
	}
}
