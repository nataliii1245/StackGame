using System.Linq;
using StackGame.Configs;
using StackGame.Units.Models;
namespace StackGame.Units.Creators
{
	/// <summary>
	/// Создатель мага
	/// </summary>
    public class WizardUnitCreator: IUnitCreator
    {
		/// <summary>
		/// Создать мага
		/// </summary>
		public IUnit CreateUnit() 
        {
            var parameters = UnitParameters.Stats.Where(p => p.Key == UnitTypes.WizardUnit).ToList().First();
            return new WizardUnit(parameters.Value.Name, parameters.Value.Health, parameters.Value.Attack, parameters.Value.Defence);
        }
    }
}
