﻿using System;
using StackGame.Units.Models;
using StackGame.Units.Abilities;
namespace StackGame.Units.Improvments
{
	/// <summary>
	/// Улучшение юнита
	/// </summary>
    /// Указывем, что UnitToBeImproved может использовать в качестве T только классы, которые реализуют IUnit, ICanBeCloned, ICanBeImproved
    public abstract class UnitToBeImproved<T> : IUnit, ICanBeImproved, ICanBeCloned where T: IUnit, ICanBeCloned, ICanBeImproved
    {
        
		#region Свойства

		/// <summary>
		/// Улучшаемая единица армии
		/// </summary>
		protected T unit;

        public string Name => unit.Name;
		public int Health => unit.Health;
        public int MaxHealth => unit.MaxHealth;
        public virtual int Attack => unit.Attack;
		public virtual int Defence => unit.Defence;

        public bool isAlive => unit.isAlive;

        #endregion



        #region Инициализация

		protected UnitToBeImproved(T unit)
		{
			this.unit = unit;
		}

		#endregion



        #region Методы

		/// <summary>
		/// Метод, который показывает, может ли быть навешено улучшение 
		/// </summary>
		public bool CanIBeImprovedWithFeatureOfThisType(Type type)
		{
            // получаем тип текущего объекта, отбрасываем параметр -> получаем универсальный параметр
            // для нашего конкретного случая на месте универсального параметра мог стоять HeavyInfantryUnit
            // сравниваем с тем улучшением, которое хотим навесить
			if (GetType().GetGenericTypeDefinition() == type)
			{
				return false;
			}
            // иначе возвращаем истину
            return unit.CanIBeImprovedWithFeatureOfThisType(type);
		}

        public abstract IUnit Clone();

		public virtual void TakeDamage(int damage)
		{
			unit.TakeDamage(damage);
		}

		#endregion
	}
}
