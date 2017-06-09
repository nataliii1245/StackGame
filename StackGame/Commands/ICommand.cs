using System;
namespace StackGame.Commands
{
    public interface ICommand
    {
		#region Методы

		/// <summary>
		/// Выполнить команду
		/// </summary>
		void Execute();
		/// <summary>
		/// Отменить результат выполнения команды
		/// </summary>
		void Undo();

		#endregion
	}
}
