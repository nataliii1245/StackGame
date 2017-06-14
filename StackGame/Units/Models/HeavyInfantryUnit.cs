using System;
using StackGame.Units.Abilities;
using System.Linq;
namespace StackGame.Units.Models
{
    public class HeavyInfantryUnit: Unit, ICanBeCloned, ICanBeImproved
    {
        #region Свойства

        public int NumberOfImprovments => 0;

        #endregion

        #region Инициализация

        public HeavyInfantryUnit(string name, int health, int attack, int defence) : base(name, health, attack, defence) { }

		#endregion

        #region Методы

		public bool CanIBeImprovedWithFeatureOfThisType(Type type)
		{
			return true;
		}

        public IUnit Clone()
		{
			var clonedUnit = (Unit)MemberwiseClone();
			clonedUnit.listOfObservers = listOfObservers.Select(observer => observer).ToList();

			return clonedUnit;
		}

		#endregion
	}
}
