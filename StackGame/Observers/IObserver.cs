using System;
namespace StackGame.Observers
{
    public interface IObserver
    {
        #region Методы

        void Update(object @object);
        #endregion
    }
}
