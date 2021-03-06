using System;
using System.Text;

namespace CryptoAlgorithms.Core.Core
{
    /// <summary>
    /// Вектор
    /// </summary>
    public class Vector : ICloneable
    {
        private double[] _values;

        public int Length { get => _values.Length; }

        public Vector(double[] values)
        {
            if (values == null)
            {
                throw new ArgumentNullException("values is null");
            }

            _values = new double[values.Length];

            for (int i = 0; i < Length; i++)
            {
                _values[i] = values[i];
            }
        }

        public Vector(int length)
        {
            _values = new double[length];
        }

        public double this[int index]
        {
            get
            {
                if (index < 0 || index > Length)
                {
                    throw new IndexOutOfRangeException();
                }

                return _values[index];
            }
            set
            {
                if (index < 0 || index > Length)
                {
                    throw new IndexOutOfRangeException();
                }

                _values[index] = value;
            }
        }

        public static Vector operator +(Vector vector1, Vector vector2)
        {
            if (vector1 == null)
            {
                throw new ArgumentNullException("vector1 is null");
            }

            if (vector2 == null)
            {
                throw new ArgumentNullException("vector2 is null");
            }

            if (vector1.Length != vector2.Length)
            {
                throw new ArgumentException($"vector1 length: {vector1.Length}, " +
                $"vector2 length: {vector2.Length}");
            }

            var vector = new Vector(vector1.Length);

            for (int i = 0; i < vector.Length; i++)
            {
                vector[i] =  vector1[i] +  vector2[i];
            }

            return vector;
        }

        public static Vector operator -(Vector vector1, Vector vector2)
        {
            if (vector1 == null)
            {
                throw new ArgumentNullException("vector1 is null");
            }

            if (vector2 == null)
            {
                throw new ArgumentNullException("vector2 is null");
            }

            if (vector1.Length != vector2.Length)
            {
                throw new ArgumentException($"vector1 length: {vector1.Length}, vector2 length: {vector2.Length}");
            }

            var vector = new Vector(vector1.Length);

            for (int i = 0; i < vector.Length; i++)
            {
                vector[i] =  vector1[i] -  vector2[i];
            }

            return vector;
        }

        public static Vector operator *(double value, Vector vector)
        {
            if (vector == null)
            {
                throw new ArgumentNullException("vector is null");
            }

            var result = new Vector(vector.Length);

            for (int i = 0; i < result.Length; i++)
            {
                result[i] = vector[i] * value;
            }

            return result;
        }

        public static Vector operator *(Vector vector, double value)
        {
            return value * vector;
        }

        public static Vector operator *(Vector vector, Matrix matrix)
        {
            var length = vector.Length;
            var rowsCount = matrix.RowNumber;
            var columnsCount = matrix.ColumnNumber;

            if (length != rowsCount)
                throw new InvalidOperationException("The length of the vector must equal to count of rows of the matrix");

            var result = new Vector(columnsCount);

            for (int i = 0; i < columnsCount; i++)
            {
                for (int j = 0; j < length; j++)
                    result[i] += vector[j] * matrix[j, i];
            }

            return result;
        }

        /// <summary>
        /// Скалярное произведение
        /// </summary>
        /// <param name="vector">Вектор</param>
        /// <returns>Скалярное произведение двух векторов</returns>
        public double DotProduct(Vector vector)
        {
            if (vector == null)
            {
                throw new ArgumentNullException("vector is null");
            }

            if (Length == 0)
            {
                return 0;
            }

            if (Length != vector.Length)
            {
                throw new ArgumentException($"values length: {_values.Length}, vector length: {vector.Length}");
            }

            var dotProduct = 0.0;

            for (int i = 0; i < Length; i++)
            {
                dotProduct += _values[i] * vector[i];
            }

            return dotProduct;

        }

        public Vector RoundInt()
        {
            var result = new Vector(Length);

            for (int i = 0; i < Length; i++)
                result[i] = Math.Round(this[i]);

            return result;
        }

        public override bool Equals(object obj)
        {            
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            var vector = (Vector) obj;
            
            if (vector.Length != Length)
            {
                return false;
            }

            for (int i = 0; i < Length; i++)
            {
                var difference = Math.Abs(this[i] * .00001);

                if (Math.Abs(this[i] - vector[i]) > difference)
                {
                    return false;
                }
            }

            return true;
        }

        public override int GetHashCode()
        {
            unchecked 
            {
                if (_values is null || Length == 0)
                {
                    return 0;
                }
                
                var hash = 17;

                for (int i = 0; i < Length; i++)
                {
                    hash = (23 * hash) ^ (_values[i].GetHashCode());
                }
                
                return hash;
            }
        }

        /// <summary>
        /// Предоставляет копию объекта
        /// </summary>
        /// <returns>Копия объекта</returns>
        public object Clone()
        {
            var values = new double[Length];

            for (int i = 0; i < Length; i++ )
            {
                values[i] = _values[i];
            }

            return new Vector(values);
        }
    }
}