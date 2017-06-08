using System;
using System.Linq;
using System.Collections.Generic;
using StackGame;
using StackGame.Army;
using StackGame.Game;
using StackGame.Units.Models;
using StackGame.Units.Abilities;
namespace StackGame.Strategy
{
    public class AllVSAll : IStrategy
    {
		#region Свойства
		#endregion

		#region Инициализаторы
		#endregion

		#region Методы

		public List<FirstStageOpponents> GetOpponentsQueue(IArmy firstArmy, IArmy secondArmy)
		{
            var firstArmyUnitsCount = firstArmy.Units.Count;
            var secondArmyUnitsCount = secondArmy.Units.Count;

            var minUnitsCountForBothArmies = Math.Min(firstArmyUnitsCount, secondArmyUnitsCount);

            var opponentsQueue = new List<FirstStageOpponents>();

            for (int i = 0; i < minUnitsCountForBothArmies; i++)
            {
                var opponents = new FirstStageOpponents(firstArmy.Units[i], secondArmy.Units[i]);

                var pairQueue = new List<FirstStageOpponents>
                {
                    opponents,
                    opponents.Swap()
                };

                Random rnd = new Random();
                opponentsQueue = opponentsQueue.Concat(pairQueue.OrderBy(item => rnd.Next())).ToList();
            }

            return opponentsQueue;
			
		}

		public IEnumerable<int> GetUnitsRangeForSpecialAbility(IArmy allyArmy, IArmy enemyArmy, IHaveSpecialAbility unit, int unitPosition)
		{
            // если напротив юнита кто-то стоит
            if (unitPosition < enemyArmy.Units.Count )
			{
				return null;

			}


			var targetArmy = unit.isFriendly ? allyArmy : enemyArmy;

			Tuple<int, int> usingOfSpecialAbilityArea;

			if (targetArmy == allyArmy)
			{
				usingOfSpecialAbilityArea = GetFirstEndLastIndexesOfAllyArmy(allyArmy, unitPosition, unit.SpecialAbilityRange);
			}
			else
			{
				if (unitPosition - unit.SpecialAbilityRange <= 0)
				{
					return null;
				}

				usingOfSpecialAbilityArea = GetFirstEndLastIndexesOfEnemyArmy(enemyArmy, unitPosition, unit.SpecialAbilityRange);
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
            var startPosition = unitPosition - unitRange;
            var endPosition = startPosition + 2 * unitRange;

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
