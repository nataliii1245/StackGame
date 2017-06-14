using System.Collections.Generic;
using StackGame.Units.Models;

namespace StackGame.Army.Factory
{
    /// <summary>
    /// Интерфейс фабричного метода для генерации армии рандомных юнитов
    /// </summary>
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
