using System;
using System.Linq;
using StackGame.Units.Proxy;
using StackGame.Loggers;
using System.Collections.Generic;
using StackGame.Observers;
using StackGame.Army;
using StackGame.Army.Factory;
using StackGame.Commands;
using StackGame.Strategy;
using StackGame.Units.Abilities;
using StackGame.Units.Models;
using StackGame.GUI;

namespace StackGame.Game
{
    public class Engine
    {
        #region Свойства

        public CommandManager CommandManager;
        /// <summary>
		/// Стратегия боя
		/// </summary>
        public IStrategy currentStrategy ;
        /// <summary>
		/// Экземпляр движка
		/// </summary>
		private static Engine EngineEntity;
        /// <summary>
        /// Первая армия
        /// </summary>
        public IArmy firstArmy { get; private set; }
        /// <summary>
		/// Вторая армия
		/// </summary>
        public IArmy secondArmy { get; private set; }
        /// <summary>
        /// Счетчик ходов, за которые ни один юнит не умер ( поле для исключения ситуации блокировки)
        /// </summary>
        public int CurrentNumOfMovementWithoutDeath { get; set; }
        /// <summary>
        /// Флаг конца игры
        /// </summary>
        public bool IsGameEndsFlag => firstArmy.IsAllDead || secondArmy.IsAllDead;/// <summary>
        /// Флаг есть ли возможность следующего хода - нет ли блокировки?
        /// </summary>
        public bool IsNextStepPossible => CurrentNumOfMovementWithoutDeath < GameConfigs.MaxNumberOfMovementsWithoutDeath;/// <summary>
        /// Счетчик ходов
        /// </summary>
        public int CountOfMovements { get; set; }
		public bool IsGameResultPrintsYet = false;

        #endregion

        #region Инициализация

        /// <summary>
        /// Конструктор
        /// </summary>
		private Engine(){ }

		#endregion

        #region Методы

		/// <summary>
		/// Получить экземпляр класса
		/// </summary>
		public static Engine GetInstance()
		{
			if (EngineEntity == null)
			{
				EngineEntity = new Engine();
			}

			return EngineEntity;
		}

        /// <summary>
        /// Начало новый игры
        /// </summary>
        public void StartNewGame(int price)
        {
            // Создание фабричного метода
			var factory = new ArmyFactory();

            // Генерация армий
			firstArmy = new Army.Army("Армия №1", factory, price);
			secondArmy = new Army.Army("Армия №2", factory, price);

            // Создание списка наблюдателей для юнитов
			List<IObserver> listOfObservers = new List<IObserver>()
			{
				new ConsoleBeepObserver(),
				new FileObserver()
			};

            // Добавление наблюдателей
			AddObservers(firstArmy, listOfObservers);
			AddObservers(secondArmy, listOfObservers);

			// Создание менеджера команд
			ILogger logger = new ConsoleLogger();
			CommandManager = new CommandManager(logger);

            logger = new FileLogger("HeavyInfantryProxyLog.txt");

            ReplaceHeavyInfantryUnitsWithProxyUnits(firstArmy, logger);
            ReplaceHeavyInfantryUnitsWithProxyUnits(secondArmy, logger);

            // Задание текущей стратегии
            currentStrategy = new OneVSOne();

            CurrentNumOfMovementWithoutDeath = 0;
            CountOfMovements = 0;
            IsGameResultPrintsYet = false;
		}
		
		/// <summary>
        /// Добавить возможных наблюдателей для армии
		/// </summary>
		private void AddObservers(IArmy targetArmy, List<IObserver> observers)
		{
			foreach (var unit in targetArmy.Units)
			{
                if (unit is ICanBeObserved ICanBeObservedUnit)
				{
					foreach (var observer in observers)
					{
                        ICanBeObservedUnit.RegisterObserver(observer);
					}
				}
			}
		}

        private void ReplaceHeavyInfantryUnitsWithProxyUnits(IArmy army, ILogger logger)
        {
            for (int i = 0; i < army.Units.Count; i++)
            {
                var currentUnit = army.Units[i];
                if (currentUnit is HeavyInfantryUnit heavyInfantryUnit)
                {
                    var heavyInfantryUnitProxy = new HeavyInfantryUnitProxy(heavyInfantryUnit, logger);
                    army.Units[i] = heavyInfantryUnitProxy;
                }
            }
        }

		/// <summary>
		/// Следующий ход
		/// </summary>
		public void NextStep()
		{
            if (IsNextStepPossible == false)
            {
                return;
            }

            if (IsGameEndsFlag == false)
            {
                var newCountOfMovements = CountOfMovements + 1;
                var command = new ChangeCountOfMovementsCommand(CountOfMovements, newCountOfMovements);
                CommandManager.Execute(command);

                Console.WriteLine($"Ход № {CountOfMovements}");
                FirstStageOfBattle();
                SecondStageOfBattle();

                // Убираем убитых из армии
                ClearBattleField();

                CommandManager.EndTheMovement();
            }
		}

        /// <summary>
        /// Осуществить ходы до победы одной из армий
        /// </summary>
        public void PlayWhileNotEnd()
        {
            while ( IsNextStepPossible )
            {
                NextStep();

                if(IsGameEndsFlag)
                {
                    return;
                }
            }
        }

		/// <summary>
		/// Атаковать противника
		/// </summary>
        private void Hit( IArmy allyArmy, int allyUnitPosition, IArmy enemyArmy, int enemyUnitPosition)
		{
            if (allyArmy.Units.Count  == 0 || enemyArmy.Units.Count == 0)
            {
                return;
            }
            var allyUnit = allyArmy.Units[allyUnitPosition];
            var enemyUnit = enemyArmy.Units[enemyUnitPosition];

            if (allyUnit.IsAlive && allyUnit.Attack > 0 && enemyUnit.IsAlive)
			{
                ICommand command = new HitCommand(allyUnit, enemyUnit, allyUnit.Attack);
				CommandManager.Execute(command);

                if (enemyUnit.IsAlive && enemyUnit is ICanBeImproved ICanBeImprovedUnit && ICanBeImprovedUnit.NumberOfImprovments > 0)
                {
                    double chance = Randomizer.CalculateChanceOfAction();
                    if (chance > 0.75)
                    {
                        command = new DeleteImprovmentCommand(ICanBeImprovedUnit, enemyArmy, enemyUnitPosition);
                        CommandManager.Execute(command);
                    }
                }
            }
		}

        /// <summary>
        /// Первая фаза хода - сражение юнитов, стоящих в первом(!!!!!) ряду
        /// </summary>
        private void FirstStageOfBattle()
        {
            // генерируем список-очередь, состоящий из пар оппонентов
            var queueForFirstStageOfBattle = currentStrategy.GetOpponentsQueue(firstArmy, secondArmy);
            foreach (var opponents in queueForFirstStageOfBattle)
			{
                Hit(opponents.AllyArmy, opponents.AllyUnitPosition, opponents.EnemyArmy, opponents.EnemyUnitPosition);
			}
        }

        /// <summary>
        /// Вторая фаза хода - использование специальных возможностей
        /// </summary>
        private void SecondStageOfBattle()
        {
            var firstArmyUnitsCount = firstArmy.Units.Count;
            var indexOfUnitInFirstArmy = 0;

            var secondArmyUnitsCount = secondArmy.Units.Count;
            var indexOfUnitInSecondArmy = 0;

            while (indexOfUnitInFirstArmy < firstArmyUnitsCount || indexOfUnitInSecondArmy < secondArmyUnitsCount)
            {
                // создаем структуру, в которой хранятся: воздействующий юнит, армия, на которую он воздействует, позиции юнитов
                // попадающих под воздействие и позиция воздействующего юнита
                var _containers = new List<SpecialAbilityContainer>();

				// для юнита из первой армии пытаемся получить структуру SpecialAbilityContainer,!!! индекс юнита передается по ссылке !!!
				var firstArmyContainer = TryGetSpecialAbilityContainers(firstArmy, secondArmy, firstArmyUnitsCount, ref indexOfUnitInFirstArmy);
                if (firstArmyContainer.HasValue)
                {
                    var specialAbilityContainer = firstArmyContainer.Value;
                    _containers.Add(specialAbilityContainer); 

                }

				// для юнита из второй армии пытаемся получить структуру SpecialAbilityContainer,!!! индекс юнита передается по ссылке !!!
                var secondArmyContainer  = TryGetSpecialAbilityContainers(secondArmy, firstArmy, secondArmyUnitsCount, ref indexOfUnitInSecondArmy);
				if (secondArmyContainer.HasValue)
				{
					var specialAbilityContainer = secondArmyContainer.Value;
					_containers.Add(specialAbilityContainer);
				}

				if (_containers.Count == 0)
				{
					continue;
				}

                // если в списке 2 контейнера - сортируем их рандомно
				if (_containers.Count > 1)
				{
                    _containers = Randomizer.IntermixIt(_containers).ToList();
				}

                foreach (var container in _containers)
				{
                    container.UnitWithSpecialAbility.DoSpecialAction(container.AffectedByUnitWithSpecialAbilityArmy, container.RangeOfUnitsAffectedByUnitWithSpecialAbility, container.PositionOfUnitWithSpecialAbility);
				}
			}
        }

		/// <summary>
		/// Пытаемся получить компоненты для применения специальных возможностей
		/// </summary>
        SpecialAbilityContainer? TryGetSpecialAbilityContainers(IArmy allyArmy, IArmy enemyArmy, int allyArmyUnitsCount, ref int allyArmyUnitIndex)
		{
            // если индекс юнита, для которого мы совершаем проверку < число юнитов в его армии
            // т.е. он принадлежит своей армии
			if (allyArmyUnitIndex < allyArmyUnitsCount)
			{
				// запоминаем индекс этого юнита в tmpArmyUnitIndex
				var tmpArmyUnitIndex = allyArmyUnitIndex;
				allyArmyUnitIndex++;

				// Пытаемся получить экземпляр юнита рассматриваемой армии
				var specialUnit = TryGetUnitWithSpecialAbulity(allyArmy, tmpArmyUnitIndex);
				if (specialUnit != null)
				{
                    // получаем список позиций юнитов, которые попадают под действие навыка
                    var range = currentStrategy.GetUnitsRangeForSpecialAbility(allyArmy, enemyArmy, specialUnit, tmpArmyUnitIndex);
					if (range != null)
					{
                        // создаем армию, на которую будет направлен эффект в зависимости от типа навыка
                        var targetArmy = specialUnit.isFriendly ? allyArmy : enemyArmy;

                        var containers = new SpecialAbilityContainer(specialUnit, targetArmy, range, tmpArmyUnitIndex);
						return containers;
					}
				}
			}

			return null;
		}

		/// <summary>
		/// Пытаемся получить экземпляр юнита с особыми навыками по армии и позиции
		/// </summary>
		IHaveSpecialAbility TryGetUnitWithSpecialAbulity(IArmy army, int unitPosition)
		{
			var unit = army.Units[unitPosition];
			if (unit.IsAlive && unit is IHaveSpecialAbility specialUnit)
			{
				return specialUnit;
			}

			return null;
		}

		/// <summary>
		/// Изменить стратегию
		/// </summary>
		public void ChangeStrategy(IStrategy strategy)
		{
            if (currentStrategy.GetType() != strategy.GetType())
			{
                ICommand command = new ChangeCurrentStrategyCommand(currentStrategy, strategy, CurrentNumOfMovementWithoutDeath);
				CommandManager.Execute(command);

                CommandManager.EndTheMovement();
			}
		}
		/// <summary>
		/// Удалить мертвых юнитов
		/// </summary>
        private void ClearBattleField()
		{

            var firstArmyNumDeadUnits = firstArmy.ClearBattleField();
            var secondArmyNumDeadUnits = secondArmy.ClearBattleField();

            int newNumOfMovementWithoutDeath;

            if (firstArmyNumDeadUnits == 0 && secondArmyNumDeadUnits == 0)
			{
                newNumOfMovementWithoutDeath = CurrentNumOfMovementWithoutDeath + 1;
			}
			else
			{
                newNumOfMovementWithoutDeath = 0;
			}

            var command = new ChangeCurrentNumOfMovementWithoutDeathCommand(CurrentNumOfMovementWithoutDeath, newNumOfMovementWithoutDeath);
			CommandManager.Execute(command);
		}

		#endregion
	}
}
