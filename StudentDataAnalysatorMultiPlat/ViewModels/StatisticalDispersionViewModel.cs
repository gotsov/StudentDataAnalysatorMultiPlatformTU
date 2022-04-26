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
    public class StatisticalDispersionViewModel : BaseViewModel
    {
        private ObservableCollection<StatisticalDispersionResult> dispersionResult;
        private List<double> studentIds;
        private List<int> coursesViewedByEachStudent;
        private ObservableCollection<Log> logsList;
        private Dictionary<double, int> studentCoursesViewedDict;
        public StatisticalDispersionViewModel()
        {
            studentIds = new List<double>();
            DispersionResult = new ObservableCollection<StatisticalDispersionResult>();
            CoursesViewedByEachStudent = new List<int>();
            StudentCoursesViewedDict = new Dictionary<double, int>();

            SingletonClass.TestEventAggregator.GetEvent<GetStatisticalDispersionResultEvent>().Subscribe(SetResultList);
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
        public ObservableCollection<StatisticalDispersionResult> DispersionResult
        {
            get { return dispersionResult; }
            set
            {
                dispersionResult = value;
                OnPropertyChanged("DispersionResult");
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
        public List<int> CoursesViewedByEachStudent
        {
            get { return coursesViewedByEachStudent; }
            set
            {
                coursesViewedByEachStudent = value;
                OnPropertyChanged("CoursesViewedByEachStudent");
            }
        }

        private void SetResultList(ObservableCollection<StatisticalDispersionResult> result)
        {
            DispersionResult = result;
        }
    }
}
