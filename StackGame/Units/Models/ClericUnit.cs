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

		public void DoSpecialAction(IArmy targetArmy, int unitPosition)
		{
            if (targetArmy.Units[unitPosition] is ICanBeHealed ICanBeHealedUnit)
			{
				ICanBeHealedUnit.Heal(SpecialAbilityPower);
			}
		}

		#endregion
	}
}
