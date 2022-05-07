using Microsoft.VisualStudio.TestTools.UnitTesting;
using StudentDataAnalysatorMultiPlat.DatasetServices;
using System;
using System.Collections.Generic;

namespace StudentDataAnalyserTests
{
    [TestClass]
    public class CentralTendencyCalculatorTest

    {

        private CentralTendencyCalculator centralTendencyCalculator;

        [TestInitialize()]
        public void Initialize()
        {
            centralTendencyCalculator = new CentralTendencyCalculator();
        }

        [TestMethod]
        public void TestGetModeReturnsOneMode()
        {
            var input = new List<double>()
                    {
                          1, 2, 3, 3, 3, 4, 4, 6
                    };
            var expectedMode = new List<double> { 3 };

            List<double> actualMode = centralTendencyCalculator.GetMode(input);

            CollectionAssert.AreEqual(expectedMode, actualMode);

        }


        [TestMethod]
        public void TestGetModeReturnsMultipleModes()
        {
            var input = new List<double>()
                    {
                           1, 2, 2, 3, 4, 5, 5, 6
                    };
            var expectedMode = new List<double> { 2, 5 };

            List<double> actualMode = centralTendencyCalculator.GetMode(input);

            CollectionAssert.AreEqual(expectedMode, actualMode);

        }


        [TestMethod]
        public void TestGetModeRetunsEmptyWhenNoModePresent()
        {
            var input = new List<double>()
                    {
                          1, 3, 5, 10, 12, 14, 20
                    };

            CollectionAssert.AreEqual(new List<double>(), centralTendencyCalculator.GetMode(input));

        }

        [TestMethod]
        public void TestGetModeThrowsExceptionOnEmptyList()
        {
            var input = new List<double>();
            var e = Assert.ThrowsException<ArgumentException>(() => centralTendencyCalculator.GetMode(input));
            Assert.AreEqual("List for calculating mode values cannot be empty", e.Message);

        }

        [TestMethod]
        public void TestGetModeThrowsExceptionOnNullArgumentList()
        {
            List<double>? nullCoursesViewedList = null;
            var e = Assert.ThrowsException<ArgumentException>(() => centralTendencyCalculator.GetMode(nullCoursesViewedList));
            Assert.AreEqual("List for calculating mode values cannot be empty", e.Message);

        }


        [TestMethod]
        public void TestGetAverage()
        {
            var input = new List<double>()
                    {
                          10, 2, 38, 23, 38, 23, 21
                    };

            double actualAverage = centralTendencyCalculator.GetAverage(input);

            Assert.AreEqual(22.14, Math.Round(actualAverage, 2));

        }

        [TestMethod]
        public void TestGetAverageOnEvenInput()
        {
            var input = new List<double>()
                    {
                          6, 40, 32, 5, 12, 24
                    };

            double actualAverage = centralTendencyCalculator.GetAverage(input);

            Assert.AreEqual(19.83, Math.Round(actualAverage, 2));

        }

        [TestMethod]
        public void TestGetAverageThrowsExceptionOnEmptyList()
        {
            var input = new List<double>();
            var e = Assert.ThrowsException<ArgumentException>(() => centralTendencyCalculator.GetAverage(input));
            Assert.AreEqual("List for calculating average value cannot be empty", e.Message);

        }

        [TestMethod]
        public void TestGetAverageThrowsExceptionOnNullArgumentList()
        {
            List<double>? nullCoursesViewedList = null;
            var e = Assert.ThrowsException<ArgumentException>(() => centralTendencyCalculator.GetAverage(nullCoursesViewedList));
            Assert.AreEqual("List for calculating average value cannot be empty", e.Message);

        }


        [TestMethod]
        public void TestGetMedianWhenInputIsEven()
        {
            var input = new List<double>()
                    {
                         1, 2, 3, 4, 5, 6,7
                    };
            double expectedMedian = 4;

            double actualMedian = centralTendencyCalculator.GetMedian(input);

            Assert.AreEqual(expectedMedian, actualMedian);

        }

        [TestMethod]
        public void TestGetMedianRepeatedNumber()
        {
            var input = new List<double>()
                    {
                        1, 2, 3, 3, 3, 4, 5, 6
                    };
            double expectedMedian = 3;

            double actualMedian = centralTendencyCalculator.GetMedian(input);

            Assert.AreEqual(expectedMedian, actualMedian);

        }

        [TestMethod]
        public void TestGetMedianWhenInputIsOdd()
        {
            var input = new List<double>()
                    {
                         1, 2, 3, 4, 5, 6, 7, 8
                    };
            double expectedMedian = 4.5;

            double actualMedian = centralTendencyCalculator.GetMedian(input);

            Assert.AreEqual(expectedMedian, actualMedian);

        }

        [TestMethod]
        public void TestGetMedianThrowsExceptionOnEmptyList()
        {
            var input = new List<double>();
            var e = Assert.ThrowsException<ArgumentException>(() => centralTendencyCalculator.GetMedian(input));
            Assert.AreEqual("List for calculating median value cannot be empty", e.Message);

        }

        [TestMethod]
        public void TestGetMedianThrowsExceptionOnNullArgumentList()
        {
            List<double>? nullCoursesViewedList = null;
            var e = Assert.ThrowsException<ArgumentException>(() => centralTendencyCalculator.GetMedian(nullCoursesViewedList));
            Assert.AreEqual("List for calculating median value cannot be empty", e.Message);

        }
    }
}