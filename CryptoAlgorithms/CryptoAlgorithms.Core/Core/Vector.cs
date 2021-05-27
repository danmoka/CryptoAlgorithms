using System;
using System.Text;

namespace CryptoAlgorithms.Core.Core
{
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

            for (int i = 0; i < _values.Length; i++)
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
                if (index < 0 || index > _values.Length)
                {
                    throw new IndexOutOfRangeException();
                }

                return _values[index];
            }
            set
            {
                if (index < 0 || index > _values.Length)
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

        public double DotProduct(Vector vector)
        {
            if (vector == null)
            {
                throw new ArgumentNullException("vector is null");
            }

            if (_values.Length == 0)
            {
                return 0;
            }

            if (_values.Length != vector.Length)
            {
                throw new ArgumentException($"values length: {_values.Length}, vector length: {vector.Length}");
            }

            var dotProduct = 0.0;

            for (int i = 0; i < _values.Length; i++)
            {
                dotProduct += _values[i] * vector[i];
            }

            return dotProduct;

        }

        // MatMul

        public override bool Equals(object obj)
        {            
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            var vector = (Vector) obj;
            
            if (vector.Length != this.Length)
            {
                return false;
            }

            for (int i = 0; i < this.Length; i++)
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
                // https://stackoverflow.com/a/263416/4340086
                var hash = (int) 2166136261;
                hash = (16777619 * hash) ^ (_values?.GetHashCode() ?? 0);
                
                return hash;
            }
        }

        public object Clone()
        {
            var values = new double[this.Length];

            for (int i = 0; i < this.Length; i++ )
            {
                values[i] = this._values[i];
            }

            return new Vector(values);
        }
    }
}