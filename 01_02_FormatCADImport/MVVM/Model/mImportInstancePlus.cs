using Autodesk.Revit.DB;
using BIMSoftLib.MVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01_02_FormatCADImport.MVVM.Model
{
    public class mImportInstancePlus: PropertyChangedBase
    {
        private ImportInstance _importInstance;
        public ImportInstance ImportInstance
        {
            get => _importInstance;
            set
            {
                _importInstance = value;
                OnPropertyChanged(nameof(ImportInstance));
            }
        }

        private string _category;
        public string Category
        {
            get => _category;
            set
            {
                _category = value;
                OnPropertyChanged(nameof(Category));
            }
        }

        private bool _isCheck;
        public bool IsCheck
        {
            get => _isCheck;
            set
            {
                _isCheck = value;
                OnPropertyChanged(nameof(IsCheck));
            }
        }

        public mImportInstancePlus(ImportInstance importInstance)
        {
            IsCheck = true;
            ImportInstance = importInstance;
            Category = importInstance.Category.Name;
        }

    }
}
