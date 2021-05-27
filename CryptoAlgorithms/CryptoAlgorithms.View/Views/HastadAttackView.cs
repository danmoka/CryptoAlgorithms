using CryptoAlgorithms.Core.HastadAttack;
using System;

namespace CryptoAlgorithms.View
{
    public class HastadAttackView : IAlgorithmView
    {
        private long[] c = { 8396, 4729, 2992 };
        private long[] m = { 23449, 21583, 14863 };

        public void Run()
        {
            var attackAlg = new HastadAttackAlg
            {
                ChipherTexts = c,
                Modules = m
            };

            Console.WriteLine($"Message: {attackAlg.GetMessage()}");
        }
    }
}
