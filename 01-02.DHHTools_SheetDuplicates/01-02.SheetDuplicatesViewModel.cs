#region Namespaces
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using Autodesk.Revit.DB.Structure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using Application = Autodesk.Revit.ApplicationServices.Application;
using Curve = Autodesk.Revit.DB.Curve;
using PlanarFace = Autodesk.Revit.DB.PlanarFace;
using Point = Autodesk.Revit.DB.Point;

// ReSharper disable All
#endregion

namespace DHHTools
{
    public class SheetDuplicatesViewModel : ViewModelBase
    {
        #region 01. Private Property
        protected internal ExternalEvent ExEvent;
        private UIApplication uiapp;
        private UIDocument uidoc;
        private Application app;
        private Document doc;
        private bool CheckedAll_VM;
        private bool CheckedNone_VM;
        #endregion
        #region 02. Public Property
        public UIDocument UiDoc;
        public Document Doc;
        public string SheetNumber { get; set; }
        public string Name { get; set; }
        public bool CheckedAll
        {
            get => CheckedAll_VM;
            set
            {
                CheckedAll_VM = value;
                OnPropertyChanged("CheckedAll");
                //if (value)
                //UpdateAllViewsExtensions();
            }
        }
        public bool CheckedNone
        {
            get => CheckedNone_VM;
            set
            {
                CheckedNone_VM = value;
                OnPropertyChanged("CheckedNone");
                //if (value)
                //UpdateAllViewsExtensions();
            }
        }
        public List<ViewSheetPlus> AllViewsSheetList { get; set; }
            = new List<ViewSheetPlus>();
        public List<ViewSheetPlus> SelectedViewsSheet{ get; set; }
            = new List<ViewSheetPlus>();
        public List<FamilySymbol> AllFamiliesTitleFrame { get; set; }
            = new List<FamilySymbol>();
        public FamilySymbol SelectedFamilyTitleFrame { get; set; }
        public string ViewNamePrefix { get; set; }
        public string ViewNameSuffix { get; set; }
        public string SheetNamePrefix { get; set; }
        public string SheetNameSuffix { get; set; }
        
        #endregion
        #region 03. View Model
        public SheetDuplicatesViewModel(ExternalCommandData commandData)
        {
            // Lưu trữ data từ Revit
           
            uiapp = commandData.Application;
            uidoc = uiapp.ActiveUIDocument;
            app = uiapp.Application;
            doc = uidoc.Document;
            UiDoc = uidoc;
            Doc = UiDoc.Document;
            FilteredElementCollector allfamilyTitleblock = new FilteredElementCollector(doc)
                .OfClass(typeof(FamilySymbol))
                .OfCategory(BuiltInCategory.OST_TitleBlocks);
             allfamilyTitleblock.ToList();
            List<ViewSheet> allview = new FilteredElementCollector(doc)
                .OfClass(typeof(ViewSheet))
                .OfCategory(BuiltInCategory.OST_Sheets)
                .Cast<ViewSheet>()
                .ToList();
            foreach (FamilySymbol e in allfamilyTitleblock)
            {
                AllFamiliesTitleFrame.Add(e);
            }


            foreach (ViewSheet vs in allview)
            {
                ViewSheetPlus viewSheetPlus = new ViewSheetPlus(vs);
                AllViewsSheetList.Add(viewSheetPlus);
                SheetNumber = viewSheetPlus.SheetNumber;
                Name = viewSheetPlus.Name;
            }
            AllViewsSheetList.Sort((v1, v2)
                => String.CompareOrdinal(v1.SheetNumber, v2.SheetNumber));
            ViewNamePrefix = "Prefix";
            ViewNameSuffix = "Suffix";
            SheetNamePrefix = "Prefix";
            SheetNameSuffix = "Suffix";

        }
        #endregion
        #region 04. Method
        public void duplicateSheet()
        {
            using (Transaction tran = new Transaction(doc))
            {
                tran.Start("Sheet Duplicates");
                foreach (ViewSheetPlus vs in AllViewsSheetList)
                {

                    ViewSheet viewSheet = ViewSheet.Create(doc, SelectedFamilyTitleFrame.GetTypeId());
                    viewSheet.SheetNumber = ViewNamePrefix + vs.SheetNumber + ViewNameSuffix;
                    ICollection<ElementId> viewID = vs.ViewSheet.GetAllViewports();
                    foreach (ElementId vID in viewID)
                    {
                        View view = doc.GetElement(vID) as View;
                        //view.Duplicate(duplicateOption);
                    }    
                }
                tran.Commit();
            }

        }
        #endregion
    }
}



