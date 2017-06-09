using System;
using System.Collections.Generic;
using System.Linq;
using StackGame.Units.Abilities;
using StackGame.Army;
using StackGame.Game;
using StackGame.Units.Improvments;
using StackGame.Commands;

namespace StackGame.Units.Models
{
    public class LightInfantryUnit: Unit, ICanBeCloned, ICanBeHealed, IHaveSpecialAbility
    {
        #region Свойства

        public int SpecialAbilityRange { get; } = StartStats.Stats.Where(p => p.Key == UnitType.LightInfantryUnit).First().Value.SpecialAbilityRange;

        public int SpecialAbilityPower { get; } = StartStats.Stats.Where(p => p.Key == UnitType.LightInfantryUnit).First().Value.SpecialAbilityPower;

        public bool isFriendly { get; private set; } = true;
        #endregion

        #region Инициализация

        public LightInfantryUnit(string name, int health, int attack) : base(name, health, attack)
        { }

		#endregion



        #region Методы

		public void Heal(int healthPower)
		{
			Health += healthPower;
			if (Health > MaxHealth)
			{
				Health = MaxHealth;
			}
		}

		public IUnit Clone()
		{
			var clonedUnit = (Unit)MemberwiseClone();
			clonedUnit.listOfObservers = listOfObservers.Select(observer => observer).ToList();

			return clonedUnit;
		}

		// реализация специального действия для легкого пехотинца
		public void DoSpecialAction(IArmy targetArmy, IEnumerable<int> possibleUnitsPositions, int currentPosition)
		{
			Random random = new Random();
			var chance = random.Next(100) / 100;

            if ( chance > 0.7) 
            {
                var possibleTargetUnits = new List<Tuple<int, ICanBeImproved>>();
                foreach (int index in possibleUnitsPositions)
                {
                    // исключаем из рассмотрения свою собственную позицию
                    if (index == currentPosition)
                    {
                        continue;
                    }

                    var currentUnit = targetArmy.Units[index];
                    // проверяем, что рассматриваемый юнит жив && рассматриваемый юнит может быть клонирован и улучшен
                    if (currentUnit.isAlive && currentUnit is ICanBeCloned && currentUnit is ICanBeImproved ICanBeImprovedUnit)
                    {
                        // если условие выполнилось, то добавляем в список кортеж из индекса и самого улучшаемого юнита
                        possibleTargetUnits.Add(new Tuple<int, ICanBeImproved>(index, ICanBeImprovedUnit));
                    }
                }

                // если массив юнитов, которые попадают под воздействие, пуст, то невозможно осуществить действие
                if (possibleTargetUnits.Count == 0)
				{
                    Console.WriteLine(" Невозможно одеть рыцаря!");
					return;
				}

                // targetRow имеет тип Tuple< int, ICanBeImproved>
                var targetRow = possibleTargetUnits[random.Next(possibleTargetUnits.Count)];
				// получили индекс целевого юнита
                var targetIndex = targetRow.Item1;
                // получили самого целевого юнита
				var targetUnit = targetRow.Item2;

                // выявляем тип улучшаемого юнита при помощи рефлексии
				var unitToBeImprovedType = typeof(UnitToBeImproved<>);

                // получили все типы сборки
                var types = unitToBeImprovedType.Assembly.GetTypes()
                                                // отбираем только те, у которых есть универсальные родительские классы
                                                .Where(type => type.BaseType != null && type.BaseType.IsGenericType)
												// если родительский тип рассматриваемого класса можем привести к универсальному параметру T и он равен 
												// приведенному к универсальному параметру T unitToBeImprovedType
												.Where(type => type.BaseType.GetGenericTypeDefinition() == unitToBeImprovedType.GetGenericTypeDefinition())
                                                // получили список доступных улучшений
                                                .ToList();

				do
				{
                    // из списка типов возможных улучшений берем произвольный
					var type = types[random.Next(types.Count)];
                    // если целевой юнит может быт улучшен улучшением рассматриваемого типа
					if (targetUnit.CanIBeImprovedWithFeatureOfThisType(type))
					{
                        // реализация рандома для каждой шмотки
                        if (random.Next(100) / 100 > 0.5)
                        {
                            // создаем улучшение универсального типа для юнита указанного типа
                            var unitImprovment = type.MakeGenericType(targetUnit.GetType());
							//// создает экземпляр улучшенного юнита, передавая тип создаваемого объекта и на кого повесить
							//var improvedUnit = (IUnit)Activator.CreateInstance(unitImprove, targetUnit);

							//// помещаем этого улучшенного (одетого уже рыцаря) обратно в армию, на то же место
							//targetArmy.Units[targetIndex] = improvedUnit;

                            var command = new ImproveCommand(this, (IUnit)targetUnit, targetArmy, targetIndex, unitImprovment );
							Engine.GetEngine().CommandManager.Execute(command);

                            break;
                        }
					}
                    // удаляем тип улучшения из списка доступных
					types.Remove(type);
				} while (types.Count != 0);
			}
		}
		#endregion
	}
}
