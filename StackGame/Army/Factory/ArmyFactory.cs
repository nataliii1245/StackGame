using System;
using System.Linq;
using StackGame.Units.Models;
using StackGame.Units.Creators;
using System.Collections.Generic;

namespace StackGame.Army.Factory
{
    public class ArmyFactory : IArmyFactory
    {
		#region Свойства

		/// <summary>
		/// Получить минимальную стоимость юнита
		/// </summary>
		public static int MinPrice
		{
			get
			{
				return StartStats.Stats.Select(parameter => parameter.Value.Price).Min();
			}
		}
		#endregion



		#region Методы

		/// <summary>
		/// Создать армию
		/// </summary>
		public List<IUnit> CreateArmy(int money)
		{
			var random = new Random();

			/// <summary>
			/// Получаем минимальную стоимость юнита
			/// </summary>
			var minUnitPrice = MinPrice;

			var units = new List<IUnit>();
			while (money >= minUnitPrice)
			{
				var availableTypes = GetUnitCheaperOrEqual(money);
				var index = random.Next(availableTypes.Count);

				var unitType = availableTypes[index];

				var unit = CreateUnit(unitType);
				units.Add(unit);

				money -= GetPrice(unitType);
			}

			return units;
		}

		/// <summary>
		/// Получить типы юнитов, стоимость которых ниже или равна заданной
		/// </summary>
		public static List<UnitType> GetUnitCheaperOrEqual(int maxCost)
		{
			return StartStats.Stats.Where(p => p.Value.Price <= maxCost).Select(p => p.Key).ToList();
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
