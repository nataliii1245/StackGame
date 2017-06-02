using System;
using System.Collections.Generic;
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

        public int Range { get; } = 5;
        public int Power { get; } = 15;

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

		public void DoSpecialAction(IArmy targetArmy, IUnit targetUnit)
		{
			targetUnit.GetDamage(Power);
		}

		#endregion
	}
}
