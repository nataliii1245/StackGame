using System;
using StackGame.GUI;
using StackGame.Game;
using StackGame.Strategy;

namespace StackGame
{
    class MainClass
    {


        public static void Main(string[] args)
        {
            ConsoleGUI.ShowInfoAboutAuthor();
            var isGameStarts = false;
            UserCommands? command = null;

            do
            {
                ConsoleGUI.ShowMenu();
                command = ConsoleGUI.ReadUserCommand();

                if (isGameStarts && command != UserCommands.StartNewGame && command != UserCommands.ShowArmies && command != UserCommands.Exit)
                {
                    if (Engine.GetInstance().IsGameEndsFlag)
                    {
                        var message = "Игра окончена. Начните новую игру!";
                        ConsoleGUI.ShowError(message);
                        Console.WriteLine();
                    }
                }
                if (isGameStarts == false && command != UserCommands.StartNewGame && command != UserCommands.Exit)

                {
                    var message = "Игра не начата. Пожалуйста, начните новую игру!";
                    ConsoleGUI.ShowError(message);
                    Console.WriteLine();

                    continue;
                }

                switch (command)
                {
                    case UserCommands.StartNewGame:
                        var armyCost = ConsoleGUI.ReadArmyCost();
                        Engine.GetInstance().StartNewGame(armyCost);
                        isGameStarts = true;

                        Console.WriteLine("\ud83c\udf3f \ud83c\udf3f \ud83c\udf3f \ud83c\udf3f \ud83c\udf3f \ud83c\udf3f \ud83c\udf3f \ud83c\udf3f \ud83c\udf3f \ud83c\udf3f");
                        Console.WriteLine("Новая игра началась!");
						Console.WriteLine("\ud83c\udf3f \ud83c\udf3f \ud83c\udf3f \ud83c\udf3f \ud83c\udf3f \ud83c\udf3f \ud83c\udf3f \ud83c\udf3f \ud83c\udf3f \ud83c\udf3f");

                        if( Engine.GetInstance().IsGameEndsFlag == true)
                        {
                            var message = "Необходимо увеличить стоимость армии.";
                            ConsoleGUI.ShowError(message);
                            Console.WriteLine();
                        }

                        Engine.GetInstance().IsGameResultPrintsYet = false;
                        Console.WriteLine(Engine.GetInstance().firstArmy);
                        Console.WriteLine(Engine.GetInstance().secondArmy);
                        break;

                    case UserCommands.NextMovement:

                        if (Engine.GetInstance().IsNextStepPossible == false)
                        {
							Console.WriteLine("⚠️  Необходимо изменить стратегию игры или начать новую игру.");
							Console.WriteLine();

							continue;
                        }

                        Engine.GetInstance().NextStep();
                        Console.WriteLine();
                        ConsoleGUI.ShowResultsOfGame();
                        break;

                    case UserCommands.PlayWhileNotEnd:

                        Engine.GetInstance().PlayWhileNotEnd();
                        Console.WriteLine();

						if (Engine.GetInstance().IsGameEndsFlag == true && Engine.GetInstance().IsGameResultPrintsYet == false)
						{
                            ConsoleGUI.ShowResultsOfGame();
                        }
                        else if (Engine.GetInstance().IsGameEndsFlag == false)
                        {
                            Console.WriteLine("⚠️  Необходимо изменить стратегию игры или начать новую игру.");
							Console.WriteLine();
						}
						break;

                    case UserCommands.ShowArmies:
                        Console.Write(Engine.GetInstance().firstArmy.ToString());
                        Console.Write(Engine.GetInstance().secondArmy.ToString());
                        break;

                    case UserCommands.ChangeStrategy:

                        if (Engine.GetInstance().IsGameEndsFlag == false)
                        {
                            ConsoleGUI.ShowChangeStrategyMenu();
                            var selectedStrategyCommand = ConsoleGUI.ReadStrategyCommand();

                            IStrategy strategy = null;

                            switch (selectedStrategyCommand)
                            {
                                case AvailiableStrategies.AllVSAll:

                                    strategy = new AllVSAll();
                                    break;
                                case AvailiableStrategies.NVSN:

                                    var n = ConsoleGUI.ReadNForNVSN();
                                    strategy = new NVSN(n);
                                    break;
                                case AvailiableStrategies.OneVSOne:

                                    strategy = new OneVSOne();
                                    break;
                                case AvailiableStrategies.Cancel:
                                    continue;
                            }

                            Engine.GetInstance().ChangeStrategy(strategy);
                            Console.WriteLine();
                        }
                        break;
                    case UserCommands.Undo:
                        if (Engine.GetInstance().IsGameEndsFlag == false)
                        {
                            if (Engine.GetInstance().CommandManager.CanUndoMovement == false)
                            {
                                var message = "Невозможно отменить ход!";
                                ConsoleGUI.ShowError(message);
                                Console.WriteLine();
                            }
                            else
                            {
                                Engine.GetInstance().CommandManager.Undo();
                                Console.WriteLine("✅ Ход назад успешно выполнен!");
                            }
                        }
                        break;
                    case UserCommands.Redo:

                        if (Engine.GetInstance().IsGameEndsFlag == false)
                        {
                            if (Engine.GetInstance().CommandManager.CanRedoMovement == false)
                            {
                                var message = "Невозможно повторить ход!";
                                ConsoleGUI.ShowError(message);
                                Console.WriteLine();
                            }
                            else
                            {
                                Engine.GetInstance().CommandManager.Redo();
                                Console.WriteLine("✅ Ход вперед успешно выполнен!");
                            }
                        }
                        break;
                    case UserCommands.Exit:
                        
                        Console.WriteLine("Игра окончена!");
                        Console.WriteLine();
                        break;
                }
            }
            while (command.Value != UserCommands.Exit);
        }
    }
}
