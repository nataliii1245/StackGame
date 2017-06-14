using System;
using System.Linq;
using System.Collections.Generic;
using StackGame.Army;
using StackGame.Game;
using StackGame.Units.Abilities;
namespace StackGame.Strategy
{
    public class NVSN: IStrategy
    {
		#region Свойства

		/// <summary>
        /// Количество единиц армии в каждом ряду
		/// </summary>
		private readonly int n;

		#endregion

        #region Инициализаторы

		public NVSN(int n)
		{
			this.n = n;
		}

		#endregion

        #region Методы

		public List<FirstStageOpponents> GetOpponentsQueue(IArmy firstArmy, IArmy secondArmy)
		{
			var firstArmyUnitsCount = firstArmy.Units.Count;
			var secondArmyUnitsCount = secondArmy.Units.Count;

            var numberUnitsInLine = Math.Min(n, Math.Min(firstArmyUnitsCount, secondArmyUnitsCount));

            var opponentsQueue = new List<FirstStageOpponents>();

            for (int i = 0; i < numberUnitsInLine; i++)
            {
                var opponents = new FirstStageOpponents(firstArmy, i, secondArmy, i);

				var pairQueue = new List<FirstStageOpponents>
				{
					opponents,
					opponents.Swap()
				};

                opponentsQueue = opponentsQueue.Concat(pairQueue.OrderBy(item => Randomizer.random.Next())).ToList();
			}
            return opponentsQueue;
        }

		public IEnumerable<int> GetUnitsRangeForSpecialAbility(IArmy allyArmy, IArmy enemyArmy, IHaveSpecialAbility unit, int unitPosition)
		{
            // если напротив юнита кто-то стоит
			if (unitPosition < n )
			{
				return null;
            }
            var targetArmy = unit.isFriendly ? allyArmy : enemyArmy;
			
			int unitX;
			int unitY;
			if (unit.isFriendly)
			{
				unitX = unitPosition % n;
				unitY = unitPosition / n;
			}
			else
			{
                if( unitPosition/3 + 1 > unit.SpecialAbilityRange)
                {
                    return null;
                }
                unitX = Math.Abs(unitPosition % n - (n - 1));
				unitY = -unitPosition / n - 1;
			}

            var indexes = GetIndexesOfAvailiableForSpecialAbilityUnitsArea(targetArmy, unitPosition, unitX, unitY, unit.SpecialAbilityRange);
			if (indexes != null)
			{
				return indexes;
			}
            return null;
        }

		/// <summary>
		/// Получить индексы юнитов , на которых может воздействовать рассматриваемый юнит с особыми навыками
		/// </summary>
		private List<int> GetIndexesOfAvailiableForSpecialAbilityUnitsArea(IArmy army, int unitPosition, int unitX, int unitY, int unitRange)
		{
            var startPosition = unitPosition - unitRange * n;
            if(startPosition < 0)
            {
                startPosition = 0;
            }

            var endPosition = unitPosition + unitRange * n;
            if(endPosition >= army.Units.Count)
            {
                endPosition = army.Units.Count - 1;
            }

            var numOfUnitsInArea = endPosition - startPosition + 1;
            if (numOfUnitsInArea == 0)
            {
                return null;
            }

            var indexes = new List<int>();

            var tmp = Enumerable.Range(startPosition, numOfUnitsInArea);
            foreach( var index in tmp )
            {
				var indexX = index % n;
				var indexY = index / n;

				var isIndexInArea = Math.Pow(indexX - unitX, 2) + Math.Pow(indexY - unitY, 2) <= Math.Pow(unitRange, 2);
				if (isIndexInArea)
				{
					indexes.Add(index);
				}
            }
            return indexes;
		}

		#endregion
	}
}
