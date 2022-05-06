using StudentDataAnalysatorMultiPlat.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatasetAnalysator.Helpers
{
    public class StudentDataHelper
    {
        private ObservableCollection<Log> logsList;
        private ObservableCollection<Student> studentsList; 


        public StudentDataHelper(ObservableCollection<Log> logsList, ObservableCollection<Student> studentsList)
        {
            this.logsList = logsList;
            this.studentsList = studentsList;
        }


        public Dictionary<double, int> CreateDictionaryWithCoursesViewedByStudents()
        {
            Dictionary<double, int> coursesViewedDict = new Dictionary<double, int>();
            int count;
            foreach (var student in studentsList)
            {
                count = 0;
                foreach (var log in logsList)
                {
                    if (log.Description.Contains(student.Id.ToString()) && log.EventName == "Course viewed")
                    {
                        count++;
                        coursesViewedDict[student.Id] = count;
                    }
                }
            }
            if (coursesViewedDict.Count == 0)
            {
                throw new InvalidOperationException("Courses viewed dictionary contains no elements");
            } 

            return coursesViewedDict;
        }
    }
}
