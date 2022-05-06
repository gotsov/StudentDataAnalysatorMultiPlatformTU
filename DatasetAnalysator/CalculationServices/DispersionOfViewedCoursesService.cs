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
        Dictionary<double, int> studentCoursesViewedDict;
        private StatisticalDispersionCalculator statisticalDispersionCalculator;

        public DispersionOfViewedCoursesService(Dictionary<double, int> studentCoursesViewedDict, StatisticalDispersionCalculator statisticalDispersionCalculator)
        {
            this.statisticalDispersionCalculator = statisticalDispersionCalculator;
            this.studentCoursesViewedDict = studentCoursesViewedDict;
            dispersionResult = new ObservableCollection<StatisticalDispersionResult>();
            coursesViewedByEachStudent = new List<int>();
        }

        public ObservableCollection<StatisticalDispersionResult> GetResults()
        {
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
            minMaxDispersion = statisticalDispersionCalculator.CalculateMinMaxDispersion(coursesViewedByEachStudent);
            variance = statisticalDispersionCalculator.CalculateVariance(coursesViewedByEachStudent);
            standartDeviation = statisticalDispersionCalculator.CalculateStandartDeviation(coursesViewedByEachStudent);
            dispersionResult.Add(new StatisticalDispersionResult(minMaxDispersion, Math.Round(variance, 2), Math.Round(standartDeviation, 2)));
        }
    }
}

