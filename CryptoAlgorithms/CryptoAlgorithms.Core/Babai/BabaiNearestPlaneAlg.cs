using CryptoAlgorithms.Core.Core;
using CryptoAlgorithms.Core.GramSchmidtProcess;
using CryptoAlgorithms.Core.LLL;
using System;

namespace CryptoAlgorithms.Core.Babai
{
    /// <summary>
    /// Алгоритм Бабаи нахождения ближайшей точки
    /// </summary>
    public static class BabaiNearestPlaneAlg
    {
        /// <summary>
        /// Находит ближайшую точку с точностью до экспоненциального множителя
        /// </summary>
        /// <param name="basis">Базис решетки</param>
        /// <param name="x">Точка</param>
        /// <returns>Ближайшую точку с точностью до экспоненциального множителя</returns>
        public static Vector GetNearest(Matrix basis, Vector x)
        {
            if (basis is null)
            {
                throw new ArgumentNullException("basis is null");
            }

            if (x is null)
            {
                throw new ArgumentNullException("x is null");
            }

            if (basis.RowNumber != x.Length)
            {
                throw new ArgumentException($"number of basis vectors is ${basis.RowNumber}, the point length is ${x.Length}");
            }

            var lllBasis = LLLAlg.Build(basis);
            GramSchmidtProcessAlg.Solve(lllBasis, out var coefficients, out var orthogonalMatrix);

            var xn = (Vector)x.Clone();
            var nearest = new Vector(x.Length);

            for (int i = nearest.Length - 1; i > -1; i--)
            {
                var r = xn.DotProduct(orthogonalMatrix[i]) / orthogonalMatrix[i].DotProduct(orthogonalMatrix[i]);
                var m = (int)Math.Floor(r + 0.5);
                var y = m * lllBasis[i];

                nearest += y;
                xn = xn + ((m - r) * orthogonalMatrix[i]) - y;
            }

            return nearest;
        }
    }
}