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

		public void Execute()
		{
            health = (int)(healPower * Randomizer.CalculateChanceOfAction());
			
            if (health > targetUnit.MaxHealth)
			{
                health = targetUnit.MaxHealth;
			}

            targetUnit.Health = health;

            Console.WriteLine($" { clericUnit.Name } восстановил { health } здоровья { targetUnit.Name }!");
		}

		public void Undo()
		{
            targetUnit.Health -= health;

		#endregion
	}
}
}