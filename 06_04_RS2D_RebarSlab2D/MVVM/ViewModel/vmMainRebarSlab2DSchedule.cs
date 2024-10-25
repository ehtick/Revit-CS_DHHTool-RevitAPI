using Microsoft.Xaml.Behaviors.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.UI;
using Autodesk.Revit.DB;
using BIMSoftLib.MVVM;
using System.Windows.Input;
using _06_04_RS2D_RebarSlab2D.MVVM.View;
using _06_04_RS2D_RebarSlab2D.MVVM.Model;

namespace _06_04_RS2D_RebarSlab2D.MVVM.ViewModel
{
    public class vmMainRebarSlab2DSchedule : PropertyChangedBase
    {
        #region Khai báo View Model
        private static vmMainRebarSlab2DSchedule _dcMainRSlabSchedule = new vmMainRebarSlab2DSchedule();
        public static vmMainRebarSlab2DSchedule DcMainRSlabSchedule { get { return _dcMainRSlabSchedule; } }
        public static UIApplication RevitUIApp;
        public static Application RevitApp;
        public static mViewPlan mViewPlan = new mViewPlan();
        #endregion

        #region View Model Property
        private ViewPlan _viewPlanThepSan;
        public ViewPlan ViewPlanThepSan
        {
            get
            {
                return _viewPlanThepSan;
            }
            set
            {
                _viewPlanThepSan = value;
                OnPropertyChanged(nameof(ViewPlanThepSan));
            }
        }

        private ActionCommand btnOK;

        public ICommand BtnOK
        {
            get
            {
                if (btnOK == null)
                {
                    btnOK = new ActionCommand(PerformBtnOK);
                }

                return btnOK;
            }
        }

        private void PerformBtnOK(object par)
        {
            (par as vMainRebarSlab2DSchedule).Close();
            // Create a filtered element collector to collect all Views in the document
            FilteredElementCollector collector = new FilteredElementCollector(RevitUIApp.ActiveUIDocument.Document)
                .OfClass(typeof(ViewPlan))
                .OfCategory(BuiltInCategory.OST_Views);

            // Use LINQ to find the view with the matching name
            ViewPlan viewPlan = collector
                .Cast<ViewPlan>()
                .FirstOrDefault(v =>  v.Name.Equals("01.MBTS_Tang01_Duoi", StringComparison.InvariantCultureIgnoreCase));
            mViewPlan.RebarSchedule2D((ViewPlan)viewPlan);
        }

        private ActionCommand _btnCancel;

        public ICommand BtnCancel
        {
            get
            {
                if (_btnCancel == null)
                {
                    _btnCancel = new ActionCommand(PerformBtnCancel);
                }

                return _btnCancel;
            }
        }
        private void PerformBtnCancel(object par)
        {
            (par as vMainRebarSlab2DSchedule).Close();
        }
        #endregion
    }
}
