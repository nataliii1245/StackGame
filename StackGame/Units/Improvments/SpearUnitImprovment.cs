using StackGame.Units.Models;
using StackGame.Units.Abilities;
using StackGame.Configs;
namespace StackGame.Units.Improvments
{
    public class SpearUnitImprovment<T>: UnitToBeImproved<T> where T : IUnit, ICanBeImproved, ICanBeCloned
    {
		
        #region Свойства

        /// <summary>
        /// Сила копья
        /// </summary>
        private int spearAttack;

        public override int Attack => base.Attack + spearAttack;

		#endregion

		#region Инициализация

		public SpearUnitImprovment(T unit) : base(unit)
        {
            var param = UnitImprovmentParameters.ImprovmentStats[UnitImprovmentTypes.Spear];
            spearAttack = param.Attack;
		}

		#endregion

		#region Методы

		public override IUnit Clone()
		{
            var clonedUnit = (T)((T)Unit).Clone();
            var improvedClonedUnit = new SpearUnitImprovment<T>(clonedUnit);

			return improvedClonedUnit;
		}

		public override string ToString()
		{
            return $"{ base.ToString() } |копье ⇐ с атакой: {spearAttack}|";
		}

		#endregion
	}
}
