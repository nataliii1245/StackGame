using StackGame.Strategy;
using StackGame.Loggers;
using StackGame.Game;

namespace StackGame.Commands
{
    /// <summary>
    /// Метод для смены текущей стратегии
    /// </summary>
    public class ChangeCurrentStrategyCommand : ICommand
    {
		#region Свойства

		/// <summary>
		/// Текущая стратегия
		/// </summary>
        private readonly IStrategy currentStrategy;
		/// <summary>
		/// Новая стратегия
		/// </summary>
        private readonly IStrategy newStrategy;
        private readonly int CurrentNumOfMovementWithoutDeath;

		#endregion

		#region Инициализация

        public ChangeCurrentStrategyCommand(IStrategy currentStrategy, IStrategy newStrategy, int CurrentNumOfMovementWithoutDeath)
		{
            this.currentStrategy = currentStrategy;
            this.newStrategy = newStrategy;
            this.CurrentNumOfMovementWithoutDeath = CurrentNumOfMovementWithoutDeath;
		}

		#endregion

		#region Методы

		public void Execute(ILogger logger)
		{
            Engine.GetInstance().currentStrategy = newStrategy;
            Engine.GetInstance().CurrentNumOfMovementWithoutDeath = 0;
		}

		public void Undo(ILogger logger)
		{
            Engine.GetInstance().currentStrategy = currentStrategy;
            Engine.GetInstance().CurrentNumOfMovementWithoutDeath = CurrentNumOfMovementWithoutDeath;
		}

		#endregion
	}
}
