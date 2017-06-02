using System.Linq;
using StackGame.Units.Models;
namespace StackGame.Units.Creators
{
    /// <summary>
    /// Интерфейс создания юнита
    /// </summary>
    public interface IUnitCreator
    {
        #region Методы

        IUnit CreateUnit();

        #endregion
    }
}
