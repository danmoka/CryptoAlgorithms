using HastadAttack;
using System;

namespace HastadAttackConsoleApp
{
    class Program
    {
        private static long[] c = { 8396, 4729, 2992};
        private static long[] m = { 23449, 21583, 14863 };
        static void Main(string[] args)
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
