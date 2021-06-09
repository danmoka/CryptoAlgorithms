using CryptoAlgorithms.Core.HastadAttack;
using System;
using System.Linq;

namespace CryptoAlgorithms.View
{
    public class HastadAttackView : IAlgorithmView
    {
        private HastadAttackAlg _hastadAttackAlg;

        public string Name => "Атака Хастада";

        public void Run()
        {
            Console.WriteLine($"\t{Name}");

            InputParams();
            GetResult();
        }

        private void InputParams()
        {
            Console.Write("Введите шифртексты (пример: 8396 4729 2992): ");
            var chipherTexts = Console.ReadLine().Split(' ').Select(c => long.Parse(c)).ToList();

            Console.Write("Введите модули (пример: 23449 21583 14863): ");
            var modules = Console.ReadLine().Split(' ').Select(m => long.Parse(m)).ToList();

            _hastadAttackAlg = new HastadAttackAlg
            {
                ChipherTexts = chipherTexts,
                Modules = modules
            };
        }

        private void GetResult()
        {
            var result = _hastadAttackAlg.GetMessage();
            Console.WriteLine($"Результат: {result}\n\n\n");
        }
    }
}
