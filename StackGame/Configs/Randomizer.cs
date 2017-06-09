using System;
using System.Collections.Generic;
using System.Linq;
namespace StackGame.Configs
{
    public static class Randomizer
    {
        /// <summary>
        /// Перемешать элементы, поданные на вход
        /// </summary>
		public static IEnumerable<T> IntermixIt <T>(this IEnumerable<T> source)
		{
			Random rnd = new Random();
			return source.OrderBy(item => rnd.Next());
		}

        public static double CalculateChanceOfAction()
        {
			Random rnd = new Random();
			return rnd.Next(101)/100;
        }
    }
}
