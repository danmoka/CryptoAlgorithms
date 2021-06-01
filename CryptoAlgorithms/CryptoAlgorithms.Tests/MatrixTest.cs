using CryptoAlgorithms.Core.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CryptoAlgorithms.Tests
{
    [TestClass]
    public class MatrixTest
    {
        [TestMethod]
        public void EqualTest()
        {
            double[,] values = { { 1.0, 2.0 }, { 3.0, 4.0 } };
            var matrix1 = new Matrix(values);
            var matrix2 = new Matrix(values);

            Assert.AreEqual(matrix1, matrix2);
        }

        [TestMethod]
        public void NotEqualInValuesTest()
        {
            double[,] values1 = { { 1.0, 2.0 }, { 3.0, 4.0 } };
            double[,] values2 = { { 1.0, 2.0 }, { 5.0, 4.0 } };
            var matrix1 = new Matrix(values1);
            var matrix2 = new Matrix(values2);

            Assert.AreNotEqual(matrix1, matrix2);
        }

        [TestMethod]
        public void NotEqualInLengthTest()
        {
            double[,] values1 = { { 1.0, 2.0 }, { 3.0, 4.0 } };
            double[,] values2 = { { 1.0 }, { 3.0 } };
            var matrix1 = new Matrix(values1);
            var matrix2 = new Matrix(values2);

            Assert.AreNotEqual(matrix1, matrix2);
        }

        [TestMethod]
        public void HashCodeTest()
        {
            double[,] values = { { 1.0, 2.0 }, { 3.0, 4.0 } };
            var matrix1 = new Matrix(values);
            var matrix2 = new Matrix(values);

            Assert.IsTrue(matrix1.GetHashCode() == matrix2.GetHashCode());
        }

        [TestMethod]
        public void SwapRowsTest()
        {
            double[,] expectedValues = { { 1.0, 2.0 }, { 3.0, 4.0 } };
            var expectedMatrix = new Matrix(expectedValues);

            double[,] values = { { 3.0, 4.0 }, { 1.0, 2.0 } };
            var matrix = new Matrix(values);
            matrix.SwapRows(0, 1);

            Assert.AreEqual(matrix, expectedMatrix);
        }

        [TestMethod]
        public void GetRowTest()
        {
            double[] expectedValues = { 1.0, 2.0 };
            var expectedVector = new Vector(expectedValues);

            double[,] values = { { 3.0, 4.0 }, { 1.0, 2.0 } };
            var matrix = new Matrix(values);
            var vector = matrix[1];

            Assert.AreEqual(vector, expectedVector);
        }

        [TestMethod]
        public void GetValueTest()
        {
            var expectedValue = 1.0;

            double[,] values = { { 3.0, 4.0 }, { 1.0, 2.0 } };
            var matrix = new Matrix(values);
            var value = matrix[1, 0];

            Assert.AreEqual(value, expectedValue);
        }
    }
}