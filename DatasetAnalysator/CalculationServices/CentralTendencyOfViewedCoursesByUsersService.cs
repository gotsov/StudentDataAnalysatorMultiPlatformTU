using DatasetAnalysator.Helpers;
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
        private Dictionary<double, int> studentCoursesViewedDict;
        private CentralTendencyCalculator centralTendencyCalculator;

        public CentralTendencyOfViewedCoursesByUsersService(Dictionary<double, int> studentCoursesViewedDict , CentralTendencyCalculator centralTendencyCalculator)
        {
            this.studentCoursesViewedDict = studentCoursesViewedDict;
            this.centralTendencyCalculator = centralTendencyCalculator;
            tendencyResult = new ObservableCollection<CentralTendencyResult>();
        }

        public ObservableCollection<CentralTendencyResult> GetResults()
        {
            List<double> coursesViewedByUser = CreateListFromCoursesViewed(studentCoursesViewedDict);

            CalculateCentralTendencyResult(coursesViewedByUser);

            return tendencyResult;
        }


        private List<double> CreateListFromCoursesViewed(Dictionary<double, int> studentCoursesViewedDict)
        {
            List<double> coursesViewedList = new List<double>();

            foreach (int coursesViewed in studentCoursesViewedDict.Values)
            {
                coursesViewedList.Add(coursesViewed);
            }

            return coursesViewedList;
        }

        private void CalculateCentralTendencyResult(List<double> results)
        {
            double median = centralTendencyCalculator.GetMedian(results);
            double average = Math.Round(centralTendencyCalculator.GetAverage(results), 2);

            List<double> modes = centralTendencyCalculator.GetMode(results);

            if (modes == null || !modes.Any())
            {
                throw new InvalidOperationException("Mode list is empty");
            }

            string mergedModes = String.Join(",", modes.ToArray());


            tendencyResult.Add(new CentralTendencyResult(median, mergedModes, average));
        }

    }
}
