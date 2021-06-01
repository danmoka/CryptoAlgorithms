using CryptoAlgorithms.Core.Babai;
using CryptoAlgorithms.Core.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace CryptoAlgorithms.Tests
{
    [TestClass]
    public class BabaiNearestTest
    {
        [TestMethod]
        public void GetNearestPlaneTest()
        {
            double[] expectedValues = { -2.0, 2.0 };
            var expectedVector = new Vector(expectedValues);

            double[] xValues = { -2.5, 1.0 };
            var x = new Vector(xValues);
            double[,] values = {
                { -1.0, 1.0 },
                { 2.0, 2.0 }
            };
            var basis = new Matrix(values);
            var closestVector = BabaiNearestPlaneAlg.GetNearest(basis, x);

            Assert.AreEqual(expectedVector, closestVector);
        }

        [TestMethod]
        public void GetNearestPlaneForLatticePointTest()
        {
            double[] expectedValues = { -4.0, 0.0 };
            var expectedVector = new Vector(expectedValues);

            double[] xValues = { -4.0, 0.0 };
            var x = new Vector(xValues);
            double[,] values = {
                { -1.0, 1.0 },
                { 2.0, 2.0 }
            };
            var basis = new Matrix(values);
            var closestVector = BabaiNearestPlaneAlg.GetNearest(basis, x);

            Assert.AreEqual(expectedVector, closestVector);
        }
    }
}
