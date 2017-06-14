using System;
using System.Linq;
using System.Collections.Generic;
using StackGame.Army;
using StackGame.Game;
using StackGame.Units.Abilities;
namespace StackGame.Strategy
{
    /// <summary>
    /// Стратегия 1:1 ( по очереди, юниты бьются 1:1)
    /// </summary>
    public class OneVSOne : IStrategy
    {
        
        #region Методы

        public List<FirstStageOpponents> GetOpponentsQueue(IArmy firstArmy, IArmy secondArmy) 
        {
            var opponents = new FirstStageOpponents(firstArmy, 0, secondArmy, 0);

            var opponentsQueue = new List<FirstStageOpponents>
            {
                opponents,
                opponents.Swap()
            };
            return opponentsQueue.OrderBy(item => Randomizer.random.Next()).ToList(); 
		}

        public IEnumerable<int> GetUnitsRangeForSpecialAbility(IArmy allyArmy, IArmy enemyArmy, IHaveSpecialAbility unit, int unitPosition)
        {
            if (unitPosition == 0)
            {
                return null;
            }

            var targetArmy = unit.isFriendly ? allyArmy : enemyArmy;

            Tuple<int, int> usingOfSpecialAbilityArea;

            if (unit.isFriendly)
            {
                usingOfSpecialAbilityArea = GetFirstEndLastIndexesOfAllyArmy(targetArmy, unitPosition, unit.SpecialAbilityRange);
            }
            else 
            {
                if (unitPosition - unit.SpecialAbilityRange >= 0)
                {
                    return null;
                }
                usingOfSpecialAbilityArea = GetFirstEndLastIndexesOfEnemyArmy(targetArmy, unitPosition, unit.SpecialAbilityRange);
            }

            var startIndex = usingOfSpecialAbilityArea.Item1;
            var endIndex = usingOfSpecialAbilityArea.Item2;

			var count = endIndex - startIndex + 1;

			if (count == 0)
			{
				return null;
			}

			var range = Enumerable.Range(startIndex, count);
			return range;
        }

        /// <summary>
        /// Получить начальный и конечный индексы юнитов во вражеской армии, на которых может воздействовать рассматриваемый юнит с 
        /// особыми навыками
        /// </summary>
        private Tuple<int, int> GetFirstEndLastIndexesOfEnemyArmy(IArmy army, int unitPosition, int unitRange) 
        {
            var startPosition = 0;
            var endPosition = Math.Abs(unitPosition - unitRange) - 1;

            if (endPosition >= army.Units.Count)
            {
                endPosition = army.Units.Count - 1;
            }
            return new Tuple<int, int>(startPosition, endPosition);
        }

		/// <summary>
		/// Получить начальный и конечный индексы юнитов армии союзников, на которых может воздействовать рассматриваемый юнит с 
		/// особыми навыками
		/// </summary>
		private Tuple<int, int> GetFirstEndLastIndexesOfAllyArmy(IArmy army, int unitPosition, int unitRange)
        {
            var startPosition = unitPosition - unitRange;

            if (startPosition < 0)
            {
                startPosition = 0;  
            }

            var endPosition = unitPosition + unitRange;

			if (endPosition >= army.Units.Count)
			{
				endPosition = army.Units.Count - 1;
			}
            return new Tuple<int, int>(startPosition, endPosition);
		}

        #endregion

    }
}
