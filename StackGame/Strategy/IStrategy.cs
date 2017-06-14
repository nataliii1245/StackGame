using System.Collections.Generic;
using StackGame.Game;
using StackGame.Army;
using StackGame.Units.Abilities;
namespace StackGame.Strategy
{
    public interface IStrategy
    {
		#region Методы

		/// <summary>
		/// Получить противников
		/// </summary>
        List<FirstStageOpponents> GetOpponentsQueue(IArmy firstArmy, IArmy secondArmy);

		/// <summary>
		/// Получить индексы юнитов, попадаюших под воздействие специальных возможностей
		/// </summary>
        IEnumerable<int> GetUnitsRangeForSpecialAbility(IArmy allyArmy, IArmy enemyArmy, IHaveSpecialAbility unit, int unitPosition);

		#endregion
	}
}
