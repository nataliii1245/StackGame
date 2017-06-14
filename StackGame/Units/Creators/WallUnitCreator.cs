using System.Linq;
using StackGame.Units.Models;
using StackGame.Configs;
namespace StackGame.Units.Creators
{
	/// <summary>
	/// Создатель стены "Гуляй-город"
	/// </summary>
    public class WallUnitCreator: IUnitCreator
    {
		/// <summary>
		/// Создать стену "Гуляй-город"
		/// </summary>
		public IUnit CreateUnit() 
        {
            var parameters = UnitParameters.Stats.Where(p => p.Key == UnitTypes.WallUnit).ToList().First();
            return new WallUnit(parameters.Value.Name, parameters.Value.Health, parameters.Value.Defence, parameters.Value.Price);
        }
    }
}
