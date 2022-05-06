﻿using DatasetAnalysator.CalculationServices;
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
        public LogDataHelper logHelper { get; set; }
        public SortedDictionary<int, int> frequencyViewedCoursesDict { get; set; }
        public ObservableCollection<FrequencyDistributionResult> frequencyResult { get; set; }

        public FrequencyOfViewedCoursesService()
        {
            frequencyViewedCoursesDict = new SortedDictionary<int, int>();
            frequencyResult = new ObservableCollection<FrequencyDistributionResult>();
        }

        public FrequencyOfViewedCoursesService(LogDataHelper logHelper)
        {
            this.logHelper = logHelper;

            frequencyResult = new ObservableCollection<FrequencyDistributionResult>();
            frequencyViewedCoursesDict = new SortedDictionary<int, int>();
        }

        public ObservableCollection<FrequencyDistributionResult>  GetResults()
        {
            Dictionary<double, int> studentCoursesViewedDict = logHelper.CreateDictionaryWithCoursesViewed();

            FillFrequencyOfViewedCourses(studentCoursesViewedDict);
            CalculateFrequencyDistributionResult();

            return frequencyResult;
        }

        public void FillFrequencyOfViewedCourses(Dictionary<double, int> studentCoursesViewedDict)
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

        public void CalculateFrequencyDistributionResult()
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
