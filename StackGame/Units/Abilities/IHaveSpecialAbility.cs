using System;
using System.Collections.Generic;
using StackGame.Army;
using StackGame.Units.Models;
namespace StackGame.Units.Abilities
{
    public interface IHaveSpecialAbility
    {
		#region Свойства

		/// <summary>
		/// Радиус поражения(измеряется в юнитах)
		/// </summary>
		int SpecialAbilityRange { get; }
		/// <summary>
		/// Сила навыка
		/// </summary>
		int SpecialAbilityPower { get; }
        /// <summary>
        /// Является ли данное действие мирным?
        /// </summary>
        bool isFriendly { get; }

		#endregion

		#region Методы

		/// <summary>
		/// Применить специальный навык
		/// </summary>
		void DoSpecialAction(IArmy targetArmy, IEnumerable<int> targetRange, int position);

		#endregion
	}
}
