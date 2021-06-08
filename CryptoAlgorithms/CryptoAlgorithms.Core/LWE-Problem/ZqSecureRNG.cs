using System;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace CryptoAlgorithms.Core.LWE_Problem
{
    /// <summary>
    /// Генератор случайных величин
    /// </summary>
    public class ZqSecureRNG
    {
        // Реализует криптографический генератор случайных чисел
        private readonly RNGCryptoServiceProvider _rng;
        private readonly int _module;

        public ZqSecureRNG(int q)
        {
            _rng = new RNGCryptoServiceProvider();
            _module = q;
        }

        /// <summary>
        /// Генерирует случайное целое число по модулю
        /// </summary>
        /// <returns>Случайное целое число по модулю</returns>
        public int GenerateInt()
        {
            var bytes = new byte[4];
            _rng.GetBytes(bytes);

            return Math.Abs(BitConverter.ToInt32(bytes)) % _module;
        }

        /// <summary>
        /// Генерирует случайное целое число по указанному модулю
        /// </summary>
        /// <param name="module">Модуль</param>
        /// <returns>Случайное целое число по указанному модулю</returns>
        public int GenerateInt(int module)
        {
            var bytes = new byte[4];
            _rng.GetBytes(bytes);

            return Math.Abs(BitConverter.ToInt32(bytes)) % module;
        }

        /// <summary>
        /// Генерирует случайный бит - 0 или 1
        /// </summary>
        /// <returns>Целое значение 0 или 1</returns>
        private int GenerateBit()
        {
            var bytes = new byte[1];
            _rng.GetBytes(bytes);

            return Math.Abs(bytes[0]) % 2;
        }

        /// <summary>
        /// Генерирует вектор целых случайных значений
        /// </summary>
        /// <param name="length">Длина вектора</param>
        /// <returns>Массив целых случайных значений</returns>
        public int[] GenerateVector(int length)
        {
            var vector = new int[length];

            for (int i = 0; i < length; i++)
                vector[i] = GenerateInt();

            return vector;
        }

        /// <summary>
        /// Определяет случайным образом упорядоченное подмножество множества {0, ..., length - 1}
        /// </summary>
        /// <param name="length">Размер исходного множества</param>
        /// <returns>Упорядоченное подмножество исходного множества</returns>
        public List<int> GetSubset(int length)
        {
            var list = new List<int>();

            for (int i = 0; i < length; i++)
            {
                var decider = GenerateBit();

                if (decider == 1)
                    list.Add(i);
            }

            return list;
        }
    }
}
