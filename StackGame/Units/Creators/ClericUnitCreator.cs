using System.Linq;
using StackGame.Units.Models;
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
		public IUnit CreateUnit(){
			var parameters = StartStats.Stats.Where(p => p.Key == UnitType.ClericUnit).ToList().First();
			return new ArcherUnit(parameters.Value.Name, parameters.Value.Health, parameters.Value.Attack);
        }
    }
}
