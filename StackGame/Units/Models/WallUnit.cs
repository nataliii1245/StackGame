using SpecialUnits;
using System.Reflection;

namespace StackGame.Units.Models
{
    public class WallUnit: Unit
    {
		#region Свойства

		/// <summary>
		/// Стена "Гуляй-город"
		/// </summary>
		private readonly GulyayGorod wall;
        /// <summary>
		/// Здоровье
		/// </summary>
		public override int Health
		{
			get => wall.GetCurrentHealth();
			set
			{
				if (value < Health)
				{
					var damage = Health - value;
					wall.TakeDamage(damage);
				}
				else
				{
					wall.GetType().GetField("_currentHealth", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(wall, value);
				}
			}
		}
        /// <summary>
		/// Максимальное здоровье
		/// </summary>
		public override int MaxHealth => wall.GetHealth();
        /// <summary>
		/// Сила
		/// </summary>
		public override int Attack => wall.GetStrength();
        /// <summary>
		/// Есть ли еще здоровье
		/// </summary>
        public override bool IsAlive => !wall.IsDead;

		#endregion

        #region Инициализация

        public WallUnit(string name,int health, int defence, int price) : base(name)
		{
			wall = new GulyayGorod(health, defence, price);
		}

		#endregion



		#region Методы



		#endregion
	}
}
