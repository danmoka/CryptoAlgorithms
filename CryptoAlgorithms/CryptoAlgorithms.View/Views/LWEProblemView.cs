using CryptoAlgorithms.Core.LWE_Problem;
using System;

namespace CryptoAlgorithms.View.Views
{
    public class LWEProblemView : IAlgorithmView
    {
        private RegevCryptosystem _regev;

        public void Run()
        {
            Console.WriteLine("\nКриптосистема Регева");

            InputParams();
            PrintParams();

            var encryptedMessage = EncryptMessage();
            DecryptMessage(encryptedMessage);

            Console.WriteLine("\n");
        }

        private void InputParams()
        {
            Console.Write("Введите размерность n: ");
            var n = int.Parse(Console.ReadLine());
            var q = -1;

            while (q < n * n || q > 2 * n * n)
            {
                Console.Write("Введите модуль q (от n^2 до 2n^2): ");
                q = int.Parse(Console.ReadLine());
            }

            _regev = new RegevCryptosystem(n, q);
        }

        private void PrintParams()
        {
            var secretKey = "{" + string.Join(", ", _regev.SecretKey) + "}";

            Console.WriteLine($"Размерность m: {_regev.M}");
            Console.WriteLine($"Закрытый ключ: {secretKey}");
            Console.WriteLine("Открытый ключ:");

            for (int i = 0; i < _regev.OpenKey.Length; i++)
                Console.WriteLine($"\t{i + 1}. {_regev.OpenKey[i].ToString()}");
        }

        private VectorIntPair[] EncryptMessage()
        {
            Console.Write("Введите сообщение: ");

            var message = Console.ReadLine();
            var encrypted = _regev.Encrypt(message);

            Console.WriteLine("Шифртекст:");
            for (int i = 0; i < encrypted.Length; i++)
                Console.WriteLine($"\t{i + 1}. {encrypted[i].ToString()}");

            return encrypted;
        }

        private void DecryptMessage(VectorIntPair[] encryptedMessage)
        {
            var decryptedMessage = _regev.Decrypt(encryptedMessage);
            Console.WriteLine($"Открытый текст: {decryptedMessage}");
        }
    }
}
