using System;
using StackGame.Game;
namespace StackGame.GUI
                   
{
    public class ConsoleGUI
    {
		#region Методы
        /// <summary>
		/// Вывести основное меню
		/// </summary>
		public static void ShowMenu()
		{
			Console.WriteLine("Меню:");
			Console.WriteLine("1️⃣  Начать новую игру");
			Console.WriteLine("2️⃣  Сделать ход");
			Console.WriteLine("3️⃣  Играть до победы");
			Console.WriteLine("4️⃣  Показать армии");
			Console.WriteLine("5️⃣  Изменить стратегию боя");
			Console.WriteLine("6️⃣  Ход назад");
			Console.WriteLine("7️⃣  Ход вперед");
			Console.WriteLine("8️⃣  Выход");
			Console.WriteLine();
		}

		/// <summary>
		/// Считать команду основного меню
		/// </summary>
		public static UserCommands ReadUserCommand()
		{
            UserCommands? command = null;

			var isSuccessful = false;
			do
			{
				Console.Write("▶️ Введите команду: ");

                if (int.TryParse(Console.ReadLine(), out int input) && Enum.IsDefined(typeof(UserCommands), input))
				{
                    command = (UserCommands)input;
					isSuccessful = true;
				}
				else
				{
					var message = " Такой команды не существует! Повторите попытку.";
                    ShowError(message);
				}

				Console.WriteLine();
			} while (!isSuccessful);

			return command.Value;
		}

		/// <summary>
		/// Вывести меню выбора стратегии
		/// </summary>
		public static void ShowChangeStrategyMenu()
		{
			Console.WriteLine("Выберите стратегию боя:");
			Console.WriteLine("1️⃣  1 на 1");
			Console.WriteLine("2️⃣  Все на всех");
			Console.WriteLine("3️⃣  N на N");
			Console.WriteLine("4️⃣  Отмена");
			Console.WriteLine();
		}

		/// <summary>
		/// Считать команду меню выбора стратегии
		/// </summary>
        public static AvailiableStrategies ReadStrategyCommand()
		{
            AvailiableStrategies? command = null;

			var isSuccessful = false;
			do
			{
				Console.Write("▶️ Введите команду: ");

                if (int.TryParse(Console.ReadLine(), out int input) && Enum.IsDefined(typeof(AvailiableStrategies), input))
				{
                    command = (AvailiableStrategies)input;
					isSuccessful = true;
				}
				else
				{
					var message = "⁉️ Такой команды не существует! Повторите попытку.";
					ShowError(message);
				}

				Console.WriteLine();
			} while (!isSuccessful);

			return command.Value;
		}

		public static void ShowError(string message)
		{
			Console.WriteLine($"🚫 { message }");
		}

		/// <summary>
		/// Считать стоимость армии
		/// </summary>
		public static int ReadArmyCost()
		{
			int? armyCost = null;

			var isSuccessful = false;
			do
			{
				Console.Write("▶️ Введите стоимость армии: ");

				if (int.TryParse(Console.ReadLine(), out int input) && input > 0)
				{
                    armyCost = input;
					isSuccessful = true;
				}
				else
				{
					var message = "⁉️ Недопустимое значение стоимости, попробуйте еще раз.";
                    ShowError(message);
				}

				Console.WriteLine();
			} while (!isSuccessful);

            return armyCost.Value;
		}

		/// <summary>
		/// Считать N из консоли для стратегии "N на N"
		/// </summary>
		public static int ReadNForNVSN()
		{
			int? n = null;

			var isSuccessful = false;
			do
			{
				Console.Write("▶️ Введите количество юнитов в ряду: ");

				if (int.TryParse(Console.ReadLine(), out int input) && input > 1)
				{
					n = input;
					isSuccessful = true;
				}
				else
				{
					var message = "⁉️ Недопустимое значение, попробуйте еще раз.";
                    ShowError(message);
				}

				Console.WriteLine();
			} while (!isSuccessful);

			return n.Value;
		}

		public static void ShowInfoAboutAuthor()
		{
            Console.WriteLine();
			Console.WriteLine("✦✦✦✦✦✦✦✦✦✦✦✦✦✦✦✦✦✦✦✦✦✦✦✦✦✦✦✦✦✦✦✦✦✦✦✦✦✦✦✦✦✦✦✦✦✦✦✦");
			Console.WriteLine("✦             Лабораторная работа              ✦");
			Console.WriteLine("✦                по  дисциплине                ✦");
			Console.WriteLine("✦  \"Проектирование программной архитектуры\"    ✦");
			Console.WriteLine("✦                                              ✦");
			Console.WriteLine("✦                                              ✦");
			Console.WriteLine("✦             Выполнила студентка              ✦");
			Console.WriteLine("✦           РЭУ им Г. В. Плеханова             ✦");
			Console.WriteLine("✦                группы ДКО-142б               ✦");
			Console.WriteLine("✦          Волкова Наталия Николаевна          ✦");
			Console.WriteLine("✦                                              ✦");
			Console.WriteLine("✦                     2017                     ✦");
			Console.WriteLine("✦✦✦✦✦✦✦✦✦✦✦✦✦✦✦✦✦✦✦✦✦✦✦✦✦✦✦✦✦✦✦✦✦✦✦✦✦✦✦✦✦✦✦✦✦✦✦✦");
			Console.WriteLine();
		}

        public static void ShowResultsOfGame()
        {
			if (Engine.GetInstance().IsGameEndsFlag == true && Engine.GetInstance().IsGameResultPrintsYet == false)
			{
				if (Engine.GetInstance().firstArmy.Units.Count == 0 && Engine.GetInstance().secondArmy.Units.Count > 0)
				{
					Console.WriteLine($"Игра завершилась победой армии { Engine.GetInstance().secondArmy.Name }");
					Console.WriteLine();

				}
				else if (Engine.GetInstance().secondArmy.Units.Count == 0 && Engine.GetInstance().firstArmy.Units.Count > 0)
				{
					Console.WriteLine($"Игра завершилась победой армии { Engine.GetInstance().firstArmy.Name }");
					Console.WriteLine();
				}
				else
				{
					Console.WriteLine($"Игра завершилась вничью!");
					Console.WriteLine();
				}

				if (Engine.GetInstance().firstArmy.Units.Count == 0)
				{
					Console.WriteLine($"Все единицы {Engine.GetInstance().firstArmy.Name} мертвы!");
					Console.WriteLine(Engine.GetInstance().secondArmy.ToString());
				}
				else if (Engine.GetInstance().secondArmy.Units.Count == 0)
				{
					Console.WriteLine($"Все единицы {Engine.GetInstance().secondArmy.Name} мертвы!");
					Console.WriteLine(Engine.GetInstance().firstArmy.ToString());
				}
				Engine.GetInstance().IsGameResultPrintsYet = true;
			} 
        }

		#endregion
    }
}
