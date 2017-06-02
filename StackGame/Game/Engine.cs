using System;
using System.Linq;
using System.Collections.Generic;
using StackGame;
using StackGame.Army;
using StackGame.Units.Models;
namespace StackGame.Game
{
    public class Engine
    {
		#region Свойства

		/// <summary>
        /// Экземпляр движка
		/// </summary>
		private static Engine EngineEntity;

		/// <summary>
		/// Первая армия
		/// </summary>
		private IArmy firstArmy { get; set; }
		/// <summary>
		/// Вторая армия
		/// </summary>
		private IArmy secondArmy { get; set; }

		#endregion

		#region Инициализация

        /// <summary>
        /// Конструктор
        /// </summary>
		private Engine()
		{
            firstArmy = new Army.Army("Армия №1");
			secondArmy = new Army.Army("Армия №2");
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

            // Генерируем пару для сражения
            Tuple<IUnit, IUnit> opponents = new Tuple<IUnit, IUnit>(firstArmy.Units[0], secondArmy.Units[0]);
			Tuple<IUnit, IUnit> opponentsRevert = new Tuple<IUnit, IUnit>(secondArmy.Units[0], firstArmy.Units[0]);

            // Генерируем очередь, в которой будут биться воины
			var battleQueue = new List<Tuple<IUnit, IUnit>> { opponents, opponentsRevert };

			var random = new Random();

			// Сортируем очередь согласно рандому
			battleQueue = battleQueue.OrderBy(opponent => random.Next()).ToList();

			// Соперники наносят удары
            foreach (var opponentsPair in battleQueue)
			{
                Hit(opponentsPair.Item1, opponentsPair.Item2);
                Console.WriteLine($" {opponentsPair.Item1} наносит удар {opponentsPair.Item2};");
			}

            // Убираем убитых из армии
            ClearBattleField(firstArmy);
            ClearBattleField(secondArmy);

            PrintArmyBeforeOrAfterStep("после");

			return true;
		}

		/// <summary>
		/// Атаковать противника
		/// </summary>
		private void Hit(IUnit first, IUnit second)
		{
            if (first.isAlive)
			{
                second.GetDamage(first.Attack);
			}
		}

		/// <summary>
		/// Удалить мертвые единицы армии
		/// </summary>
        private void ClearBattleField(IArmy army)
		{
            var dead = army.Units.Where(unit => !unit.isAlive).ToList();
			foreach (var unitToBeDeleted in dead)
			{
				army.Units.Remove(unitToBeDeleted);
			}
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
