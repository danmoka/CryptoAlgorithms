using System;
using System.Text;

namespace CryptoAlgorithms.Core.Core
{
    /// <summary>
    /// Матрица
    /// </summary>
    public class Matrix
    {
        private double[,] _values;

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

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            var matrix = (Matrix) obj;

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
    }
}