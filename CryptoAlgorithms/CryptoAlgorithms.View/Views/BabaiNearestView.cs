using CryptoAlgorithms.Core.Babai;
using System;

namespace CryptoAlgorithms.View.Views
{
    public class BabaiNearestView : IAlgorithmView
    {
        public string Name => "Алгоритм Бабаи";

        public void Run()
        {
            Console.WriteLine($"\t{Name}");

            var point = ViewUtils.InputVector("Введите координаты точки");
            var basis = ViewUtils.InputMatrix("Введите базис");
            var nearest = BabaiNearestPlaneAlg.GetNearest(basis, point);

            ViewUtils.PrintVector("Ближайшая точка", nearest);
            Console.WriteLine("\n\n");
        }
    }
}
