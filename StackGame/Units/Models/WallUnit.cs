using SpecialUnits;

using System;
namespace StackGame.Units.Models
{
    public class WallUnit: Unit
    {
		#region Свойства

		/// <summary>
		/// Стена "Гуляй-город"
		/// </summary>
		private readonly GulyayGorod wall;

        public override string Name { get; } = "Гуляй-Город";
        /// <summary>
		/// Здоровье
		/// </summary>
		public override int Health => wall.GetCurrentHealth();
        /// <summary>
		/// Максимальное здоровье
		/// </summary>
		public override int MaxHealth => wall.GetHealth();
        /// <summary>
		/// Защита
		/// </summary>
		public override int Defence => wall.GetDefence();
        /// <summary>
		/// Сила
		/// </summary>
		public override int Attack => wall.GetStrength();
        /// <summary>
		/// Есть ли еще здоровье
		/// </summary>
        public override bool isAlive => !wall.IsDead;

		#endregion




		#region Инициализация

        public WallUnit(string name,int health, int attack, int defence, int price) : base(name, health, attack)
		{
			wall = new GulyayGorod(health, defence, 0);
		}

		#endregion



		#region Методы

		/// <summary>
		/// Получить урон
		/// </summary>
		public override void TakeDamage(int damage)
		{
			wall.TakeDamage(damage);
		}

		#endregion
	}
}
