using System;
using System.Collections.Generic;

namespace HastadAttack
{
    /// <summary>
    /// Китайская теорема об остатках
    /// </summary>
    public static class ChineseRemainderTheorem
    {
        /// <summary>
        /// Поиск решения системы линейных уравнений по модулю.
        /// </summary>
        /// <remarks>
        /// Система вида: { x = ri mod ai
        /// </remarks>
        /// <param name="items">Пары: (ri, ai)</param>
        /// <remarks>
        /// Все ai должны быть попарно взаимно просты. Любой ri: 0 <= ri < ai
        /// </remarks>
        /// <returns>Решение системы уравнений</returns>
        public static long Solve(IEnumerable<Tuple<long, long>> items)
        {
            var m = 1L;

            foreach (var item in items)
            {
                m *= item.Item2;
            }

            // Решение по модулю m
            var result = 0L;

            foreach (var item in items)
            {
                var mi = m / item.Item2;
                var revMi = ReversedElement(mi, item.Item2);
                result += item.Item1 * mi * revMi;
            }

            return result % m;
        }

        /// <summary>
        /// Поиск обратного элемента по модулю.
        /// </summary>
        /// <remarks>
        /// a * x = 1 (mod m) -> a * x + m * y = 1
        /// ExtendedGCD(a, m) = r, r = a * x + m * y ->
        /// если r != 1, то обратный не существует, иначе
        /// x - обратный к a.
        /// </remarks>
        /// <param name="element">Элемент</param>
        /// <param name="module">Модуль</param>
        /// <returns>Элемент обратный по модулю</returns>
        private static long ReversedElement(long element, long module)
        {
            var gcd = ExtendedGCD.GCD(element, module, out var x, out var y);

            return gcd != 1
                ? throw new Exception("No solution")
                : (x % module + module) % module;
        }
    }
}
