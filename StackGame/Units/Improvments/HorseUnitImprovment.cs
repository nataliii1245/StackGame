using System;
using System.Linq;
using System.Collections.Generic;
using StackGame.Units.Abilities;
using StackGame.Units.Models;
using StackGame.Army;

namespace StackGame.Units.Improvments
{
    public class HorseUnitImprovment<T>: UnitToBeImproved<T> where T : IUnit, ICanBeImproved, ICanBeCloned
    {
		#region Свойства

		/// <summary>
		/// Здоровье лошади
		/// </summary>
		private int horseHealth;
		/// <summary>
		/// Сила лошади
		/// </summary>
		private int horseAttack;

		public override int Defence => base.Defence + horseHealth;

        public override int Attack => base.Attack + horseAttack;

		#endregion


		#region Инициализация

        public HorseUnitImprovment(T unit) : base(unit)
        {
            var param = StartStats.ImprovmentStats[UnitImprovmentTypes.Horse];
            horseAttack = param.Attack;
            horseHealth = param.Health;
		}

		#endregion

		#region Методы

		public override IUnit Clone()
		{
			var clonedUnit = (T)unit.Clone();
            var improvedClonedUnit = new HorseUnitImprovment<T>(clonedUnit);

			return improvedClonedUnit;
		}

		public override void TakeDamage(int damage)
		{
			if (horseHealth > 0)
			{
				horseHealth -= damage;
				if (horseHealth < 0)
				{
					base.TakeDamage(Math.Abs(horseHealth));
					horseHealth = 0;
				}
			}
			else
			{
				base.TakeDamage(damage);
			}
		}

		public override string ToString()
		{
			return $"{ base.ToString() } |лошадь { horseHealth }|";
		}

		#endregion
	}
}
