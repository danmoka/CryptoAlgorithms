using CryptoAlgorithms.Core.Core;
using System;
using System.Linq;

namespace CryptoAlgorithms.View.Views
{
    public static class ViewUtils
    {
        public static Matrix InputMatrix(string message)
        {
            Console.WriteLine($"{message}:");

            Console.Write("Введите количество строк: ");
            var rowsCount = int.Parse(Console.ReadLine());

            Console.Write("Введите количество столбцов: ");
            var columnsCount = int.Parse(Console.ReadLine());

            var matrix = new double[rowsCount, columnsCount];

            for (int i = 0; i < rowsCount; i++)
            {
                Console.Write($"{i + 1}: ");
                var row = Console.ReadLine().Split(' ').Select(c => double.Parse(c)).ToList();

                for (int j = 0; j < columnsCount; j++)
                    matrix[i, j] = row[j];
            }

            return new Matrix(matrix);
        }

        public static void PrintMatrix(string message, Matrix matrix)
        {
            Console.WriteLine($"{message}:");

            for (int i = 0; i < matrix.RowNumber; i++)
            {
                for (int j = 0; j < matrix.ColumnNumber; j++)
                    Console.Write($"{Math.Round(matrix[i, j])} ");

                Console.WriteLine();
            }
        }

        public static Vector InputVector(string message)
        {
            Console.Write($"{message}: ");
            var values = Console.ReadLine().Split(' ').Select(c => double.Parse(c)).ToArray();

            return new Vector(values);
        }

        public static void PrintVector(string message, Vector vector)
        {
            Console.Write($"{message}: ");

            for (int i = 0; i < vector.Length; i++)
                Console.Write($"{Math.Round(vector[i])} ");
        }
    }
}
