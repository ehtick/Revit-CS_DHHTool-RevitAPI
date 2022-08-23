#region Namespaces
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Excel = Microsoft.Office.Interop.Excel;
using System.Linq;
using System.Windows.Forms;
using Application = Autodesk.Revit.ApplicationServices.Application;
using System.Collections.ObjectModel;
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
        public ObservableCollection<ExcelDataExtension> AllExcelData { get; set; } =
            new ObservableCollection<ExcelDataExtension>();
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
            
        }
        #endregion

        #region 04. Select Excel File
        public void SelectExcelFile()
        {
            OpenFileDialog file = new OpenFileDialog();
            {
                file.InitialDirectory = "C:\\";
                file.DefaultExt = "xls";
                file.Filter = "Excel File(*.xls; *.xlsx; *.xlsm)|*.xls; *.xlsx" + "|All Files (*.*)|*.*";
                file.FilterIndex = 1;
                file.RestoreDirectory = true;
            }
            if (file.ShowDialog() == DialogResult.OK) //if there is a file chosen by the user
            {
                Excel.Application xlsApp = new Excel.Application();
                Excel.Workbook xlsworkbook = xlsApp.Workbooks.Open(file.FileName);
                Excel.Range xlRange = xlsworkbook.Worksheets["Beam"].UsedRange;
                int rowCnt = 0;
                for (rowCnt = 36; rowCnt <= xlRange.Rows.Count; rowCnt++)
                {
                    string beamName = "";
                    if ((xlRange.Cells[rowCnt, 2] as Microsoft.Office.Interop.Excel.Range).Value == null)
                    {
                        beamName = (xlRange.Cells[rowCnt - 1, 2] as Microsoft.Office.Interop.Excel.Range).Value;

                    }
                    else
                    {
                        beamName =  (xlRange.Cells[rowCnt , 2] as Microsoft.Office.Interop.Excel.Range).Value;
                    }
                    string Location = (xlRange.Cells[rowCnt, 3] as Microsoft.Office.Interop.Excel.Range).Value;
                    double b = (xlRange.Cells[rowCnt, 6] as Microsoft.Office.Interop.Excel.Range).Value;
                    double h = (xlRange.Cells[rowCnt, 7] as Microsoft.Office.Interop.Excel.Range).Value;
                    string section = b.ToString() + "x" + h.ToString();
                    double sLThepTrenL1 = (xlRange.Cells[rowCnt, 18] as Microsoft.Office.Interop.Excel.Range).Value;
                    double dkThepTrenL1 = (xlRange.Cells[rowCnt, 19] as Microsoft.Office.Interop.Excel.Range).Value;
                    double sLThepTrenL2 = 0;
                    if ((xlRange.Cells[rowCnt, 20] as Microsoft.Office.Interop.Excel.Range).Value == null)
                    {
                        sLThepTrenL2 = 0;
                    }
                    else
                    {
                        sLThepTrenL2 = (xlRange.Cells[rowCnt, 20] as Microsoft.Office.Interop.Excel.Range).Value;
                    }
                    double dkThepTrenL2 = 0;
                    if ((xlRange.Cells[rowCnt, 21] as Microsoft.Office.Interop.Excel.Range).Value == null)
                    {
                        dkThepTrenL2 = 0;
                    }
                    else
                    {
                        dkThepTrenL2 = (xlRange.Cells[rowCnt, 21] as Microsoft.Office.Interop.Excel.Range).Value;
                    }
                    double sLThepDuoiL1 = 0;
                    if ((xlRange.Cells[rowCnt, 22] as Microsoft.Office.Interop.Excel.Range).Value == null)
                    {
                        sLThepDuoiL1 = 0;
                    }
                    else
                    {
                        sLThepDuoiL1 = (xlRange.Cells[rowCnt, 22] as Microsoft.Office.Interop.Excel.Range).Value;
                    }
                    double dkThepDuoiL1 = 0;
                    if ((xlRange.Cells[rowCnt, 23] as Microsoft.Office.Interop.Excel.Range).Value == null)
                    {
                        dkThepDuoiL1 = 0;
                    }
                    else
                    {
                        dkThepDuoiL1 = (xlRange.Cells[rowCnt, 23] as Microsoft.Office.Interop.Excel.Range).Value;
                    }
                    double sLThepDuoiL2 = 0;
                    if ((xlRange.Cells[rowCnt, 24] as Microsoft.Office.Interop.Excel.Range).Value == null)
                    {
                        sLThepDuoiL2 = 0;
                    }
                    else
                    {
                        sLThepDuoiL2 = (xlRange.Cells[rowCnt, 24] as Microsoft.Office.Interop.Excel.Range).Value;
                    }
                    double dkThepDuoiL2 = 0;
                    if ((xlRange.Cells[rowCnt, 25] as Microsoft.Office.Interop.Excel.Range).Value == null)
                    {
                        dkThepDuoiL2 = 0;
                    }
                    else
                    {
                        dkThepDuoiL2 = (xlRange.Cells[rowCnt, 25] as Microsoft.Office.Interop.Excel.Range).Value;
                    }
                    double dkThepDai = (xlRange.Cells[rowCnt, 8] as Microsoft.Office.Interop.Excel.Range).Value;
                    double sLThepDai = (xlRange.Cells[rowCnt, 9] as Microsoft.Office.Interop.Excel.Range).Value;
                    double kcThepDai = (xlRange.Cells[rowCnt, 10] as Microsoft.Office.Interop.Excel.Range).Value;


                    ExcelDataExtension excelDataExtension = new ExcelDataExtension()
                    {
                        BeamName = beamName,
                        Location = Location,
                        b = b,
                        h = h,
                        Section = section,
                        SLThepTrenL1 = sLThepTrenL1,
                        DkThepTrenL1 = dkThepTrenL1,
                        SLThepTrenL2 = sLThepTrenL2,
                        DkThepTrenL2 = dkThepTrenL2,
                        SLThepDuoiL1 = sLThepDuoiL1,
                        DkThepDuoiL1 = dkThepDuoiL1,
                        SLThepDuoiL2 = sLThepDuoiL2,
                        DkThepDuoiL2 = dkThepDuoiL2,
                        DkThepDai = dkThepDai,
                        SLThepDai = sLThepDai,
                        KCThepDai = kcThepDai, 
                    };
                    AllExcelData.Add(excelDataExtension);
                }

                AllExcelData.Distinct();
                xlsworkbook.Close(true, null, null);
                xlsApp.Quit();
            }
        } 

        #endregion
    }
}



