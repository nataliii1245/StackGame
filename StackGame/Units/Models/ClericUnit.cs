using System;
using System.Collections.Generic;
using System.Linq;
using StackGame.Units.Abilities;
using StackGame.Army;
using StackGame.Game;
using StackGame.Commands;

namespace StackGame.Units.Models
{
    public class ClericUnit: Unit, ICanBeCloned, ICanBeHealed, IHaveSpecialAbility
    {
		#region Свойства

        public int SpecialAbilityRange { get; } = StartStats.Stats.Where(p => p.Key == UnitType.ClericUnit).First().Value.SpecialAbilityRange;

		public int SpecialAbilityPower { get; } = StartStats.Stats.Where(p => p.Key == UnitType.ClericUnit).First().Value.SpecialAbilityPower;

        public bool isFriendly { get; private set; } = true;
		#endregion

		#region Инициализация

		public ClericUnit(string name, int health, int attack) : base(name, health, attack)
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

		// реализация специального действия для клирика
		public void DoSpecialAction(IArmy targetArmy, IEnumerable<int> possibleUnitsPositions, int position)
		{
			// Генерируем рандомную вероятность попадания 
			Random random = new Random();
			var chance = random.Next(100) / 100;

			// 
			if (chance != 0) 

			{
				// генерируем список доступных юнитов
				var possibleTargetUnits = new List<ICanBeHealed>();

				// для каждого индекса доступных целей
				foreach (var index in possibleUnitsPositions)
				{
					var unit = targetArmy.Units[index];
					// если юнит жив и может быть исцелен
					if (unit.isAlive && unit is ICanBeHealed ICanBeHealedUnit)
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
				var targetUnit = possibleTargetUnits[random.Next(possibleTargetUnits.Count)];
				// отправляем юнита лечиться
                var command = new HealCommand(this, (IUnit)targetUnit, this.SpecialAbilityPower);
				Engine.GetEngine().CommandManager.Execute(command);

			}
		}

		#endregion
	}
}
