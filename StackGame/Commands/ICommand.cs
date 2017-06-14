using StackGame.Loggers;

namespace StackGame.Commands
{
    /// <summary>
    /// Интерфейс для команд
    /// </summary>
    public interface ICommand
    {
		#region Методы

		/// <summary>
		/// Выполнить команду
		/// </summary>
        void Execute(ILogger logger);
		/// <summary>
		/// Отменить результат выполнения команды
		/// </summary>
        void Undo(ILogger logger);

		#endregion
	}
}
