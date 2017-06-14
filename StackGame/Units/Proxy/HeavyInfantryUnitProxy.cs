using System;
using StackGame.Units.Abilities;
using StackGame.Units.Models;
using StackGame.Loggers;

namespace StackGame.Units.Proxy
{
    public class HeavyInfantryUnitProxy : IUnit, ICanBeCloned, ICanBeImproved
    {
        #region Свойства

        private readonly HeavyInfantryUnit heavyInfantryUnit;

        private ILogger logger;

        public virtual string Name => heavyInfantryUnit.Name;

        public virtual int Health 
        {
            get => heavyInfantryUnit.Health; 
            set => heavyInfantryUnit.Health = value;
        }

        public virtual int MaxHealth => heavyInfantryUnit.MaxHealth;

        public virtual int Attack => heavyInfantryUnit.Attack;

        public virtual int Defence => heavyInfantryUnit.Defence;

        public virtual bool IsAlive => heavyInfantryUnit.IsAlive;

        public virtual int NumberOfImprovments => heavyInfantryUnit.NumberOfImprovments;

		#endregion

		#region Инициализация

        public HeavyInfantryUnitProxy(HeavyInfantryUnit heavyInfantryUnit, ILogger logger)
		{
            this.heavyInfantryUnit = heavyInfantryUnit;
            this.logger = logger;
		}

		#endregion


		#region Методы

		/// <summary>
		/// Получить урон
		/// </summary>
		public void TakeDamage(int damage)
		{
            heavyInfantryUnit.TakeDamage(damage);

            var message = $"{ DateTime.Now }: 💢 { this.Name } получил урон { damage }";
			logger.Log(message);

			if (!IsAlive)
			{
                message = $"{ DateTime.Now }: ☠️ { this.Name } умер";
				logger.Log(message);
			}
		}

		public bool CanIBeImprovedWithFeatureOfThisType(Type type)
		{
            return heavyInfantryUnit.CanIBeImprovedWithFeatureOfThisType(type);
		}

		public IUnit Clone()
		{
            var clonedUnit = (HeavyInfantryUnit)heavyInfantryUnit.Clone();
            var clonedUnitProxy = new HeavyInfantryUnitProxy(clonedUnit, logger);

            var message = $"{ DateTime.Now }: \ud83d\udc6f‍♂️ { this.Name } клонирован";
			logger.Log(message);

            return clonedUnitProxy;
		}

        public override string ToString()
        {
            return heavyInfantryUnit.ToString();
        }

        #endregion

    }
}
