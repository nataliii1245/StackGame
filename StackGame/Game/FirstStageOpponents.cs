using StackGame.Army;
namespace StackGame.Game
{
    public struct FirstStageOpponents
    {
		#region Свойства

		/// <summary>
		/// Исходная армия
		/// </summary>
        public IArmy AllyArmy { get; private set; }
		/// <summary>
		/// Вражеская армия
		/// </summary>
        public IArmy EnemyArmy { get; private set; }
		/// <summary>
		/// Позиция юнита в исходной армии
		/// </summary>
        public int AllyUnitPosition { get; private set; }
		/// <summary>
		/// Позиция юнита во вражеской армии
		/// </summary>
        public int EnemyUnitPosition { get; private set; }

		#endregion

        #region Инициализация

		public FirstStageOpponents(IArmy AllyArmy,int AllyUnitPosition,IArmy EnemyArmy, int EnemyUnitPosition)
		{
            this.AllyArmy = AllyArmy;
            this.AllyUnitPosition = AllyUnitPosition;
            this.EnemyArmy = EnemyArmy;
            this.EnemyUnitPosition = EnemyUnitPosition;
		}

		#endregion

        #region Методы

		/// <summary>
		/// Поменять единицы армий местами
		/// </summary>
		public FirstStageOpponents Swap()
		{
            var opponents = new FirstStageOpponents(EnemyArmy, EnemyUnitPosition, AllyArmy, AllyUnitPosition);
			return opponents;
		}

        #endregion
	}
}
