using CryptoAlgorithms.Core.Core;
using CryptoAlgorithms.Core.GramSchmidtProcess;
using System;

namespace CryptoAlgorithms.Core.LLL
{
    /// <summary>
    /// ���������� LLL-������������ ������
    /// </summary>
    public static class LLLAlg
    {
        /// <summary>
        /// ������ LLL-����������� �����
        /// </summary>
        /// <param name="basis">�����</param>
        /// <returns>LLL-����������� �����</returns>
        public static Matrix Build(Matrix basis)
        {
            if (basis.RowNumber < 2)
            {
                throw new ArgumentException($"basis row number is {basis.RowNumber}");
            }

            var k = 1;
            // ���������� �������������� ������ (+ ������� �������������)
            GramSchmidtProcessAlg.Solve(basis, out var coefficients, out var orthogonalMatrix);

            // ��� ���� �������� ������, ������� �� 1-��
            while (k < basis.RowNumber)
            {
                // ����������
                for (int j = k - 1; j > -1; j--)
                {
                    if (Math.Abs(coefficients[k, j]) > 0.5)
                    {
                        basis[k] -= Math.Round(coefficients[k, j]) * basis[j];
                        GramSchmidtProcessAlg.Solve(basis, out coefficients, out orthogonalMatrix);
                    }
                }

                var currentVectorDotProduct = orthogonalMatrix[k].DotProduct(orthogonalMatrix[k]);
                var previousVectorDotProduct = orthogonalMatrix[k - 1].DotProduct(orthogonalMatrix[k - 1]);

                if ((0.75 - Math.Pow(coefficients[k, k - 1], 2)) * previousVectorDotProduct > currentVectorDotProduct)
                {
                    // ������������
                    basis.SwapRows(k, k - 1);
                    GramSchmidtProcessAlg.Solve(basis, out coefficients, out orthogonalMatrix);
                    k = Math.Max(k - 1, 1);
                }
                else
                {
                    k++;
                }
            }

            return basis;
        }
    }
}