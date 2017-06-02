using System;
using System.Collections.Generic;
using StackGame.Units.Abilities;
using StackGame.Army;
namespace StackGame.Units.Models
{
    public class HeavyInfantryUnit: Unit, ICanBeCloned, ICanBeImproved
    {
		#region Инициализация

        public HeavyInfantryUnit(string name, int health, int attack) : base(name, health, attack)
        { }

		#endregion

		#region Методы

		/// <summary>
		///Переопределена функция для получения урона с учетом брони
		/// </summary>
		public override void GetDamage(int damage)
		{
			if (Defence > 0)
			{
				Defence -= damage;
				if (Defence < 0)
				{
					base.GetDamage(Math.Abs(Defence));
					Defence = 0;
				}
			}
			else
			{
				base.GetDamage(damage);
			}
		}

		public IUnit Clone()
		{
			return (IUnit)MemberwiseClone();
		}
		#endregion
	}
}
