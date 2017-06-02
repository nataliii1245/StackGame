using System.Linq;
using StackGame.Units.Models;
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
		public IUnit CreateUnit() {
            var parameters = StartStats.Stats.Where(p => p.Key == UnitType.LightInfantryUnit).ToList().First();
			return new ArcherUnit(parameters.Value.Name, parameters.Value.Health, parameters.Value.Attack);
        }
    }
}
