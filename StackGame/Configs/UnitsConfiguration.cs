using System;
using System.Collections.Generic;
using StackGame.Units.Models;
using System.IO;

namespace StackGame
{
	public enum UnitType
	{
		/// <summary>
		/// Легкий пехотинец
		/// </summary>
		LightInfantryUnit,
		/// <summary>
		/// Тяжелый пехотинец
		/// </summary>
		HeavyInfantryUnit,
		/// <summary>
		/// Лучник
		/// </summary>
		ArcherUnit,
		/// <summary>
		/// Священник
		/// </summary>
		ClericUnit,
		/// <summary>
		/// Маг
		/// </summary>
		WizardUnit,
		/// <summary>
		/// Стена
		/// </summary>
		WallUnit
	}

	public class Parameters
	{
		public string Name;
		public int Attack;
		public int Defence;
		public int Health;
		public int SpecialAbilityPower;
		public int SpecialAbilityRange;
		public int Price;
	}

	public static class StartStats
	{
        public static readonly Dictionary<UnitType, Parameters> Stats = new Dictionary<UnitType, Parameters>()

        {
            {   UnitType.ArcherUnit, new Parameters {
                Name = "Лучник",
                Attack = 8,
                Defence = 4,
                Health = 10,
                SpecialAbilityPower = 14,
                SpecialAbilityRange = 3,
                Price = 100
                }
            },

            {
                UnitType.LightInfantryUnit, new Parameters {
                Name = "Легкий пехотинец",
                Attack = 12,
                Defence = 6,
                Health = 10,
                SpecialAbilityPower = 0,
                SpecialAbilityRange = 1,
                Price = 100
                }
            },

            {
                UnitType.HeavyInfantryUnit, new Parameters {
                Name = "Тяжелый пехотинец",
                Attack = 14,
                Defence = 8,
                Health = 12,
                Price = 200
                }
            },

            {   UnitType.ClericUnit, new Parameters {
                Name = "Клирик",
                Attack = 5,
                Defence = 3,
                Health = 8,
                SpecialAbilityPower = 1,
                SpecialAbilityRange = 4,
                Price = 200
                }
            },

            {   UnitType.WizardUnit, new Parameters {
                Name = "Маг",
                Attack = 5,
                Defence = 3,
                Health = 6,
                SpecialAbilityPower = 20,
                SpecialAbilityRange = 5,
                Price = 250
                }
            },

            {   UnitType.WallUnit, new Parameters {
				Name = "Гуляй-Город",
				Defence = 8,
				Health = 10,
				Price = 150
                }
			}

		};


        public static readonly Dictionary<UnitImprovmentTypes, UnitImprovmentStats> ImprovmentStats = new Dictionary<UnitImprovmentTypes, UnitImprovmentStats> 
		{
			{
                UnitImprovmentTypes.Helmet, new UnitImprovmentStats
				{
					Defence = 10
				}
			},
			{
				UnitImprovmentTypes.Shield, new UnitImprovmentStats
				{
					Defence = 15
				}
			},
			{
				UnitImprovmentTypes.Spear, new UnitImprovmentStats
				{
                    Attack = 10
				}
			},
			{
				UnitImprovmentTypes.Horse, new UnitImprovmentStats
				{
                    Defence = 15,
                    Attack = 10
				}
			}
		};

		public const int TotalPriceOfArmy = 100;
		public static string[] PlayerName = { "First", "Second" };
	}
}