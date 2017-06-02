using System;
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
		int Range { get; }
		/// <summary>
		/// Сила навыка
		/// </summary>
		int Power { get; }

		#endregion

		#region Методы

		/// <summary>
		/// Применить специальный навык
		/// </summary>
		void DoSpecialAction(IArmy targetArmy, IUnit targetUnit);

		#endregion
	}
}
