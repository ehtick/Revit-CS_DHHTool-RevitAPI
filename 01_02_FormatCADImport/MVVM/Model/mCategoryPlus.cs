using Autodesk.Revit.DB;
using BIMSoftLib.MVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace _01_02_FormatCADImport.MVVM.Model
{
    public class mCategoryPlus : PropertyChangedBase
    {
        private Category _category;
        public Category Category
        {
            get => _category;
            set
            {
                _category = value;
                OnPropertyChanged(nameof(Category));
            }
        }

        private string _name;
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        private string _lineWeightSelect;
        public string LineWeightSelect
        {
            get => _lineWeightSelect;
            set
            {
                _lineWeightSelect = value;
                OnPropertyChanged(nameof(LineWeightSelect));
                OnPropertyChanged(nameof(AllLineWeight));

            }
        }

        private ObservableRangeCollection<string> _allLineWeight = new ObservableRangeCollection<string>();
        public ObservableRangeCollection<string> AllLineWeight
        {
            get
            {

                return _allLineWeight;
            }
            set
            {
                _allLineWeight = value;
                OnPropertyChanged(nameof(AllLineWeight));
                OnPropertyChanged(nameof(LineWeightSelect));
            }
        }

        private string _linePatternSelect;
        public string LinePatternSelect
        {
            get => _linePatternSelect;
            set
            {
                _linePatternSelect = value;
                OnPropertyChanged(nameof(LinePatternSelect));
                OnPropertyChanged(nameof(AllLinePattern));

            }
        }

        private ObservableRangeCollection<string> _allLinePattern = new ObservableRangeCollection<string>();
        public ObservableRangeCollection<string> AllLinePattern
        {
            get
            {

                return _allLinePattern;
            }
            set
            {
                _allLinePattern = value;
                OnPropertyChanged(nameof(AllLinePattern));
                OnPropertyChanged(nameof(LinePatternSelect));
            }
        }

        public mCategoryPlus(Category category, Document document)
        {
            List<int> numberList = Enumerable.Range(1, 16).ToList();
            foreach (int number in numberList)
            {
                AllLineWeight.Add(number.ToString());
            }
            AllLineWeight.Insert(0, "<No Override>");
            LineWeightSelect = AllLineWeight[1];
            List<LinePatternElement> linePatterns = new FilteredElementCollector(document)
                .OfClass(typeof(LinePatternElement))
                .Cast<LinePatternElement>()
                .ToList();
            List<LinePatternElement> linePatternElements = linePatterns.OrderBy(x => x.Name).ToList();
            foreach (LinePatternElement linePattern in linePatternElements) { AllLinePattern.Add(linePattern.Name); }
            AllLinePattern.Insert(0, "Solid");
            AllLinePattern.Insert(0, "<No Override>");
            LinePatternSelect = AllLinePattern[0];
            Category = category;
            Name = category.Name;

        }
    }
}

