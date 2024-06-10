using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using BIMSoftLib.MVVM;
using Microsoft.Xaml.Behaviors.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using _01_02_FormatCADImport.MVVM.View;
using _01_02_FormatCADImport.MVVM.Model;
using System.Windows;
using Application = Autodesk.Revit.ApplicationServices.Application;
using Autodesk.Revit.Creation;
using static _01_02_FormatCADImport.MVVM.Model.mHandler;


namespace _01_02_FormatCADImport.MVVM.ViewModel
{
    public class vmMain : PropertyChangedBase
    {
        private static vmMain _dcMain = new vmMain();
        public static vmMain DcMain { get { return _dcMain; } }
        public static UIApplication RevitApp;
        public static Application RevitAppService;  

        private ObservableRangeCollection<mImportInstancePlus> _dgSelectdImportCAD = new ObservableRangeCollection<mImportInstancePlus>();
        public ObservableRangeCollection<mImportInstancePlus> DgSelectedImportCAD
        {
            get
            {
                return _dgSelectdImportCAD;
            }
            set
            {
                _dgSelectdImportCAD = value;
                OnPropertyChanged(nameof(DgSelectedImportCAD));
            }
        }

        private ObservableRangeCollection<mImportInstancePlus> _dgAllImportCAD = new ObservableRangeCollection<mImportInstancePlus>();
        public ObservableRangeCollection<mImportInstancePlus> DgAllImportCAD
        {
            get
            {
                return _dgAllImportCAD;
            }
            set
            {
                _dgAllImportCAD = value;
                OnPropertyChanged(nameof(DgAllImportCAD));
            }
        }

        private ObservableRangeCollection<mCategoryPlus> _dgCategory = new ObservableRangeCollection<mCategoryPlus>();
        public ObservableRangeCollection<mCategoryPlus> DgCategory
        {
            get
            {
                return _dgCategory;
            }
            set
            {
                _dgCategory = value;
                OnPropertyChanged(nameof(DgCategory));
            }
        }

        private ObservableRangeCollection<mCategoryPlus> _dgCategoryGetTotal = new ObservableRangeCollection<mCategoryPlus>();
        public ObservableRangeCollection<mCategoryPlus> DgCategoryGetTotal
        {
            get
            {
                return _dgCategoryGetTotal;
            }
            set
            {
                _dgCategoryGetTotal = value;
                OnPropertyChanged(nameof(DgCategoryGetTotal));
                OnPropertyChanged(nameof(MaxValue));
            }
        }

        private ObservableRangeCollection<mCategoryPlus> _categoryPlusListChange = new ObservableRangeCollection<mCategoryPlus>();
        public ObservableRangeCollection<mCategoryPlus> CategoryPlusListChange
        {
            get
            {
                return _categoryPlusListChange;
            }
            set
            {
                _categoryPlusListChange = value;
                OnPropertyChanged(nameof(CategoryPlusListChange));
            }
        }

        private ObservableRangeCollection<string> _categoryListChange = new ObservableRangeCollection<string>();
        public ObservableRangeCollection<string> CategoryListChange
        {
            get
            {
                return _categoryListChange;
            }
            set
            {
                _categoryListChange = value;
                OnPropertyChanged(nameof(CategoryListChange));
            }
        }

        private System.Windows.Visibility _visibleWindow;

        public System.Windows.Visibility VisibleWindow
        {
            get => _visibleWindow;
            set
            {
                _visibleWindow = value;
                OnPropertyChanged(nameof(VisibleWindow));
            }

        }

        #region Process Bar 
        private double _percent;
        public double Percent
        {
            get
            {
                if( MaxValue == 0)
                {
                    _percent = 0;
                }
                else { _percent = LoadValue * 100 / MaxValue; }
                return _percent ;
            }
            set
            {
                _percent = value;
                OnPropertyChanged(nameof(LoadValue));
                OnPropertyChanged(nameof(Percent));
            }
        }

        private int _loadValue;
        public int LoadValue
        {
            get => _loadValue;
            set
            {
                _loadValue = value;
                OnPropertyChanged(nameof(LoadValue));
                OnPropertyChanged(nameof(Percent));
            }
        }

        private int _maxValue;
        public int MaxValue
        {
            get
            {
                return _maxValue =  DgCategoryGetTotal.Count; ;
            }
            set
            {
                _maxValue = value;
                OnPropertyChanged(nameof(MaxValue));
                OnPropertyChanged(nameof(Percent));
                OnPropertyChanged(nameof(DgCategoryGetTotal));
            }
        }

        #endregion

        #region vMain Command
        //Select
        private ActionCommand selectCADFile;
        public ICommand SelectCADFile
        {
            get
            {
                if (selectCADFile == null)
                {
                    selectCADFile = new ActionCommand(PerformSelectCADFile);
                }

                return selectCADFile;
            }
        }
        private void PerformSelectCADFile()
        {
            try
            {
                VisibleWindow = System.Windows.Visibility.Hidden;
                vGetCADFile vGetCADFileWin = new vGetCADFile();
                vGetCADFileWin.Show();
                DgAllImportCAD = methodAddCADFile.GetCADFile(RevitApp.ActiveUIDocument.Document);
                
            }
            catch { }
        }
        
        //Cancel
        private ActionCommand cancelSetCADFile;
        public ICommand CancelSetCADFile
        {
            get
            {
                if (cancelSetCADFile == null)
                {
                    cancelSetCADFile = new ActionCommand(PerformCancelSetCADFile);
                }

                return cancelSetCADFile;
            }
        }
        private void PerformCancelSetCADFile(object para)
        {
           (para as vMain).Close();
        }
        
        //Confirm
        private ActionCommand setCADFile;
        public ICommand SetCADFile
        {
            get
            {
                if (setCADFile == null)
                {
                    setCADFile = new ActionCommand(PerformSetCADFile);
                }

                return setCADFile;
            }
        }
        private void PerformSetCADFile(object par)
        {
            try
            {
                mHandler.DgCategory = DgCategory;
                mHandler.DgSelectedImportCAD = DgSelectedImportCAD;
                mHandlerEvent.Raise();
                //(par as vMain).Close();
            }
            catch { }
        }
        #endregion

        #region vGetFileCAD
        //Confirm
        private ActionCommand cFCADFile;
        public ICommand CFCADFile
        {
            get
            {
                if (cFCADFile == null)
                {
                    cFCADFile = new ActionCommand(PerformCFCADFile);
                }

                return cFCADFile;
            }
        }
        private void PerformCFCADFile(object par)
        {
            try
            {
                (par as vGetCADFile).Close();
                DgSelectedImportCAD.Clear();
                foreach(mImportInstancePlus instancePlus in DgAllImportCAD)
                {
                    if(instancePlus.IsCheck == true)
                    {
                        DgSelectedImportCAD.Add(instancePlus);
                    }    
                }
                DgCategory.Clear();
                DgCategory = methodAddCADFile.GetcategoryUnique(DgSelectedImportCAD,RevitApp.ActiveUIDocument.Document);
                DgCategoryGetTotal = methodAddCADFile.Getcategory(DgSelectedImportCAD, RevitApp.ActiveUIDocument.Document);
                VisibleWindow = System.Windows.Visibility.Visible;
            }
            catch { }

        }
        
        //Cancel
        private ActionCommand cancelSelectCADFile;
        public ICommand CancelSelectCADFile
        {
            get
            {
                if (cancelSelectCADFile == null)
                {
                    cancelSelectCADFile = new ActionCommand(PerformCancelSelectCADFile);
                }

                return cancelSelectCADFile;
            }
        }
        private void PerformCancelSelectCADFile(object parr)
        {
            (parr as  vGetCADFile).Close();
        }

        //Select None
        private ActionCommand selectNoneCAD;
        public ICommand SelectNoneCAD
        {
            get
            {
                if (selectNoneCAD == null)
                {
                    selectNoneCAD = new ActionCommand(PerformSelectNoneCAD);
                }

                return selectNoneCAD;
            }
        }
        private void PerformSelectNoneCAD()
        {
            methodAddCADFile.SelectNoneCAD(DgAllImportCAD);
        }

        //Select All
        private ActionCommand selectAllCAD;
        public ICommand SelectAllCAD
        {
            get
            {
                if (selectAllCAD == null)
                {
                    selectAllCAD = new ActionCommand(PerformSelectAllCAD);
                }

                return selectAllCAD;
            }
        }
        private void PerformSelectAllCAD()
        {
            methodAddCADFile.SelectAllCAD(DgAllImportCAD);
        }




        #endregion
    }
}

