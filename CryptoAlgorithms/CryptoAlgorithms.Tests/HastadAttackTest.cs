using CryptoAlgorithms.Core.HastadAttack;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CryptoAlgorithms.Tests
{
    [TestClass]
    public class HastadAttackTest
    {
        [TestMethod]
        public void TestGettingMessage()
        {
            long[] c = { 8396, 4729, 2992 };
            long[] m = { 23449, 21583, 14863 };

            var attackAlg = new HastadAttackAlg
            {
                ChipherTexts = c,
                Modules = m
            };

            Assert.AreEqual(123L, attackAlg.GetMessage());
        }
    }
}
