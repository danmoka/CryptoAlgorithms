using CryptoAlgorithms.Core.Core;
using CryptoAlgorithms.Core.LLL;
using System;

namespace CryptoAlgorithms.View.Views
{
    public class LLLView : IAlgorithmView
    {
        public string Name => "LLL-алгоритм";

        public void Run()
        {
            Console.WriteLine($"\t{Name}");

            var matrix = InputParams();
            GetResult(matrix);
        }

        private Matrix InputParams()
        {
            return ViewUtils.InputMatrix("Введите базис");
        }

        private void GetResult(Matrix matrix)
        {
            var result = LLLAlg.Build(matrix);
            ViewUtils.PrintMatrix("\nРезультат", result);
            Console.WriteLine("\n\n");
        }
    }
}
