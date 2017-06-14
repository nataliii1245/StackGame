using StackGame.Loggers;
using StackGame.Game;

namespace StackGame.Commands
{
    /// <summary>
    /// Команда для изменения количества ходов
    /// </summary>
    public class ChangeCountOfMovementsCommand : ICommand
    {
		#region Свойства

		/// <summary>
		/// Исходное количество ходов
		/// </summary>
		private readonly int CountOfMovements;
		/// <summary>
		/// Новое количество ходов
		/// </summary>
		private readonly int newCountOfMovements;

		#endregion

        #region Инициализация

		public ChangeCountOfMovementsCommand(int CountOfMovements, int newCountOfMovements)
		{
			this.CountOfMovements = CountOfMovements;
			this.newCountOfMovements = newCountOfMovements;
		}

		#endregion

        #region Методы

		public void Execute(ILogger logger)
		{
            Engine.GetInstance().CountOfMovements = newCountOfMovements;
		}

		public void Undo(ILogger logger)
		{
            Engine.GetInstance().CountOfMovements = CountOfMovements;
		}

		#endregion
    }
}
