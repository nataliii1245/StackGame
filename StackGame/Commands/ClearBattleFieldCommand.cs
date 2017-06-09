using System;
using System.Collections.Generic;
using StackGame;
using StackGame.Army;
using StackGame.Strategy;
using StackGame.Configs;
using StackGame.Units.Models;
using StackGame.Units.Abilities;
namespace StackGame.Commands
{
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

		public void Execute()
		{
			foreach (var element in listOfDeadUnits)
			{
                targetArmy.Units.RemoveAt(element.Key);
			}

            Console.WriteLine($"Армия {targetArmy.Name} потеряла {listOfDeadUnits.Count} солдат!");
		}

		public void Undo()
		{
			foreach (var element in listOfDeadUnits)
			{
                targetArmy.Units.Insert(element.Key, element.Value);
			}
		}

		#endregion
	}
}
