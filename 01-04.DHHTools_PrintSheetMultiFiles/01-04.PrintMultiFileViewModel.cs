// ReSharper disable All
#region Namespaces
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
// ReSharper disable once RedundantUsingDirective
using System.IO;
using System.Windows.Forms;
using Application = Autodesk.Revit.ApplicationServices.Application;
using View = Autodesk.Revit.DB.View;

// ReSharper disable All
#endregion

namespace DHHTools
{
    public class PrintMultiFileViewModel : ViewModelBase
    {
        #region 01. Private Property
        protected internal ExternalEvent ExEvent;
        private UIApplication uiapp;
        private UIDocument uidoc;
        private Application app;
        private Document doc;
        private string modelPath_VM;
        private ViewSheetSet documentSelectSheetSet_VM;
        private string selectPrinter_VM;
        private string selectCADVersion_VM;
        private string selectFolder_VM;
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
        public string Modelpath
        {
            get => modelPath_VM;
            set
            {
                modelPath_VM = value;
                OnPropertyChanged("Modelpath");
            }

        }
        public ViewSheetSet DocumentSelectSheetSet
        {
            get => documentSelectSheetSet_VM;
            set
            {
                documentSelectSheetSet_VM = value;
                OnPropertyChanged("DocumentSelectSheetSet");
            }
        }
        public ObservableCollection<ViewSheetSet> DocumentsAllSheetSet 

        { get; set; }
            = new ObservableCollection<ViewSheetSet>();
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
        public ObservableCollection<Document> AllDocumentsList {  get; set; }
           = new ObservableCollection<Document>();
        public DocumentPlus DocPlus { get; set; }
        public List<string> AllCADVersionsList { get; set; }
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
        public PrintMultiFileViewModel(ExternalCommandData commandData)
        {
            // Lưu trữ data từ Revit
            uiapp = commandData.Application;

            uidoc = uiapp.ActiveUIDocument;
            app = uiapp.Application;
            doc = uidoc.Document;
            UiDoc = uidoc;
            Doc = UiDoc.Document;

            // Thư mục xuất file mặc định
            SelectFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            //Lấy tất cả các máy in và chọn máy in mặc đinh
            foreach (string printer in System.Drawing.Printing.PrinterSettings.InstalledPrinters)
            { AllPrinterList.Add(printer); }
            AllPrinterList.Sort();
            SelectPrinter = "Microsoft Print to PDF";

            //Lấy về tất cả các phiên bản CAD và chọn phiên bản mặc định
            AllCADVersionsList = new List<string> { "AutoCAD 2007", "AutoCAD 2010", "AutoCAD 2013", "AutoCAD 2018" };
            SelectCADVersion = AllCADVersionsList[0];
            DocPlus = new DocumentPlus(doc);
            AllDocumentsList.Add(DocPlus.Document);

            //foreach (DocumentPlus doc in AllDocumentsList)
            //{
            //   Modelpath = doc.Document.Title;
            //   DocumentsAllSheetSet = DhhDocumentUtil.GetAllSheetSet(doc.Document);
            //   DocumentSelectSheetSet = DocumentsAllSheetSet[0];
            //}
            IsCADSelected = true;
            IsDWFSelected = true;
            IsPDFSelected = true;
            IsSeprateFolder = false;
        }
        #endregion
        #region 04. Method
        //public void exportDWF()
        //{
        //    if (IsDWFSelected == true)
        //    {
        //        foreach (Document document in AllDocumentsList)
        //        {
        //            string exportFolder = document.Title;
        //            using (Transaction tran = new Transaction(document))
        //            {
        //                tran.Start("Export DWF");
        //                DWFExportOptions dWFExportOptions = new DWFExportOptions();
        //                dWFExportOptions.MergedViews = true;

        //                string DWFFolder = "";
        //                if (IsSeprateFolder == true)
        //                {
        //                    System.IO.Directory.CreateDirectory(SelectFolder + "\\" + exportFolder + "\\DWF");
        //                    DWFFolder = SelectFolder + "\\"+ exportFolder + "\\DWF";
        //                }
        //                else
        //                {
        //                    System.IO.Directory.CreateDirectory(SelectFolder + "\\" + exportFolder);
        //                    DWFFolder = SelectFolder + "\\" + exportFolder;
        //                }
        //                //documentPlus.Document.Export(DWFFolder, exportFolder, documentPlus.DocumentSelectSheetSet.Views, dWFExportOptions);
        //                tran.Commit();
        //            }
        //        }
                
        //    }

        //}
        //public void exportDWG()
        //{
        //    if (IsCADSelected == true)
        //    {
        //        foreach (DocumentPlus document in AllDocumentsList)
        //        {
        //            List<ElementId> sheetIDs = new List<ElementId>();
        //            using (Transaction tran = new Transaction(doc))
        //            {
        //                string exportFolder = document.Document.Title;
        //                tran.Start("Export DWG");
        //                DWGExportOptions dWGExportOptions = new DWGExportOptions();
        //                dWGExportOptions.MergedViews = true;
        //                dWGExportOptions.FileVersion = ACADVersion.R2007;
        //                if (SelectCADVersion == "AutoCAD 2007")
        //                { dWGExportOptions.FileVersion = ACADVersion.R2007; }
        //                else if (SelectCADVersion == "AutoCAD 2010")
        //                { dWGExportOptions.FileVersion = ACADVersion.R2010; }
        //                else if (SelectCADVersion == "AutoCAD 2013")
        //                { dWGExportOptions.FileVersion = ACADVersion.R2013; }
        //                else if (SelectCADVersion == "AutoCAD 2018")
        //                { dWGExportOptions.FileVersion = ACADVersion.R2018; }
        //                dWGExportOptions.TargetUnit = ExportUnit.Millimeter;
        //                foreach (View item in DocumentSelectSheetSet.Views)
        //                { sheetIDs.Add(item.Id); }
        //                string DWGFolder = "";
        //                if (IsSeprateFolder == true)
        //                {
        //                    System.IO.Directory.CreateDirectory(SelectFolder + "\\" + exportFolder + "\\DWG");
        //                    DWGFolder = SelectFolder + "\\" + exportFolder + "\\DWG";
        //                }
        //                else
        //                {
        //                    System.IO.Directory.CreateDirectory(SelectFolder + "\\" + exportFolder);
        //                    DWGFolder = SelectFolder + "\\" + exportFolder;
        //                }
        //                document.Document.Export(DWGFolder, " ", sheetIDs, dWGExportOptions);
        //                tran.Commit();
        //            }

        //        }
        //    }
        //}
        //public void exportPDF()
        //{
        //    if (IsPDFSelected == true)
        //    {
        //        foreach (DocumentPlus documentPlus in AllDocumentsList)
        //        {
        //            string exportFolder = documentPlus.Document.Title;
        //            using (Transaction tran = new Transaction(documentPlus.Document))
        //            {
        //                tran.Start("Export PDF");
        //                PrintManager printManager = documentPlus.Document.PrintManager;
        //                // Set the printer name to "PDF"
        //                printManager.SelectNewPrintDriver(SelectPrinter);
        //                printManager.CombinedFile = true;
        //                printManager.PrintToFile = true;
        //                printManager.PrintToFileName = SelectFolder + @"\" + exportFolder + ".pdf";
        //                printManager.Apply();
        //                doc.Print(documentPlus.DocumentSelectSheetSet.Views);

        //            }
        //        }  
        //    }

        //}
        public void deletePCPFile()
        {
            foreach (Document document in AllDocumentsList)
            {
                string exportFolder = document.Title;
                if (IsCADSelected == true)
                {
                    string DWGFolder = "";
                    if (IsSeprateFolder == true)
                    {
                        DWGFolder = SelectFolder + "\\" + exportFolder + "\\DWG";
                    }
                    else
                    {
                        DWGFolder = SelectFolder + "\\" + exportFolder;
                    }
                    string[] stringPath = Directory.GetFiles(DWGFolder, "*.pcp", SearchOption.TopDirectoryOnly);
                    foreach (string path in stringPath)
                    { System.IO.File.Delete(path); }
                }
            }
            
        }
        public void addFile()
        {
            OpenFileDialog file = new OpenFileDialog();
            {
                file.InitialDirectory = "C:\\";
                file.DefaultExt = "xls";
                file.Filter = "Revit File(*.rvt)|*.rvt" + "|All Files (*.*)|*.*";
                file.FilterIndex = 1;
                file.Multiselect = true;
                file.RestoreDirectory = true;
            }
            if (file.ShowDialog() == DialogResult.OK) //if there is a file chosen by the user
            {
                foreach (String filename in file.FileNames)
                {
                    Document openedDoc = app.OpenDocumentFile(filename);
                    DocumentPlus docPlus = new DocumentPlus(openedDoc);
                    AllDocumentsList.Add(openedDoc);
                }
            }
        }

        //public void UpdateAllDocumentList()
        //{
        //    foreach (DocumentPlus doc in AllDocumentsList)
        //    {
        //        Modelpath = doc.Document.Title;
        //        DocumentsAllSheetSet = DhhDocumentUtil.GetAllSheetSet(doc.Document);
                
        //    }

        //}
        #endregion
    }
}
