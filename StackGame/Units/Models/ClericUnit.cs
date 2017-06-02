using System;
using System.Collections.Generic;
using StackGame.Units.Abilities;
using StackGame.Army;
namespace StackGame.Units.Models
{
    public class ClericUnit: Unit, ICanBeCloned, ICanBeHealed, IHaveSpecialAbility
    {
		#region Свойства

		public int Range { get; } = 3;
		public int Power { get; } = 10;

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

		public void DoSpecialAction(IArmy targetArmy, IUnit targetUnit)
		{
			if (targetUnit is ICanBeHealed ICanBeHealedUnit)
			{
				ICanBeHealedUnit.Heal(Power);
			}
		}

		#endregion
	}
}
