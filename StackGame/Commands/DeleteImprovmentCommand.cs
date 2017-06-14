using System;
using StackGame.Army;
using StackGame.Loggers;
using StackGame.Units.Models;
using StackGame.Units.Improvments;
using StackGame.Units.Abilities;

namespace StackGame.Commands
{
    /// <summary>
    /// Команда сброса улучшения с тяжелого юнита
    /// </summary>
    public class DeleteImprovmentCommand : ICommand
    {
		#region Свойства

		/// <summary>
		/// Единица армии, с которой необходимо удалить улучшение
		/// </summary>
        private readonly ICanBeImproved targetUnit;
		/// <summary>
		/// Армия, в которой находится единица армии, с которой необходимо удалить улучшение
		/// </summary>
		private readonly IArmy targetArmy;
		/// <summary>
		/// Позиция единицы армии, с которой необходимо удалить улучшение
		/// </summary>
		private readonly int targetUnitPosition;
		
		#endregion

        #region Инициализация

        public DeleteImprovmentCommand(ICanBeImproved targetUnit, IArmy targetArmy, int targetUnitPosition)
		{
			this.targetUnit = targetUnit;
			this.targetArmy = targetArmy;
			this.targetUnitPosition = targetUnitPosition;
		}

		#endregion

        #region Методы

		public void Execute(ILogger logger)
		{
            var internalUnit = ((IUnitToBeImproved)targetUnit).Unit;
            targetArmy.Units[targetUnitPosition] = internalUnit;

            Console.WriteLine($"\ud83d\uddd1 С {internalUnit.Name } упала вещь!");
		}

		public void Undo(ILogger logger)
		{
            targetArmy.Units[targetUnitPosition] = (IUnit)targetUnit;
		}

		#endregion
	}
}
