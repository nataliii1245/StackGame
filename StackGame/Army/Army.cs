using System;
using System.Collections.Generic;
using StackGame.Commands;
using StackGame.Game;
using StackGame.Units;
using StackGame.Units.Models;
namespace StackGame.Army
{
    public class Army: IArmy 
    {
        #region Свойства

		/// <summary>
		/// Единицы армии
		/// </summary>
		public List<IUnit> Units { get; private set; }

		/// <summary>
		/// Все ли единицы армии мертвы
		/// </summary>
		public bool IsAllDead
		{
			get
			{
				return Units.Count == 0;
			}
		}

        public string Name { get; private set; }

		#endregion

		#region Инициализация

        /// <summary>
        /// Конструктор
        /// </summary>
		public Army(string name)
		{
            Units = CreateArmy(StartStats.TotalPriceOfArmy);
            Name = name;
		}

		#endregion

		#region Методы

		/// <summary>
		/// Создать армию
		/// </summary>
		protected List<IUnit> CreateArmy(int money)
		{
            var random = new Random();

			/// <summary>
			/// Получаем минимальную стоимость юнита
			/// </summary>
			var minUnitPrice = UnitsFactoryMethod.MinPrice;

			var units = new List<IUnit>();
            while (money >= minUnitPrice)
			{
                var availableTypes = UnitsFactoryMethod.GetUnitCheaperOrEqual(money);
                var index = random.Next(availableTypes.Count);

				var unitType = availableTypes[index];

                var unit = UnitsFactoryMethod.CreateUnit(unitType);
				units.Add(unit);

                money -= UnitsFactoryMethod.GetPrice(unitType);
			}

			return units;
		}
		/// <summary>
		/// Удалить мертвых юнитов
		/// </summary>
		public void ClearBattleField()
		{
			var listOfDeadUnits = new List<KeyValuePair<int, IUnit>>();
			for (var i = 0; i < Units.Count; i++)
			{
				var unit = Units[i];
                if (unit.isAlive == false)
				{
					var element = new KeyValuePair<int, IUnit>(i, unit);
                    listOfDeadUnits.Add(element);
				}
			}

            if (listOfDeadUnits.Count > 0)
			{
                var command = new ClearBattleFieldCommand(this, listOfDeadUnits);
                Engine.GetEngine().CommandManager.Execute(command);
			}
		}

		/// <summary>
		/// Преобразовать в строку
		/// </summary>
		public override string ToString()
		{
			string army = $"Армия: { Name } :\n";
			foreach (var unit in Units)
			{
				army += unit.ToString() + "\n";
			}

			return army;
		}

		#endregion
	}
}
