using System;
using StackGame.Army;
using StackGame.Loggers;
using StackGame.Units.Models;

namespace StackGame.Commands
{
    /// <summary>
    /// Команда улучшения тяжелого юнита
    /// </summary>
    public class ImproveCommand : ICommand
    {
		#region Свойства

		/// <summary>
		/// Единица армии, которая пытается улучшить соседнего юнита
		/// </summary>
		private readonly IUnit lightInfantryUnit;
		/// <summary>
		/// Улучшаемая единица армии
		/// </summary>
		private readonly IUnit targetUnit;
		/// <summary>
		/// Армия, в которой находится улучшаемая единица армии
		/// </summary>
		private readonly IArmy targetArmy;
		/// <summary>
		/// Позиция улучшаемой единицы армии
		/// </summary>
		private readonly int targetUnitPosition;
        /// <summary>
        /// Тип улучшения
        /// </summary>
        private readonly Type typeOfImprovment;

		#endregion

		#region Инициализация

		public ImproveCommand(IUnit lightInfantryUnit, IUnit targetUnit, IArmy targetArmy, int targetUnitPosition, Type typeOfImprovment)
		{
			this.lightInfantryUnit = lightInfantryUnit;
			this.targetUnit = targetUnit;
			this.targetArmy = targetArmy;
			this.targetUnitPosition = targetUnitPosition;
			this.typeOfImprovment = typeOfImprovment;
		}

		#endregion

		#region Методы

		public void Execute(ILogger logger)
		{
            var improvedUnit = (IUnit)Activator.CreateInstance(typeOfImprovment, targetUnit);
			targetArmy.Units[targetUnitPosition] = improvedUnit;

            var message = $"❇️ { lightInfantryUnit.Name } надел { typeOfImprovment.GetGenericTypeDefinition() } на { targetUnit.Name }";
            logger.Log(message);
		}

		public void Undo(ILogger logger)
		{
			targetArmy.Units[targetUnitPosition] = targetUnit;
		}

		#endregion
	}
}
