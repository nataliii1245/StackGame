using System;
using System.Linq;
using System.Collections.Generic;
using StackGame.Units.Abilities;
using StackGame.Army;
namespace StackGame.Units.Models
{
    public class WizardUnit: Unit, ICanBeHealed, IHaveSpecialAbility
    {
        public int SpecialAbilityRange { get; } = StartStats.Stats.Where(p => p.Key == UnitType.WizardUnit).First().Value.SpecialAbilityRange;
        public int SpecialAbilityPower { get; } = StartStats.Stats.Where(p => p.Key == UnitType.WizardUnit).First().Value.SpecialAbilityPower;

        #region Инициализация

        public WizardUnit(string name, int health, int attack) : base(name, health, attack)
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

		// реализация специального действия для мага
		public void DoSpecialAction(IArmy targetArmy, IEnumerable<int> possibleUnitsPositions, int position)
		{
			// Генерируем рандомную вероятность попадания 
			Random random = new Random();
			var chance = random.Next(100) / 100;

			// 
			if (chance != 0)

			{
				// генерируем список доступных юнитов
                var possibleTargetUnits = new List<ICanBeCloned>();

				// для каждого индекса доступных целей
				foreach (var index in possibleUnitsPositions)
				{
                    if (index == position)
                    {
                        continue;
                    }

					var unit = targetArmy.Units[index];
					// если юнит жив
					if (unit.isAlive && unit is ICanBeCloned ICanBeClonedUnit)
					{
						// добавляем его в список юнитов, на которых мы можем повлиять
						possibleTargetUnits.Add(ICanBeClonedUnit);
					}
				}

                if (possibleTargetUnits.Count == 0)
				{
					return;
				}

				//  выбираем рандомно юнита из списка доступных
				var targetUnit = possibleTargetUnits[random.Next(possibleTargetUnits.Count)];
                //  отправляем юнита клонироваться
				var clonedUnit = targetUnit.Clone();
				targetArmy.Units.Add(clonedUnit);

                Console.WriteLine($"{ToString()} клонировал {targetUnit.ToString()}");
			}
		}

		#endregion
	}
}
