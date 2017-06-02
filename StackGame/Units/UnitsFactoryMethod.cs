using System;
using System.Linq;
using System.Collections.Generic;
using StackGame.Units.Models;
using StackGame.Units.Creators;
namespace StackGame.Units
{
    /// <summary>
    /// Фабрика юнитов  
    /// </summary>
    public static class UnitsFactoryMethod
    {
        #region Свойства

        /// <summary>
        /// Получить минимальную стоимость единицы армии
        /// </summary>
        public static int MinPrice
        {
            get
            {
                return StartStats.Stats.Select(parameter => parameter.Value.Price).Min();
            }
        }

		/// <summary>
		/// Все типы юнитов 
		/// </summary>
        private static UnitType[] UnitTypes
		{
			get
			{
				return (UnitType[])Enum.GetValues(typeof(UnitType));
			}
		}

		#endregion


		#region Методы

		/// <summary>
        /// Получить типы юнитов, стоимость которых ниже иди равна заданной
        /// </summary>
        public static List<UnitType> GetUnitCheaperOrEqual(int maxCost)
		{
            return StartStats.Stats.Where(p => p.Value.Price <= maxCost).Select( p => p.Key).ToList();
		}


		/// <summary>
		/// Создать единицу армии
		/// </summary>
		public static IUnit CreateUnit(UnitType unitType)
		{
			var creator = GetCreator(unitType);
            return creator.CreateUnit();
		}

		/// <summary>
		/// Получить стоимость конкретного юнита из его типа
		/// </summary>
		public static int GetPrice(UnitType unitType)
		{
            return StartStats.Stats.Where(p => p.Key == unitType).Select(p => p.Value.Price).First();
		}

		/// <summary>
		/// Получить создателя юнита
		/// </summary>
		private static IUnitCreator GetCreator(UnitType unitType)
		{
			switch (unitType)
			{
                case UnitType.LightInfantryUnit:
                    return new LightInfantryUnitCreator();
                case UnitType.HeavyInfantryUnit:
                    return new HeavyInfantryUnitCreator();
				case UnitType.ArcherUnit:
					return new ArcherUnitCreator();
				case UnitType.ClericUnit:
					return new ClericUnitCreator();
                case UnitType.WizardUnit:
                    return new WizardUnitCreator();
				case UnitType.WallUnit:
					return new WallUnitCreator();

				default:
					return null;
			}
		}

		#endregion
	}
}
