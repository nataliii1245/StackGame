using System;
using System.Collections.Generic;
using System.Linq;
using StackGame.Configs;
using StackGame.Units.Abilities;
using StackGame.Army;
using StackGame.Game;
using StackGame.Commands;

namespace StackGame.Units.Models
{
    public class WizardUnit: Unit, ICanBeHealed, IHaveSpecialAbility
    {
        public int SpecialAbilityRange { get; } = UnitParameters.Stats.Where(p => p.Key == UnitTypes.WizardUnit).First().Value.SpecialAbilityRange;

        public int SpecialAbilityPower { get; } = UnitParameters.Stats.Where(p => p.Key == UnitTypes.WizardUnit).First().Value.SpecialAbilityPower;

        public bool isFriendly { get; private set; } = true;

        #region Инициализация

        public WizardUnit(string name, int health, int attack, int defence) : base(name, health, attack, defence)
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
            double chance = Randomizer.CalculateChanceOfAction();

			// 
			if (chance >= 0.85)
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
					if (unit.IsAlive && unit is ICanBeCloned ICanBeClonedUnit)
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
				var targetUnit = possibleTargetUnits[Randomizer.random.Next(possibleTargetUnits.Count)];
                //  отправляем юнита клонироваться
				//var clonedUnit = targetUnit.Clone();
				//targetArmy.Units.Add(clonedUnit);

                var command = new CloneCommand(this, targetUnit, targetArmy);
				Engine.GetInstance().CommandManager.Execute(command);
			}
		}

		#endregion
	}
}
