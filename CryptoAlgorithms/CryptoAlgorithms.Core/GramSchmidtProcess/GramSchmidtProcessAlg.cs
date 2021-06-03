using CryptoAlgorithms.Core.Core;
using System;

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
            // добавляем первый вектор в набор ортогональных векторов
            orthogonalMatrix[0] = matrix[0];
            coefficients = new Matrix(matrix.RowNumber, matrix.ColumnNumber);

            // для всех остальных
            for (int i = 1; i < orthogonalMatrix.RowNumber; i++)
            {
                // добавляем вектор в набор ортогональных векторов
                orthogonalMatrix[i] = matrix[i];

                // считаем проекцию этого вектора на векторы, которые добавлены в набор ортогональных
                for (int j = 0; j < i; j++)
                {
                    var vector = orthogonalMatrix[j];
                    coefficients[i, j] = matrix[i].DotProduct(vector) / vector.DotProduct(vector);
                    // и вычитаем из него
                    orthogonalMatrix[i] -= coefficients[i, j] * vector;
                }

                var normBi2 = orthogonalMatrix[i].DotProduct(orthogonalMatrix[i]);

                if (normBi2 == 0.0)
                {
                    throw new ArgumentException("matrix is not a system of linearly independent vectors");
                }
            }
        }
    }
}