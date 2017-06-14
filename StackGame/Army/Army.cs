using System.Collections.Generic;
using StackGame.Commands;
using StackGame.Game;
using StackGame.Units.Models;
using StackGame.Army.Factory;

namespace StackGame.Army
{
    /// <summary>
    /// Армия
    /// </summary>
    public class Army: IArmy 
    {
        #region Свойства

		/// <summary>
		/// Список юнитов в армии
		/// </summary>
		public List<IUnit> Units { get; private set; }

		/// <summary>
		/// Все ли единицы армии мертвы?
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
        public Army(string name, IArmyFactory factory, int price)
		{
            Units = factory.CreateArmy(price);
            Name = name;
		}

		#endregion

        #region Методы

        /// <summary>
		/// Реализация функции удаления мертвых юнитов
		/// </summary>
		public int ClearBattleField()
		{
			var listOfDeadUnits = new List<KeyValuePair<int, IUnit>>();
			for (var i = 0; i < Units.Count; i++)
			{
				var unit = Units[i];
                if (unit.IsAlive == false)
				{
					var element = new KeyValuePair<int, IUnit>(i, unit);
                    listOfDeadUnits.Add(element);
				}
			}

            if (listOfDeadUnits.Count > 0)
			{
                var command = new ClearBattleFieldCommand(this, listOfDeadUnits);
                Engine.GetInstance().CommandManager.Execute(command);
			}

            return listOfDeadUnits.Count;
		}

		/// <summary>
		/// Преобразовать армию в строковое представление
		/// </summary>
		public override string ToString()
		{
			string army = $" { Name } :\n";
			foreach (var unit in Units)
			{
				army += unit.ToString() + "\n";
			}

			return army;
		}

		#endregion
	}
}
