using System;
namespace StackGame.Units.Abilities
{
    public interface ICanBeHealed
    {
		#region Методы

		/// <summary>
		/// Вылечить
		/// </summary>
		void Heal(int healthPower);

		#endregion
	}
}
