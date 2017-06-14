using System.Collections.Generic;
using StackGame.Units.Models;

namespace StackGame.Army
{
    public interface IArmy
    {
		#region Свойства

		/// <summary>
		/// Список юнитов в армии
		/// </summary>
		List<IUnit> Units { get; }

		/// <summary>
		/// Все юниты в армии убиты
		/// </summary>
		bool IsAllDead { get; }

        /// <summary>
        /// Название армии
        /// </summary>
        string Name { get; }

		#endregion

        #region Методы

        /// <summary>
        /// Метод, отвечающий за удаление убитых юнитов
        /// </summary>
        int ClearBattleField();

		/// <summary>
		/// Преобразовать армию в строковое представление
		/// </summary>
		string ToString();

		#endregion
	}
}
