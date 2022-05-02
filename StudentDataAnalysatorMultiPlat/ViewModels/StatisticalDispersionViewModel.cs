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
        private ObservableCollection<Log> logsList;

        public StatisticalDispersionViewModel()
        {
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

        private void SetResultList(ObservableCollection<StatisticalDispersionResult> result)
        {
            DispersionResult = result;
        }
    }
}
