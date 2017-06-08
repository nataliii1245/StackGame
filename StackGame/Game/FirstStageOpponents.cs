using System;
using System.Linq;
using System.Collections.Generic;
using StackGame;
using StackGame.Army;
using StackGame.Units.Models;
namespace StackGame.Game
{
    public struct FirstStageOpponents
    {
		#region Свойства

		/// <summary>
		/// Единица армии
		/// </summary>
		public IUnit AllyUnit { get; private set; }
		/// <summary>
		/// Единица вражеской армии
		/// </summary>
		public IUnit EnemyUnit { get; private set; }

		#endregion



        #region Инициализация

		public FirstStageOpponents(IUnit allyUnit, IUnit enemyUnit)
		{
            AllyUnit = allyUnit;
			EnemyUnit = enemyUnit;
		}

		#endregion



        #region Методы

		/// <summary>
		/// Поменять единицы армий местами
		/// </summary>
		public FirstStageOpponents Swap()
		{
            var opponents = new FirstStageOpponents(EnemyUnit, AllyUnit);
			return opponents;
		}

		
		#endregion
	}
}
