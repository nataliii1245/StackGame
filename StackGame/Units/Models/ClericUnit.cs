using System.Collections.Generic;
using System.Linq;
using StackGame.Configs;
using StackGame.Units.Abilities;
using StackGame.Army;
using StackGame.Game;
using StackGame.Commands;

namespace StackGame.Units.Models
{
    public class ClericUnit: Unit, ICanBeCloned, ICanBeHealed, IHaveSpecialAbility
    {
		#region Свойства

        public int SpecialAbilityRange { get; } = UnitParameters.Stats.Where(p => p.Key == UnitTypes.ClericUnit).First().Value.SpecialAbilityRange;

        public int SpecialAbilityPower { get; } = UnitParameters.Stats.Where(p => p.Key == UnitTypes.ClericUnit).First().Value.SpecialAbilityPower;

        public bool isFriendly { get; private set; } = true;

		#endregion

		#region Инициализация

        public ClericUnit(string name, int health, int attack, int defence) : base(name, health, attack, defence) { }

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

		// реализация специального действия для клирика
		public void DoSpecialAction(IArmy targetArmy, IEnumerable<int> possibleUnitsPositions, int position)
		{
			// Генерируем рандомную вероятность попадания 
			double chance = Randomizer.CalculateChanceOfAction();

			if (chance >= 0.7) 
			{
				// генерируем список доступных юнитов
				var possibleTargetUnits = new List<ICanBeHealed>();

				// для каждого индекса доступных целей
				foreach (var index in possibleUnitsPositions)
				{
					// исключаем из рассмотрения свою собственную позицию
                    if (index == position)
					{
						continue;
					}

					var unit = targetArmy.Units[index];
					// если юнит жив и может быть исцелен
                    if (unit.IsAlive && unit.Health < unit.MaxHealth && unit is ICanBeHealed ICanBeHealedUnit)
					{
						// добавляем его в список юнитов, на которых мы можем повлиять
                        possibleTargetUnits.Add(ICanBeHealedUnit);
					}
				}

				// если массив юнитов, на которых мы можем повлиять пуст
				if (possibleTargetUnits.Count == 0)
				{
					return;
				}

				//  выбираем рандомно юнита из списка доступных
				var targetUnit = possibleTargetUnits[Randomizer.random.Next(possibleTargetUnits.Count)];
				// отправляем юнита лечиться
                var command = new HealCommand(this, (IUnit)targetUnit, SpecialAbilityPower);
				Engine.GetInstance().CommandManager.Execute(command);

			}
		}

		#endregion
	}
}
