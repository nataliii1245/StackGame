using System.Linq;
using StackGame.Units.Models;
using StackGame.Configs;
namespace StackGame.Units.Creators
{
	/// <summary>
	/// Создатель тяжелого пехотинца
	/// </summary>
    public class HeavyInfantryUnitCreator: IUnitCreator
    {
		/// <summary>
        /// Создать тяжелого пехотинца 
		/// </summary>
		public IUnit CreateUnit()
        {
            var parameters = UnitParameters.Stats.Where(p => p.Key == UnitTypes.HeavyInfantryUnit).ToList().First();
            return new HeavyInfantryUnit(parameters.Value.Name, parameters.Value.Health, parameters.Value.Attack, parameters.Value.Defence);
        }
    }
}
