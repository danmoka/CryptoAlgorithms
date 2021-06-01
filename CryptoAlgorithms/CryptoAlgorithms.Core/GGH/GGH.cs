using CryptoAlgorithms.Core.Core;

namespace CryptoAlgorithms.Core.GGH
{
    public class GGH
    {
        public Matrix GeneratePublicKey(Matrix privateKey)
        {
            var unimodular = GenerateUnimodularMatrix(privateKey.RowNumber);
            return unimodular * privateKey;
        }

        private Matrix GenerateUnimodularMatrix(int dimension)
        {
            var matrix = Matrix.Identity(dimension);
            var k = 5;

            while (k != 0)
            {
                var A = Matrix.Random(-5, 5, (dimension, dimension));
                var det = A.Determinant();

                if (det == 1.0)
                {
                    matrix *= A;
                    k--;
                }
            }

            return matrix;

        }

        public Vector Encrypt(Vector message, Matrix publicKey, Vector error)
        {
            return message * publicKey + error;
        }

        public Vector Decrypt(Vector message, Matrix publicKey, Matrix privateKey)
        {
            var U = message * privateKey.Inverse();
            U = U.RoundInt() * privateKey;

            var result = U * publicKey.Inverse();

            return result;
        }
    }
}
