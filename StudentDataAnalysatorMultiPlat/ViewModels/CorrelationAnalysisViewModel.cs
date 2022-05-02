﻿using StudentDataAnalysatorMultiPlat.Events;
using StudentDataAnalysatorMultiPlat.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentDataAnalysatorMultiPlat.ViewModels
{
    public class CorrelationAnalysisViewModel : BaseViewModel
    { 

        private ObservableCollection<CorrelationAnalysisResult> correlationResult;
        private ObservableCollection<Log> logsList;
        private ObservableCollection<Student> studentsList;


        public CorrelationAnalysisViewModel()
        {
            SingletonClass.TestEventAggregator.GetEvent<GetCorrelationAnalysisEvent>().Subscribe(SetResultList);
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

        public ObservableCollection<CorrelationAnalysisResult> CorrelationResult
        {
            get { return correlationResult; }
            set
            {
                correlationResult = value;
                OnPropertyChanged("CorrelationResult");
            }
        }

        private void SetResultList(ObservableCollection<CorrelationAnalysisResult> result)
        {
            CorrelationResult = result;
        }
    }
}
