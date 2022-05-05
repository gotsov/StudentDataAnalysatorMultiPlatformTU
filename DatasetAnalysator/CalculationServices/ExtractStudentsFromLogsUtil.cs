using StudentDataAnalysatorMultiPlat.Models;
using System;
using System.Collections.ObjectModel;

namespace DatasetAnalysator.CalculationServices
{
    public static class ExtractStudentsFromLogsUtil
    {
        public static List<double> StudentsIds { get; set; } = new List<double>();

        public static void ExtractAllStudentsFromLogs(ObservableCollection<Log> logsList)
        {
            double studentId;

            foreach (Log log in logsList)
            {
                studentId = Double.Parse(log.Description.Substring(18, 4));
                if (!StudentsIds.Contains(studentId))
                {
                    StudentsIds.Add(studentId);
                }
            }
        }
    }
}
