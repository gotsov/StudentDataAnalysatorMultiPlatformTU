using StudentDataAnalysatorMultiPlat.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatasetAnalysator.Helpers
{
    public class LogDataHelper
    {
        private ObservableCollection<Log> logsList;
        private List<double> studentIds;


        public LogDataHelper(ObservableCollection<Log> logsList)
        {
            this.logsList = logsList;
            studentIds = ExtractAllStudentsFromLogs();
        }

        private List<double> ExtractAllStudentsFromLogs()
        {
            List<double> studentIds = new List<double>();
            double studentId;

            foreach (Log log in logsList)
            {
                studentId = double.Parse(log.Description.Substring(18, 4));
                if (!studentIds.Contains(studentId))
                {
                    studentIds.Add(studentId);
                }
            }

            return studentIds;
        }

        public Dictionary<double, int> CreateDictionaryWithCoursesViewedFromLog()
        {
            Dictionary<double, int> coursesViewedDict = new Dictionary<double, int>();
            int coursesViewed;
            foreach (double id in studentIds)
            {
                coursesViewed = 0;
                foreach (Log log in logsList)
                {
                    if (log.Description.Contains(id.ToString()) && log.EventName == "Course viewed")
                    {
                        coursesViewed++;
                        coursesViewedDict[id] = coursesViewed;
                    }
                }
            }
            return coursesViewedDict;
        }

    }
}
