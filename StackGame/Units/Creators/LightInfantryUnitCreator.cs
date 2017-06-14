using System.Linq;
using StackGame.Units.Models;
using StackGame.Configs;
namespace StackGame.Units.Creators
{
	/// <summary>
	/// Создатель легкого пехотинца
	/// </summary>
    public class LightInfantryUnitCreator: IUnitCreator
    {
		/// <summary>
		/// Создать легкого пехотинца
		/// </summary>
		public IUnit CreateUnit() 
        {
            var parameters = UnitParameters.Stats.Where(p => p.Key == UnitTypes.LightInfantryUnit).ToList().First();
            return new LightInfantryUnit(parameters.Value.Name, parameters.Value.Health, parameters.Value.Attack, parameters.Value.Defence);
        }
    }
}
