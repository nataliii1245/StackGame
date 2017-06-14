using System.Linq;
using StackGame.Units.Models;
using StackGame.Configs;
namespace StackGame.Units.Creators
{
	/// <summary>
	/// Создатель клирика
	/// </summary>
    public class ClericUnitCreator: IUnitCreator
    {
		/// <summary>
		/// Создать клирика
		/// </summary>
		public IUnit CreateUnit()
        {
            var parameters = UnitParameters.Stats.Where(p => p.Key == UnitTypes.ClericUnit).ToList().First();
			return new ClericUnit(parameters.Value.Name, parameters.Value.Health, parameters.Value.Attack, parameters.Value.Defence);
        }
    }
}
