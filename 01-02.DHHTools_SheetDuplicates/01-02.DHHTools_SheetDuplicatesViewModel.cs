#region Namespaces
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Structure;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
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
        
        #endregion
        #region 02. Public Property
        public UIDocument UiDoc;
        public Document Doc;

        public List<ViewSheet> AllViewsSheetList { get; set; }
            = new List<ViewSheet>();
        public List<ViewSheet> SelectedViewsSheet{ get; set; }
            = new List<ViewSheet>();
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
            List<ViewSheet> allview = new FilteredElementCollector(doc)
                .OfClass(typeof(ViewSheet))
                .OfCategory(BuiltInCategory.OST_Sheets)
                .Cast<ViewSheet>()
                .ToList();
            foreach (ViewSheet vs in allview)
            {
                AllViewsSheetList.Add(vs);
            }
            AllViewsSheetList.Sort((v1, v2)
                => String.CompareOrdinal(v1.SheetNumber, v2.SheetNumber));
        }
        #endregion
       

    }
}



