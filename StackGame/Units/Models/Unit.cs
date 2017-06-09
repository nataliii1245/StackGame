using System;
namespace StackGame.Units.Models
{
    public abstract class Unit : IUnit
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
		public virtual bool isAlive
		{
			get
			{
				return Health != 0;
			}
		}

		#endregion

		#region Инициализаторы

		/// <summary>
		////Конструктор класса
		/// </summary>
		protected Unit(string name, int health, int attack)
		{
            Name = name + count.ToString();
            count++;
			Health = health;
			MaxHealth = health;
			Attack = attack;
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
				Health = MaxHealth;
			if (Health < 0)
			{
				Health = 0;
				// оповещение о том, что юнит умер!!!
			}
		}

		/// <summary>
		/// Преобразовать в строку
		/// </summary>
		public override string ToString()
		{
			return $"Имя: {Name}, Уровень жизни: {Health}, Урон: {Attack}, Защита: {Defence}";
		}

		#endregion
	}
}
