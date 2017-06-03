using System;
using System.Linq;
using StackGame.Units.Models;
using System.Collections.Generic;
using StackGame.Units.Abilities;
using StackGame.Units.Improvments;
using StackGame.Army;
namespace StackGame.Units.Improvments
{
    public class ShieldUnitImprovment<T>: UnitToBeImproved<T> where T : IUnit, ICanBeImproved, ICanBeCloned
    {
		#region Свойства

		/// <summary>
		/// Защита щита
		/// </summary>
		private int shieldDefence;

		public override int Defence => base.Defence + shieldDefence;

		#endregion

		#region Инициализация

		public ShieldUnitImprovment(T unit) : base(unit)
        {
            var param = StartStats.ImprovmentStats[UnitImprovmentTypes.Shield];
			shieldDefence = param.Defence;
		}

		#endregion

		#region Методы

		public override IUnit Clone()
		{
			var clonedUnit = (T)unit.Clone();
			var improvedClonedUnit = new ShieldUnitImprovment<T>(clonedUnit);

			return improvedClonedUnit;
		}

		public override void TakeDamage(int damage)
		{
			if (shieldDefence > 0)
			{
				shieldDefence -= damage;
				if (shieldDefence < 0)
				{
					base.TakeDamage(Math.Abs(shieldDefence));
					shieldDefence = 0;
				}
			}
			else
			{
				base.TakeDamage(damage);
			}
		}

		public override string ToString()
		{
			return $"{ base.ToString() } |щит { shieldDefence }|";
		}

		#endregion
	}
}
