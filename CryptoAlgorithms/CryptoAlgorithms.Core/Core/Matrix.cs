using System;
using System.Text;

namespace CryptoAlgorithms.Core.Core
{
    /// <summary>
    /// Матрица
    /// </summary>
    public class Matrix
    {
        private readonly double[,] _values;

        public int RowNumber { get => _values.GetLength(0); }

        public int ColumnNumber { get => _values.GetLength(1); }

        public Matrix(double[,] values)
        {
            if (values is null)
            {
                throw new ArgumentNullException("values is null");
            }

            _values = values;
        }

        public Matrix(int rowNumber, int columnNumber)
        {
            _values = new double[rowNumber, columnNumber];
        }

        public Vector this[int index]
        {
            get
            {
                if (index < 0 || index > RowNumber)
                {
                    throw new IndexOutOfRangeException($"index is {index}, row number: {RowNumber}");
                }

                var vector = new Vector(ColumnNumber);

                for (int i = 0; i < vector.Length; i++)
                {
                    vector[i] = _values[index, i];
                }

                return vector;
            }
            set
            {
                if (index < 0 || index > RowNumber)
                {
                    throw new IndexOutOfRangeException($"index is {index}, row number: {RowNumber}");
                }

                if (value is null)
                {
                    throw new ArgumentNullException("value is null");
                }

                var vectorLength = value.Length;

                if (vectorLength != ColumnNumber)
                {
                    throw new ArgumentException($"value length: {vectorLength}, column number: {ColumnNumber}");
                }

                for (int i = 0; i < ColumnNumber; i++)
                {
                    _values[index, i] = value[i];
                }
            }
        }

        public double this[int row, int column]
        {
            get
            {
                if (row < 0 || row > RowNumber)
                {
                    throw new IndexOutOfRangeException($"row is {row}, row number: {RowNumber}");
                }

                if (column < 0 || column > ColumnNumber)
                {
                    throw new IndexOutOfRangeException($"column is {column}, column number: {ColumnNumber}");
                }

                return _values[row, column];
            }
            set
            {
                if (row < 0 || row > RowNumber)
                {
                    throw new IndexOutOfRangeException($"row is {row}, row number: {RowNumber}");
                }

                if (column < 0 || column > ColumnNumber)
                {
                    throw new IndexOutOfRangeException($"column is {column}, column number: {ColumnNumber}");
                }

                _values[row, column] = value;
            }
        }

        public static Matrix operator *(Matrix first, Matrix second)
        {
            var firstRowsCount = first.RowNumber;
            var firstColumnsCount = first.ColumnNumber;
            var secondRowsCount = second.RowNumber;
            var secondColumnsCount = second.ColumnNumber;

            if (firstColumnsCount != secondRowsCount)
                throw new InvalidOperationException("Count of columns of the first matrix must equal to count of rows of the second matrix");

            var result = new Matrix(firstRowsCount, secondColumnsCount);

            for (int i = 0; i < firstRowsCount; i++)
            {
                for (int j = 0; j < secondColumnsCount; j++)
                {
                    for (int k = 0; k < firstColumnsCount; k++)
                        result[i, j] += first[i, k] * second[k, j];
                }
            }

            return result;
        }

        /// <summary>
        /// Меняет местами строки
        /// </summary>
        /// <param name="rowInd1">Индекс первой строки</param>
        /// <param name="rowInd2">Индекс второй строки</param>
        public void SwapRows(int rowInd1, int rowInd2)
        {
            if (rowInd1 < 0 || rowInd1 > RowNumber)
            {
                throw new IndexOutOfRangeException($"row1 is {rowInd1}, row number: {RowNumber}");
            }

            if (rowInd2 < 0 || rowInd2 > RowNumber)
            {
                throw new IndexOutOfRangeException($"row2 is {rowInd1}, row number: {RowNumber}");
            }

            var temp = this[rowInd1];

            this[rowInd1] = this[rowInd2];
            this[rowInd2] = temp;
        }

        public Matrix Inverse()
        {
            if (RowNumber != ColumnNumber)
                throw new InvalidOperationException("Only square matrices can be inverted.");

            var dimension = RowNumber;
            var result = _values.Clone() as double[,];
            var identity = _values.Clone() as double[,];

            for (int _row = 0; _row < dimension; _row++)
            {
                for (int _col = 0; _col < dimension; _col++)
                    identity[_row, _col] = (_row == _col) ? 1.0 : 0.0;

            }

            for (int i = 0; i < dimension; i++)
            {
                var temp = result[i, i];

                for (int j = 0; j < dimension; j++)
                {
                    result[i, j] = result[i, j] / temp;
                    identity[i, j] = identity[i, j] / temp;
                }

                for (int k = 0; k < dimension; k++)
                {
                    if (i != k)
                    {
                        temp = result[k, i];

                        for (int n = 0; n < dimension; n++)
                        {
                            result[k, n] = result[k, n] - temp * result[i, n];
                            identity[k, n] = identity[k, n] - temp * identity[i, n];
                        }
                    }
                }
            }

            return new Matrix(identity);
        }

        public static Matrix Identity(int dimension)
        {
            var matrix = new Matrix(dimension, dimension);

            for (int i = 0; i < dimension; i++)
                matrix[i, i] = 1;

            return matrix;
        }

        public static Matrix Random(int min, int max, (int, int) size)
        {
            var matrix = new Matrix(size.Item1, size.Item2);
            var rnd = new Random();

            for (int i = 0; i < size.Item1; i++)
            {
                for (int j = 0; j < size.Item2; j++)
                    matrix[i, j] = rnd.Next(min, max);
            }

            return matrix;
        }

        public double Determinant()
        {
            double[,] lum = MatrixDecompose(out int[] perm, out int toggle);

            if (lum == null)
                return 0;

            double result = toggle;

            for (int i = 0; i < lum.GetLength(0); ++i)
                result *= lum[i, i];

            return result;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            var matrix = (Matrix)obj;

            if (RowNumber != matrix.RowNumber)
            {
                return false;
            }

            if (ColumnNumber != matrix.ColumnNumber)
            {
                return false;
            }

            for (int i = 0; i < matrix.RowNumber; i++)
            {
                for (int j = 0; j < matrix.ColumnNumber; j++)
                {
                    double difference = Math.Abs(this[i, j] * .00001);

                    if (Math.Abs(this[i, j] - matrix[i, j]) > difference)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                if (_values is null || RowNumber == 0)
                {
                    return 0;
                }

                var hash = 17;

                for (int i = 0; i < RowNumber; i++)
                {
                    for (int j = 0; j < ColumnNumber; j++)
                    {
                        hash = (23 * hash) ^ (_values[i, j].GetHashCode());
                    }
                }

                return hash;
            }
        }

        private double[,] MatrixDecompose(out int[] perm, out int toggle)
        {
            var rows = RowNumber;
            var cols = ColumnNumber;

            if (rows != cols)
                throw new Exception("Attempt to MatrixDecompose a non-square mattrix");

            var result = MatrixDuplicate();
            perm = new int[rows];

            for (int i = 0; i < rows; ++i)
                perm[i] = i;

            toggle = 1;

            for (int j = 0; j < rows - 1; ++j)
            {
                double colMax = Math.Abs(result[j, j]);
                var pRow = j;

                for (int i = j + 1; i < rows; ++i)
                {
                    if (result[i, j] > colMax)
                    {
                        colMax = result[i, j];
                        pRow = i;
                    }
                }

                if (pRow != j)
                {
                    double[] rowPtr = new double[result.GetLength(1)];

                    for (int k = 0; k < result.GetLength(1); k++)
                        rowPtr[k] = result[pRow, k];

                    for (int k = 0; k < result.GetLength(1); k++)
                        result[pRow, k] = result[j, k];

                    for (int k = 0; k < result.GetLength(1); k++)
                        result[j, k] = rowPtr[k];

                    var tmp = perm[pRow];
                    perm[pRow] = perm[j];
                    perm[j] = tmp;

                    toggle = -toggle;
                }

                if (Math.Abs(result[j, j]) < 1.0E-20)
                    return null;

                for (int i = j + 1; i < rows; ++i)
                {
                    result[i, j] /= result[j, j];

                    for (int k = j + 1; k < rows; ++k)
                        result[i, k] -= result[i, j] * result[j, k];
                }
            }

            return result;
        }

        private double[,] MatrixDuplicate()
        {
            var result = new double[RowNumber, ColumnNumber];

            for (int i = 0; i < RowNumber; ++i)
            {
                for (int j = 0; j < ColumnNumber; ++j)
                    result[i, j] = _values[i, j];
            }

            return result;
        }
    }
}