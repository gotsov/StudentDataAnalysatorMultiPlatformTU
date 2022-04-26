using StudentDataAnalysatorMultiPlat.DatasetServices;
using StudentDataAnalysatorMultiPlat.Events;
using StudentDataAnalysatorMultiPlat.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentDataAnalysatorMultiPlat.ViewModels
{
    public class CentralTendencyViewModel : BaseViewModel
    {
        private ObservableCollection<CentralTendencyResult> tendencyResult;
        private ObservableCollection<Student> studentsList;
        private ObservableCollection<Log> logsList;
        private List<double> results = new List<double>();
        private Dictionary<double, int> studentCoursesViewedDict;

        public CentralTendencyViewModel()
        {
            TendencyResult = new ObservableCollection<CentralTendencyResult>();
            StudentCoursesViewedDict = new Dictionary<double, int>();

            SingletonClass.TestEventAggregator.GetEvent<GetCentralTendencyResultEvent>().Subscribe(SetResultList);
            SingletonClass.TestEventAggregator.GetEvent<UpdateListsEvent>().Publish("");
        }

        public ObservableCollection<Student> StudentsList
        {
            get
            {
                return studentsList;
            }
            set
            {
                studentsList = value;
                OnPropertyChanged("StudentsList");
            }
        }

        public ObservableCollection<Log> LogsList
        {
            get
            {
                return logsList;
            }
            set
            {
                logsList = value;
                OnPropertyChanged("LogsList");
            }
        }

        public ObservableCollection<CentralTendencyResult> TendencyResult
        {
            get { return tendencyResult; }
            set
            {
                tendencyResult = value;
                OnPropertyChanged("TendencyResult");
            }
        }

        public Dictionary<double, int> StudentCoursesViewedDict
        {
            get { return studentCoursesViewedDict; }
            set
            {
                studentCoursesViewedDict = value;
                OnPropertyChanged("StudentCoursesViewedDict");
            }
        }

        private void GetAllStudentsResults()
        {
            foreach (Student student in StudentsList)
            {
                results.Add(student.Result);
            }
        }

        private void SetResultList(ObservableCollection<CentralTendencyResult> result)
        {
            TendencyResult = result;
        }
    }
}
