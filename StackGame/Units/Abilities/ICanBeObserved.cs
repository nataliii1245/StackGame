using StackGame.Observers;
namespace StackGame.Units.Abilities
{
    public interface ICanBeObserved
    {
        #region Методы

        /// <summary>
        ///  Добавить наблюдателя
        /// </summary>
        void RegisterObserver(IObserver observer);
        /// <summary>
        /// Удалить наблюдателя
        /// </summary>
        void RemoveObserver(IObserver observer);
        /// <summary>
        /// Оповестить наблюдателя
        /// </summary>
        void NotifyObservers(object @object);

        #endregion
    }
}
