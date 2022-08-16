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
using System.Windows.Controls;
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
        
        //private string BeamName_VM;
        //private string Location_VM;
        //private string Section_VM;
        //private double b_VM;
        //private double h_VM;
        //private double DkThepTrenL1_VM;
        //private double SLThepTrenL1_VM;
        //private double DkThepTrenL2_VM;
        //private double SLThepTrenL2_VM;
        //private double DkThepDuoiL1_VM;
        //private double SLThepDuoiL1_VM;
        //private double DkThepDuoiL2_VM;
        //private double SLThepDuoiL2_VM;
        //private double DkThepDai_VM;
        //private double SLThepDai_VM;
        //private double KCThepDai_VM;
        //private ObservableCollection<object> AllDataExcel_VM;
        //private System.Windows.Controls.DataGrid dataGrid_VM;



        #endregion
        #region 02. Public Property
        public UIDocument UiDoc;
        public Document Doc;
        public List<ExcelDataExtension> AllExcelData { get; set; } = new List<ExcelDataExtension>();

        //public string BeamName
        //{
        //    get => BeamName_VM;
        //    set
        //    {
        //        if (BeamName_VM != value)
        //        {
        //            BeamName_VM = value;
        //            OnPropertyChanged("Location");
        //        }
        //    }
        //}
        //public string Location
        //{
        //    get => Location_VM;
        //    set
        //    {
        //        if (Location_VM != value)
        //        {
        //            Location_VM = value;
        //            OnPropertyChanged("Location");
        //        }
        //    }
        //}
        //public double b
        //{
        //    get => b_VM;
        //    set
        //    {
        //        if (b_VM != value)
        //        {
        //            b_VM = value;
        //            OnPropertyChanged("b");
        //        }
        //    }
        //}
        //public double h
        //{
        //    get => h_VM;
        //    set
        //    {
        //        if (h_VM != value)
        //        {
        //            h_VM = value;
        //            OnPropertyChanged("h");
        //        }
        //    }
        //}
        //public double DkThepTrenL1
        //{
        //    get => DkThepTrenL1_VM;
        //    set
        //    {
        //        if (DkThepTrenL1_VM != value)
        //        {
        //            DkThepTrenL1_VM = value;
        //            OnPropertyChanged("DkThepTrenL1");
        //        }
        //    }
        //}
        //public double SLThepTrenL1
        //{
        //    get => SLThepTrenL1_VM;
        //    set
        //    {
        //        if (SLThepTrenL1_VM != value)
        //        {
        //            SLThepTrenL1_VM = value;
        //            OnPropertyChanged("SLThepTrenL1");
        //        }
        //    }
        //}
        //public double DkThepTrenL2
        //{
        //    get => DkThepTrenL2_VM;
        //    set
        //    {
        //        if (DkThepTrenL2_VM != value)
        //        {
        //            DkThepTrenL2_VM = value;
        //            OnPropertyChanged("DkThepTrenL2");
        //        }
        //    }
        //}
        //public double SLThepTrenL2
        //{
        //    get => SLThepTrenL2_VM;
        //    set
        //    {
        //        if (SLThepTrenL2_VM != value)
        //        {
        //            SLThepTrenL2_VM = value;
        //            OnPropertyChanged("SLThepTrenL2");
        //        }
        //    }
        //}
        //public double DkThepDuoiL1
        //{
        //    get => DkThepDuoiL1_VM;
        //    set
        //    {
        //        if (DkThepDuoiL1_VM != value)
        //        {
        //            DkThepDuoiL1_VM = value;
        //            OnPropertyChanged("DkThepDuoiL1");
        //        }
        //    }
        //}
        //public double SLThepDuoiL1
        //{
        //    get => SLThepDuoiL1_VM;
        //    set
        //    {
        //        if (SLThepDuoiL1_VM != value)
        //        {
        //            SLThepDuoiL1_VM = value;
        //            OnPropertyChanged("SLThepDuoiL1");
        //        }
        //    }
        //}
        //public double DkThepDuoiL2
        //{
        //    get => DkThepDuoiL2_VM;
        //    set
        //    {
        //        if (DkThepDuoiL2_VM != value)
        //        {
        //            DkThepDuoiL2_VM = value;
        //            OnPropertyChanged("DkThepDuoiL2");
        //        }
        //    }
        //}
        //public double SLThepDuoiL2
        //{
        //    get => SLThepDuoiL2_VM;
        //    set
        //    {
        //        if (SLThepDuoiL2_VM != value)
        //        {
        //            SLThepDuoiL2_VM = value;
        //            OnPropertyChanged("SLThepDuoiL2");
        //        }
        //    }
        //}
        //public double DkThepDai
        //{
        //    get => DkThepDai_VM;
        //    set
        //    {
        //        if (DkThepDai_VM != value)
        //        {
        //            DkThepDai_VM = value;
        //            OnPropertyChanged("DkThepDai");
        //        }
        //    }
        //}
        //public double SLThepDai
        //{
        //    get => SLThepDai_VM;
        //    set
        //    {
        //        if (SLThepDai_VM != value)
        //        {
        //            SLThepDai_VM = value;
        //            OnPropertyChanged("SLThepDai");
        //        }
        //    }
        //}
        //public double KCThepDai
        //{
        //    get => KCThepDai_VM;
        //    set
        //    {
        //        if (KCThepDai_VM != value)
        //        {
        //            KCThepDai_VM = value;
        //            OnPropertyChanged("KCThepDai");
        //        }
        //    }
        //}
        //public string Section
        //{
        //    get
        //    {
        //        return b.ToString() + "x" + h.ToString();
        //    }
        //    set
        //    {
        //        if (Section_VM != value)
        //        {
        //            Section_VM = value;
        //        }
        //    }
        //}
        //public ObservableCollection<object> AllDataExcel 
        //{ 
        //    get => AllDataExcel_VM;
        //    set
        //    {
        //        if (AllDataExcel_VM != value)
        //        {
        //            AllDataExcel_VM = value;
        //            OnPropertyChanged("AllDataExcel");
        //        }
        //    }
        //}
        //public System.Windows.Controls.DataGrid dataGrid
        //{
        //    get => dataGrid_VM;
        //    set
        //    {
        //        if (dataGrid_VM != value)
        //        {
        //            dataGrid_VM = value;
        //            OnPropertyChanged("dataGrid");
        //        }
        //    }
        //}

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
                DataTable dtTable = new DataTable();
                for (int i=1; i<=15; i++)
                    {
                        dtTable.Columns.Add();
                    }
                
                double slColumn = dtTable.Columns.Count;
                for (rowCnt = 36; rowCnt <= xlRange.Rows.Count; rowCnt++)
                {
                    string beamName = (xlRange.Cells[rowCnt, 2] as Microsoft.Office.Interop.Excel.Range).Value;
                    
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
                    double dLThepDai = (xlRange.Cells[rowCnt, 9] as Microsoft.Office.Interop.Excel.Range).Value;
                    double kcThepDai = (xlRange.Cells[rowCnt, 10] as Microsoft.Office.Interop.Excel.Range).Value;

                }


                
                xlsworkbook.Close(true, null, null);
                xlsApp.Quit();
            }
        } 

        #endregion
    }
}



