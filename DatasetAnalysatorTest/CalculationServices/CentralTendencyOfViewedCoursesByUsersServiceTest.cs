using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using StudentDataAnalysatorMultiPlat.DatasetServices;
using StudentDataAnalysatorMultiPlat.Models;
using StudentDataAnalysatorMultiPlat.Services.CalculationServices;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatasetAnalysatorTest.CalculationServices
{
    public class CentralTendencyOfViewedCoursesByUsersServiceTest
    {
        [TestClass]
        public class DispersionOfViewedCoursesServiceTest
        {
            private Mock<CentralTendencyCalculator> mockCalculator;

            [TestInitialize()]
            public void Initialize()
            {
                mockCalculator = new Mock<CentralTendencyCalculator>();
            }

            [TestMethod]
            public void TestGetResultsForMode()
            {
                string expectedResult = "15,26,39,16,29";
                var coursesViewedList = getMockDataForCoursesViewedList();
                var coursesViewedForEachStudent = getMockDataForCoursesViewedForEachStudent();

                mockCalculator.Setup(m => m.GetMode(coursesViewedList)).Returns(coursesViewedList);
                CentralTendencyOfViewedCoursesByUsersService centralTendencyOfViewedCoursesByUsersService = new CentralTendencyOfViewedCoursesByUsersService(coursesViewedForEachStudent, mockCalculator.Object);

                ObservableCollection<CentralTendencyResult> result = centralTendencyOfViewedCoursesByUsersService.GetResults();
                Assert.AreEqual(expectedResult, result.ElementAt(0).Mode);
            }

            [TestMethod]
            public void TestGetResultsForAverage()
            {
                var coursesViewedList = getMockDataForCoursesViewedList();
                var coursesViewedForEachStudent = getMockDataForCoursesViewedForEachStudent();

                double expectedResult = 25;
                mockCalculator.Setup(m => m.GetAverage(coursesViewedList)).Returns(expectedResult);
                mockCalculator.Setup(m => m.GetMode(coursesViewedList)).Returns(coursesViewedList);

                CentralTendencyOfViewedCoursesByUsersService centralTendencyOfViewedCoursesByUsersService = new CentralTendencyOfViewedCoursesByUsersService(coursesViewedForEachStudent, mockCalculator.Object);

                ObservableCollection<CentralTendencyResult> result = centralTendencyOfViewedCoursesByUsersService.GetResults();
                Assert.AreEqual(expectedResult, result.ElementAt(0).Average);
            }

            [TestMethod]
            public void TestGetResultsForMedian()
            {
                var coursesViewedList = getMockDataForCoursesViewedList();
                var coursesViewedForEachStudent = getMockDataForCoursesViewedForEachStudent();
                double expectedResult = 26;
                mockCalculator.Setup(m => m.GetMode(coursesViewedList)).Returns(coursesViewedList);
                mockCalculator.Setup(m => m.GetMedian(coursesViewedList)).Returns(expectedResult);


                CentralTendencyOfViewedCoursesByUsersService centralTendencyOfViewedCoursesByUsersService = new CentralTendencyOfViewedCoursesByUsersService(coursesViewedForEachStudent, mockCalculator.Object);

                ObservableCollection<CentralTendencyResult> result = centralTendencyOfViewedCoursesByUsersService.GetResults();
                Assert.AreEqual(expectedResult, result.ElementAt(0).Median);
            }

            [TestMethod]
            public void TestFailsOnEmptyModeList()
            {
                var coursesViewedList = getMockDataForCoursesViewedList();
                var coursesViewedForEachStudent = getMockDataForCoursesViewedForEachStudent();
                mockCalculator.Setup(m => m.GetMode(coursesViewedList)).Returns(new List<double>());

                CentralTendencyOfViewedCoursesByUsersService centralTendencyOfViewedCoursesByUsersService = new CentralTendencyOfViewedCoursesByUsersService(coursesViewedForEachStudent, mockCalculator.Object);

                var e = Assert.ThrowsException<InvalidOperationException>(() => centralTendencyOfViewedCoursesByUsersService.GetResults());
                Assert.AreEqual(e.Message, "Mode list is empty");
            }


            private List<double> getMockDataForCoursesViewedList()
            {
                return new List<double>()
                    {
                      15, 26, 39, 16, 29
                    };
            }
            private Dictionary<double, int> getMockDataForCoursesViewedForEachStudent()
            {
                return new Dictionary<double, int>()
                {
                    {1, 15},
                    {2, 26},
                    {3, 39},
                    {4, 16},
                    {5, 29}
                };
            }

        }
    }
}
