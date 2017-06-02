using System;
using System.Collections.Generic;
using StackGame.Units.Abilities;
using StackGame.Army;
namespace StackGame.Units.Models
{
    public class WizardUnit: Unit, ICanBeHealed, IHaveSpecialAbility
    {
		public int Range { get; } = 3;
		public int Power { get; } = 0;

        #region Инициализация

        public WizardUnit(string name, int health, int attack) : base(name, health, attack)
        { }

		#endregion

		#region Методы

		public void Heal(int healthPower)
		{
			Health += healthPower;
			if (Health > MaxHealth)
			{
				Health = MaxHealth;
			}
		}

		public void DoSpecialAction(IArmy targetArmy, IUnit targetUnit)
		{
			var random = new Random();
            var chance = random.Next(1000 / DateTime.Now.Millisecond);

			if (chance > 0 && targetUnit is ICanBeCloned ICanBeClonedUnit)
			{
				var unit = ICanBeClonedUnit.Clone();
				targetArmy.Units.Add(unit);
			}
		}

		#endregion
	}
}
