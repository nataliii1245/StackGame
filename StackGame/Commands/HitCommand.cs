using System;
using System.Linq;
using System.Collections.Generic;
using StackGame;
using StackGame.Army;
using StackGame.Strategy;
using StackGame.Configs;
using StackGame.Units.Models;
using StackGame.Units.Abilities;
namespace StackGame.Commands
{
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

		public void Execute()
		{
            damage = unitDamage * (100 - (int)(enemy.Defence * Randomizer.CalculateChanceOfAction()))/100 ;

			if (damage > enemy.Health)
			{
				damage = enemy.Health;
			}

			enemy.TakeDamage(damage);

			Console.WriteLine($" { unit.Name } нанес { damage } урона { enemy.Name }!");
		}

        public void Undo()
		{
            enemy.Health += damage;
		}

		#endregion
	}
}
