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
    public class FrequencyDistibutionCalculatorTest
    {
        [TestMethod]
        public void TestCalculateRelativeFrequency()
        {
            var inputSortedDictionary = new SortedDictionary<int, int>()
            {
                {2, 3},
                {5, 2},
                {7, 2},
                {9, 1},
                {10, 2},
                {11, 1},
                {15, 3}
            };

            int inputFrequency = 3;

            double expectedRelativeFrequency = 21.43;

            double actualRelativeFrequency = FrequencyDistributionCalculator.CalculateRelativeFrequency(inputSortedDictionary, inputFrequency);

            Assert.AreEqual(expectedRelativeFrequency, actualRelativeFrequency);
        }

        [TestMethod]
        public void TestCalculateAbsoluteFrequency()
        {
            var input = new SortedDictionary<int, int>()
            {
                {2, 3},
                {5, 2},
                {7, 2},
                {9, 1},
                {10, 2},
                {11, 1},
                {15, 3}
            };

            int expectedAbsoluteFrequency = 14;

            int actualAbsoluteFrequency = FrequencyDistributionCalculator.CalculateAbsoluteFrequency(input);

            Assert.AreEqual(expectedAbsoluteFrequency, actualAbsoluteFrequency);
        }
    }
}
