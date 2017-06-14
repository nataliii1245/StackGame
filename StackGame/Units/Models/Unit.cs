using System;
using System.Collections.Generic;
using StackGame.Observers;
using StackGame.Units.Abilities;
namespace StackGame.Units.Models
{
    public abstract class Unit : IUnit, ICanBeObserved
    {
        public static int count = 0;
		#region Свойства

		/// <summary>
		/// Имя юнита
		/// </summary>
		public virtual string Name { get; }
        /// <summary>
		/// Максимальный урон, который может нанести юнит.
		/// </summary>
		public virtual int Attack { get; }
        /// <summary>
		/// Броня юнита.
		/// </summary>
		public virtual int Defence { get; protected set; } = 0;
        /// <summary>
		/// Текущий  уровень здоровья юнита.
		/// </summary>
		public virtual int Health { get; set; }
        /// <summary>
		/// Максимальный уровень здоровья юнита.
		/// </summary>
        public virtual int MaxHealth { get; }
        /// <summary>
		/// Жив ли юнит?
		/// </summary>

		public virtual bool IsAlive
		{
			get
			{
				return Health > 0;
			}
		}

		/// <summary>
		/// список наблюдателей
		/// </summary>
		internal List<IObserver> listOfObservers = new List<IObserver>();

		#endregion

		#region Инициализаторы

		/// <summary>
		/// Конструктор класса
		/// </summary>
		protected Unit(string name, int health, int attack, int defence)
		{
            Name = name + "-" + count.ToString();
            count++;
			Health = health;
			MaxHealth = health;
			Attack = attack;
            Defence = defence;
		}

		/// <summary>
		/// Конструктор класса
		/// </summary>
		protected Unit(string name)
		{
			Name = name + count.ToString();
			count++;
		}

		#endregion

		#region Методы

		/// <summary>
		/// Метод позволяющий юниту получить урон
		/// </summary>
		public  void TakeDamage(int damage)
		{
			Health -= damage;
            if (Health > MaxHealth)
            {
                Health = MaxHealth;
            }

            if (Health == 0)
            {
				var message = $" ☠️☠️☠️ {this.Name } был убит! ☠️☠️☠️";
				NotifyObservers(message);
            }
		}

		/// <summary>
		/// Преобразовать в строку
		/// </summary>
		public override string ToString()
		{
			return $"{Name} [Уровень жизни: {Health}, Урон: {Attack}, Защита: {Defence}]";
		}

        /// <summary>
        /// Зарегистрировать наблюдателя
        /// </summary> 
        public void RegisterObserver(IObserver observer)
        {
            listOfObservers.Add(observer);
        }

        /// <summary>
        /// Удалить наблюдателя из списка
        /// </summary>
        public void RemoveObserver(IObserver observer)
        {
            listOfObservers.Remove(observer);
        }

        /// <summary>
        /// Оповестить наблюдателя
        /// </summary>
        public void NotifyObservers(object @object)
        {
            foreach (var observer in listOfObservers)
            {
                observer.Update(@object);
            }
        }

        #endregion
    }
}
