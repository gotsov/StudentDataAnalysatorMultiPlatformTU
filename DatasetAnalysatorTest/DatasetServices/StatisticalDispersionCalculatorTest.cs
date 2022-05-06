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
    public class StatisticalDispersionCalculatorTest
    {
        private StatisticalDispersionCalculator statisticalDispersionCalculator;

        [TestInitialize()]
        public void Initialize()
        {
            statisticalDispersionCalculator = new StatisticalDispersionCalculator();
        }

        [TestMethod]
        public void TestCalculateMinMaxDispersion()
        {
            var x = new List<int>()
                    {
                         10, 11, 13, 15, 15, 17, 19, 20
                    };
            double expectedDispersion = 10;

            double actualDispersion = statisticalDispersionCalculator.CalculateMinMaxDispersion(x);
            Assert.AreEqual(expectedDispersion, actualDispersion);
        }
        [TestMethod]
        public void TestCalculateVariance()
        {
            var x = new List<int>()
                    {
                         46, 69,  32,  60,  52,  41
                    };
            double expectedVariance = 177.2;

            double actualVariance = statisticalDispersionCalculator.CalculateVariance(x);
            Assert.AreEqual(expectedVariance, actualVariance);
        }

        [TestMethod]
        public void TestCalculateStandartDeviation()
        {
            var x = new List<int>()
                    {
                        46,  69,  32,  60,  52,  41
                    };
            double expectedDeviation = 13.31;

            double actualDeviation = statisticalDispersionCalculator.CalculateStandartDeviation(x);
            Assert.AreEqual(expectedDeviation, Math.Round(actualDeviation, 2));
        }


    }
}
