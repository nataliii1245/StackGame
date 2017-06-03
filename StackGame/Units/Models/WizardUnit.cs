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

		public void DoSpecialAction(IArmy targetArmy, int unitPosition)
		{
			var random = new Random();
            var chance = random.Next(1000 / DateTime.Now.Millisecond);

            if (chance > 0 && targetArmy.Units[unitPosition] is ICanBeCloned ICanBeClonedUnit)
			{
				var clonedUnit = ICanBeClonedUnit.Clone();
                targetArmy.Units.Add(clonedUnit);
			}
		}

		#endregion
	}
}
