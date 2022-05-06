using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using StudentDataAnalysatorMultiPlat.DatasetServices;
using StudentDataAnalysatorMultiPlat.Models;
using StudentDataAnalysatorMultiPlat.Services.CalculationServices;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace DatasetAnalysatorTest.CalculationServices
{
    [TestClass]
    public class DispersionOfViewedCoursesServiceTest
    {
        private Mock<StatisticalDispersionCalculator> mockCalculator;

        [TestInitialize()]
        public void Initialize()
        {
            mockCalculator = new Mock<StatisticalDispersionCalculator>();
        }

        [TestMethod]
        public void TestsGetResultsForVariance()
        {
            List<int> CoursesViewedByEachStudent = getSampleDataForCoursesViewedByEachStudent();
            var studentCoursesViewedDict = getDataForStudentCoursesViewedDict();

            mockCalculator.Setup(m => m.CalculateVariance(CoursesViewedByEachStudent)).Returns(100);
            DispersionOfViewedCoursesService dispersionOfViewedCoursesService = new DispersionOfViewedCoursesService(studentCoursesViewedDict, mockCalculator.Object);

            ObservableCollection<StatisticalDispersionResult> result = dispersionOfViewedCoursesService.GetResults();
            Assert.AreEqual(100, result.ElementAt(0).Variance);
        }

        [TestMethod]
        public void TestsGetResultsForMinMaxDispersion()
        {
            var CoursesViewedByEachStudent = getSampleDataForCoursesViewedByEachStudent();
            var studentCoursesViewedDict = getDataForStudentCoursesViewedDict();

            mockCalculator.Setup(m => m.CalculateStandartDeviation(CoursesViewedByEachStudent)).Returns(100);
            DispersionOfViewedCoursesService dispersionOfViewedCoursesService = new DispersionOfViewedCoursesService(studentCoursesViewedDict, mockCalculator.Object);

            ObservableCollection<StatisticalDispersionResult> result = dispersionOfViewedCoursesService.GetResults();
            Assert.AreEqual(100, result.ElementAt(0).StandartDeviation);
        }

        [TestMethod]
        public void TestsGetResultsForStandartDeviation()
        {
            var CoursesViewedByEachStudent = getSampleDataForCoursesViewedByEachStudent();
            var studentCoursesViewedDict = getDataForStudentCoursesViewedDict();

            mockCalculator.Setup(m => m.CalculateMinMaxDispersion(CoursesViewedByEachStudent)).Returns(100);
            DispersionOfViewedCoursesService dispersionOfViewedCoursesService = new DispersionOfViewedCoursesService(studentCoursesViewedDict, mockCalculator.Object);

            ObservableCollection<StatisticalDispersionResult> result = dispersionOfViewedCoursesService.GetResults();
            Assert.AreEqual(100, result.ElementAt(0).MinMaxDispersion);
        }

        private List<int> getSampleDataForCoursesViewedByEachStudent()
        {
            return new List<int>()
                    {
                        10, 12, 16
                    };
        }

        private Dictionary<double, int>  getDataForStudentCoursesViewedDict()
        {

            return new Dictionary<double, int>()
            {

                {1, 10},
                {2, 12},
                {3, 16}
            };
        }


}
}
