using CryptoAlgorithms.Core.Core;

namespace CryptoAlgorithms.Core.GGH
{
    /// <summary>
    /// Схема шифрования GGH (Голдрейха-Гольдвассер-Халеви)
    /// </summary>
    public class GGH
    {
        /// <summary>
        /// Генерация открытого ключа: унимодулярная матрица * базис решетки
        /// </summary>
        /// <param name="privateKey">Базис решетки</param>
        /// <returns>Открытый ключ</returns>
        public Matrix GeneratePublicKey(Matrix privateKey)
        {
            var unimodular = GenerateUnimodularMatrix(privateKey.RowNumber);
            return unimodular * privateKey;
        }

        /// <summary>
        /// Генерирует унимодулярную матрицу (квадратная матрица с целыми коэффициентами, определитель которой равен +1 или -1)
        /// </summary>
        /// <param name="dimension">Размер</param>
        /// <returns>Унимодулярная матрица</returns>
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

        /// <summary>
        /// Процесс зашифрования
        /// </summary>
        /// <param name="message">Сообщение</param>
        /// <param name="publicKey">Открытый ключ</param>
        /// <param name="error">Вектор ошибки</param>
        /// <returns>Зашифрованное сообщение</returns>
        public Vector Encrypt(Vector message, Matrix publicKey, Vector error)
        {
            return message * publicKey + error;
        }

        /// <summary>
        /// Процесс расшифрования
        /// </summary>
        /// <param name="message">Зашифрованное сообщение</param>
        /// <param name="publicKey">Открытый ключ</param>
        /// <param name="privateKey">Закрытый ключ</param>
        /// <returns>Исходное сообщение</returns>
        public Vector Decrypt(Vector message, Matrix publicKey, Matrix privateKey)
        {
            var U = message * privateKey.Inverse();
            U = U.RoundInt() * privateKey;

            var result = U * publicKey.Inverse();

            return result;
        }
    }
}
