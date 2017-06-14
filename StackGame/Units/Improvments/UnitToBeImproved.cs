﻿using System;
using StackGame.Units.Models;
using StackGame.Units.Abilities;
namespace StackGame.Units.Improvments
{
	/// <summary>
	/// Улучшение юнита
	/// </summary>
    /// Указывем, что UnitToBeImproved может использовать в качестве T только классы, которые реализуют IUnit, ICanBeCloned, ICanBeImproved
    public abstract class UnitToBeImproved<T> : IUnitToBeImproved, IUnit, ICanBeImproved, ICanBeCloned where T: IUnit, ICanBeCloned, ICanBeImproved
    {
        
		#region Свойства

		/// <summary>
		/// Улучшаемая единица армии
		/// </summary>
        public IUnit Unit { get; protected set; }

        public string Name => Unit.Name;
		public int Health
		{
			get => Unit.Health;
			set => Unit.Health = value;
		}
        public int MaxHealth => Unit.MaxHealth;
        public virtual int Attack => Unit.Attack;
		public virtual int Defence => Unit.Defence;

        public bool IsAlive => Unit.IsAlive;

        public int NumberOfImprovments => ((T)Unit).NumberOfImprovments + 1;

        #endregion

        #region Инициализация

		protected UnitToBeImproved(T unit)
		{
			this.Unit = unit;
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
            return ((T)Unit).CanIBeImprovedWithFeatureOfThisType(type);
		}

        public abstract IUnit Clone();

		public virtual void TakeDamage(int damage)
		{
			Unit.TakeDamage(damage);
		}

        public override string ToString()
        {
            return Unit.ToString();
        }

		#endregion
	}
}
