using System;
using CryptoAlgorithms.Core.Core;

namespace CryptoAlgorithms.Core.GramSchmidtProcess
{
    /// <summary>
    /// Процесс Грама-Шмидта
    /// </summary>
    public static class GramSchmidtProcessAlg
    {
        /// <summary>
        /// Процесс ортогонализации
        /// </summary>
        /// <param name="matrix">Матрица векторов</param>
        /// <param name="coefficients">Коэффициенты</param>
        /// <param name="orthogonalMatrix">Ортогональная система векторов</param>
        public static void Solve(Matrix matrix, out Matrix coefficients, out Matrix orthogonalMatrix)
        {
            if (matrix is null)
            {
                throw new NullReferenceException("matrix is null");
            }

            orthogonalMatrix = new Matrix(matrix.RowNumber, matrix.ColumnNumber);
            orthogonalMatrix[0] = matrix[0];
            coefficients = new Matrix(matrix.RowNumber, matrix.ColumnNumber);

            for (int i = 1; i < orthogonalMatrix.RowNumber; i++)
            {
                orthogonalMatrix[i] = matrix[i];

                for (int j = 0; j < i; j++)
                {
                    var vector = orthogonalMatrix[j];
                    coefficients[i, j] = matrix[i].DotProduct(vector) / vector.DotProduct(vector);
                    orthogonalMatrix[i] -= coefficients[i, j] * vector;
                }
            }
        }
    }
}