using System;
using System.Linq;
using StackGame.Units.Models;
using System.Collections.Generic;
using StackGame.Units.Abilities;
using StackGame.Units.Improvments;
using StackGame.Army;
namespace StackGame.Units.Improvments
{
    public class SpearUnitImprovment<T>: UnitToBeImproved<T> where T : IUnit, ICanBeImproved, ICanBeCloned
    {
		
        #region Свойства

        /// <summary>
        /// Сила копья
        /// </summary>
        private int spearAttack;

        public override int Attack => base.Attack + spearAttack;

		#endregion

		#region Инициализация

		public SpearUnitImprovment(T unit) : base(unit)
        {
            var param = StartStats.ImprovmentStats[UnitImprovmentTypes.Spear];
            spearAttack = param.Attack;
		}

		#endregion

		#region Методы

		public override IUnit Clone()
		{
			var clonedUnit = (T)unit.Clone();
            var improvedClonedUnit = new SpearUnitImprovment<T>(clonedUnit);

			return improvedClonedUnit;
		}

		public override string ToString()
		{
            return $"{ base.ToString() } |копье ⇐ с атакой: {spearAttack} вручено рыцарю!|";
		}

		#endregion
	}
}
