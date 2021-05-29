using CryptoAlgorithms.View.Views;
using System;
using System.Collections.Generic;

namespace CryptoAlgorithms.View
{
    class Program
    {
        private static Dictionary<int, IAlgorithmView> _views;

        static void Main(string[] args)
        {
            GenerateViews();
            Run();
        }

        private static void Run()
        {
            var input = string.Empty;

            while (input.ToLower() != "exit")
            {
                Console.WriteLine("Выберите алгоритм. Введите \"exit\" для выхода.");

                foreach (var kvp in _views)
                    Console.WriteLine($"{kvp.Key}. {kvp.Value.GetType().Name.Replace("View", "")}");

                Console.Write("Ответ: ");
                input = Console.ReadLine();
                int.TryParse(input, out int algorithmNumber);
                Console.WriteLine("\n");

                if (_views.ContainsKey(algorithmNumber))
                    _views[algorithmNumber].Run();
            }

            Console.WriteLine();
        }

        private static void GenerateViews()
        {
            _views = new Dictionary<int, IAlgorithmView>
            {
                { 1, new HastadAttackView() },
                { 2, new LWEProblemView() }
            };
        }
    }
}
