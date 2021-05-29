using CryptoAlgorithms.Core.LWE_Problem;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CryptoAlgorithms.Tests
{
    [TestClass]
    public class LWEProblemTests
    {
        [TestMethod]
        public void RegevCryptosystem_EncryptDecrypt()
        {
            var n = 10;
            var q = 145;
            var regev = new RegevCryptosystem(n, q);

            var message = "Hello, world!";
            var encrypted = regev.Encrypt(message);
            var decrypted = regev.Decrypt(encrypted);

            Assert.AreEqual(message, decrypted);
        }
    }
}
