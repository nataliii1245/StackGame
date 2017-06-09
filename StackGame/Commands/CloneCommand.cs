using System;
using System.Linq;
using System.Collections.Generic;
using StackGame;
using StackGame.Army;
using StackGame.Strategy;
using StackGame.Configs;
using StackGame.Units.Models;
using StackGame.Units.Abilities;
namespace StackGame.Commands
{
    /// <summary>
    /// Команда "клонировать"
    /// </summary>
    public class CloneCommand : ICommand
    {
        #region Свойства

        /// <summary>
        /// Единица армии, которая пытается клонировать
        /// </summary>
        private readonly IUnit wizardUnit;
        /// <summary>
        /// Улучшаемая единица армии
        /// </summary>
        private ICanBeCloned targetUnit;
        /// <summary>
        /// Армия, в которой находится улучшаемая единица армии
        /// </summary>
        private readonly IArmy targetArmy;

        #endregion

        #region Инициализация

        public CloneCommand(IUnit wizardUnit, ICanBeCloned targetUnit, IArmy targetArmy)
        {
            this.wizardUnit = wizardUnit;
            this.targetUnit = targetUnit;
            this.targetArmy = targetArmy;

        }

        #endregion

        #region Методы

        public void Execute()
        {
            var clonedUnit = targetUnit.Clone();
            targetArmy.Units.Add(clonedUnit);

            var tmp = (IUnit)targetUnit;

            Console.WriteLine($" { wizardUnit.Name } клонировал { tmp.Name }. В полку прибыло!");
        }

        public void Undo()
        {
            targetArmy.Units.RemoveAt(targetArmy.Units.Count - 1);
        }

        #endregion
    }
}
