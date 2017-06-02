using System.Linq;
using StackGame.Units.Models;
namespace StackGame.Units.Creators
{
	/// <summary>
	/// Создатель лучника
	/// </summary>
	public class ArcherUnitCreator: IUnitCreator
    {
		/// <summary>
		/// Создать лучника
		/// </summary>
		public IUnit CreateUnit() {
            var parameters = StartStats.Stats.Where(p => p.Key == UnitType.ArcherUnit).ToList().First();
            return new ArcherUnit(parameters.Value.Name, parameters.Value.Health, parameters.Value.Attack);
        }
    }
}
