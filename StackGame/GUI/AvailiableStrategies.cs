using System;
namespace StackGame.GUI
{
    public enum AvailiableStrategies
    {
        /// <summary>
        /// Стратегия "Один на один"
        /// </summary>
        OneVSOne = 1,
        /// <summary>
        /// Стратегия "Стенка на стенку"
        /// </summary>
        AllVSAll = 2,
        /// <summary>
        /// Стратегия n*n
        /// </summary>
        NVSN = 3,
        /// <summary>
        /// Отменить выбор стратегии
        /// </summary>
        Cancel = 4
    }
}
