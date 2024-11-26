using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BIMSoftLib.MVVM;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using DHHTools;

namespace _06_04_RS2D_RebarSlab2D.Object
{
    public class ViewPlanInfor : PropertyChangedBase
    {
        private ViewPlan _selectViewPlan;
        public ViewPlan SelectViewPlan
        {
            get => _selectViewPlan;
            set
            {
                _selectViewPlan = value;
                OnPropertyChanged(nameof(SelectViewPlan));
                OnPropertyChanged(nameof(AllViewPlan));
            }
        }

        private ObservableRangeCollection<ViewPlan> _allViewPlan = new ObservableRangeCollection<ViewPlan>();
        public ObservableRangeCollection<ViewPlan> AllViewPlan
        {
            get
            {

                return _allViewPlan;
            }
            set
            {
                _allViewPlan = value;
                OnPropertyChanged(nameof(AllViewPlan));
                OnPropertyChanged(nameof(SelectViewPlan));
            }
        }

        private string _viewNameselect;
        public string ViewNameSelect
        {
            get => _viewNameselect;
            set
            {
                _viewNameselect = value;
                OnPropertyChanged(nameof(ViewNameSelect));
            }
        }


    }
}
