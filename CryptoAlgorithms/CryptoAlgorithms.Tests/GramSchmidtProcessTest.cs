using CryptoAlgorithms.Core.Core;
using CryptoAlgorithms.Core.GramSchmidtProcess;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CryptoAlgorithms.Tests
{
    [TestClass]
    public class GramSchmidtProcessTest
    {
        [TestMethod]
        public void ProcessTest()
        {
            double[,] expectedValues = { 
                { 1.0, 2.0, 2.0, -1.0 }, 
                { 2.0, 3.0, -3.0, 2.0 },
                { 2.0, -1.0, -1.0, -2.0 }
            };
            var expectedMatrix = new Matrix(expectedValues);

            double[,] values = { 
                { 1.0, 2.0, 2.0, -1.0 }, 
                { 1.0, 1.0, -5.0, 3.0 },
                { 3.0, 2.0, 8.0, -7.0 }
            };
            var matrix = new Matrix(values);

            GramSchmidtProcessAlg.Solve(
                matrix,
                out var coefficients,
                out var orthogonalMatrix
            );

            Assert.AreEqual(expectedMatrix, orthogonalMatrix);
        }
    }
}