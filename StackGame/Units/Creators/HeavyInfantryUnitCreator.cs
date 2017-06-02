using System.Linq;
using StackGame.Units.Models;
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
		public IUnit CreateUnit() {
            var parameters = StartStats.Stats.Where(p => p.Key == UnitType.HeavyInfantryUnit).ToList().First();
			return new ArcherUnit(parameters.Value.Name, parameters.Value.Health, parameters.Value.Attack);
        }
    }
}
