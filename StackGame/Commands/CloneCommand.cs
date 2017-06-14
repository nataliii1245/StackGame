using StackGame.Loggers;
using StackGame.Army;
using StackGame.Units.Models;
using StackGame.Units.Abilities;

namespace StackGame.Commands
{
    /// <summary>
    /// Команда клонирования единицы армии
    /// </summary>
    public class CloneCommand : ICommand
    {
        #region Свойства

        /// <summary>
        /// Единица армии, которая пытается клонировать
        /// </summary>
        private readonly IUnit wizardUnit;
        /// <summary>
        /// Клонируемая единица армии
        /// </summary>
        private ICanBeCloned targetUnit;
        /// <summary>
        /// Армия, в которой находится клонируемая единица армии
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

        public void Execute(ILogger logger)
        {
            var clonedUnit = targetUnit.Clone();
            targetArmy.Units.Add(clonedUnit);

            var message = $"\ud83d\udd2e { wizardUnit.Name } клонировал { ((IUnit)targetUnit).Name }. В полку прибыло!";
            logger.Log(message);
        }

        public void Undo(ILogger logger)
        {
            targetArmy.Units.RemoveAt(targetArmy.Units.Count - 1);
        }

        #endregion
    }
}
