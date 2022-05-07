using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentDataAnalysatorMultiPlat.DatasetServices
{
    public class FrequencyDistributionCalculator
    {
        public virtual double CalculateRelativeFrequency(SortedDictionary<int, int> FrequencyViewedCoursesDict, int frequency)
        {
            double relativeFrequency;
            int absoluteFrequency = CalculateAbsoluteFrequency(FrequencyViewedCoursesDict);
            relativeFrequency = (Math.Round(((double)frequency / (double)absoluteFrequency) * 100, 2));
            return relativeFrequency;
        }

        public virtual int CalculateAbsoluteFrequency(SortedDictionary<int, int> FrequencyViewedCoursesDict)
        {
            int absoluteFrequency = 0;
            foreach (var entry in FrequencyViewedCoursesDict)
            {
                absoluteFrequency += entry.Value;
            }
            return absoluteFrequency;
        }
    }
}
