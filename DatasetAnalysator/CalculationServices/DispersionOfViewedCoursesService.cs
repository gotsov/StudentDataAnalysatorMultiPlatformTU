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
    public class DispersionOfViewedCoursesService
    {
        private ObservableCollection<StatisticalDispersionResult> dispersionResult;
        private ObservableCollection<Log> logsList;
        private List<double> studentIds;
        private Dictionary<double, int> studentCoursesViewedDict;
        private List<int> coursesViewedByEachStudent;

        public DispersionOfViewedCoursesService(ObservableCollection<Log> logsList, List<double> studentIds)
        {
            this.logsList = logsList;
            this.studentIds = studentIds;

            dispersionResult = new ObservableCollection<StatisticalDispersionResult>();
            studentCoursesViewedDict = new Dictionary<double, int>();
            coursesViewedByEachStudent = new List<int>();
        }

        public ObservableCollection<StatisticalDispersionResult> GetResults()
        {
            FillDictionaryWithCoursesViewedData();
            FillCountOfViewedCoursesByStudents();
            CalculateDispersionResult();

            return dispersionResult;
        }
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
        private void FillCountOfViewedCoursesByStudents()
        {
            foreach (var student in studentCoursesViewedDict)
            {
                coursesViewedByEachStudent.Add(student.Value);
            }
            coursesViewedByEachStudent = coursesViewedByEachStudent.OrderBy(n => n).ToList();
        }
        private void CalculateDispersionResult()
        {
            int minMaxDispersion;
            double variance, standartDeviation;
            minMaxDispersion = StatisticalDispersionCalculator.CalculateMinMaxDispersion(coursesViewedByEachStudent);
            variance = StatisticalDispersionCalculator.CalculateVariance(coursesViewedByEachStudent);
            standartDeviation = StatisticalDispersionCalculator.CalculateStandartDeviation(coursesViewedByEachStudent);
            dispersionResult.Add(new StatisticalDispersionResult(minMaxDispersion, Math.Round(variance, 2), Math.Round(standartDeviation, 2)));
        }
    }
}
