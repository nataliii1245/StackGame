using System.Collections.Generic;
namespace StackGame.Configs
{
    /// <summary>
    /// Параметры для каждого типа улучшений
    /// </summary>
    public static class UnitImprovmentParameters
    {
        public static readonly Dictionary<UnitImprovmentTypes, UnitImprovmentParameterTypes> ImprovmentStats = new Dictionary<UnitImprovmentTypes, UnitImprovmentParameterTypes>
		{
			{
                UnitImprovmentTypes.Helmet, new UnitImprovmentParameterTypes
				{
					Defence = 10
				}
			},
			{
                UnitImprovmentTypes.Shield, new UnitImprovmentParameterTypes
				{
					Defence = 15
				}
			},
			{
                UnitImprovmentTypes.Spear, new UnitImprovmentParameterTypes
				{
					Attack = 10
				}
			},
			{
                UnitImprovmentTypes.Horse, new UnitImprovmentParameterTypes
				{
					Defence = 15,
					Attack = 10
				}
			}
		};

	}
}
