using System;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace CryptoAlgorithms.Core.LWE_Problem
{
    public class ZqSecureRNG
    {
        private readonly RNGCryptoServiceProvider _rng;
        private readonly int _module;

        public ZqSecureRNG(int q)
        {
            _rng = new RNGCryptoServiceProvider();
            _module = q;
        }

        public int GenerateInt()
        {
            var bytes = new byte[4];
            _rng.GetBytes(bytes);

            return Math.Abs(BitConverter.ToInt32(bytes)) % _module;
        }

        public int GenerateInt(int module)
        {
            var bytes = new byte[4];
            _rng.GetBytes(bytes);

            return Math.Abs(BitConverter.ToInt32(bytes)) % module;
        }

        private int GenerateBit()
        {
            var bytes = new byte[1];
            _rng.GetBytes(bytes);

            return Math.Abs(bytes[0]) % 2;
        }

        public int[] GenerateVector(int length)
        {
            var vector = new int[length];

            for (int i = 0; i < length; i++)
                vector[i] = GenerateInt();

            return vector;
        }

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
