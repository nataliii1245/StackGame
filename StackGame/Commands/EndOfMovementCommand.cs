using StackGame.Game;
using StackGame.Loggers;

namespace StackGame.Commands
{
    /// <summary>
    /// Команда для обозначения конца хода
    /// </summary>
    public class EndOfMovementCommand : ICommand
    {
        #region Методы

        public void Execute(ILogger logger)
        {
            Engine.GetInstance().CountOfMovements++;
            var message = $"➡️ Ход № { Engine.GetInstance().CountOfMovements }";
            logger.Log(message);
        }

        public void Undo(ILogger logger)
        {
            Engine.GetInstance().CountOfMovements--;
			var message = $"➡️ Ход № { Engine.GetInstance().CountOfMovements }";
			logger.Log(message);
            
        }

        #endregion
    }
}
