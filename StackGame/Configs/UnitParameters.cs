using System.Collections.Generic;
namespace StackGame.Configs
{
    /// <summary>
    /// Параметры юнитов
    /// </summary>
    public static class UnitParameters
    {
		public static readonly Dictionary<UnitTypes, UnitParameterTypes> Stats = new Dictionary<UnitTypes, UnitParameterTypes>()

		{
			{   UnitTypes.ArcherUnit, new UnitParameterTypes {
				Name = "Лучник",
				Attack = 8,
				Defence = 4,
				Health = 70,
				SpecialAbilityPower = 14,
				SpecialAbilityRange = 3,
				Price = 100
				}
			},

			{
				UnitTypes.LightInfantryUnit, new UnitParameterTypes {
				Name = "Легкий пехотинец",
				Attack = 12,
				Defence = 6,
				Health = 60,
				SpecialAbilityPower = 0,
				SpecialAbilityRange = 1,
				Price = 100
				}
			},

			{
				UnitTypes.HeavyInfantryUnit, new UnitParameterTypes {
				Name = "Тяжелый пехотинец",
				Attack = 14,
				Defence = 8,
				Health = 90,
				Price = 200
				}
			},

			{   UnitTypes.ClericUnit, new UnitParameterTypes {
				Name = "Клирик",
				Attack = 5,
				Defence = 3,
				Health = 80,
				SpecialAbilityPower = 14,
				SpecialAbilityRange = 4,
				Price = 200
				}
			},

			{   UnitTypes.WizardUnit, new UnitParameterTypes {
				Name = "Маг",
				Attack = 5,
				Defence = 3,
				Health = 60,
				SpecialAbilityPower = 20,
				SpecialAbilityRange = 5,
				Price = 250
				}
			},

			{   UnitTypes.WallUnit, new UnitParameterTypes {
				Name = "Гуляй-Город",
				Defence = 0,
				Health = 100,
				Price = 150
				}
			}
		};
    }
}
