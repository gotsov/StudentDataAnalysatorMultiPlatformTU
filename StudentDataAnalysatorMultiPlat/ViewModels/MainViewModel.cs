using StudentDataAnalysatorMultiPlat.Commands;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentDataAnalysatorMultiPlat.ViewModels
{
    public class MainViewModel : BaseViewModel
    {  
        private string _selectedPath;
        private bool _isStudentsPathSelected;
        private bool _isLogsPathSelected;
        private bool _areBothPathsSelected;

        private AsyncCommand _searchFileCommand;

        public MainViewModel()
        {
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
                OnPropertyChanged("SelectedPath");
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
            //ExcelFileLoaderService _excelDataReader = new ExcelFileLoaderService(path);

            //if (IsTableStudentsResults())
            //{
            //    //StudentsList = _excelDataReader.StudentListFromExcelTable();
            //    //SelectedPathStudentsResults = SelectedPath;
            //    IsStudentsPathSelected = true;
            //}
            //else
            //{
            //    //LogsList = _excelDataReader.LogListFromExcelTable();
            //    //SelectedPathLogs = SelectedPath;
            //    IsLogsPathSelected = true;
            //}
        }

        private async Task ExecuteOpenFileDialogAsync()
        {
            var result = await FilePicker.PickAsync();
            SelectedPath = result.FileName;
        }

        private bool CanExecuteOpenFileDialog()
        {
            return true;
        }
    }
}
