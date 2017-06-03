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

		public bool CanIBeImprovedWithFeatureOfThisType(Type type)
		{
			return true;
		}

		/// <summary>
		///Переопределена функция для получения урона с учетом брони
		/// </summary>
		public override void TakeDamage(int damage)
		{
			if (Defence > 0)
			{
				Defence -= damage;
				if (Defence < 0)
				{
					base.TakeDamage(Math.Abs(Defence));
					Defence = 0;
				}
			}
			else
			{
				base.TakeDamage(damage);
			}
		}

		public IUnit Clone()
		{
			return (IUnit)MemberwiseClone();
		}
		#endregion
	}
}
