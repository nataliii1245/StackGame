using System;
using System.Collections.Generic;
using System.Linq;
using StackGame.Units.Abilities;
using StackGame.Army;

namespace StackGame.Units.Models
{
    /// <summary>
    /// Лучник
    /// </summary>
    public class ArcherUnit: Unit, ICanBeHealed, ICanBeCloned, IHaveSpecialAbility
    {
        #region Свойства

        public int SpecialAbilityRange { get; } = StartStats.Stats.Where( p => p.Key == UnitType.ArcherUnit).First().Value.SpecialAbilityRange;
        public int SpecialAbilityPower { get; } = StartStats.Stats.Where(p => p.Key == UnitType.ArcherUnit).First().Value.SpecialAbilityPower;

		#endregion

		#region Инициализация

		public ArcherUnit(string name, int health, int attack) : base(name, health, attack)
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

        // ПЕРЕПИСАТЬ
        public void DoSpecialAction(IArmy targetArmy, IEnumerable<int> targetRange, int position)
		{
			// Генерируем рандомную вероятность попадания 
			Random random = new Random();
            var chance = random.Next(100)/100;

            // 
            if (chance != 0)

            {
				var targetUnits = new List<IUnit>();
				foreach (var index in targetRange)
				{
					var unit = targetArmy.Units[index];
					if (unit.isAlive)
					{
						targetUnits.Add(unit);
					}
				}

				if (targetUnits.Count == 0)
				{
					return;
				}

				var targetUnit = targetUnits[random.Next(targetUnits.Count)];
                targetUnit.TakeDamage(SpecialAbilityPower);

                Console.WriteLine($"{ToString()} нанес {SpecialAbilityPower} {targetUnit.ToString()}");
            }
		}

		#endregion
	}
}
