#region Namespaces
//using System.Linq;
//using System.Windows.Documents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using static DHHTools.DhhUnitUtils;
using Application = Autodesk.Revit.ApplicationServices.Application;

// ReSharper disable All
#endregion


namespace DHHTools
{
    [Transaction(TransactionMode.Manual)]
    public class LoadMultiFile : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Application app = uiapp.Application;
            Document doc = uidoc.Document;
            string SelectFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            //Test mở file, không liên quan đến code bên dưới
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

            }
                List<string> modelPathList = new List<string>();
            modelPathList.Add("D:\\DESIGN\\2023\\59 - NPQN_NP_VoDinhLo_A.Canh\\FILE REVIT\\NPQN_BMVC_ThietKeTC_230207.rvt");
            modelPathList.Add("D:\\DESIGN\\2023\\58 - NPQN_NP_HoVanTung_A.Canh\\FILE REVIT\\NPQN_HVT_ThietKeTC_230130.rvt");
            foreach (string modelPath in modelPathList)
            {
                Document openedDoc = app.OpenDocumentFile(modelPath);
                FilteredElementCollector colec = new FilteredElementCollector(openedDoc);
                List<Element> allsheetset = colec.OfClass(typeof(ViewSheetSet)).ToElements().ToList();
                List<ViewSheetSet> AllSheetSetList = new List<ViewSheetSet>();
                foreach (Element item in allsheetset)
                {
                    AllSheetSetList.Add(item as ViewSheetSet);
                }
                ViewSheetSet selSheetSet = AllSheetSetList[1];
                DWFExportOptions dWFExportOptions = new DWFExportOptions();
                dWFExportOptions.MergedViews = true;


                using (Transaction tran = new Transaction(openedDoc))
                {
                    tran.Start("Export DWG");
                    openedDoc.Export(SelectFolder, openedDoc.Title, selSheetSet.Views, dWFExportOptions);
                }

            }

            return Result.Succeeded;
        }
    }
}
