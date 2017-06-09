using System;
using System.Collections.Generic;
using StackGame.Units.Abilities;
using StackGame.Army;
using StackGame.Configs;
using System.Linq;
namespace StackGame.Units.Models
{
    public class HeavyInfantryUnit: Unit, ICanBeCloned, ICanBeImproved
    {
        #region Свойства

        public int NumberOfImprovments => 0;

        #endregion



        #region Инициализация

        public HeavyInfantryUnit(string name, int health, int attack) : base(name, health, attack)
        { }

		#endregion



		#region Методы

		public bool CanIBeImprovedWithFeatureOfThisType(Type type)
		{
			return true;
		}


		public IUnit Clone()
		{
			return (IUnit)MemberwiseClone();
		}
		#endregion
	}
}
