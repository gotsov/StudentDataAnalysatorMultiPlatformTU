﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentDataAnalysatorMultiPlat.Services
{
    public interface IExcelFileReaderService
    {
        public void ReadExcel(string path);
    }
}
