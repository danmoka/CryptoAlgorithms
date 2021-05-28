using CryptoAlgorithms.Core.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace VectorTests
{
    [TestClass]
    public class VectorTest
    {
        [TestMethod]
        public void EqualTest()
        {
            double[] values = { 0.1, 0.2, 0.3 };
            var vector1 = new Vector(values);
            var vector2 = new Vector(values);

            Assert.AreEqual(vector1, vector2);
        }

        [TestMethod]
        public void NotEqualInValuesTest()
        {
            double[] values1 = { 0.1, 0.2, 0.3 };
            double[] values2 = { 0.2, 0.1, 0.3 };
            var vector1 = new Vector(values1);
            var vector2 = new Vector(values2);

            Assert.AreNotEqual(vector1, vector2);
        }

        [TestMethod]
        public void NotEqualInLengthTest()
        {
            double[] values1 = { 0.1, 0.2, 0.3 };
            double[] values2 = { 0.1, 0.2 };
            var vector1 = new Vector(values1);
            var vector2 = new Vector(values2);

            Assert.AreNotEqual(vector1, vector2);
        }

        [TestMethod]
        public void HashCodeTest()
        {
            double[] values = { 0.1, 0.2, 0.3 };
            var vector1 = new Vector(values);
            var vector2 = new Vector(values);

            Assert.IsTrue(vector1.GetHashCode() == vector2.GetHashCode());
        }

        [TestMethod]
        public void CloneTest()
        {
            double[] values = { 0.1, 0.2, 0.3 };
            var vector1 = new Vector(values);
            var vector2 = (Vector) vector1.Clone();

            Assert.AreEqual(vector1, vector2);
            Assert.IsTrue(vector1.GetHashCode() == vector2.GetHashCode());
        }

        [TestMethod]
        public void SumTest()
        {
            double[] expectedValues = { 1.0, 5.0, 20.0 };
            var expectedVector = new Vector(expectedValues);

            double[] values1 = { 0.0, 2.0, 3.0 };
            double[] values2 = { 1.0, 3.0, 17.0 };
            var vector1 = new Vector(values1);
            var vector2 = new Vector(values2);
            var sumResultVector = vector1 + vector2;

            Assert.AreEqual(expectedVector, sumResultVector);
        }

        [TestMethod]
        public void SubtractionTest()
        {
            double[] expectedValues = { -1.0, -1.0, -14.0 };
            var expectedVector = new Vector(expectedValues);

            double[] values1 = { 0.0, 2.0, 3.0 };
            double[] values2 = { 1.0, 3.0, 17.0 };
            var vector1 = new Vector(values1);
            var vector2 = new Vector(values2);
            var sumResultVector = vector1 - vector2;

            Assert.AreEqual(expectedVector, sumResultVector);
        }

        [TestMethod]
        public void DotProductTest()
        {
            var expectedResult = 6.0 + 51.0;

            double[] values1 = { 0.0, 2.0, 3.0 };
            double[] values2 = { 1.0, 3.0, 17.0 };
            var vector1 = new Vector(values1);
            var vector2 = new Vector(values2);
            var dotProduct = vector1.DotProduct(vector2);

            Assert.AreEqual(expectedResult, dotProduct);
        }

        [TestMethod]
        public void GetValueTest()
        {
            var expectedValue = 2.0;

            double[] values = { 0.0, 2.0, 3.0 };
            var vector = new Vector(values);
            var value = vector[1];

            Assert.AreEqual(expectedValue, value);
        }

        [TestMethod]
        public void MultiplicationTest()
        {
            double[] expectedValues = { 3.0, 6.0, -9.0 };
            var expectedVector = new Vector(expectedValues);

            double[] values = { 1.0, 2.0, -3.0 };
            var vector = new Vector(values);
            vector = vector * 3.0;

            Assert.AreEqual(expectedVector, vector);
        }
    }
}