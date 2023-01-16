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
    public class PrintSheetViewModel : ViewModelBase
    {
        #region 01. Private Property
        protected internal ExternalEvent ExEvent;
        private UIApplication uiapp;
        private UIDocument uidoc;
        private Application app;
        private Document doc;

        #endregion

        #region 02. Public Property
        public UIDocument UiDoc;
        public Document Doc;
        public string SheetNumber { get; set; }
        public string Name { get; set; }
        
        public List<ViewSheetSet> AllSheetSetList { get; set; } = new List<ViewSheetSet>();
        public ViewSheetSet SelectedSheetSet { get; set; }
        public List<ViewSheetPlus> AllViewsSheetList { get; set; }
           = new List<ViewSheetPlus>();
        public List<ViewSheetPlus> SelectedViewsSheet { get; set; }
            = new List<ViewSheetPlus>();

        #endregion
        #region 03. View Model
        public PrintSheetViewModel(ExternalCommandData commandData)
        {
            // Lưu trữ data từ Revit
           
            uiapp = commandData.Application;
            uidoc = uiapp.ActiveUIDocument;
            app = uiapp.Application;
            doc = uidoc.Document;
            UiDoc = uidoc;
            Doc = UiDoc.Document;


            FilteredElementCollector colec = new FilteredElementCollector(doc);
            List<Element> allsheetset = colec.OfClass(typeof(ViewSheetSet)).ToElements().ToList();
            foreach (ViewSheetSet vs in allsheetset)
            {
                AllSheetSetList.Add(vs);
            }
            SelectedSheetSet = AllSheetSetList[0];

            List<ViewSheet> allviewsheet = new FilteredElementCollector(doc)
                .OfClass(typeof(ViewSheet))
                .OfCategory(BuiltInCategory.OST_Sheets)
                .Cast<ViewSheet>()
                .ToList();
            foreach (ViewSheet vs in allviewsheet)
            {
                ViewSheetPlus viewSheetPlus = new ViewSheetPlus(vs);
                AllViewsSheetList.Add(viewSheetPlus);
                SheetNumber = viewSheetPlus.SheetNumber;
                Name = viewSheetPlus.Name;
            }
            AllViewsSheetList.Sort((v1, v2)
                => String.CompareOrdinal(v1.SheetNumber, v2.SheetNumber));


        }
        #endregion
        #region 04. Method
        #endregion
    }
}



