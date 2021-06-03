using CryptoAlgorithms.Core.Core;
using CryptoAlgorithms.Core.LLL;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace CryptoAlgorithms.Tests
{
    [TestClass]
    public class LLLTest
    {
        [TestMethod]
        public void ReduceNotABasisTest()
        {
            double[,] values = {
                { 1.0, -1.0, 3.0 },
                { 1.0, -1.0, 3.0 },
                { 1.0, 2.0, 6.0 }
            };
            var basis = new Matrix(values);

            Assert.ThrowsException<ArgumentException>(() => LLLAlg.Build(basis));
        }

        [TestMethod]
        public void ReduceTest()
        {
            double[,] expectedValues = {
                { -1.0, 1.0 },
                { 2.0, 2.0 }
            };
            var expectedReducedBasis = new Matrix(expectedValues);

            double[,] values = {
                { 1.0, 3.0 },
                { 0.0, 4.0 }
            };
            var basis = new Matrix(values);
            var reducedBasis = LLLAlg.Build(basis);

            Assert.AreEqual(reducedBasis, expectedReducedBasis);
        }
    }
}