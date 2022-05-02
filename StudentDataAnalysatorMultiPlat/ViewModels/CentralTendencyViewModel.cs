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

        public CentralTendencyViewModel()
        {
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

        private void SetResultList(ObservableCollection<CentralTendencyResult> result)
        {
            TendencyResult = result;
        }
    }
}
