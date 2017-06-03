using System;
namespace StackGame.Units.Abilities
{
    public interface ICanBeImproved
    {
		#region Методы

		/// <summary>
		/// Можно ли улучшить
		/// </summary>
		bool CanIBeImprovedWithFeatureOfThisType(Type type);

		#endregion
	}
}
