using StackGame.Game;
using StackGame.Loggers;

namespace StackGame.Commands
{
    /// <summary>
    /// Команда для изменения количества ходов без смертей
    /// </summary>
    public class ChangeCurrentNumOfMovementWithoutDeathCommand : ICommand
    {
		#region Свойства

		/// <summary>
		/// Исходное количество шагов без смертей
		/// </summary>
		private readonly int currentNumOfMovementWithoutDeath;
		/// <summary>
		/// Количество шагов без смертей
		/// </summary>
		private readonly int newNumOfMovementWithoutDeath;

		#endregion

        #region Инициализация

		public ChangeCurrentNumOfMovementWithoutDeathCommand(int currentNumOfMovementWithoutDeath, int newNumOfMovementWithoutDeath)
		{
            this.currentNumOfMovementWithoutDeath = currentNumOfMovementWithoutDeath;
            this.newNumOfMovementWithoutDeath = newNumOfMovementWithoutDeath;
		}

		#endregion

        #region Методы

		public void Execute(ILogger logger)
		{
            Engine.GetInstance().CurrentNumOfMovementWithoutDeath = newNumOfMovementWithoutDeath;
		}

		public void Undo(ILogger logger)
		{
            Engine.GetInstance().CurrentNumOfMovementWithoutDeath = currentNumOfMovementWithoutDeath;
		}

		#endregion
	}
}
