using System;
using System.Collections.Generic;
using System.Linq;
namespace StackGame
{
    /// <summary>
    /// Класс для генерации рандомных величин
    /// </summary>
    public static class Randomizer
    {
        public static readonly Random random = new Random();
        /// <summary>
        /// Перемешать элементы, поданные на вход
        /// </summary>
		public static IEnumerable<T> IntermixIt <T>(this IEnumerable<T> source)
		{
			return source.OrderBy(item => random.Next());
		}

        public static double CalculateChanceOfAction()
        {
			return random.NextDouble();
        }
    }
}
