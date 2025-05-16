using Microsoft.Xaml.Behaviors.Core;
using Autodesk.Revit.UI;
using BIMSoftLib.MVVM;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Autodesk.Revit.DB;
using System.Collections.Generic;
using DHHTools.MVVM.Model;
using Application = Autodesk.Revit.ApplicationServices.Application;
using static _01_04_PrintMultiFiles.MVVM.Model.mRevitDoc;
using _01_04_PrintMultiFiles.MVVM.Model;
using System;
using System.IO;

namespace _01_04_PrintMultiFiles.MVVM.ViewModel
{
    public class vmMain : PropertyChangedBase
    {
        private static vmMain _dcMain = new vmMain();
        public static vmMain DcMain { get { return _dcMain; } }
        private ObservableRangeCollection<mRevitDoc> _dgRevitFile = new ObservableRangeCollection<mRevitDoc>();
        public ObservableRangeCollection<mRevitDoc> DgRevitFile
        {
            get
            {

                return _dgRevitFile;
            }
            set
            {
                _dgRevitFile = value;
                OnPropertyChanged(nameof(DgRevitFile));
            }
        }
        public static UIApplication RevitApp;
        public static Application RevitAppService;
        public List<string> CADVersionList { get; set; } = new List<string> { "AutoCAD 2007", "AutoCAD 2010", "AutoCAD 2013", "AutoCAD 2018" };
        private ObservableRangeCollection<string> _allPrinterList = new ObservableRangeCollection<string>();
        public ObservableRangeCollection<string> AllPrinterList
        {
            get
            {
                foreach (string printer in System.Drawing.Printing.PrinterSettings.InstalledPrinters)
                {
                    _allPrinterList.Add(printer);
                }
                return _allPrinterList;
            }
            set
            {
                _allPrinterList = value; OnPropertyChanged(nameof(AllPrinterList));
            }
        }
        private string _selectPrinter = "Microsoft Print to PDF";
        public string SelectPrinter
        {
            get => _selectPrinter;
            set
            {
                _selectPrinter = value;
                OnPropertyChanged(nameof(SelectPrinter));
            }
        }
        readonly mRevit mRevit = new mRevit();
        private string _selectCADVersion = "AutoCAD 2007";
        public string SelectCADVersion
        {
            get => _selectCADVersion;
            set
            {
                _selectCADVersion = value;
                OnPropertyChanged(nameof(SelectCADVersion));
            }
        }
        private string _selectFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        public string SelectFolder

        {
            get => _selectFolder;
            set
            {
                _selectFolder = value;
                OnPropertyChanged(nameof(SelectFolder));
            }
        }
        private bool _isCADSelected = true;
        public bool IsCADSelected
        {
            get => _isCADSelected;
            set
            {
                _isCADSelected = value;
                OnPropertyChanged(nameof(IsCADSelected));
            }
        }
        private bool _isDWFSelected = true;
        public bool IsDWFSelected
        {
            get => _isDWFSelected;
            set
            {
                _isDWFSelected = value;
                OnPropertyChanged(nameof(IsDWFSelected));
            }
        }
        private bool _isPDFSelected = false;
        public bool IsPDFSelected
        {
            get => _isPDFSelected;
            set
            {
                _isPDFSelected = value;
                OnPropertyChanged(nameof(IsPDFSelected));
            }
        }
        private bool _isSeprateByFile = true;
        public bool IsSeprateByFile
        {
            get => _isSeprateByFile;
            set
            {
                _isSeprateByFile = value;
                OnPropertyChanged(nameof(IsSeprateByFile));
            }
        }
        private bool _isSeprateFolder = true;
        public bool IsSeprateFolder
        {
            get => _isSeprateFolder;
            set
            {
                _isSeprateFolder = value;
                OnPropertyChanged(nameof(IsSeprateFolder));
            }
        }
        private bool _isAddSheetName = true;
        public bool IsAddSheetName
        {
            get => _isAddSheetName;
            set
            {
                _isAddSheetName = value;
                OnPropertyChanged(nameof(IsAddSheetName));
            }
        }


        #region Method
        private ActionCommand _addFileBtn;
        public ICommand AddFileBtn
        {
            get
            {
                if (_addFileBtn == null)
                {
                    _addFileBtn = new ActionCommand(PerformAddFileBtn);
                }

                return _addFileBtn;
            }
        }
        private void PerformAddFileBtn()
        {
            try
            {
                List<Document> listDoc = mRevit.addFile();
                foreach (Document doc in listDoc)
                {
                    mRevitDoc revitDoc = mRevit.CreaterevitDoc(doc);
                    DgRevitFile.Add(revitDoc);
                }
            }
            catch
            {
            }

        }

        private ActionCommand exportFile;
        public ICommand ExportFile
        {
            get
            {
                if (exportFile == null)
                {
                    exportFile = new ActionCommand(PerformExportFile);
                }

                return exportFile;
            }
        }
        private void PerformExportFile()
        {
            try
            {   
                foreach (mRevitDoc mRevitDoc in DgRevitFile)
                {
                    string FileFullName = mRevitDoc.FileName;
                    FileInfo fileInfo = new FileInfo(FileFullName);
                    string FileName = Path.GetFileNameWithoutExtension(fileInfo.FullName);
                    mRevit.createFolder(FileName, IsSeprateByFile, IsSeprateFolder, SelectFolder, IsCADSelected, IsDWFSelected, IsPDFSelected);
                    mRevit.exportDWF(mRevitDoc, SelectFolder, IsSeprateFolder, IsSeprateByFile, FileName);
                    mRevit.exportPDF(mRevitDoc, SelectFolder, IsSeprateFolder, IsSeprateByFile, FileName);
                    mRevit.exportDWG(mRevitDoc, SelectFolder, SelectCADVersion, IsCADSelected, IsSeprateFolder,IsSeprateByFile ,IsAddSheetName, FileName);
                    mRevit.deletePCPFile(IsCADSelected,IsSeprateByFile, IsSeprateFolder, SelectFolder, FileName);
                }
            }
            catch { }
        }

        private ActionCommand saveLocation;

        public ICommand SaveLocation
        {
            get
            {
                if (saveLocation == null)
                {
                    saveLocation = new ActionCommand(PerformSaveLocation);
                }

                return saveLocation;
            }
        }

        private void PerformSaveLocation()
        {
            SelectFolder = mRevit.SelectFolder();
        }
        #endregion
    }
}