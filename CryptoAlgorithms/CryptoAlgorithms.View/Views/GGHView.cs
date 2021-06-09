using CryptoAlgorithms.Core.Core;
using CryptoAlgorithms.Core.GGH;
using System;

namespace CryptoAlgorithms.View.Views
{
    public class GGHView : IAlgorithmView
    {
        private readonly GGH _ggh;

        public GGHView()
        {
            _ggh = new GGH();
        }

        public string Name => "GGH";

        public void Run()
        {
            Console.WriteLine($"\t{Name}");

            var message = InputParams();
            var encryptedMessage = EncryptMessage(message);
            DecryptMessage(encryptedMessage);
        }

        private Vector InputParams()
        {
            var privateKey = ViewUtils.InputMatrix("Введите закрытый ключ");
            var publicKey = _ggh.GeneratePublicKey(privateKey);
            ViewUtils.PrintMatrix("Открытый ключ", publicKey);

            return ViewUtils.InputVector("Введите сообщение");
        }

        private Vector EncryptMessage(Vector message)
        {
            var error = ViewUtils.InputVector("Введите вектор ошибок");
            var encryptedMessage = _ggh.Encrypt(message, _ggh.PublicKey, error);
            ViewUtils.PrintVector("Зашифрованное сообщение", encryptedMessage);
            Console.WriteLine();

            return encryptedMessage;
        }

        private void DecryptMessage(Vector encryptedMessage)
        {
            var decryptedMessage = _ggh.Decrypt(encryptedMessage, _ggh.PublicKey, _ggh.PrivateKey);
            ViewUtils.PrintVector("Расшифрованное сообщение", decryptedMessage);
            Console.WriteLine("\n\n");
        }
    }
}
