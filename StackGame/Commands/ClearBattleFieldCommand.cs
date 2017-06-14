using System.Collections.Generic;
using System.Linq;
using StackGame.Army;
using StackGame.Loggers;
using StackGame.Units.Models;

namespace StackGame.Commands
{
    /// <summary>
    /// Команда для удаления мертвых единиц армии
    /// </summary>
    public class ClearBattleFieldCommand: ICommand
    {
		#region Свойства

		/// <summary>
		/// Армия, из которой требуется удалить мертвых воинов 
		/// </summary>
		private readonly IArmy targetArmy;

        /// <summary>
        /// Список мертвых юнитов
        /// </summary>
        private readonly List<KeyValuePair<int, IUnit>> listOfDeadUnits;

		#endregion

        #region Инициализация

		public ClearBattleFieldCommand(IArmy targetArmy, List<KeyValuePair<int, IUnit>> listOfDeadUnits)
		{
			this.targetArmy = targetArmy;
			this.listOfDeadUnits = listOfDeadUnits;
		}

		#endregion

        #region Методы

        public void Execute(ILogger logger)
		{
            var _listOfDeadUnits = listOfDeadUnits.Select(unit => unit).ToList();
            // удаляем с конца во избежание смены индексов
            _listOfDeadUnits.Reverse();

			foreach (var element in _listOfDeadUnits)
			{
                targetArmy.Units.RemoveAt(element.Key);
			}

            var message = $"✉️ В {targetArmy.Name} потери: {listOfDeadUnits.Count} !";
            logger.Log(message);
		}

		public void Undo(ILogger logger)
		{
			foreach (var element in listOfDeadUnits)
			{
                targetArmy.Units.Insert(element.Key, element.Value);
			}
		}

		#endregion
	}
}
