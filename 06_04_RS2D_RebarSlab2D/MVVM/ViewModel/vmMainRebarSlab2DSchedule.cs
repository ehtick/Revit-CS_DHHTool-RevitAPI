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
using _06_04_RS2D_RebarSlab2D.Object;
using System.Windows;
using DHHTools;

namespace _06_04_RS2D_RebarSlab2D.MVVM.ViewModel
{
    public class vmMainRebarSlab2DSchedule : PropertyChangedBase
    {
        #region Khai báo View Model
        private static vmMainRebarSlab2DSchedule _dcMainRSlabSchedule = new vmMainRebarSlab2DSchedule();
        public static vmMainRebarSlab2DSchedule DcMainRSlabSchedule { get { return _dcMainRSlabSchedule; } }
        public static UIApplication RevitUIApp;
        public static Autodesk.Revit.ApplicationServices.Application RevitApp;
        private ObservableRangeCollection<ViewPlanInfor> _dgViewPlanInfor = new ObservableRangeCollection<ViewPlanInfor>();
        public ObservableRangeCollection<ViewPlanInfor> DgViewPlanInfor
        {
            get
            {

                return _dgViewPlanInfor;
            }
            set
            {
                _dgViewPlanInfor = value;
                OnPropertyChanged(nameof(DgViewPlanInfor));
            }
        }

        public static mRebarSchedule2D mRebarSchedule = new mRebarSchedule2D();
        public static ViewPlanInfor ViewPlanInforDraft = new ViewPlanInfor();
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
        #endregion

        #region Button
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
            //// Create a filtered element collector to collect all Views in the document
            //FilteredElementCollector collector = new FilteredElementCollector(RevitUIApp.ActiveUIDocument.Document)
            //    .OfClass(typeof(ViewPlan))
            //    .OfCategory(BuiltInCategory.OST_Views);

            //// Use LINQ to find the view with the matching name
            //ViewPlan viewPlan = collector
            //    .Cast<ViewPlan>()
            //    .FirstOrDefault(v =>  v.Name.Equals("01.MBTS_Tang01_Duoi", StringComparison.InvariantCultureIgnoreCase));
            XYZ startpointTitle = new XYZ (0, 0, 0);
            mRebarSchedule.RebarSchedule2DTitle(startpointTitle);
            int j = 0;
            for (int i = 0; i < DgViewPlanInfor.Count; i++)
            {
                XYZ startpointSchedule = new XYZ(0, 0, 0);
                if (i == 0)
                {
                    startpointSchedule = new XYZ(0, DhhUnitUtils.MmToFeet(-12), 0);
                }
                else
                {
                    FamilySymbol fsymbol_RebarSlab = (FamilySymbol)new FilteredElementCollector(RevitUIApp.ActiveUIDocument.Document)
                        .WhereElementIsElementType()
                        .OfCategory(BuiltInCategory.OST_DetailComponents)
                        .OfClass(typeof(FamilySymbol))
                        .Cast<FamilySymbol>()
                        .FirstOrDefault(s => s.Name.Contains("DHH_KC_ThepSan"));
                    FamilyInstanceFilter filter = new FamilyInstanceFilter(RevitUIApp.ActiveUIDocument.Document, fsymbol_RebarSlab.Id);
                    FilteredElementCollector elementsFilter = new FilteredElementCollector(RevitUIApp.ActiveUIDocument.Document, DgViewPlanInfor[i - 1].SelectViewPlan.Id);
                    List<Element> familyInstances = elementsFilter.WherePasses(filter).ToElements().ToList();
                    j = j + familyInstances.Count;
                    startpointSchedule = new XYZ(0, DhhUnitUtils.MmToFeet(-12 - j * 8), 0);
                }
                mRebarSchedule.RebarSchedule2D(DgViewPlanInfor[i].SelectViewPlan, startpointSchedule);
            }    
        }

        private ActionCommand _btnCancel;
        public ICommand BtnCancel
        {
            get
            {
                if (_btnCancel == null)
                { _btnCancel = new ActionCommand(PerformBtnCancel);}
                return _btnCancel;
            }
        }
        private void PerformBtnCancel(object par)
        {
            (par as vMainRebarSlab2DSchedule).Close();
        }

        private ActionCommand btnAdd;
        public ICommand BtnAdd
        {
            get
            {
                if (btnAdd == null)
                {
                    btnAdd = new ActionCommand(PerformBtnAdd);
                }

                return btnAdd;
            }
        }
        private void PerformBtnAdd()
        {
            ObservableRangeCollection<ViewPlan> allViewPlandraft = new ObservableRangeCollection<ViewPlan>();
            FilteredElementCollector collector = new FilteredElementCollector(RevitUIApp.ActiveUIDocument.Document)
                 .OfClass(typeof(ViewPlan))
                 .OfCategory(BuiltInCategory.OST_Views);
            List<ViewPlan> viewPlans = collector
                            .Cast<ViewPlan>()
                            .ToList();
            
            foreach (ViewPlan viewPlan in viewPlans)
            {
                allViewPlandraft.Add(viewPlan);
            }
            ViewPlan viewPlandraft = allViewPlandraft[0];
            ViewPlanInfor viewPlanInfor =
                new ViewPlanInfor
                {
                    AllViewPlan = allViewPlandraft,
                    SelectViewPlan = viewPlandraft,
                };
            DgViewPlanInfor.Add(viewPlanInfor);
        }

        private ActionCommand btnRemove;
        public ICommand BtnRemove
        {
            get
            {
                if (btnRemove == null)
                {
                    btnRemove = new ActionCommand(PerformBtnRemove);
                }

                return btnRemove;
            }
        }
        private void PerformBtnRemove()
        {
        }
        #endregion
    }
}
