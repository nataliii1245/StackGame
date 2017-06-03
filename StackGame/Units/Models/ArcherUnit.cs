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

        public void DoSpecialAction(IArmy targetArmy, int unitPosition)
		{
			// Генерируем рандомную вероятность попадания 
			Random random = new Random();
            var chance = random.Next(100)/100;

            // Затем, в зависимости от радиуса поражения, считаем силу, с которой прилетит стрела в колено (ну или не в колено)
            if (chance != 0)
                switch (SpecialAbilityRange) {
                    case 3:
                        chance *= 3;
                        break;
                    case 4:
						chance *= 2;
						break;
                    case 5:
                        chance *= 1;
                        break;
                    default:
                        break;
            }
			if (chance > 1)
				chance = 1;
            targetArmy.Units[unitPosition].TakeDamage((int)SpecialAbilityPower * chance);
		}

		#endregion
	}
}
