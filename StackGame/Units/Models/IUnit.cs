namespace StackGame.Units.Models
{
	/// <summary>
	/// Интерфейс, описывающий основные поля, универсальные для всех классов
	/// </summary>
	public interface IUnit
    {
		/// <summary>
		/// Имя юнита
		/// </summary>
		string Name { get; }

		/// <summary>
		/// Максимальный урон, который может нанести юнит.
		/// </summary>
		int Attack { get; }

		/// <summary>
		/// Броня юнита.
		/// </summary>
		int Defence { get; }

		/// <summary>
		/// Текущий  уровень здоровья юнита.
		/// </summary>
        int Health { get; set; }

		/// <summary>
		/// Максимальный уровень здоровья юнита.
		/// </summary>
		int MaxHealth { get; }

        bool IsAlive { get; }

		void TakeDamage(int damage);
		string ToString();
    }
}
