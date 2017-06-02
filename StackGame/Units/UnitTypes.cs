using System;
namespace StackGame.Units
{
    public class UnitTypes
    {
		/// <summary>
		/// Тип юнита
		/// </summary>
		public enum UnitType
		{
            /// <summary>
            /// Легкий пехотинец
            /// </summary>
            LightInfantryUnit,
            /// <summary>
            /// Тяжелый пехотинец
            /// </summary>
            HeavyInfantryUnit,
			/// <summary>
			/// Лучник
			/// </summary>
			ArcherUnit,
			/// <summary>
			/// Священник
			/// </summary>
			ClericUnit,
			/// <summary>
			/// Маг
			/// </summary>
			WizardUnit,
			/// <summary>
			/// Стена
			/// </summary>
			WallUnit
		}
    }
}
