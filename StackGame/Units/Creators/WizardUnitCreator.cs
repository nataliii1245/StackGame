using System.Linq;
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
		public IUnit CreateUnit() {
            var parameters = StartStats.Stats.Where(p => p.Key == UnitType.WizardUnit).ToList().First();
			return new ArcherUnit(parameters.Value.Name, parameters.Value.Health, parameters.Value.Attack);
        }
    }
}
