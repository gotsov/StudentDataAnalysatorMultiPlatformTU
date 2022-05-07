using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentDataAnalysatorMultiPlat.DatasetServices
{
    public class CentralTendencyCalculator
    {
        public virtual double GetMedian(List<double> list)
        {
            VerifyIfListIsEmpty(list, "List for calculating median value cannot be empty");
            int size = list.Count;
            int mid = size / 2;
            double median = (size % 2 != 0) ? list[mid] : (list[mid] + list[mid - 1]) / 2;
            return median;
        }

        public virtual List<Double> GetMode(List<double> list)
        {
            VerifyIfListIsEmpty(list, "List for calculating mode values cannot be empty");

            int maxcount = list.GroupBy(i => i).Max(grp => grp.Count());

            if (maxcount == 1)
            {
                return new List<double>();
            }
            List<double> modeList = list.GroupBy(i => i).Where(grp => grp.Count() == maxcount).Select(grp => grp.Key).ToList();
            return modeList;
        }

        public virtual double GetAverage(List<double> list)
        {
            VerifyIfListIsEmpty(list, "List for calculating average value cannot be empty");
            return list.Average(r => r);
        }

        private void VerifyIfListIsEmpty(List<double> list, string messageOnFail)
        {
            if (IsListEmpty(list))
            {
                throw new ArgumentException(messageOnFail);
            }
        }

        private bool IsListEmpty(List<double> list)
        {
            return list == null || !list.Any();
        }
    }
}
