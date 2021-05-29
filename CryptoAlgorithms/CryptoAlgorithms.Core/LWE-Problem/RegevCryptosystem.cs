using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CryptoAlgorithms.Core.LWE_Problem
{
    public class RegevCryptosystem
    {
        private readonly ZqSecureRNG _rng;

        public RegevCryptosystem(int n, int q)
        {
            var dimension = (1 + double.Epsilon) * (1 + n) * Math.Log(q);

            N = n;
            Q = q;
            M = Convert.ToInt32(Math.Round(dimension));

            _rng = new ZqSecureRNG(q);
            SecretKey = _rng.GenerateVector(n);
            GenerateOpenKey();
        }

        //  N - размерность
        //  Q - модуль (N^2 < Q < 2N^2)
        //  M - число уравнений

        public int N { get; set; }
        public int Q { get; set; }
        public int M { get; set; }
        public int[] SecretKey { get; set; }
        public VectorIntPair[] OpenKey { get; set; }

        private void GenerateOpenKey()
        {
            OpenKey = new VectorIntPair[M];

            for (int i = 0; i < M; i++)
            {
                var a = _rng.GenerateVector(N);
                var error = _rng.GenerateInt(2);
                var b = (CalculateScalar(a, SecretKey) + error) % Q;

                OpenKey[i] = new VectorIntPair(a, b);
            }
        }

        public VectorIntPair[] Encrypt(string message)
        {
            var binary = string.Join("", Encoding.UTF8.GetBytes(message).Select(n => Convert.ToString(n, 2).PadLeft(8, '0')));
            var encrypted = new VectorIntPair[binary.Length];

            for (int i = 0; i < binary.Length; i++)
            {
                var subset = _rng.GetSubset(M);
                var a = new int[N];
                var b = int.Parse(binary[i].ToString()) + Q;

                if (b % 2 == 1)
                    b += Q;

                b /= 2;

                for (int j = 0; j < subset.Count; j++)
                {
                    a = CalculateVectorSum(a, OpenKey[subset[j]].A);
                    b += OpenKey[subset[j]].B;
                }

                b %= Q;
                encrypted[i] = new VectorIntPair(a, b);
            }

            return encrypted;
        }

        public string Decrypt(VectorIntPair[] message)
        {
            var bits = new List<char>();

            for (int i = 0; i < message.Length; i++)
            {
                var dividend = (((message[i].B - CalculateScalar(message[i].A, SecretKey)) % Q) + Q) % Q;
                var x = (double)dividend / Q;

                if (x < 0.25 || x > 0.75)
                    bits.Add('0');
                else
                    bits.Add('1');
            }

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
