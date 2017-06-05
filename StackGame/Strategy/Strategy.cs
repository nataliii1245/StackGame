using System;
using System.Collections.Generic;
using StackGame.Game;
using StackGame.Army;
using StackGame.Units.Abilities;
using StackGame.Units.Models;
namespace StackGame.Strategy
{
    public abstract class Strategy: IStrategy
    {
        public abstract List<FirstStageOpponents> GetOpponentsQueue(IArmy firstArmy, IArmy secondArmy);

		public abstract IEnumerable<int> GetUnitsRangeForSpecialAbility(IArmy allyArmy, IArmy enemyArmy, IHaveSpecialAbility unit, int unitPosition);

        /// <summary>
        /// Получить начальный и конечный индексы во вражеской армии
        /// </summary>
        protected abstract Tuple<int, int> GetFirstEndLastIndexesOfEmemyArmy(IArmy army, int unitPosition, int unitRange);

		/// <summary>
		/// Получить начальный и конечный индексы в армии союзников
		/// </summary>
		protected abstract Tuple<int, int> GetFirstEndLastIndexesOfAllyArmy(IArmy army, int unitPosition, int unitRange);
    }
}
