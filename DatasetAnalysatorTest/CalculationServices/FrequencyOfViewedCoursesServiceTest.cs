using DatasetAnalysator.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
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
    [TestClass]
    public class FrequencyOfViewedCoursesServiceTest
    {
        private ObservableCollection<FrequencyDistributionResult> frequencyResultTest;
        private SortedDictionary<int, int> frequencyViewedCoursesDictTest;
        private LogDataHelper logHelperTest;

        public FrequencyOfViewedCoursesService frequencySerivce;

        [TestMethod]
        public void FillFrequencyOfViewedCoursesTest()
        {
            var input = new Dictionary<double, int>
            {
                {8429, 41},
                {8401, 106},
                {8380, 161},
                {8414, 173},
                {8427, 169}
            };

            SortedDictionary<int, int> expectedFrequencyViewedCoursesDict = new SortedDictionary<int, int>
            {
                {41, 1},
                {106, 1},
                {161, 1},
                {169, 1},
                {173, 1}
            };

            frequencySerivce = new FrequencyOfViewedCoursesService();

            frequencySerivce.FillFrequencyOfViewedCourses(input);
            frequencyViewedCoursesDictTest = frequencySerivce.frequencyViewedCoursesDict;

            for (int i = 0; i < frequencyViewedCoursesDictTest.Count(); i++)
            {
                Assert.AreEqual(expectedFrequencyViewedCoursesDict.TryGetValue(i, out _), 
                                    frequencyViewedCoursesDictTest.TryGetValue(i, out _));
            }
        }

        [TestMethod]
        public void CalculateFrequencyDistributionResultTest()
        {
            var input = new SortedDictionary<int, int>
            {
                {41, 1},
                {106, 1},
                {161, 1},
                {169, 1},
                {173, 1}
            };

            var expectedFrequencyResult = new ObservableCollection<FrequencyDistributionResult>
            {
                { new FrequencyDistributionResult("41", 1, "20%") },
                { new FrequencyDistributionResult("106", 1, "20%") },
                { new FrequencyDistributionResult("161", 1, "20%") },
                { new FrequencyDistributionResult("169", 1, "20%") },
                { new FrequencyDistributionResult("173", 1, "20%") },
                { new FrequencyDistributionResult("Общо", 1, "20%") }
            };

            frequencySerivce = new FrequencyOfViewedCoursesService();

            frequencySerivce.frequencyViewedCoursesDict = new SortedDictionary<int, int>
            {
                {41, 1},
                {106, 1},
                {161, 1},
                {169, 1},
                {173, 1}
            };

            frequencySerivce.CalculateFrequencyDistributionResult();
            frequencyResultTest = frequencySerivce.frequencyResult;

            for (int i = 0; i < frequencyResultTest.Count(); i++)
            {
                Assert.AreEqual(frequencyResultTest.ElementAt(i).ViewedCourses, 
                    expectedFrequencyResult.ElementAt(i).ViewedCourses);
            }
        }

        [TestMethod]
        public void GetResultsTest()
        {
            frequencyViewedCoursesDictTest = new SortedDictionary<int, int>
            {
                {41, 1},
                {106, 1},
                {161, 1},
                {169, 1},
                {173, 1}
            };

            DateTime dateTime = DateTime.Now;
            string exampleEventContext = "Example";
            string exampleComponent = "Example";
            string exampleEventName = "Example";
       

            logHelperTest = new LogDataHelper(new ObservableCollection<Log>
            {
                { new Log(dateTime, exampleEventContext, exampleComponent, "Course viewed", "The user with id '8429' viewed the course with id '130'.")},
                { new Log(dateTime, exampleEventContext, exampleComponent, exampleEventName, "The user with id '8429' viewed the 'wiki' activity with course module id '6512'.")},
                { new Log(dateTime, exampleEventContext, exampleComponent, "Course viewed", "The user with id '8429' viewed the course with id '130'.")},
                { new Log(dateTime, exampleEventContext, exampleComponent, "Course viewed", "The user with id '8401' viewed the course with id '130'.")},
                { new Log(dateTime, exampleEventContext, exampleComponent, "Course viewed", "The user with id '8380' viewed the course with id '130'.")},
                { new Log(dateTime, exampleEventContext, exampleComponent, "Course viewed", "The user with id '8414' viewed the course with id '130'.")}
            });

            var expectedFrequencyResult = new ObservableCollection<FrequencyDistributionResult>
            {
                { new FrequencyDistributionResult("1", 3, "75%") },
                { new FrequencyDistributionResult("2", 1, "25%") },
                { new FrequencyDistributionResult("Общо", 4, "100%") }
            };

            frequencySerivce = new FrequencyOfViewedCoursesService(logHelperTest);

            var actualFrequencyResult = frequencySerivce.GetResults();

            for (int i = 0; i < actualFrequencyResult.Count(); i++)
            {
                Assert.AreEqual(actualFrequencyResult.ElementAt(i).ViewedCourses,
                    expectedFrequencyResult.ElementAt(i).ViewedCourses);
            }
        }
    }
}
