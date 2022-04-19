using StudentDataAnalysatorMultiPlat.Commands;
using StudentDataAnalysatorMultiPlat.Models;
using StudentDataAnalysatorMultiPlat.Services;
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
        private string _selectedPath;
        private string _selectedPathStudentsResults;
        private string _selectedPathLogs;
        private bool _isStudentsPathSelected;
        private bool _isLogsPathSelected;
        private bool _areBothPathsSelected;
        private ExcelFileLoaderService _excelDataReader;

        private ObservableCollection<Student> studentsList;
        private ObservableCollection<Log> logsList;

        private AsyncCommand _searchFileCommand;

        public MainViewModel()
        {
            SelectedPathStudentsResults = "Избери файл с резултати на студентите (StudentsResults)";
            SelectedPathLogs = "Избери файл с дейности на студентите (Logs_Course)";
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

                //SingletonClass.TestEventAggregator.GetEvent<GetStudentsResultsListEvent>().Publish(StudentsList);
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

                //SingletonClass.TestEventAggregator.GetEvent<GetLogsListEvent>().Publish(LogsList);
            }
        }

        public string SelectedPath
        {
            get
            {
                return _selectedPath;
            }
            set
            {
                _selectedPath = value;
                _excelDataReader = new ExcelFileLoaderService(SelectedPath);

                if (IsSelectedFileExcel())
                {
                    OnPropertyChanged("SelectedPath");

                    GetExcelData(SelectedPath);
                }
            }
        }

        public string SelectedPathStudentsResults
        {
            get { return _selectedPathStudentsResults; }
            set
            {
                _selectedPathStudentsResults = value;
                OnPropertyChanged("SelectedPathStudentsResults");
            }
        }

        public string SelectedPathLogs
        {
            get { return _selectedPathLogs; }
            set
            {
                _selectedPathLogs = value;
                OnPropertyChanged("SelectedPathLogs");
            }
        }

        public bool IsStudentsPathSelected
        {
            get { return _isStudentsPathSelected; }
            set
            {
                _isStudentsPathSelected = value;
                OnPropertyChanged("IsStudentsPathSelected");

                AreBothPathsSelected = IsStudentsPathSelected && IsLogsPathSelected ? true : false;
            }
        }

        public bool IsLogsPathSelected
        {
            get { return _isLogsPathSelected; }
            set
            {
                _isLogsPathSelected = value;
                OnPropertyChanged("IsLogsPathSelected");

                AreBothPathsSelected = IsStudentsPathSelected && IsLogsPathSelected ? true : false;
            }
        }

        public bool AreBothPathsSelected
        {
            get { return _areBothPathsSelected; }
            set
            {
                _areBothPathsSelected = value;
                OnPropertyChanged("AreBothPathsSelected");
            }
        }

        public AsyncCommand SearchFileCommand
        {
            get
            {
                if (_searchFileCommand == null)
                {
                    _searchFileCommand = new AsyncCommand(ExecuteOpenFileDialogAsync, CanExecuteOpenFileDialog);
                }
                return _searchFileCommand;
            }
        }

        private void GetExcelData(string path)
        {
            ExcelFileLoaderService _excelDataReader = new ExcelFileLoaderService(path);

            if (IsTableStudentsResults())
            {
                StudentsList = _excelDataReader.StudentListFromExcelTable();
                SelectedPathStudentsResults = SelectedPath;
                IsStudentsPathSelected = true;
            }
            else
            {
                LogsList = _excelDataReader.LogListFromExcelTable();
                SelectedPathLogs = SelectedPath;
                IsLogsPathSelected = true;
            }
        }

        private bool IsTableStudentsResults()
        {
            bool test = _excelDataReader.GetTableType() == (int)TableTypeEnum.StudentsResultTable;
            return test;
        }

        private bool IsSelectedFileExcel()
        {
            return _excelDataReader.IsFileExcel(SelectedPath);
        }

        private async Task ExecuteOpenFileDialogAsync()
        {
            var result = await FilePicker.PickAsync();
            SelectedPath = result.FullPath;
        }

        private bool CanExecuteOpenFileDialog()
        {
            return true;
        }


    }
}
