using System;
using StackGame.Loggers;
using StackGame.Units.Models;

namespace StackGame.Commands
{
    /// <summary>
    /// Команда для получения урона
    /// </summary>
    public class HitCommand : ICommand
    {
		#region Свойства

		/// <summary>
		/// Единица армии, наносящая урон
		/// </summary>
		private readonly IUnit unit;
		/// <summary>
		/// Вражеская единица армии
		/// </summary>
		private  IUnit enemy;
		/// <summary>
		/// Возможный урон
		/// </summary>
		private readonly int unitDamage;
        /// <summary>
		/// Наносимый урон
		/// </summary>
		private int damage;

		#endregion

        #region Инициализация

		public HitCommand(IUnit unit, IUnit enemy, int unitDamage)
		{
			this.unit = unit;
			this.enemy = enemy;
			this.unitDamage = unitDamage;
        }

		#endregion

        #region Методы

		public void Execute(ILogger logger)
		{
            damage = unitDamage * (100 - (enemy.Defence))/100 ;

			if (damage > enemy.Health)
			{
				damage = enemy.Health;
			}

			enemy.TakeDamage(damage);

			Console.WriteLine($"\ud83d\udde1 { unit.Name } нанес { damage } урона { enemy.Name }!");

            if (enemy.IsAlive == false) 
            {
                var message =  $"☠️ {enemy.Name } был убит {unit.Name}! ️";
                logger.Log(message);
            }
		}

        public void Undo(ILogger logger)
		{
            enemy.Health += damage;
		}

		#endregion
	}
}
