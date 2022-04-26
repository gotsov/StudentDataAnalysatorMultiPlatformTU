using StudentDataAnalysatorMultiPlat.DatasetServices;
using StudentDataAnalysatorMultiPlat.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentDataAnalysatorMultiPlat.Services.CalculationServices
{
    public class CentralTendencyOfViewedCoursesByUsersService
    {
        private ObservableCollection<CentralTendencyResult> tendencyResult;
        private ObservableCollection<Student> studentsList;
        private ObservableCollection<Log> logsList;
        private Dictionary<double, int> studentCoursesViewedDict;

        public CentralTendencyOfViewedCoursesByUsersService(ObservableCollection<Student> studentsList, ObservableCollection<Log> logsList)
        {
            this.studentsList = studentsList;
            this.logsList = logsList;

            tendencyResult = new ObservableCollection<CentralTendencyResult>();
            studentCoursesViewedDict = new Dictionary<double, int>();
        }

        public ObservableCollection<CentralTendencyResult> GetResults()
        {
            FillDictionaryWithCoursesViewedData();

            List<double> coursesViewedByUser = SetListToCalculateTendencies();

            CalculateCentralTendencyResult(coursesViewedByUser);

            return tendencyResult;
        }

        private void CalculateCentralTendencyResult(List<double> results)
        {
            double median = CentralTendencyCalculator.GetMedian(results);
            double average = Math.Round(CentralTendencyCalculator.GetAverage(results), 2);

            string modesToString = MergeModesToOneString(results);

            tendencyResult.Add(new CentralTendencyResult(median, modesToString, average));
        }

        private string MergeModesToOneString(List<double> results)
        {
            List<double> modes = CentralTendencyCalculator.GetMode(results);
            string modesToString = "";

            if (modes.Count() > 1)
            {
                modesToString += modes[0].ToString();
                modes.RemoveAt(0);

                foreach (double mode in modes)
                {
                    modesToString += ", " + mode.ToString();
                }
            }
            else
                modesToString += modes[0];

            return modesToString;
        }

        private void FillDictionaryWithCoursesViewedData()
        {
            int count;
            foreach (var student in studentsList)
            {
                count = 0;
                foreach (var log in logsList)
                {
                    if (log.Description.Contains(student.Id.ToString()) && log.EventName == "Course viewed")
                    {
                        count++;
                        studentCoursesViewedDict[student.Id] = count;
                    }
                }
            }
        }

        private List<double> SetListToCalculateTendencies()
        {
            List<double> result = new List<double>();

            foreach (int coursesViewed in studentCoursesViewedDict.Values)
            {
                result.Add(coursesViewed);
            }

            return result;
        }
    }
}
