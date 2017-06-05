using System;
using System.Linq;
using System.Collections.Generic;
using StackGame;
using StackGame.Army;
using StackGame.Strategy;
using StackGame.Units.Models;
using StackGame.Units.Abilities;
namespace StackGame.Game
{
	/// <summary>
	/// Элементы задействованные специальными возможностями
	/// </summary>
	public struct SpecialAbilityContainer
    {
		#region Свойства

		/// <summary>
		/// Единица армии применяющая специальные возможности
		/// </summary>
		public IHaveSpecialAbility UnitWithSpecialAbility { get; private set; }
        /// <summary>
        /// Армия, попадающая под воздействие специальных возможностей
        /// </summary>
        public IArmy AffectedByUnitWithSpecialAbilityArmy { get; private set; }
        /// <summary>
        /// Индексы единиц армии, попадающих под область действия специальных возможностей
        /// </summary>
        public IEnumerable<int> RangeOfUnitsAffectedByUnitWithSpecialAbility { get; private set; }
        /// <summary>
        /// Позиция единицы армии, применяющей специальные возможности
        /// </summary>
        public int PositionOfUnitWithSpecialAbility { get; private set; }

		#endregion

		#region Инициализация

		public SpecialAbilityContainer(IHaveSpecialAbility unitWithSpecialAbility, IArmy affectedByUnitWithSpecialAbilityArmyy, 
                                       IEnumerable<int> rangeOfUnitsAffectedByUnitWithSpecialAbility, int positionOfUnitWithSpecialAbility)
		{
            UnitWithSpecialAbility = unitWithSpecialAbility;
            AffectedByUnitWithSpecialAbilityArmy = affectedByUnitWithSpecialAbilityArmyy;
            RangeOfUnitsAffectedByUnitWithSpecialAbility = rangeOfUnitsAffectedByUnitWithSpecialAbility;
            PositionOfUnitWithSpecialAbility = positionOfUnitWithSpecialAbility;
		}

		#endregion
	}
}
