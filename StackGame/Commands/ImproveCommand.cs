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

		public void Execute()
		{
            var improvedUnit = (IUnit)Activator.CreateInstance(typeOfImprovment);
			targetArmy.Units[targetUnitPosition] = improvedUnit;

            Console.WriteLine($" { lightInfantryUnit.Name } надел { typeOfImprovment.GetGenericTypeDefinition() } на { targetUnit.Name }");
		}

		public void Undo()
		{
			targetArmy.Units[targetUnitPosition] = targetUnit;
		}

		#endregion
	}
}
