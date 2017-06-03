using System;
using System.Linq;
using System.Collections.Generic;
using StackGame.Units.Abilities;
using StackGame.Army;
namespace StackGame.Units.Models
{
    public class WizardUnit: Unit, ICanBeHealed, IHaveSpecialAbility
    {
        public int SpecialAbilityRange { get; } = StartStats.Stats.Where(p => p.Key == UnitType.WizardUnit).First().Value.SpecialAbilityRange;
        public int SpecialAbilityPower { get; } = StartStats.Stats.Where(p => p.Key == UnitType.WizardUnit).First().Value.SpecialAbilityPower;

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

        // ПЕРЕПИСАТЬ
		public void DoSpecialAction(IArmy targetArmy, IEnumerable<int> targetRange, int position)
		{
			var random = new Random();
			var chance = random.Next(100)/100 == 0;

			if (chance)
			{
                var targetUnits = new List<ICanBeCloned>();
				foreach (var index in targetRange)
				{
					if (index == position)
					{
						continue;
					}

					var unit = targetArmy.Units[index];
					if (unit.isAlive && unit is ICanBeCloned ICanBeClonedUnit)
					{
						targetUnits.Add(ICanBeClonedUnit);
					}
				}

				if (targetUnits.Count == 0)
				{
					return;
				}

				var targetUnit = targetUnits[random.Next(targetUnits.Count)];
				var clonedUnit = targetUnit.Clone();
				targetArmy.Units.Add(clonedUnit);

				Console.WriteLine($"{ToString()} клонировал {targetUnit.ToString()}");
			}
		}

		#endregion
	}
}
