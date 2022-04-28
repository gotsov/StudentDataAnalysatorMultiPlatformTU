using Microsoft.VisualStudio.TestTools.UnitTesting;
using StudentDataAnalysatorMultiPlat.DatasetServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatasetAnalysatorTest.DatasetServices
{
    [TestClass]
    public class CorrelationCoefficentCalculatorTest
    {
        [TestMethod]
        public void TestGetCorrelation()
        {
            var x = new List<double>()
                    {
                         11, 13, 18, 12, 16, 14
                    };
            var y = new List<double>()
                    {
                         67, 73, 78, 71, 73, 70
                    };
            double expectedCorrelation = 0.87;

            double actualCorrelation = CorrelationCoefficentCalculator.calculateCorrelationCoefficent(x.ToArray(), y.ToArray());
            Assert.AreEqual(expectedCorrelation, Math.Round(actualCorrelation, 2));
        }
        [TestMethod]
        public void TestGetCorrelationThrowsExceptionDiffernArrayLengths()
        {
            var x = new List<double>()
                    {
                         11, 13, 18, 12, 16
                    };
            var y = new List<double>()
                    {
                         67, 73, 78, 71, 73, 70, 33
                    };

       
            Assert.ThrowsException<ArgumentException>(() => CorrelationCoefficentCalculator.calculateCorrelationCoefficent(x.ToArray(), y.ToArray()));
        }
    }
}
