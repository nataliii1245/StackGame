using System;
using StackGame.Units.Models;
using System.Collections.Generic;

namespace StackGame.Army.Factory
{
    public interface IArmyFactory 
    {
		#region Методы

		/// <summary>
		/// Создать армию
		/// </summary>
		List<IUnit> CreateArmy(int money);

		#endregion
	}
}
