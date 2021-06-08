using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CryptoAlgorithms.Core.LWE_Problem
{
    /// <summary>
    /// Криптосистема Регева
    /// </summary>
    public class RegevCryptosystem
    {
        /// <summary>
        /// Генератор случайных величин
        /// </summary>
        private readonly ZqSecureRNG _rng;

        /// <summary>
        /// Параметр надежности
        /// </summary>
        public int N { get; set; }

        /// <summary>
        /// Модуль
        /// </summary>
        public int Q { get; set; }

        /// <summary>
        /// Размерность
        /// </summary>
        public int M { get; set; }

        /// <summary>
        /// Секретный ключ
        /// </summary>
        public int[] SecretKey { get; set; }

        /// <summary>
        /// Открытый ключ
        /// </summary>
        /// <remarks>
        /// Состоит из пар: вектор, скаляр
        /// </remarks>
        public VectorIntPair[] OpenKey { get; set; }

        /// <summary>
        /// Инициализирует экземпляр криптосистемы
        /// </summary>
        /// <remarks>
        /// N - размерность
        /// Q - модуль (N^2 < Q < 2N^2)
        /// M - число уравнений
        /// </remarks>
        /// <param name="n">Параметр надежности</param>
        /// <param name="q">Модуль</param>
        public RegevCryptosystem(int n, int q)
        {
            var dimension = (1 + double.Epsilon) * (1 + n) * Math.Log(q);

            N = n;
            Q = q;
            M = Convert.ToInt32(Math.Round(dimension));

            _rng = new ZqSecureRNG(q);
            SecretKey = _rng.GenerateVector(n); // вектор случайных значений
            GenerateOpenKey();
        }

        /// <summary>
        /// Генерация открытого ключа
        /// </summary>
        private void GenerateOpenKey()
        {
            OpenKey = new VectorIntPair[M]; // M пар: вектор, скаляр

            for (int i = 0; i < M; i++)
            {
                var a = _rng.GenerateVector(N); // вектор случайных значений
                var error = _rng.GenerateInt(2); // ошибка
                var b = (CalculateScalar(a, SecretKey) + error) % Q; // скалярное произведение а и секрет. ключа + ошибка

                OpenKey[i] = new VectorIntPair(a, b);
            }
        }

        /// <summary>
        /// Шифрование сообщения
        /// </summary>
        /// <param name="message">Сообщение</param>
        /// <returns>Набор пар, представляющих зашифрованные биты</returns>
        public VectorIntPair[] Encrypt(string message)
        {
            // представление сообщения в виде набора 0 и 1
            var binary = string.Join("", Encoding.UTF8.GetBytes(message).Select(n => Convert.ToString(n, 2).PadLeft(8, '0')));
            var encrypted = new VectorIntPair[binary.Length];

            for (int i = 0; i < binary.Length; i++)
            {
                var subset = _rng.GetSubset(M); // выбираем некоторое подмножество размера M
                var a = new int[N];
                var b = int.Parse(binary[i].ToString()) + Q;

                if (b % 2 == 1)
                    b += Q;

                b /= 2;

                // по размеру подмножества subset
                for (int j = 0; j < subset.Count; j++)
                {
                    // вычисляем сумму векторов a и subset[j]-ого вектора в наборе пар открытого ключа
                    a = CalculateVectorSum(a, OpenKey[subset[j]].A);
                    // увеличиваем b на subset[j]-ое значение скаляра в наборе пар открытого ключа
                    b += OpenKey[subset[j]].B;
                }

                b %= Q;
                encrypted[i] = new VectorIntPair(a, b); // получаем i-ый зашифрованный бит
            }

            return encrypted;
        }

        /// <summary>
        /// Алгоритм расшифрования
        /// </summary>
        /// <param name="message">Зашифрованное сообщение</param>
        /// <returns>Исходное сообщение</returns>
        public string Decrypt(VectorIntPair[] message)
        {
            var bits = new List<char>();

            for (int i = 0; i < message.Length; i++)
            {
                // вычисляем x как |b - <a, s>/q|
                var dividend = (((message[i].B - CalculateScalar(message[i].A, SecretKey)) % Q) + Q) % Q;
                var x = (double)dividend / Q;

                if (Math.Abs(x) < 0.5) // x < 0.25 || x > 0.75 ?
                    bits.Add('0');
                else
                    bits.Add('1');
            }

            // далее переводим биты в строковое значение
            var bytes = new byte[message.Length / 8];

            for (int i = 0; i < message.Length / 8; i++)
            {
                var bitsForByte = new StringBuilder();

                for (int j = 0; j < 8; j++)
                    bitsForByte.Append(bits[i * 8 + j]);

                bytes[i] = Convert.ToByte(bitsForByte.ToString(), 2);
            }

            return Encoding.UTF8.GetString(bytes);
        }

        private int[] CalculateVectorSum(int[] v1, int[] v2)
        {
            if (v1.Length == v2.Length)
            {
                var sum = new int[v1.Length];

                for (int i = 0; i < v1.Length; i++)
                    sum[i] = (v1[i] + v2[i]) % Q;

                return sum;
            }

            return null;
        }

        private int CalculateScalar(int[] v1, int[] v2)
        {
            if (v1.Length == v2.Length)
            {
                var sum = 0;

                for (int i = 0; i < v1.Length; i++)
                    sum += v1[i] * v2[i];

                return sum;
            }

            return -1;
        }
    }
}
