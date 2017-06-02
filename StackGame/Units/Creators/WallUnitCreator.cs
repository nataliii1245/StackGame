using System.Linq;
using StackGame.Units.Models;
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
		public IUnit CreateUnit() {
            var parameters = StartStats.Stats.Where(p => p.Key == UnitType.WallUnit).ToList().First();
			return new ArcherUnit(parameters.Value.Name, parameters.Value.Health, parameters.Value.Attack);
        }
    }
}
