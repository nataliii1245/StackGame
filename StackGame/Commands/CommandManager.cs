using System.Collections.Generic;
using StackGame.Loggers;

namespace StackGame.Commands
{
    /// <summary>
    /// Командный менеджер
    /// </summary>
    public class CommandManager
    {
		#region Свойства

		/// <summary>
		/// Возможно ли отменить ход
		/// </summary>
		public bool CanUndoMovement => undoStack.Count > 0;
		/// <summary>
		/// Возможно ли повторить ход
		/// </summary>
		public bool CanRedoMovement => redoStack.Count > 0;

		/// <summary>
		/// Стек команд для отмены
		/// </summary>
		private readonly Stack<ICommand> undoStack = new Stack<ICommand>();
		/// <summary>
		/// Стек команд для повтора
		/// </summary>
		private readonly Stack<ICommand> redoStack = new Stack<ICommand>();

        /// <summary>
        /// Журнал событий 
        /// </summary>
        private readonly ILogger logger;

        #endregion

        #region Инициализация

        public CommandManager( ILogger logger)
        {
            this.logger = logger;
        }
        #endregion

        #region Методы

        /// <summary>
        ///  Выполнить команду
        /// </summary>
        public void Execute(ICommand command)
		{
            command.Execute(logger);
            undoStack.Push(command);
        }

        /// <summary>
        ///  Завершение текущего хода
        /// </summary>
        public void EndTheMovement()
		{
            ICommand separator = new EndOfMovementCommand();
            undoStack.Push(separator);

            // очистить стек действий для повторения
            redoStack.Clear();
		}

		/// <summary>
        /// Отменить 
        /// </summary>
		public void Undo()
		{
            var emptyCommand = undoStack.Pop();

            while (CanUndoMovement && undoStack.Peek().GetType() != typeof(EndOfMovementCommand))
            {
                ICommand lastCommand = undoStack.Pop();
                lastCommand.Undo(logger);

                redoStack.Push(lastCommand);
            }
            redoStack.Push(emptyCommand);
		}

		/// <summary>
        /// Повторить
        /// </summary>
		public void Redo()
		{
			var emptyCommand = redoStack.Pop();
			while (CanRedoMovement && redoStack.Peek().GetType() != typeof(EndOfMovementCommand))
			{
				ICommand command = redoStack.Pop();
                command.Execute(logger);

				undoStack.Push(command);
			}
            undoStack.Push(emptyCommand);
			
		}

		#endregion
	}
}
