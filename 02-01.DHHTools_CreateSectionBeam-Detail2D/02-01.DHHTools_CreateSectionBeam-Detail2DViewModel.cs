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
        private string BeamName_VM;
        private string Location_VM;
        private string Section_VM;
        private double b_VM;
        private double h_VM;
        private double DkThepTrenL1_VM;
        private double SLThepTrenL1_VM;
        private double DkThepTrenL2_VM;
        private double SLThepTrenL2_VM;
        private double DkThepDuoiL1_VM;
        private double SLThepDuoiL1_VM;
        private double DkThepDuoiL2_VM;
        private double SLThepDuoiL2_VM;
        private double DkThepDai_VM;
        private double SLThepDai_VM;
        private double KCThepDai_VM;


        #endregion
        #region 02. Public Property
        public UIDocument UiDoc;
        public Document Doc;
        public string BeamName
        {
            get => BeamName_VM;
            set
            {
                if (BeamName_VM != value)
                {
                    BeamName_VM = value;
                    OnPropertyChanged("Location");
                }
            }
        }
        public string Location
        {
            get => Location_VM;
            set
            {
                if (Location_VM != value)
                {
                    Location_VM = value;
                    OnPropertyChanged("Location");
                }
            }
        }
        public double b
        {
            get => b_VM;
            set
            {
                if (b_VM != value)
                {
                    b_VM = value;
                    OnPropertyChanged("b");
                }
            }
        }
        public double h
        {
            get => h_VM;
            set
            {
                if (h_VM != value)
                {
                    h_VM = value;
                    OnPropertyChanged("h");
                }
            }
        }
        public double DkThepTrenL1
        {
            get => DkThepTrenL1_VM;
            set
            {
                if (DkThepTrenL1_VM != value)
                {
                    DkThepTrenL1_VM = value;
                    OnPropertyChanged("DkThepTrenL1");
                }
            }
        }
        public double SLThepTrenL1
        {
            get => SLThepTrenL1_VM;
            set
            {
                if (SLThepTrenL1_VM != value)
                {
                    SLThepTrenL1_VM = value;
                    OnPropertyChanged("SLThepTrenL1");
                }
            }
        }
        public double DkThepTrenL2
        {
            get => DkThepTrenL2_VM;
            set
            {
                if (DkThepTrenL2_VM != value)
                {
                    DkThepTrenL2_VM = value;
                    OnPropertyChanged("DkThepTrenL2");
                }
            }
        }
        public double SLThepTrenL2
        {
            get => SLThepTrenL2_VM;
            set
            {
                if (SLThepTrenL2_VM != value)
                {
                    SLThepTrenL2_VM = value;
                    OnPropertyChanged("SLThepTrenL2");
                }
            }
        }
        public double DkThepDuoiL1
        {
            get => DkThepDuoiL1_VM;
            set
            {
                if (DkThepDuoiL1_VM != value)
                {
                    DkThepDuoiL1_VM = value;
                    OnPropertyChanged("DkThepDuoiL1");
                }
            }
        }
        public double SLThepDuoiL1
        {
            get => SLThepDuoiL1_VM;
            set
            {
                if (SLThepDuoiL1_VM != value)
                {
                    SLThepDuoiL1_VM = value;
                    OnPropertyChanged("SLThepDuoiL1");
                }
            }
        }
        public double DkThepDuoiL2
        {
            get => DkThepDuoiL2_VM;
            set
            {
                if (DkThepDuoiL2_VM != value)
                {
                    DkThepDuoiL2_VM = value;
                    OnPropertyChanged("DkThepDuoiL2");
                }
            }
        }
        public double SLThepDuoiL2
        {
            get => SLThepDuoiL2_VM;
            set
            {
                if (SLThepDuoiL2_VM != value)
                {
                    SLThepDuoiL2_VM = value;
                    OnPropertyChanged("SLThepDuoiL2");
                }
            }
        }
        public double DkThepDai
        {
            get => DkThepDai_VM;
            set
            {
                if (DkThepDai_VM != value)
                {
                    DkThepDai_VM = value;
                    OnPropertyChanged("DkThepDai");
                }
            }
        }
        public double SLThepDai
        {
            get => SLThepDai_VM;
            set
            {
                if (SLThepDai_VM != value)
                {
                    SLThepDai_VM = value;
                    OnPropertyChanged("SLThepDai");
                }
            }
        }
        public double KCThepDai
        {
            get => KCThepDai_VM;
            set
            {
                if (KCThepDai_VM != value)
                {
                    KCThepDai_VM = value;
                    OnPropertyChanged("KCThepDai");
                }
            }
        }
        public string Section
        {
            get
            {
                return b.ToString() + "x" + h.ToString();
            }
            set
            {
                if (Section_VM != value)
                {
                    Section_VM = value;
                }
            }
        }
        public object AllDataExcel { get; set; }

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
                    DataRow dr = dtTable.NewRow();
                    BeamName = (xlRange.Cells[rowCnt, 2] as Microsoft.Office.Interop.Excel.Range).Value;
                    dr[1] = BeamName;
                    Location = (xlRange.Cells[rowCnt, 3] as Microsoft.Office.Interop.Excel.Range).Value;
                    dr[2] = BeamName;
                    b = (xlRange.Cells[rowCnt, 6] as Microsoft.Office.Interop.Excel.Range).Value;
                    h = (xlRange.Cells[rowCnt, 7] as Microsoft.Office.Interop.Excel.Range).Value;
                    dr[3] = b.ToString() + "x" + h.ToString();

                    SLThepTrenL1 = (xlRange.Cells[rowCnt, 18] as Microsoft.Office.Interop.Excel.Range).Value;
                    dr[4] = SLThepTrenL1;

                    DkThepTrenL1 = (xlRange.Cells[rowCnt, 19] as Microsoft.Office.Interop.Excel.Range).Value;
                    dr[5] = DkThepTrenL1;

                    if ((xlRange.Cells[rowCnt, 20] as Microsoft.Office.Interop.Excel.Range).Value == null)
                    {
                        SLThepTrenL2 = 0;
                    }
                    else
                    {
                        SLThepTrenL2 = (xlRange.Cells[rowCnt, 20] as Microsoft.Office.Interop.Excel.Range).Value;
                    }
                    dr[6] = SLThepTrenL2;

                    if ((xlRange.Cells[rowCnt, 21] as Microsoft.Office.Interop.Excel.Range).Value == null)
                    {
                        DkThepTrenL2 = 0;
                    }
                    else
                    {
                        DkThepTrenL2 = (xlRange.Cells[rowCnt, 21] as Microsoft.Office.Interop.Excel.Range).Value;
                    }
                    dr[7] = SLThepTrenL2;

                    if ((xlRange.Cells[rowCnt, 22] as Microsoft.Office.Interop.Excel.Range).Value == null)
                    {
                        SLThepDuoiL1 = 0;
                    }
                    else
                    {
                        SLThepDuoiL1 = (xlRange.Cells[rowCnt, 22] as Microsoft.Office.Interop.Excel.Range).Value;
                    }
                    dr[8] = SLThepTrenL2;

                    if ((xlRange.Cells[rowCnt, 23] as Microsoft.Office.Interop.Excel.Range).Value == null)
                    {
                        DkThepDuoiL1 = 0;
                    }
                    else
                    {
                        DkThepDuoiL1 = (xlRange.Cells[rowCnt, 23] as Microsoft.Office.Interop.Excel.Range).Value;
                    }
                    dr[9] = SLThepTrenL2;

                    if ((xlRange.Cells[rowCnt, 24] as Microsoft.Office.Interop.Excel.Range).Value == null)
                    {
                        SLThepDuoiL2 = 0;
                    }
                    else
                    {
                        SLThepDuoiL2 = (xlRange.Cells[rowCnt, 24] as Microsoft.Office.Interop.Excel.Range).Value;
                    }
                    dr[10] = SLThepTrenL2;

                    if ((xlRange.Cells[rowCnt, 25] as Microsoft.Office.Interop.Excel.Range).Value == null)
                    {
                        DkThepDuoiL2 = 0;
                    }
                    else
                    {
                        DkThepDuoiL2 = (xlRange.Cells[rowCnt, 25] as Microsoft.Office.Interop.Excel.Range).Value;
                    }
                    dr[11] = SLThepTrenL2;

                    DkThepDai = (xlRange.Cells[rowCnt, 8] as Microsoft.Office.Interop.Excel.Range).Value;
                    dr[12] = SLThepTrenL2;

                    SLThepDai = (xlRange.Cells[rowCnt, 9] as Microsoft.Office.Interop.Excel.Range).Value;
                    dr[13] = SLThepTrenL2;

                    KCThepDai = (xlRange.Cells[rowCnt, 10] as Microsoft.Office.Interop.Excel.Range).Value;
                    dr[14] = SLThepTrenL2;



                    dtTable.Rows.Add(dr);
                    dtTable.AcceptChanges();
                }

                AllDataExcel = dtTable.DefaultView;
                xlsworkbook.Close(true, null, null);
                xlsApp.Quit();
            }
        } 

        #endregion
    }
}



