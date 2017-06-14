using StackGame.Units.Models;
namespace StackGame.Units.Abilities
{
    public interface ICanBeCloned
    {
		#region Методы

		/// <summary>
		/// Клонировать
		/// </summary>
		IUnit Clone();

		#endregion
	}
}
