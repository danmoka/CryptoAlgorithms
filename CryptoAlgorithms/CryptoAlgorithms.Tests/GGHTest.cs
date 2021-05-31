using CryptoAlgorithms.Core.Core;
using CryptoAlgorithms.Core.GGH;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CryptoAlgorithms.Tests
{
    [TestClass]
    public class GGHTest
    {
        private readonly GGH _ggh;

        public GGHTest()
        {
            _ggh = new GGH();
        }

        [TestMethod]
        public void GGH_EncryptDecrypt()
        {
            var privateKey = new Matrix(new double[,] { { 7, 0 }, { 0, 3 } });
            var publicKey = _ggh.GeneratePublicKey(privateKey);

            var message = new Vector(new double[] { 3, -7 });
            var error = new Vector(new double[] { 1, -1 });

            var encrypted = _ggh.Encrypt(message, publicKey, error);
            var decrypted = _ggh.Decrypt(encrypted, publicKey, privateKey);

            Assert.IsTrue(message.Equals(decrypted));
        }
    }
}
