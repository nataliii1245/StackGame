using StackGame.Loggers;
using StackGame.Units.Models;

namespace StackGame.Commands
{
    /// <summary>
    /// Команда для лечения юнита
    /// </summary>
    public class HealCommand: ICommand
    {
		#region Свойства

		/// <summary>
		/// Единица армии,которая лечит
		/// </summary>
		private readonly IUnit clericUnit;
		/// <summary>
		/// Единица армии, которую лечат
		/// </summary>
		private IUnit targetUnit;
		/// <summary>
		/// Максимальный уровень здоровья,который может восстановить клирик
		/// </summary>
		private readonly int healPower;
        /// <summary>
		/// Уровень здоровья
		/// </summary>
		private int health;

		#endregion

        #region Инициализация

		public HealCommand(IUnit clericUnit, IUnit targetUnit, int healPower)
		{
            this.clericUnit = clericUnit;
            this.targetUnit = targetUnit;
            this.healPower = healPower;
		}

		#endregion

        #region Методы

		public void Execute(ILogger logger)
		{
            health = healPower;
			
            if (targetUnit.Health + health > targetUnit.MaxHealth)
			{
                health = targetUnit.MaxHealth - targetUnit.Health;
			}

            targetUnit.Health += health;

            var message = $"\ud83d\udc8a { clericUnit.Name } восстановил { health } здоровья { targetUnit.Name }!";
            logger.Log(message);
		}

		public void Undo(ILogger logger)
		{
            targetUnit.Health -= health;
    	}
		#endregion
	}
}