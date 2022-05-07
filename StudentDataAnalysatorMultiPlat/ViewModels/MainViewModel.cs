using DatasetAnalysator.Helpers;
using StudentDataAnalysatorMultiPlat.Commands;
using StudentDataAnalysatorMultiPlat.DatasetServices;
using StudentDataAnalysatorMultiPlat.Events;
using StudentDataAnalysatorMultiPlat.Models;
using StudentDataAnalysatorMultiPlat.Services.CalculationServices;
using StudentDataAnalysatorMultiPlat.Services.ExcelServices;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static StudentDataAnalysatorMultiPlat.Enums.Enums;

namespace StudentDataAnalysatorMultiPlat.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        #region Fields
        private string selectedPath;
        private string selectedPathStudentsResults;
        private string selectedPathLogs;
        private bool isStudentsPathSelected;
        private bool isLogsPathSelected;
        private bool areBothPathsSelected;
        private string calculationButtonText;

        private Services.ExcelServices.ExcelDataReaderService excelDataReader = new Services.ExcelServices.ExcelDataReaderService();
        private CentralTendencyOfViewedCoursesByUsersService centralTendencyOfViewedCoursesByUsersService;
        private DispersionOfViewedCoursesService dispersionOfViewedCoursesService;
        private FrequencyOfViewedCoursesService frequencyOfViewedCoursesService;
        private CorrelationAnalysisOfEditedWikisService correlationAnalysisOfEditedWikisService;

        private ObservableCollection<Student> studentsList;
        private ObservableCollection<Log> logsList;

        private AsyncCommand searchFileCommand;
        private RelayCommand calculateCommand;

        //Calculation Results
        private ObservableCollection<FrequencyDistributionResult> frequencyResult;
        private ObservableCollection<CentralTendencyResult> tendencyResult;
        private ObservableCollection<StatisticalDispersionResult> dispersionResult;
        private ObservableCollection<CorrelationAnalysisResult> correlationResult; 
        #endregion

        public MainViewModel()
        {
            SelectedPathStudentsResults = "Избери файл с резултати на студентите (StudentsResults)";
            SelectedPathLogs = "Избери файл с дейности на студентите (Logs_Course)";
            CalculationButtonText = "Не са избрани файлове";

            SingletonClass.TestEventAggregator.GetEvent<UpdateListsEvent>().Subscribe(SendListsToSubscribedViewModels);
        }

        #region Properties
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

        public string SelectedPath
        {
            get
            {
                return selectedPath;
            }
            set
            {
                selectedPath = value;
                excelDataReader = new Services.ExcelServices.ExcelDataReaderService(SelectedPath);

                OnPropertyChanged("SelectedPath");

                GetExcelData(SelectedPath);
            }
        }

        public string SelectedPathStudentsResults
        {
            get { return selectedPathStudentsResults; }
            set
            {
                selectedPathStudentsResults = value;
                OnPropertyChanged("SelectedPathStudentsResults");
            }
        }

        public string SelectedPathLogs
        {
            get { return selectedPathLogs; }
            set
            {
                selectedPathLogs = value;
                OnPropertyChanged("SelectedPathLogs");
            }
        }

        public bool IsStudentsPathSelected
        {
            get { return isStudentsPathSelected; }
            set
            {
                isStudentsPathSelected = value;
                OnPropertyChanged("IsStudentsPathSelected");

                AreBothPathsSelected = IsStudentsPathSelected && IsLogsPathSelected ? true : false;
            }
        }

        public bool IsLogsPathSelected
        {
            get { return isLogsPathSelected; }
            set
            {
                isLogsPathSelected = value;
                OnPropertyChanged("IsLogsPathSelected");

                AreBothPathsSelected = IsStudentsPathSelected && IsLogsPathSelected ? true : false;
            }
        }

        public bool AreBothPathsSelected
        {
            get { return areBothPathsSelected; }
            set
            {
                areBothPathsSelected = value;
                OnPropertyChanged("AreBothPathsSelected");

                UpdateCommands();

                if (AreBothPathsSelected)
                    CalculationButtonText = "Калкулирай";
            }
        }

        public string CalculationButtonText
        {
            get { return calculationButtonText; }
            set
            {
                calculationButtonText = value;
                OnPropertyChanged("CalculationButtonText");
            }
        }
        #endregion

        #region Commands
        public AsyncCommand SearchFileCommand
        {
            get
            {
                if (searchFileCommand == null)
                {
                    searchFileCommand = new AsyncCommand(ExecuteOpenFileDialogAsync, CanExecuteOpenFileDialog);
                }
                return searchFileCommand;
            }
        }

        public RelayCommand CalculateCommand
        {
            get
            {
                if (calculateCommand == null)
                {
                    calculateCommand = new RelayCommand(CalculateStatistics, CanExecuteCalculateStatistics);
                }
                return calculateCommand;
            }
        }
        #endregion

        #region Methods
        private void GetExcelData(string path)
        {
            Services.ExcelServices.ExcelDataReaderService _excelDataReader = new Services.ExcelServices.ExcelDataReaderService(path);

            if (IsTableStudentsResults())
            {
                StudentsList = _excelDataReader.GetStudentListFromExcelTable();
                SelectedPathStudentsResults = "..." + SelectedPath[^37..];
                IsStudentsPathSelected = true;
            }
            else
            {
                LogsList = _excelDataReader.GetLogListFromExcelTable();
                SelectedPathLogs = "..." + SelectedPath[^38..];
                IsLogsPathSelected = true;
            }
        }

        private bool IsTableStudentsResults()
        {
            return excelDataReader.IsTableStudentsResults();
        }

        private bool IsSelectedFileExcel(string path)
        {
            return excelDataReader.IsFileExcel(path);
        }

        public void SendListsToSubscribedViewModels(string test)
        {
            SingletonClass.TestEventAggregator.GetEvent<GetFrequencyDistributionResultEvent>().Publish(frequencyResult);
            SingletonClass.TestEventAggregator.GetEvent<GetCentralTendencyResultEvent>().Publish(tendencyResult);
            SingletonClass.TestEventAggregator.GetEvent<GetStatisticalDispersionResultEvent>().Publish(dispersionResult);
            SingletonClass.TestEventAggregator.GetEvent<GetCorrelationAnalysisEvent>().Publish(correlationResult);
        }

        private async Task ExecuteOpenFileDialogAsync()
        {
            var result = await FilePicker.PickAsync();
            if (result != null && IsSelectedFileExcel(result.FullPath))
                SelectedPath = result.FullPath;
        }

        private bool CanExecuteOpenFileDialog()
        {
            return true;
        }

        private void CalculateStatistics(object o)
        {
            LogDataHelper logDataHelper = new LogDataHelper(LogsList);
            Dictionary<double, int> coursesViewedFromLog = logDataHelper.CreateDictionaryWithCoursesViewedFromLog();

            StudentDataHelper studentDataHelper = new StudentDataHelper(LogsList, StudentsList);
            Dictionary<double, int> coursesViewedFromStudents = studentDataHelper.CreateDictionaryWithCoursesViewedByStudents();

            frequencyOfViewedCoursesService = new FrequencyOfViewedCoursesService(logDataHelper, new FrequencyDistributionCalculator());
            frequencyResult = frequencyOfViewedCoursesService.GetResults();

            centralTendencyOfViewedCoursesByUsersService = new CentralTendencyOfViewedCoursesByUsersService(coursesViewedFromStudents, new CentralTendencyCalculator());
            tendencyResult = centralTendencyOfViewedCoursesByUsersService.GetResults();

            dispersionOfViewedCoursesService = new DispersionOfViewedCoursesService(coursesViewedFromLog, new StatisticalDispersionCalculator());
            dispersionResult = dispersionOfViewedCoursesService.GetResults();

            correlationAnalysisOfEditedWikisService = new CorrelationAnalysisOfEditedWikisService(StudentsList, LogsList);
            correlationResult = correlationAnalysisOfEditedWikisService.GetResults();

            CalculationButtonText = "Резултатите са калкулирани";
            AreBothPathsSelected = false;

            SendListsToSubscribedViewModels("");
        }


        private bool CanExecuteCalculateStatistics(object o)
        {
            return AreBothPathsSelected;
        }


        private void UpdateCommands()
        {
            CalculateCommand.RaiseCanExecuteChanged();
        } 
        #endregion
    }
}
