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
        private FrequencyDistributionCalculator frequencyCalculator;

        [TestInitialize()]
        public void Initialize()
        {
            frequencyCalculator = new FrequencyDistributionCalculator();
        }

        [TestMethod]
        public void CalculateRelativeFrequencyTest()
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

            double actualRelativeFrequency = frequencyCalculator.CalculateRelativeFrequency(inputSortedDictionary, inputFrequency);

            Assert.AreEqual(expectedRelativeFrequency, actualRelativeFrequency);
        }

        [TestMethod]
        public void CalculateRelativeFrequencyWithEmptySortedDictionaryTest()
        {
            var inputSortedDictionary = new SortedDictionary<int, int>();
            var inputFrequency = 3;

            Assert.ThrowsException<ArgumentException>(() => frequencyCalculator.CalculateRelativeFrequency(inputSortedDictionary, inputFrequency));
        }

        [TestMethod]
        public void CalculateAbsoluteFrequencyTest()
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

            int actualAbsoluteFrequency = frequencyCalculator.CalculateAbsoluteFrequency(input);

            Assert.AreEqual(expectedAbsoluteFrequency, actualAbsoluteFrequency);
        }

        [TestMethod]
        public void CalculateAbsoluteFrequencyWithEmptySortedDictionaryTest()
        {
            var inputSortedDictionary = new SortedDictionary<int, int>();

            Assert.ThrowsException<ArgumentException>(() => frequencyCalculator.CalculateAbsoluteFrequency(inputSortedDictionary));
        }
    }
}
