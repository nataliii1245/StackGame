﻿using System;
using System.Collections.Generic;
using System.Linq;
using StackGame.Units.Abilities;
using StackGame.Army;
using StackGame.Game;
using StackGame.Commands;

namespace StackGame.Units.Models
{
    /// <summary>
    /// Лучник, может быть исцелен, может быть клонирован, имеет специальную особенность
    /// </summary>
    public class ArcherUnit: Unit, ICanBeHealed, ICanBeCloned, IHaveSpecialAbility
    {
        
        #region Свойства

        /// <summary>
        /// устанавливаем радиус специального действия
        /// </summary>
        public int SpecialAbilityRange { get; } = StartStats.Stats.Where( p => p.Key == UnitType.ArcherUnit).First().Value.SpecialAbilityRange;

		/// <summary>
		/// устанавливаем силу специального действия
		/// </summary>
		public int SpecialAbilityPower { get; } = StartStats.Stats.Where(p => p.Key == UnitType.ArcherUnit).First().Value.SpecialAbilityPower;

        public bool isFriendly { get; private set; } = false;

		#endregion



        #region Инициализация

        /// <summary>
        /// конструктор лучника
        /// </summary>
		public ArcherUnit(string name, int health, int attack) : base(name, health, attack)
		{ }

		#endregion



		#region Методы

        /// <summary>
        /// метод лечения для лучника
        /// </summary>
		public void Heal(int healthPower)
		{
			Health += healthPower;
			if (Health > MaxHealth)
			{
				Health = MaxHealth;
			}
		}

        /// <summary>
        /// метод клонирования лучника
        /// </summary>
		public IUnit Clone()
		{
            var clonedUnit = (Unit)MemberwiseClone();
            clonedUnit.listOfObservers = listOfObservers.Select(observer => observer).ToList();

			return clonedUnit;
        }


        // реализация специального действия для лучника
        public void DoSpecialAction(IArmy targetArmy, IEnumerable<int> possibleUnitsPositions, int position)
		{
			// Генерируем рандомную вероятность попадания 
			Random random = new Random();
            var chance = random.Next(100)/100;

            // 
            if (chance != 0)

            {
                // генерируем список доступных юнитов
                var possibleTargetUnits = new List<IUnit>();
				
                // для каждого индекса доступных целей
                foreach (var index in possibleUnitsPositions)
				{
					var unit = targetArmy.Units[index];
					// если юнит жив
                    if (unit.isAlive)
					{
                        // добавляем его в список юнитов, на которых мы можем повлиять
						possibleTargetUnits.Add(unit);
					}
				}

                // если массив юнитов, на которых мы можем повлиять пуст
				if (possibleTargetUnits.Count == 0)
				{
					return;
				}

                //  выбираем рандомно юнита из списка доступных
				var targetUnit = possibleTargetUnits[random.Next(possibleTargetUnits.Count)];
                // отправляем юнита получать урон
                var command = new HitCommand(this, targetUnit, this.SpecialAbilityPower);
				Engine.GetEngine().CommandManager.Execute(command);
	
            }
		}

		#endregion
	}
}
