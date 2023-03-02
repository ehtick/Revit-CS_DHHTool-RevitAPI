#region Namespaces
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using Autodesk.Revit.DB.Structure;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Interop;
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
        private string selectFolder_VM;
        private bool isSelected_VM;
        private bool isCADSelected_VM;
        private bool isDWFSelected_VM;
        private bool isPDFSelected_VM;
        private bool isSeprateFolder_VM;
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
                OnPropertyChanged("SelectedSheetSet");
                OnPropertyChanged("SelectedViewsSheet");
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
        public string SelectFolder

        {
            get => selectFolder_VM;
            set
            {
                selectFolder_VM = value;
                OnPropertyChanged("SelectFolder");
            }
        }
        public bool IsSelected
        {
            get => isSelected_VM;
            set
            {
                isSelected_VM = value;
                OnPropertyChanged("IsSelected");
            }
        }
        public bool IsCADSelected
        {
            get => isCADSelected_VM;
            set
            {
                isCADSelected_VM = value;
                OnPropertyChanged("IsCADSelected");
            }
        }
        public bool IsDWFSelected
        {
            get => isDWFSelected_VM;
            set
            {
                isDWFSelected_VM = value;
                OnPropertyChanged("IsDWFSelected");
            }
        }
        public bool IsPDFSelected
        {
            get => isPDFSelected_VM;
            set
            {
                isPDFSelected_VM = value;
                OnPropertyChanged("IsPDFSelected");
            }
        }
        public bool IsSeprateFolder
        {
            get => isSeprateFolder_VM;
            set
            {
                isSeprateFolder_VM = value;
                OnPropertyChanged("IsSeprateFolder");
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
            SelectFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
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
            AllCADVersionsList = new List<string>{"AutoCAD 2007", "AutoCAD 2010", "AutoCAD 2013", "AutoCAD 2018" };
            SelectCADVersion = AllCADVersionsList[0];
            SelectPrinter = "Microsoft Print to PDF";
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
                viewSheetPlus.IsSelected = false;
            }
            AllViewsSheetList.Sort((v1, v2)
                => String.CompareOrdinal(v1.SheetNumber, v2.SheetNumber));
            IsCADSelected = true;
            IsDWFSelected = true;
            IsPDFSelected = true;
            IsSeprateFolder = false;


        }
        #endregion
        #region 04. Method
        public void exportDWF()
        {
            if (IsDWFSelected==true)
            {using (Transaction tran = new Transaction(doc))
                {
                    tran.Start("Export DWF");
                    DWFExportOptions dWFExportOptions = new DWFExportOptions();
                    dWFExportOptions.MergedViews = true;
                    string DWFFolder = "";
                    if (IsSeprateFolder == true)
                    {
                        DWFFolder = SelectFolder + "\\DWF";
                    }
                    else
                    {
                        DWFFolder = SelectFolder;
                    }
                    doc.Export(DWFFolder, doc.Title, SelectedSheetSet.Views, dWFExportOptions);
                    tran.Commit();
                }
            }
            
        }
        public void exportDWG()
        {
            if(IsCADSelected == true)
            {using (Transaction tran = new Transaction(doc))
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
                    string DWGFolder = "";
                    if (IsSeprateFolder == true)
                    {
                        DWGFolder = SelectFolder + "\\DWG";
                    }
                    else
                    {
                        DWGFolder = SelectFolder;
                    }
                    doc.Export(DWGFolder, " ", sheetIDs, dWGExportOptions);
                    tran.Commit();
                }
            }
        }
        public void exportPDF()
        {
            if (IsPDFSelected ==true)
            {
                using (Transaction tran = new Transaction(doc))
                {
                    tran.Start("Export PDF");
                    PrintManager printManager = doc.PrintManager;
                    // Set the printer name to "PDF"
                    printManager.SelectNewPrintDriver(SelectPrinter);
                    printManager.CombinedFile = true;
                    printManager.PrintToFile = true;
                    printManager.PrintToFileName = SelectFolder + @"\" + doc.Title + ".pdf";
                    
                    printManager.Apply();
                    doc.Print(SelectedSheetSet.Views);
                    tran.Commit();
                }
            }
            
        }
        public void updateViewSheet()
        {
            foreach (ViewSheetPlus vsPlus in AllViewsSheetList)
            { vsPlus.IsSelected = false;}
            List<string> SNList = new List<string>();
            foreach (ViewSheetPlus vsPlus in AllViewsSheetList)
            { SNList.Add(vsPlus.SheetNumber);}
            ViewSet viewSet = SelectedSheetSet.Views;
            foreach (ViewSheet vSheet in viewSet)
            { int i = SNList.IndexOf(vSheet.SheetNumber);
                if (i > -1) {AllViewsSheetList[i].IsSelected = true;}}
        }
        public void deletePCPFile()
        {
            if (IsCADSelected == true)
            {
                string DWGFolder = "";
                if (IsSeprateFolder == true)
                {
                    DWGFolder = SelectFolder + "\\DWG";
                }
                else
                {
                    DWGFolder = SelectFolder;
                }
                string[] stringPath = Directory.GetFiles(DWGFolder, "*.pcp", SearchOption.TopDirectoryOnly);
                foreach (string path in stringPath)
                { System.IO.File.Delete(path); }
            }
        }
        public void createFolder()
        {
            if (IsSeprateFolder == true)
            {
                if (IsCADSelected == true)
                {
                    System.IO.Directory.CreateDirectory(SelectFolder + "\\DWG");
                }

                if (IsDWFSelected == true)
                {
                    System.IO.Directory.CreateDirectory(SelectFolder + "\\DWF");
                }

                if (IsPDFSelected == true)
                {
                    System.IO.Directory.CreateDirectory(SelectFolder + "\\PDF");
                }

            }
        }
        #endregion
    }
}
