using CryptoAlgorithms.Core.Core;
using CryptoAlgorithms.Core.LLL;
using System;

namespace CryptoAlgorithms.Core.Babai
{
    /// <summary>
    /// �������� ����� ���������� ��������� �����
    /// </summary>
    public static class BabaiNearestPlaneAlg
    {
        /// <summary>
        /// ������� ��������� ����� � ��������� �� ����������������� ���������
        /// </summary>
        /// <param name="basis">����� �������</param>
        /// <param name="x">�����</param>
        /// <returns>��������� ����� � ��������� �� ����������������� ���������</returns>
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
            //GramSchmidtProcessAlg.Solve(lllBasis, out var coefficients, out var orthogonalMatrix);

            var xn = (Vector)x.Clone();
            var nearest = new Vector(x.Length);

            for (int i = nearest.Length - 1; i > -1; i--)
            {
                var r = xn.DotProduct(lllBasis[i]) / lllBasis[i].DotProduct(lllBasis[i]);
                var m = (int)Math.Floor(r + 0.5);
                var y = m * lllBasis[i];

                nearest += y;
                xn = xn + ((m - r) * lllBasis[i]) - y;
            }

            return nearest;
        }
    }
}