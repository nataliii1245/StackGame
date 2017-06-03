using System;
using System.Linq;
using System.Collections.Generic;
using StackGame.Units.Abilities;
using StackGame.Army;
namespace StackGame.Units.Models
{
    public class ClericUnit: Unit, ICanBeCloned, ICanBeHealed, IHaveSpecialAbility
    {
		#region Свойства

        public int SpecialAbilityRange { get; } = StartStats.Stats.Where(p => p.Key == UnitType.ClericUnit).First().Value.SpecialAbilityRange;
		public int SpecialAbilityPower { get; } = StartStats.Stats.Where(p => p.Key == UnitType.ClericUnit).First().Value.SpecialAbilityPower;

		#endregion

		#region Инициализация

		public ClericUnit(string name, int health, int attack) : base(name, health, attack)
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

		public IUnit Clone()
		{
			return (IUnit)MemberwiseClone();
		}

        // ПЕРЕПИСАТЬ!!!
		public void DoSpecialAction(IArmy targetArmy, IEnumerable<int> targetRange, int position)
		{
			var random = new Random();
			var chance = random.Next(100)/100 == 0;

			if (chance)
			{
                var targetUnits = new List<ICanBeHealed>();
				foreach (var index in targetRange)
				{
					var unit = targetArmy.Units[index];
					if (unit.isAlive  && unit is ICanBeHealed ICanBeHealedUnit)
					{
						targetUnits.Add(ICanBeHealedUnit);
					}
				}

				if (targetUnits.Count == 0)
				{
					return;
				}

				var targetUnit = targetUnits[random.Next(targetUnits.Count)];
                targetUnit.Heal(SpecialAbilityPower);

                Console.WriteLine($"{ToString()} вылечил на {SpecialAbilityPower} {targetUnit.ToString()}");
			}
		}

		#endregion
	}
}
