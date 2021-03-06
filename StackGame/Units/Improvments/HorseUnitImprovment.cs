﻿using System;
using StackGame.Units.Abilities;
using StackGame.Units.Models;
using StackGame.Configs;

namespace StackGame.Units.Improvments
{
    public class HorseUnitImprovment<T>: UnitToBeImproved<T> where T : IUnit, ICanBeImproved, ICanBeCloned
    {
        #region Свойства

        /// <summary>
        /// Здоровье лошади
        /// </summary>
        private int horseDefence;
		/// <summary>
		/// Сила лошади
		/// </summary>
		private int horseAttack;
        /// <summary>
		/// Переопределяем защиту
		/// </summary>
		public override int Defence => base.Defence + horseDefence;
		/// <summary>
		/// Переопределяем атаку
		/// </summary>
		public override int Attack => base.Attack + horseAttack;

		#endregion



        #region Инициализация

        // конструктор коня - улучшения 
        public HorseUnitImprovment(T unit) : base(unit)
        {
            var param = UnitImprovmentParameters.ImprovmentStats[UnitImprovmentTypes.Horse];
            horseAttack = param.Attack;
            horseDefence = param.Defence;
		}

		#endregion

        #region Методы

		// метод, который позволяет клонировать коня
		public override IUnit Clone()
		{
            var clonedUnit = (T)((T)Unit).Clone();
            var improvedClonedUnit = new HorseUnitImprovment<T>(clonedUnit);

			return improvedClonedUnit;
		}

		// метод, позволяющий получать урон по коню
		public override void TakeDamage(int damage)
		{
			if (horseDefence > 0)
			{
				horseDefence -= damage;
				if (horseDefence < 0)
				{
					base.TakeDamage(Math.Abs(horseDefence));
					horseDefence = 0;
				}
			}
			else
			{
				base.TakeDamage(damage);
			}
		}

		public override string ToString()
		{
            return $"{ base.ToString() } |лошадь ♞ c защитой : { horseDefence } и атакой:| {horseAttack}|";
		}

		#endregion
	}
}
