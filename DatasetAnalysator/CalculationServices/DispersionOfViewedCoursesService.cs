using DatasetAnalysator.CalculationServices;
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
        private List<int> coursesViewedByEachStudent;
        private LogDataHelper logHelper;

        public DispersionOfViewedCoursesService(LogDataHelper logHelper)
        {
            this.logHelper = logHelper;
            dispersionResult = new ObservableCollection<StatisticalDispersionResult>();
            coursesViewedByEachStudent = new List<int>();
        }

        public ObservableCollection<StatisticalDispersionResult> GetResults()
        {
            Dictionary<double, int> studentCoursesViewedDict = logHelper.CreateDictionaryWithCoursesViewed();
            FillCountOfViewedCoursesByStudents(studentCoursesViewedDict);
            CalculateDispersionResult();

            return dispersionResult;
        }

        private void FillCountOfViewedCoursesByStudents(Dictionary<double, int> studentCoursesViewedDict)
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
