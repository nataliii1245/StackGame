namespace StackGame.GUI
{
    public enum UserCommands
    {
        /// <summary>
        /// Начать новую игру
        /// </summary>
        StartNewGame = 1,
        /// <summary>
        /// Сделать следующий шаг
        /// </summary>
        NextMovement = 2,
        /// <summary>
        /// Осуществить ходы до конца игры
        /// </summary>
        PlayWhileNotEnd = 3,
        /// <summary>
        /// Отобразить армии
        /// </summary>
        ShowArmies = 4,
        /// <summary>
        /// Сменить стратегию
        /// </summary>
        ChangeStrategy = 5,
        /// <summary>
        /// Отменить ход
        /// </summary>
        Undo = 6,
        /// <summary>
        /// Повторить ход
        /// </summary>
        Redo = 7,
        /// <summary>
        /// Выйти из прогаммы
        /// </summary>
        Exit = 8

    }
}
