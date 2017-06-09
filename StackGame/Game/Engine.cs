using System;
using System.Linq;
using System.Collections.Generic;
using StackGame;
using StackGame.Army;
using StackGame.Commands;
using StackGame.Strategy;
using StackGame.Units.Models;
using StackGame.Units.Abilities;
namespace StackGame.Game
{
    public class Engine
    {
        #region Свойства

        public readonly CommandManager CommandManager = new CommandManager();
		/// <summary>
		/// Стратегия боя
		/// </summary>
		public static IStrategy currentStrategy;

		/// <summary>
		/// Экземпляр движка
		/// </summary>
		private static Engine EngineEntity;

        /// <summary>
        /// Первая армия
        /// </summary>
        private readonly IArmy firstArmy = new Army.Army("Армия №1");
		/// <summary>
		/// Вторая армия
		/// </summary>
		private readonly IArmy secondArmy = new Army.Army("Армия №2");
		#endregion



        #region Инициализация

        /// <summary>
        /// Конструктор
        /// </summary>
		private Engine()
		{
            
		}

		#endregion



		#region Методы

		
		/// <summary>
		/// Получить экземпляр класса
		/// </summary>
		public static Engine GetEngine()
		{
            if (EngineEntity == null)
			{
                EngineEntity = new Engine();
			}

            return EngineEntity;
		}

		/// <summary>
		/// Следующий ход
		/// </summary>
		public bool NextStep()
		{
			if (firstArmy.IsAllDead || secondArmy.IsAllDead)
			{
				return false;
			}

            PrintArmyBeforeOrAfterStep("до");

            FirstStageOfBattle();
            SecondStageOfBattle();

            // Убираем убитых из армии
            ClearBattleField();

            PrintArmyBeforeOrAfterStep("после");
            CommandManager.EndTheMovement();

			return true;
		}

		/// <summary>
		/// Атаковать противника
		/// </summary>
		private void Hit(IUnit first, IUnit second)
		{
            if (first.isAlive && first.Attack > 0)
			{
                var command = new HitCommand(first, second, first.Attack);
				CommandManager.Execute(command);
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
                Hit(opponents.AllyUnit, opponents.EnemyUnit);
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
				var firstArmyContainers = TryGetSpecialAbilityContainers(firstArmy, secondArmy, firstArmyUnitsCount, ref indexOfUnitInFirstArmy);
                if (firstArmyContainers.HasValue)
                {
                    var specialAbilityContainers = firstArmyContainers.Value;
                    _containers.Add(specialAbilityContainers); 

                }

				// для юнита из второй армии пытаемся получить структуру SpecialAbilityContainer,!!! индекс юнита передается по ссылке !!!
                var secondArmyContainers  = TryGetSpecialAbilityContainers(secondArmy, firstArmy, secondArmyUnitsCount, ref indexOfUnitInSecondArmy);
				if (secondArmyContainers.HasValue)
				{
					var specialAbilityContainers = secondArmyContainers.Value;
					_containers.Add(specialAbilityContainers);
				}

				if (_containers.Count == 0)
				{
					continue;
				}

                // если в списке 2 контейнера - сортируем их рандомно
				if (_containers.Count == 2)
				{
                    Random rnd = new Random();
					_containers = _containers.OrderBy(item => rnd.Next()).ToList();
				}

				foreach (var container in _containers)
				{
                    Console.WriteLine($" ❓❓❓ {container.UnitWithSpecialAbility.ToString()} проверяет возможность воздействия специальным навыком в " +
                                      $" радиусе {container.RangeOfUnitsAffectedByUnitWithSpecialAbility.First()} ❓❓❓");
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
			if (unit.isAlive && unit is IHaveSpecialAbility specialUnit)
			{
				return specialUnit;
			}

			return null;
		}

		/// <summary>
		/// Удалить мертвых юнитов
		/// </summary>
        private void ClearBattleField()
		{
            firstArmy.ClearBattleField();
            secondArmy.ClearBattleField();
		}

        private void PrintArmyBeforeOrAfterStep( string state) 
        {
            Console.WriteLine("К бою!");
            Console.WriteLine("*********************************");
            Console.WriteLine($"Армия '{firstArmy.Name}' \"{state}\":");
			Console.WriteLine(firstArmy.ToString());
            Console.WriteLine($"Армия '{secondArmy.Name}' \"{state}\":");
			Console.WriteLine("*********************************");
			Console.WriteLine(secondArmy.ToString());
            Console.WriteLine("*********************************");
        }
		#endregion
	}
}
