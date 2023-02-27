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
        private ViewSheetSet selectedSheetSet_VM;
        private string selectPrinter_VM;
        private string selectCADVersion_VM;
        #endregion
        #region 02. Public Property
        public UIDocument UiDoc;
        public Document Doc;
        public string SheetNumber { get; set; }
        public string Name { get; set; }
        public List<ViewSheetSet> AllSheetSetList { get; set; } = new List<ViewSheetSet>();
        public ViewSheetSet SelectedSheetSet 
        {
            get => selectedSheetSet_VM;
            set
            {
                selectedSheetSet_VM = value;
                OnPropertyChanged("selectedSheetSet");
            }
        }
        public List<string> AllPrinterList { get; set; } = new List<string>();
        public string SelectPrinter
        {
            get => selectPrinter_VM;
            set
            {
                selectPrinter_VM = value;
                OnPropertyChanged("SelectPrinter");
            }
        }
        public List<ViewSheetPlus> AllViewsSheetList { get; set; }
           = new List<ViewSheetPlus>();
        public List<ViewSheetPlus> SelectedViewsSheet { get; set; }
            = new List<ViewSheetPlus>();
        public List<string> AllCADVersionsList { get; set; }
        List<ElementId> sheetIDs { get; set; } = new List<ElementId>();
        public string SelectCADVersion
        
        {
            get => selectCADVersion_VM;
            set
            {
                selectCADVersion_VM = value;
                OnPropertyChanged("SelectCADVersion");
            }
        }
        
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
            foreach (Element item in allsheetset)
            {
                AllSheetSetList.Add(item as ViewSheetSet);
            }
            SelectedSheetSet = AllSheetSetList[0];

            foreach (string printer in System.Drawing.Printing.PrinterSettings.InstalledPrinters)
            {
                AllPrinterList.Add(printer);
            }
            AllPrinterList.Sort();
            SelectPrinter = AllPrinterList[0];
            AllCADVersionsList = new List<string>{"AutoCAD 2007", "AutoCAD 2010", "AutoCAD 2013", "AutoCAD 2018" };
            SelectCADVersion = AllCADVersionsList[0];
            
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
        public void exportDWF()
        {
            using (Transaction tran = new Transaction(doc))
            {
                tran.Start("Export DWF");
                DWFExportOptions dWFExportOptions = new DWFExportOptions();
                dWFExportOptions.MergedViews = true;
                //dWFExportOptions.PaperFormat = ExportPaperFormat.ISO_A1;
                
                doc.Export("C:\\Users\\Admin\\Documents", doc.Title, SelectedSheetSet.Views, dWFExportOptions);
                tran.Commit();
            }
        }
        public void exportDWG()
        {
            using (Transaction tran = new Transaction(doc))
            {
                tran.Start("Export DWG");
                DWGExportOptions dWGExportOptions = new DWGExportOptions();
                dWGExportOptions.MergedViews = true;
                dWGExportOptions.FileVersion = ACADVersion.R2007;
                if (SelectCADVersion == "AutoCAD 2007")
                { dWGExportOptions.FileVersion = ACADVersion.R2007; }
                else if (SelectCADVersion == "AutoCAD 2010")
                { dWGExportOptions.FileVersion = ACADVersion.R2010; }
                else if (SelectCADVersion == "AutoCAD 2013")
                { dWGExportOptions.FileVersion = ACADVersion.R2013; }
                else if (SelectCADVersion == "AutoCAD 2018")
                { dWGExportOptions.FileVersion = ACADVersion.R2018; }
                dWGExportOptions.TargetUnit = ExportUnit.Millimeter;
                foreach (View item in SelectedSheetSet.Views)
                { sheetIDs.Add(item.Id); }
                doc.Export("C:\\Users\\Admin\\Documents", " ", sheetIDs, dWGExportOptions);
                tran.Commit();
            }
        }
        public void exportPDF()
        {
            using (Transaction tran = new Transaction(doc))
            {
                tran.Start("Export PDF");
                PrintManager printManager = doc.PrintManager;
                // Set the printer name to "PDF"
                printManager.SelectNewPrintDriver("Microsoft Print to PDF");
                printManager.CombinedFile = true;
                printManager.PrintToFile = true;
                printManager.Apply();
                doc.Print(SelectedSheetSet.Views);
                tran.Commit();
            }
        }
        #endregion
    }
}
