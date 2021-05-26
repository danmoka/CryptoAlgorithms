namespace HastadAttack
{
    /// <summary>
    /// Расширенный алгоритм Евклида
    /// </summary>
    public static class ExtendedGCD
    {
        /// <summary>
        /// Наибольший общий делитель
        /// </summary>
        /// <param name="a">Первое число</param>
        /// <param name="b">Второе число</param>
        /// <param name="x">Коэффициент Безу</param>
        /// <param name="y">Коэффициент Безу</param>
        /// <returns>Наибольший общий делитель</returns>
        public static long GCD(long a, long b, out long x, out long y)
        {
            if (a == 0)
            {
                x = 0;
                y = 1;

                return b;
            }

            var d = GCD(b % a, a, out var x1, out var y1);

            x = y1 - (b / a) * x1;
            y = x1;

            return d;
        }
    }
}
