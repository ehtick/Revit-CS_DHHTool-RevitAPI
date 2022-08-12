#region Namespaces
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Structure;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using Excel = Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Application = Autodesk.Revit.ApplicationServices.Application;
using Curve = Autodesk.Revit.DB.Curve;
using DataTable = System.Data.DataTable;
using PlanarFace = Autodesk.Revit.DB.PlanarFace;
// ReSharper disable All
#endregion

namespace DHHTools
{
    public class CreateSectionBeam2DViewModel : ViewModelBase
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
        #endregion
        #region 03. View Model
        public CreateSectionBeam2DViewModel(ExternalCommandData commandData)
        {
            uiapp = commandData.Application;
            uidoc = uiapp.ActiveUIDocument;
            app = uiapp.Application;
            doc = uidoc.Document;
            UiDoc = uidoc;
            Doc = UiDoc.Document;
            
            CreateSectionBeam2DViewModel createSectionBeam2DViewModel = this;
            CreateSectionBeam2DWindow createSectionBeam2DWindow = new CreateSectionBeam2DWindow(createSectionBeam2DViewModel);
            createSectionBeam2DWindow.Show();
        }
        #endregion
        #region 04. Select Excel File
        public void SelectExcelFile()
        {
            OpenFileDialog file = new OpenFileDialog();
            {
                file.InitialDirectory = "C:\\";
                file.Filter = "xls (*.xls)|*.xlsx|All files (*.*)|*.*";
                file.FilterIndex = 2;
                file.RestoreDirectory = true;
            }
            if (file.ShowDialog() == DialogResult.OK) //if there is a file chosen by the user
            {
                Excel.Application xlsApp = new Excel.Application();
                Excel.Workbook xlsworkbook = xlsApp.Workbooks.Open(file.FileName);
                Excel.Range xlRange = xlsworkbook.Worksheets["Sheet1"].UsedRange;
                DataTable dtTable = new DataTable();
                int xlRow;
                for (xlRow = 37; xlRow <= xlRange.Rows.Count; xlRow++)
                {
                        
                }
                xlsworkbook.Close();
                xlsApp.Quit();
            }
        } 

        #endregion
    }
}



