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
    public class FrequencyOfViewedCoursesService
    {
        private ObservableCollection<FrequencyDistributionResult> frequencyResult;
        private ObservableCollection<Log> logsList;
        private List<double> studentIds;
        private Dictionary<double, int> studentCoursesViewedDict;
        private SortedDictionary<int, int> frequencyViewedCoursesDict;

        public FrequencyOfViewedCoursesService(ObservableCollection<Log> logsList, List<double> studentIds)
        {
            this.logsList = logsList;
            this.studentIds = studentIds;

            frequencyResult = new ObservableCollection<FrequencyDistributionResult>();
            studentCoursesViewedDict = new Dictionary<double, int>();
            frequencyViewedCoursesDict = new SortedDictionary<int, int>();
        }

        public ObservableCollection<FrequencyDistributionResult>  GetResults()
        {
            //ExtractAllStudentsFromLogs();
            FillDictionaryWithCoursesViewedData();
            FillFrequencyOfViewedCourses();
            CalculateFrequencyDistributionResult();

            return frequencyResult;
        }

        //private void ExtractAllStudentsFromLogs()
        //{
        //    double studentId;
        //    foreach (Log log in logsList)
        //    {
        //        studentId = Double.Parse(log.Description.Substring(18, 4));
        //        if (!studentIds.Contains(studentId))
        //        {
        //            studentIds.Add(studentId);
        //        }
        //    }
        //}

        private void FillDictionaryWithCoursesViewedData()
        {
            int coursesViewed;
            foreach (double id in studentIds)
            {
                coursesViewed = 0;
                foreach (Log log in logsList)
                {
                    if (log.Description.Contains(id.ToString()) && log.EventName == "Course viewed")
                    {
                        coursesViewed++;
                        studentCoursesViewedDict[id] = coursesViewed;
                    }
                }
            }
        }

        private void FillFrequencyOfViewedCourses()
        {
            Dictionary<int, int> UnsortedFrequencies = new Dictionary<int, int>();
            int studentsCount;
            foreach (var student in studentCoursesViewedDict)
            {
                if (!UnsortedFrequencies.ContainsKey(student.Value))
                {
                    studentsCount = 1;
                    UnsortedFrequencies[student.Value] = studentsCount;
                }
                else
                {
                    UnsortedFrequencies.TryGetValue(student.Value, out studentsCount);
                    studentsCount++;
                    UnsortedFrequencies[student.Value] = studentsCount;
                }
            }
            frequencyViewedCoursesDict = new SortedDictionary<int, int>(UnsortedFrequencies);
        }

        private void CalculateFrequencyDistributionResult()
        {
            int absoluteFrequency;
            double relativeFrequency, totalPercentage = 0;

            absoluteFrequency = FrequencyDistributionCalculator.CalculateAbsoluteFrequency(frequencyViewedCoursesDict);

            foreach (var frequency in frequencyViewedCoursesDict)
            {
                relativeFrequency = FrequencyDistributionCalculator.CalculateRelativeFrequency(frequencyViewedCoursesDict, frequency.Value);
                frequencyResult.Add(new FrequencyDistributionResult(frequency.Key.ToString(), frequency.Value, relativeFrequency.ToString() + "%"));
                totalPercentage += relativeFrequency;
            }

            frequencyResult.Add(new FrequencyDistributionResult(
                "Общо",
                absoluteFrequency,
                Math.Round(totalPercentage, 1).ToString() + "%")
                );
        }
    }
}
