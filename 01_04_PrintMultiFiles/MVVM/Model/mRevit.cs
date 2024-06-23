using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using _01_04_PrintMultiFiles.MVVM.Model;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Application = Autodesk.Revit.ApplicationServices.Application;
using BIMSoftLib.MVVM;
using System.IO;
using _01_04_PrintMultiFiles.MVVM.ViewModel;
using System.Windows.Controls;
using View = Autodesk.Revit.DB.View;
using System.Windows.Shapes;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Diagnostics;
using System.Windows.Media.Media3D;
using Path = System.IO.Path;


namespace DHHTools.MVVM.Model
{
    public class mRevit : PropertyChangedBase
    {
        private Application _revitApp;
        public Application RevitApp
        {
            get
            {
                _revitApp = vmMain.RevitAppService;
                return _revitApp;
            }
            set
            {
                _revitApp = value;
                OnPropertyChanged(nameof(RevitApp));
            }
        }
        private UIDocument _uiDocument;
        public UIDocument uiDocument
        {
            get
            {
                _uiDocument = vmMain.RevitApp.ActiveUIDocument; return _uiDocument;
            }
            set
            {
                _uiDocument = value;
                OnPropertyChanged(nameof(uiDocument));
            }
        }
        private Document _document;
        public Document Document
        {
            get
            {
                _document = uiDocument.Document;
                return _document;
            }
            set
            {
                _document = value;
                OnPropertyChanged(nameof(_document));
            }
        }
        public List<Document> Listdoc { get; set; } = new List<Document>();

        public List<Document> addFile()
        {


            OpenFileDialog file = new OpenFileDialog();
            {
                file.InitialDirectory = "C:\\";
                file.DefaultExt = "rvt";
                file.Filter = "Revit File(*.rvt)|*.rvt" + "|All Files (*.*)|*.*";
                file.FilterIndex = 1;
                file.Multiselect = true;
                file.RestoreDirectory = true;
            }
            if (file.ShowDialog() == DialogResult.OK) //if there is a file chosen by the user
            {

                foreach (string filename in file.FileNames)
                {
                    FileInfo fileInfo = new FileInfo(filename);

                    try
                    {
                        Document document = RevitApp.OpenDocumentFile(filename);
                        if (document != null)
                        {

                            Listdoc.Add(document);
                        }
                    }
                    catch { }

                }

            }
            return Listdoc;
        }

        public mRevitDoc CreaterevitDoc(Document DocumentRevit)
        {
            string fileName = DocumentRevit.PathName;
            ObservableRangeCollection<ViewSheetSet> sheet = DhhDocumentUtil.GetAllSheetSet(DocumentRevit);
            ViewSheetSet viewSheetSet = sheet[0];
            double numberSheet = viewSheetSet.Views.Size;
            return new mRevitDoc()
            {

                RevitFile = DocumentRevit,
                AllSheetSet = sheet,
                DocumentSelectSheetSet = viewSheetSet,
                NumberSheet = numberSheet,
                FileName = fileName,
            };
        }

        public void createFolder(string fileName, bool IsSeprateByFile, bool IsSeprateFolder, string SelectFolder, bool IsCADSelected, bool IsDWFSelected, bool IsPDFSelected)
        {
            if (IsSeprateByFile == true)
            {
                if (IsSeprateFolder == true)
                {
                    if (IsCADSelected == true)
                    {
                        System.IO.Directory.CreateDirectory(SelectFolder + "\\" + fileName + "\\DWG");
                    }

                    if (IsDWFSelected == true)
                    {
                        System.IO.Directory.CreateDirectory(SelectFolder + "\\" + fileName + "\\DWF");
                    }

                    if (IsPDFSelected == true)
                    {
                        System.IO.Directory.CreateDirectory(SelectFolder + "\\" + fileName + "\\PDF");
                    }
                }
                else
                {
                    System.IO.Directory.CreateDirectory(SelectFolder + "\\" + fileName);

                }
            }
            else
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
        }

        public void exportDWF(mRevitDoc revitDoc, string SelectFolder, bool IsSeprateFolder, bool IsSeprateByFile, string FileName)
        {
            using (Transaction tran = new Transaction(revitDoc.RevitFile))
            {
                tran.Start("Export DWF");
                DWFExportOptions dWFExportOptions = new DWFExportOptions();
                dWFExportOptions.MergedViews = true;
                string DWFFolder = "";
                if (IsSeprateByFile == true)
                {
                    if (IsSeprateFolder == true)
                    {
                        DWFFolder = SelectFolder + "\\" + FileName + "\\DWF";
                    }
                    else
                    {
                        DWFFolder = SelectFolder + "\\" + FileName;
                    }
                }
                else
                {
                    if (IsSeprateFolder == true)
                    {
                        DWFFolder = SelectFolder + "\\DWF";
                    }
                    else
                    {
                        DWFFolder = SelectFolder;
                    }
                }

                revitDoc.RevitFile.Export(DWFFolder, revitDoc.RevitFile.Title, revitDoc.DocumentSelectSheetSet.Views, dWFExportOptions);
                tran.Commit();
            }

        }

        public void exportPDF(mRevitDoc revitDoc, string SelectFolder, bool IsSeprateFolder, bool IsSeprateByFile, string FileName)
        {
            using (Transaction tran = new Transaction(revitDoc.RevitFile))
            {
                tran.Start("Export PDF");
                PrintManager printManager = revitDoc.RevitFile.PrintManager;
                string PDFFolder = "";
                if (IsSeprateByFile == true)
                {
                    if (IsSeprateFolder == true)
                    {
                        PDFFolder = SelectFolder + "\\" + FileName + "\\PDF";
                    }
                    else
                    {
                        PDFFolder = SelectFolder + "\\" + FileName;
                    }
                }
                else
                {
                    if (IsSeprateFolder == true)
                    {
                        PDFFolder = SelectFolder + "\\PDF";
                    }
                    else
                    {
                        PDFFolder = SelectFolder;
                    }
                }

                printManager.PrintSetup.CurrentPrintSetting = printManager.PrintSetup.InSession;
                printManager.PrintRange = Autodesk.Revit.DB.PrintRange.Select;
                ViewSheetSetting viewSheetSetting = printManager.ViewSheetSetting;
                viewSheetSetting.CurrentViewSheetSet.Views = revitDoc.DocumentSelectSheetSet.Views;
                printManager.CombinedFile = true;

                //printManager.SelectNewPrintDriver("Adobe PDF");
                PrintSetup pSetup = printManager.PrintSetup;
                PrintParameters pParam = pSetup.CurrentPrintSetting.PrintParameters;
                //revitDoc.RevitFile.Export(PDFFolder, revitDoc.RevitFile.Title, revitDoc.DocumentSelectSheetSet.Views, dWFExportOptions);
                pParam.ZoomType = ZoomType.FitToPage;

                pParam.PaperPlacement = PaperPlacementType.Center;
                foreach (Autodesk.Revit.DB.PaperSize pSize in printManager.PaperSizes)
                {
                    if (pSize.Name.Equals("A1"))//Work required to get actual Sheet size)
                    {
                        pParam.PaperSize = pSize;
                        break;
                    }
                }


                printManager.PrintToFile = true;
                string fileName = Path.Combine(PDFFolder, FileName, ".pdf");
                printManager.PrintToFileName = fileName;
                //revitDoc.RevitFile.Print(revitDoc.DocumentSelectSheetSet.Views);

                printManager.Apply();
                try
                {
                    pSetup.SaveAs("C:\\Users\\Admin\\Desktop\\Test.pdf");
                }
                catch
                {

                }
                printManager.SubmitPrint();

                tran.Commit();
            }

        }

        public void exportDWG(mRevitDoc revitDoc, string SelectFolder, string SelectCADVersion, bool IsCADSelected, bool IsSeprateFolder, bool IsSeprateByFile, bool IsAddSheetName, string FileName)
        {
            List<ElementId> sheetIDs = new List<ElementId>();
            List<string> sheetNames = new List<string>();
            List<string> sheetNumbers = new List<string>();
            if (IsCADSelected == true)
            {
                using (Transaction tran = new Transaction(revitDoc.RevitFile))
                {
                    tran.Start("Export DWG");
                    DWGExportOptions dWGExportOptions = new DWGExportOptions();
                    dWGExportOptions.MergedViews = true;
                    if (SelectCADVersion == "AutoCAD 2007")
                    { dWGExportOptions.FileVersion = ACADVersion.R2007; }
                    else if (SelectCADVersion == "AutoCAD 2010")
                    { dWGExportOptions.FileVersion = ACADVersion.R2010; }
                    else if (SelectCADVersion == "AutoCAD 2013")
                    { dWGExportOptions.FileVersion = ACADVersion.R2013; }
                    else if (SelectCADVersion == "AutoCAD 2018")
                    { dWGExportOptions.FileVersion = ACADVersion.R2018; }
                    dWGExportOptions.TargetUnit = ExportUnit.Millimeter;
                    foreach (ViewSheet item in revitDoc.DocumentSelectSheetSet.Views)
                    {
                        sheetIDs.Add(item.Id);
                        sheetNames.Add(item.Name);
                        sheetNumbers.Add(item.SheetNumber);
                    }
                    string DWGFolder = "";
                    if (IsSeprateByFile == true)
                    {
                        if (IsSeprateFolder == true)
                        {
                            DWGFolder = SelectFolder + "\\" + FileName + "\\DWG";
                        }
                        else
                        {
                            DWGFolder = SelectFolder + "\\" + FileName;
                        }
                    }
                    else
                    {
                        if (IsSeprateFolder == true)
                        {
                            DWGFolder = SelectFolder + "\\DWG";
                        }
                        else
                        {
                            DWGFolder = SelectFolder;
                        }
                    }
                    revitDoc.RevitFile.Export(DWGFolder, "", sheetIDs, dWGExportOptions);
                    string[] stringPath = Directory.GetFiles(DWGFolder, "*.dwg", SearchOption.TopDirectoryOnly);
                    for (int i = 0; i < sheetNumbers.Count; i++)
                    {
                        string selectPath = stringPath.Single(x => x.Contains(sheetNumbers[i]));
                        FileInfo fileInfo = new FileInfo(selectPath);
                        string directory = System.IO.Path.GetDirectoryName(selectPath);
                        string newname = null;
                        if (IsAddSheetName == true)
                        {
                            newname = sheetNumbers[i] + " - " + sheetNames[i];
                        }
                        else
                        {
                            newname = sheetNumbers[i];
                        }
                        fileInfo.MoveTo(directory + "\\" + newname + fileInfo.Extension);
                    }
                    tran.Commit();
                }
            }
        }

        public void deletePCPFile(bool IsCADSelected, bool IsSeprateByFile, bool IsSeprateFolder, string SelectFolder, string FileName)
        {
            string DWGFolder = "";
            if (IsSeprateByFile == true)
            {
                if (IsCADSelected == true)
                {

                    if (IsSeprateFolder == true)
                    {
                        DWGFolder = SelectFolder + "\\" + FileName + "\\DWG";
                    }
                    else
                    {
                        DWGFolder = SelectFolder + "\\" + FileName;
                    }

                }
            }
            else
            {
                if (IsCADSelected == true)
                {

                    if (IsSeprateFolder == true)
                    {
                        DWGFolder = SelectFolder + "\\DWG";
                    }
                    else
                    {
                        DWGFolder = SelectFolder;
                    }

                }
            }
            string[] stringPath = Directory.GetFiles(DWGFolder, "*.pcp", SearchOption.TopDirectoryOnly);
            foreach (string path in stringPath)
            { System.IO.File.Delete(path); }

        }

        public string SelectFolder()
        {
            string SelectFolder = null;
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.ShowNewFolderButton = true;
            // Show the FolderBrowserDialog.  
            DialogResult result = dialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                SelectFolder = dialog.SelectedPath;
            }
            else
            {
                SelectFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            }
            return SelectFolder;
        }

    }
}
