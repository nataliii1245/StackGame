using System.Linq;
using System.Collections.Generic;
using StackGame.Configs;
using StackGame.Units.Models;
using StackGame.Units.Creators;

namespace StackGame.Army.Factory
{
    /// <summary>
    /// Фабричный метод для генерации армии из рандомных юнитов
    /// </summary>
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
                return UnitParameters.Stats.Select(parameter => parameter.Value.Price).Min();
			}
		}
		#endregion

        #region Методы
        /// <summary>
		/// Создать армию
		/// </summary>
		public List<IUnit> CreateArmy(int money)
		{
			// Получаем минимальную стоимость юнита
			var minUnitPrice = MinPrice;
            // список юнитов в армии
			var units = new List<IUnit>();

			while (money >= minUnitPrice)
			{
                // получили список всех типов юнитов, которые мы можем купить
				var availableTypes = GetUnitCheaperOrEqual(money);
                var index = Randomizer.random.Next(availableTypes.Count);
                // получили рандомный тип юнита из списка доступных для покупки
				var unitType = availableTypes[index];
                // создали юнита
				var unit = CreateUnit(unitType);

				// добавили его в список юнитов армии
                units.Add(unit);
                // вычли стоимость
				money -= GetPrice(unitType);
			}

            return units;
        }

		/// <summary>
		/// Получить типы юнитов, стоимость которых ниже или равна заданной
		/// </summary>
		private List<UnitTypes> GetUnitCheaperOrEqual(int maxCost)
		{
            return UnitParameters.Stats.Where(p => p.Value.Price <= maxCost).Select(p => p.Key).ToList();
		}

        /// <summary>
		/// Создать единицу армии
		/// </summary>
		private IUnit CreateUnit(UnitTypes unitType)
		{
            // получили конкретного создателя
			var creator = GetCreator(unitType);
            return creator.CreateUnit();
		}

		/// <summary>
		/// Получить стоимость конкретного юнита из его типа
		/// </summary>
		private int GetPrice(UnitTypes unitType)
		{
            return UnitParameters.Stats.Where(p => p.Key == unitType).Select(p => p.Value.Price).First();
		}

		/// <summary>
		/// Получить создателя юнита
		/// </summary>
		private IUnitCreator GetCreator(UnitTypes unitType)
		{
			switch (unitType)
			{
				case UnitTypes.LightInfantryUnit:
					return new LightInfantryUnitCreator();
				case UnitTypes.HeavyInfantryUnit:
					return new HeavyInfantryUnitCreator();
				case UnitTypes.ArcherUnit:
					return new ArcherUnitCreator();
				case UnitTypes.ClericUnit:
					return new ClericUnitCreator();
				case UnitTypes.WizardUnit:
					return new WizardUnitCreator();
				case UnitTypes.WallUnit:
					return new WallUnitCreator();
                default:
					return null;
			}
		}

		#endregion
	}
}
