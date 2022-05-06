using DatasetAnalysator.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StudentDataAnalysatorMultiPlat.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatasetAnalysatorTest.Helpers
{
    [TestClass]
    public class StudentDataHelperTest
    {
        [TestMethod]
        public void testCreateDictionaryWithCoursesViewedByStudents()
        {
            DateTime dateTime = DateTime.Now;
            string exampleEventContext = "Example";
            string exampleComponent = "Example";

            ObservableCollection<Log> testLogData = (new ObservableCollection<Log>
            {
                { new Log(dateTime, exampleEventContext, exampleComponent, "Course viewed", "The user with id '8429' viewed the course with id '130'.")},
                { new Log(dateTime, exampleEventContext, exampleComponent, "Course viewed", "The user with id '8429' viewed the 'wiki' activity with course module id '6512'.")},
                { new Log(dateTime, exampleEventContext, exampleComponent, "Course viewed", "The user with id '8429' viewed the course with id '130'.")},
                { new Log(dateTime, exampleEventContext, exampleComponent, "Wiki updated",  "The user with id '8401' updated 'wiki' with course module id '6512'.")},
                { new Log(dateTime, exampleEventContext, exampleComponent, "Course viewed", "The user with id '8401' viewed the course with id '130'.")},
                { new Log(dateTime, exampleEventContext, exampleComponent, "Course viewed", "The user with id '8414' viewed the course with id '130'.")}
            });

            ObservableCollection<Student> testStudentsList = new ObservableCollection<Student>()
            {
                { new Student(8401, 4) },
                { new Student(8429, 6)},
                { new Student(8414, 2) }
            };

            StudentDataHelper studentDataHelper = new StudentDataHelper(testLogData , testStudentsList);

            Dictionary<double, int> expectedResult = new Dictionary<double, int>()
            {
                { 8429, 1 },
                { 8401, 3 },
                { 8414, 1 }
            };

            Dictionary<double, int> actualResult =  studentDataHelper.CreateDictionaryWithCoursesViewedByStudents();
            for (int i = 0; i < actualResult.Count(); i++)
            {
                Assert.AreEqual(actualResult.ElementAt(i).Value,
                    expectedResult.ElementAt(i).Value);
            }
        }
        [TestMethod]
        public void testThrowsExceptionWhenCoursesViewedIsEmpty()
        {
            StudentDataHelper studentDataHelper = new StudentDataHelper(new ObservableCollection <Log>() , new ObservableCollection<Student>());
            var e = Assert.ThrowsException<InvalidOperationException>(() => studentDataHelper.CreateDictionaryWithCoursesViewedByStudents());
            Assert.AreEqual(e.Message, "Courses viewed dictionary contains no elements");
        }

    }
}
