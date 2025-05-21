using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Autodesk.Revit.DB;
using BIMSoftLib.MVVM;
using DHHTools.MVVM.Model;
using Microsoft.Xaml.Behaviors.Core;

namespace _01_04_PrintMultiFiles.MVVM.Model
{
    public class mRevitDoc: PropertyChangedBase
    {
        private Document _revitFile;
        public Document RevitFile
        {
            get => _revitFile;
            set
            {
                _revitFile = value;
                OnPropertyChanged(nameof(RevitFile));
            }
        }
        private string _fileName;
        public string FileName
        {
            get => _fileName;
            set
            {
                _fileName = value;
                OnPropertyChanged(nameof(FileName));
            }
        }
        private ViewSheetSet _documentSelectSheetSet;
        public ViewSheetSet DocumentSelectSheetSet
        {
            get => _documentSelectSheetSet;
            set
            {
                _documentSelectSheetSet = value;
                OnPropertyChanged(nameof(DocumentSelectSheetSet));
                OnPropertyChanged(nameof(AllSheetSet));
                OnPropertyChanged(nameof(NumberSheet));
            }
        }
        private ObservableRangeCollection<ViewSheetSet> _allSheetSet = new ObservableRangeCollection<ViewSheetSet>();
        public ObservableRangeCollection<ViewSheetSet> AllSheetSet
        {
            get
            {

                return _allSheetSet;
            }
            set
            {
                _allSheetSet = value;
                OnPropertyChanged(nameof(AllSheetSet));
                OnPropertyChanged(nameof(DocumentSelectSheetSet));
            }
        }

        private ObservableRangeCollection<PrintSetting> _allPrintSetting = new ObservableRangeCollection<PrintSetting>();
        public ObservableRangeCollection<PrintSetting> AllPrintSetting
        {
            get
            {

                return _allPrintSetting;
            }
            set
            {
                _allPrintSetting = value;
                OnPropertyChanged(nameof(AllPrintSetting));
            }
        }

        private PrintSetting _printSettingSelect;
        public PrintSetting PrintSettingSelect
        {
            get => _printSettingSelect;
            set
            {
                _printSettingSelect = value;
                OnPropertyChanged(nameof(PrintSettingSelect));
                OnPropertyChanged(nameof(AllPrintSetting));
            }
        }

        private double _numberSheet;
        public double NumberSheet
        {
            get
            {
                _numberSheet = DocumentSelectSheetSet.Views.Size;
                return _numberSheet;
            }
            set
            {
                _numberSheet = value;
                OnPropertyChanged(nameof(NumberSheet));
            }
        }


    }
}
