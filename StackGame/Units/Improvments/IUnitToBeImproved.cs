using StackGame.Units.Models;
namespace StackGame.Units.Improvments
{
    public interface IUnitToBeImproved
    {
		#region Свойства

		/// <summary>
		/// Стандартный юнит
		/// </summary>
		IUnit Unit { get; }

		#endregion
	}
}
