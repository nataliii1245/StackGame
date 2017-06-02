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
		public int SpecialActionPower;
		public int SpecialActionRange;
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
                SpecialActionPower = 14,
                SpecialActionRange = 3,
                Price = 100
                }
            },

            {
                UnitType.LightInfantryUnit, new Parameters {
                Name = "Легкий пехотинец",
                Attack = 12,
                Defence = 6,
                Health = 10,
                SpecialActionPower = 0,
                SpecialActionRange = 1,
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
                SpecialActionPower = 1,
                SpecialActionRange = 4,
                Price = 200
                }
            },

            {   UnitType.WizardUnit, new Parameters {
                Name = "Маг",
                Attack = 5,
                Defence = 3,
                Health = 6,
                SpecialActionPower = 20,
                SpecialActionRange = 5,
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

		
		public const int TotalPriceOfArmy = 100;
		public static string[] PlayerName = { "First", "Second" };
	}
}