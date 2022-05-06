using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentDataAnalysatorMultiPlat.DatasetServices
{
    public class StatisticalDispersionCalculator
    {
        public virtual int CalculateMinMaxDispersion(List<int> CoursesViewedByEachStudent)
        {
            int minDispersion, maxDispersion, minMaxDispersion;
            minDispersion = CoursesViewedByEachStudent.Min();
            maxDispersion = CoursesViewedByEachStudent.Max();
            minMaxDispersion = maxDispersion - minDispersion;
            return minMaxDispersion;
        }
        public virtual double CalculateVariance(List<int> CoursesViewedByEachStudent)
        {
            double averageCountOfViewedCourses;
            averageCountOfViewedCourses = CoursesViewedByEachStudent.Average();
            double sumOfSquares = 0.0;
            foreach (int num in CoursesViewedByEachStudent)
            {
                sumOfSquares += Math.Pow((num - averageCountOfViewedCourses), 2.0);
            }
            return sumOfSquares / (double)(CoursesViewedByEachStudent.Count - 1);
        }
        public virtual double CalculateStandartDeviation(List<int> CoursesViewedByEachStudent)
        {
            return Math.Sqrt(CalculateVariance(CoursesViewedByEachStudent));
        }
    }
}
