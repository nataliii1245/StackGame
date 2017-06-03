using System;
using System.Linq;
using System.Collections.Generic;
using StackGame.Units.Abilities;
using StackGame.Units.Models;
using StackGame.Army;

namespace StackGame.Units.Improvments
{
	/// <summary>
	/// Шлем
	/// </summary>
    public class HelmetUnitImprovment<T> : UnitToBeImproved<T> where T: IUnit, ICanBeImproved, ICanBeCloned
    {
		#region Свойства

		/// <summary>
		/// Защита шлема
		/// </summary>
		private int helmetDefence;

        /// <summary>
        /// Переопределяем защиту
        /// </summary>
        /// <value>The defence.</value>
		public override int Defence => base.Defence + helmetDefence;

		#endregion


		#region Инициализация

		public HelmetUnitImprovment(T unit) : base(unit)
        {
            var param = StartStats.ImprovmentStats[UnitImprovmentTypes.Helmet];
			helmetDefence = param.Defence;
		}

		#endregion

		#region Методы

		public override IUnit Clone()
		{
			var clonedUnit = (T)unit.Clone();
            var improvedClonedUnit = new HelmetUnitImprovment<T>(clonedUnit);

			return improvedClonedUnit;
		}

		public override void TakeDamage(int damage)
		{
			if (helmetDefence > 0)
			{
				helmetDefence -= damage;
				if (helmetDefence < 0)
				{
					base.TakeDamage(Math.Abs(helmetDefence));
					helmetDefence = 0;
				}
			}
			else
			{
				base.TakeDamage(damage);
			}
		}

		public override string ToString()
		{
			return $"{ base.ToString() } | шлем { helmetDefence }|";
		}

		#endregion
	}
}
