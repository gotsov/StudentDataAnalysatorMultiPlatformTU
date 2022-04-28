using StudentDataAnalysatorMultiPlat.DatasetServices;
using StudentDataAnalysatorMultiPlat.Models;
using System.Collections.ObjectModel;

namespace StudentDataAnalysatorMultiPlat.Services.CalculationServices
{
    public class CorrelationAnalysisOfEditedWikisService
    {

        private ObservableCollection<CorrelationAnalysisResult> correlationResultCollection;
        private ObservableCollection<Student> studentsList;
        private ObservableCollection<Log> logsList;
        private List<double> studentGrades;
        private List<int> editedWikis;
        private float result;

        public CorrelationAnalysisOfEditedWikisService(ObservableCollection<Student> studentsList, ObservableCollection<Log> logsList)
        {
            this.logsList = logsList;
            this.studentsList = studentsList;
            correlationResultCollection = new ObservableCollection<CorrelationAnalysisResult>();
            editedWikis = new List<int>();
            studentGrades = new List<double>();
        }

        public ObservableCollection<CorrelationAnalysisResult> GetResults()
        {
            FillListsWithData();
            CalculateCorrelationResult();

            return correlationResultCollection;
        }
        private void FillListsWithData()
        {
            int studentsCount = 0;
            foreach (var student in studentsList)
            {
                countWikisEdited(student.Id.ToString(), studentsCount);
                studentGrades.Add(student.Result);
                studentsCount++;
            }
        }

        private void countWikisEdited(string studentId, int studentCount)
        {
            int wikisEdited = 0;
            foreach (Log log in logsList)
            {
                if (log.Description.Contains(studentId) && log.EventName == "Wiki page updated")
                {
                    wikisEdited++;
                }
            }
            editedWikis.Insert(studentCount, wikisEdited);
        }

        private void CalculateCorrelationResult()
        {
            result = CorrelationCoefficentCalculator.calculateCorrelationCoefficent(studentGrades.ToArray(), editedWikis.Select(Convert.ToDouble).ToArray());
            correlationResultCollection.Add(new CorrelationAnalysisResult(result));
        }

    }

}

