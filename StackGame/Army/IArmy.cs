using System;
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
		/// Армия уничтожена
		/// </summary>
		bool IsAllDead { get; }

        /// <summary>
        /// Имя армии
        /// </summary>
        string Name { get; }

		#endregion

		#region Методы

		/// <summary>
		/// Преобразовать в строку
		/// </summary>
		string ToString();

        void ClearBattleField();

		#endregion
	}
}
