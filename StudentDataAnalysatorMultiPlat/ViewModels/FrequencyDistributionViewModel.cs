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
    public class FrequencyDistributionViewModel : BaseViewModel
    {
        private ObservableCollection<FrequencyDistributionResult> frequencyResult;
        private List<double> studentIds;
        private ObservableCollection<Log> logsList;
        private List<double> results = new List<double>();
        private Dictionary<double, int> studentCoursesViewedDict;
        private SortedDictionary<int, int> frequencyViewedCoursesDict;
        public FrequencyDistributionViewModel()
        {
            SingletonClass.TestEventAggregator.GetEvent<GetFrequencyDistributionResultEvent>().Subscribe(SetResultList);
           
            SingletonClass.TestEventAggregator.GetEvent<UpdateListsEvent>().Publish("");
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
        public ObservableCollection<FrequencyDistributionResult> FrequencyResult
        {
            get { return frequencyResult; }
            set
            {
                frequencyResult = value;
                OnPropertyChanged("FrequencyResult");
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
        public SortedDictionary<int, int> FrequencyViewedCoursesDict
        {
            get { return frequencyViewedCoursesDict; }
            set
            {
                frequencyViewedCoursesDict = value;
                OnPropertyChanged("FrequencyViewedCoursesDict");
            }
        }

        private void SetResultList(ObservableCollection<FrequencyDistributionResult> result)
        {
            FrequencyResult = result;
        }
    }
}
