#region Namespaces
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Excel = Microsoft.Office.Interop.Excel;
using System.Linq;
using System.Windows.Forms;
using Application = Autodesk.Revit.ApplicationServices.Application;
using System.Collections.ObjectModel;
using System;
using System.Collections.Generic;
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
                    #region Thông tin dầm
                    string beamName = "";
                    if ((xlRange.Cells[rowCnt, 2] as Microsoft.Office.Interop.Excel.Range).Value == null)
                    {
                        beamName = (xlRange.Cells[rowCnt - 1, 2] as Microsoft.Office.Interop.Excel.Range).Value;

                    }
                    else
                    {
                        beamName = (xlRange.Cells[rowCnt, 2] as Microsoft.Office.Interop.Excel.Range).Value;
                    }
                    string Location = (xlRange.Cells[rowCnt, 3] as Microsoft.Office.Interop.Excel.Range).Value;
                    double b = (xlRange.Cells[rowCnt, 6] as Microsoft.Office.Interop.Excel.Range).Value;
                    double h = (xlRange.Cells[rowCnt, 7] as Microsoft.Office.Interop.Excel.Range).Value;
                    string section = b.ToString() + "x" + h.ToString();
                    #endregion

                    #region Thép chủ
                    #region Biến phụ
                    double slThepTrenL1_P1 = 0;
                    double slThepTrenL1_P2 = 0;
                    double dkThepTrenL1_P1 = 0;
                    double dkThepTrenL1_P2 = 0;

                    double slThepTrenL2_P1 = 0;
                    double slThepTrenL2_P2 = 0;
                    double dkThepTrenL2_P1 = 0;
                    double dkThepTrenL2_P2 = 0;

                    double slThepDuoiL1_P1 = 0;
                    double slThepDuoiL1_P2 = 0;
                    double dkThepDuoiL1_P1 = 0;
                    double dkThepDuoiL1_P2 = 0;

                    double slThepDuoiL2_P1 = 0;
                    double slThepDuoiL2_P2 = 0;
                    double dkThepDuoiL2_P1 = 0;
                    double dkThepDuoiL2_P2 = 0;
                    #endregion
                    #region Biến chính
                    double slThepTrenL1 = 0;
                    double dkThepTrenL1 = 0;
                    double slThepTrenL2 = 0;
                    double dkThepTrenL2 = 0;
                    double slThepDuoiL1 = 0;
                    double dkThepDuoiL1 = 0;
                    double slThepDuoiL2 = 0;
                    double dkThepDuoiL2 = 0;
                    #endregion
                    #region Gán biến
                    if (Location.Contains("END") | Location.Contains("GỐI"))
                    {
                        slThepTrenL1_P1 = (xlRange.Cells[rowCnt, 18] as Microsoft.Office.Interop.Excel.Range).Value;
                        dkThepTrenL1_P1 = (xlRange.Cells[rowCnt, 19] as Microsoft.Office.Interop.Excel.Range).Value;

                        if ((xlRange.Cells[rowCnt, 20] as Microsoft.Office.Interop.Excel.Range).Value == null)
                        { slThepTrenL1_P2 = 0; }
                        else { slThepTrenL1_P2 = (xlRange.Cells[rowCnt, 20] as Microsoft.Office.Interop.Excel.Range).Value; }

                        if ((xlRange.Cells[rowCnt, 21] as Microsoft.Office.Interop.Excel.Range).Value == null)
                        { dkThepTrenL1_P2 = 0; }
                        else { dkThepTrenL1_P2 = (xlRange.Cells[rowCnt, 21] as Microsoft.Office.Interop.Excel.Range).Value; }

                        if ((xlRange.Cells[rowCnt, 22] as Microsoft.Office.Interop.Excel.Range).Value == null)
                        { slThepTrenL2_P1 = 0; }
                        else { slThepTrenL2_P1 = (xlRange.Cells[rowCnt, 22] as Microsoft.Office.Interop.Excel.Range).Value; }

                        if ((xlRange.Cells[rowCnt, 23] as Microsoft.Office.Interop.Excel.Range).Value == null)
                        { dkThepTrenL2_P1 = 0; }
                        else { dkThepTrenL2_P1 = (xlRange.Cells[rowCnt, 23] as Microsoft.Office.Interop.Excel.Range).Value; }

                        if ((xlRange.Cells[rowCnt, 24] as Microsoft.Office.Interop.Excel.Range).Value == null)
                        { slThepTrenL2_P2 = 0; }
                        else { slThepTrenL2_P2 = (xlRange.Cells[rowCnt, 24] as Microsoft.Office.Interop.Excel.Range).Value; }

                        if ((xlRange.Cells[rowCnt, 25] as Microsoft.Office.Interop.Excel.Range).Value == null)
                        { dkThepTrenL2_P2 = 0; }
                        else { dkThepTrenL2_P2 = (xlRange.Cells[rowCnt, 25] as Microsoft.Office.Interop.Excel.Range).Value; }

                        slThepDuoiL1 = (xlRange.Cells[rowCnt + 1, 18] as Microsoft.Office.Interop.Excel.Range).Value;
                        dkThepDuoiL1 = (xlRange.Cells[rowCnt + 1, 19] as Microsoft.Office.Interop.Excel.Range).Value;

                        if (dkThepTrenL1_P1 == dkThepTrenL1_P2)
                        {
                            slThepTrenL1 = slThepTrenL1_P1 + slThepTrenL1_P2;

                        }

                    }
                    else if (Location.Contains("CEN") | Location.Contains("NHỊP"))
                    {
                        slThepTrenL1 = (xlRange.Cells[rowCnt - 1, 18] as Microsoft.Office.Interop.Excel.Range).Value;
                        dkThepTrenL1 = (xlRange.Cells[rowCnt - 1, 19] as Microsoft.Office.Interop.Excel.Range).Value;

                        slThepDuoiL1_P1 = (xlRange.Cells[rowCnt, 18] as Microsoft.Office.Interop.Excel.Range).Value;
                        dkThepDuoiL1_P1 = (xlRange.Cells[rowCnt, 19] as Microsoft.Office.Interop.Excel.Range).Value;

                        if ((xlRange.Cells[rowCnt, 20] as Microsoft.Office.Interop.Excel.Range).Value == null)
                        { slThepDuoiL1_P2 = 0; }
                        else { slThepDuoiL1_P2 = (xlRange.Cells[rowCnt, 20] as Microsoft.Office.Interop.Excel.Range).Value; }

                        if ((xlRange.Cells[rowCnt, 21] as Microsoft.Office.Interop.Excel.Range).Value == null)
                        { dkThepDuoiL1_P2 = 0; }
                        else { dkThepDuoiL1_P2 = (xlRange.Cells[rowCnt, 21] as Microsoft.Office.Interop.Excel.Range).Value; }

                        if ((xlRange.Cells[rowCnt, 22] as Microsoft.Office.Interop.Excel.Range).Value == null)
                        { dkThepDuoiL2_P1 = 0; }
                        else { slThepDuoiL2_P1 = (xlRange.Cells[rowCnt, 22] as Microsoft.Office.Interop.Excel.Range).Value; }

                        if ((xlRange.Cells[rowCnt, 23] as Microsoft.Office.Interop.Excel.Range).Value == null)
                        { dkThepDuoiL2_P1 = 0; }
                        else { dkThepDuoiL2_P1 = (xlRange.Cells[rowCnt, 23] as Microsoft.Office.Interop.Excel.Range).Value; }

                        if ((xlRange.Cells[rowCnt, 24] as Microsoft.Office.Interop.Excel.Range).Value == null)
                        { dkThepDuoiL2_P2 = 0; }
                        else { slThepDuoiL2_P2 = (xlRange.Cells[rowCnt, 24] as Microsoft.Office.Interop.Excel.Range).Value; }

                        if ((xlRange.Cells[rowCnt, 25] as Microsoft.Office.Interop.Excel.Range).Value == null)
                        { dkThepDuoiL2_P2 = 0; }
                        else { dkThepDuoiL2_P2 = (xlRange.Cells[rowCnt, 25] as Microsoft.Office.Interop.Excel.Range).Value; }
                    }
                    #endregion
                    #endregion

                    #region Thép giá
                    double dkThepDai = (xlRange.Cells[rowCnt, 8] as Microsoft.Office.Interop.Excel.Range).Value;
                    double sLThepDai = (xlRange.Cells[rowCnt, 9] as Microsoft.Office.Interop.Excel.Range).Value;
                    double kcThepDai = (xlRange.Cells[rowCnt, 10] as Microsoft.Office.Interop.Excel.Range).Value;
                    #endregion

                    #region Thép giá
                    double sLThepGia = 0;
                    double dkThepGia = 0;
                    if (h <= 700)
                    {
                        sLThepGia = 0;
                        dkThepGia = 0;
                    }
                    else if (h < 1000)
                    {
                        sLThepGia = 2;
                        dkThepGia = 12;
                    }
                    else
                    {
                        sLThepGia = Math.Ceiling((h - 1000) / 400) * 2 + 2;
                        dkThepGia = 12;
                    }
                    #endregion

                    ExcelDataExtension excelDataExtension = new ExcelDataExtension()
                    {
                        BeamName = beamName,
                        Location = Location,
                        b = b,
                        h = h,
                        Section = section,
                        SLThepTrenL1 = slThepTrenL1,
                        DkThepTrenL1 = dkThepTrenL1,
                        SLThepTrenL2 = slThepTrenL2,
                        DkThepTrenL2 = dkThepTrenL2,
                        SLThepDuoiL1 = slThepDuoiL1,
                        DkThepDuoiL1 = dkThepDuoiL1,
                        SLThepDuoiL2 = slThepDuoiL2,
                        DkThepDuoiL2 = dkThepDuoiL2,
                        DkThepDai = dkThepDai,
                        SLThepDai = sLThepDai,
                        KCThepDai = kcThepDai,
                        DkThepGia = dkThepGia,
                        SLThepGia = sLThepGia,
                    };
                    AllExcelData.Add(excelDataExtension);
                }
                AllExcelData.Distinct();
                xlsworkbook.Close(true, null, null);
                xlsApp.Quit();
            }
        }

        #endregion

        #region 05. Draw Beam Detail 2D
        public void DrawBeamDetail2D()
        {
            List<double> blist = new List<double>();
            List<double> hlist = new List<double>() ;
            List<int> slTop1List = new List<int>() ;
            List<int> slBot1List = new List<int>() ;
            List<int> slTop2List = new List<int>() ;
            List<int> slBot2List = new List<int>() ;
            List<int> slnhanhdaiList = new List<int>();
            for (int i=0; i<AllExcelData.Count;i++)
            {

            }

            double bkhung = blist[0];
            double hkhung = hlist[0];
            for (int i = 0; i < blist.Count; i++)
            {
                if (bkhung < blist[i])
                    bkhung = blist[i];
                if (hkhung < hlist[i])
                    hkhung = hlist[i];
            }
            bkhung = bkhung + 700;
            hkhung = hkhung + 700;

            FamilySymbol fselement = (FamilySymbol)new FilteredElementCollector(Doc)
                 .WhereElementIsElementType()
                 .OfCategory(BuiltInCategory.OST_DetailComponents)
                 .OfClass(typeof(FamilySymbol))
                 .Cast<FamilySymbol>()
                 .FirstOrDefault(s => s.Name.Equals("ICIC_KC_ThepDamV2"));
            using (Transaction tran = new Transaction(Doc))
            {
                tran.Start("Create Detail Beam 2D");
                for (int i = 0; i < AllExcelData.Count; i++)
                {
                    XYZ xyz = XYZ.Zero;
                    XYZ insertPointX = new XYZ(DhhUnitUtils.MmToFeet(bkhung) * i, 0, 0);
                    XYZ insertPointXmm = new XYZ(bkhung * i, 0, 0);
                    xyz = XYZ.Zero.Add(insertPointX);
                    FamilyInstance newinstance = doc.Create.NewFamilyInstance(xyz, fselement, doc.ActiveView);

                    Parameter b_khungParameter = newinstance.LookupParameter("b_khung");
                    Parameter h_khungParameter = newinstance.LookupParameter("h_khung");
                    b_khungParameter.Set(DhhUnitUtils.MmToFeet(bkhung));
                    h_khungParameter.Set(DhhUnitUtils.MmToFeet(hkhung));

                    Parameter bParameter = newinstance.LookupParameter("b");
                    Parameter hParameter = newinstance.LookupParameter("h");
                    bParameter.Set(DhhUnitUtils.MmToFeet(blist[i]));
                    hParameter.Set(DhhUnitUtils.MmToFeet(hlist[i]));

                    Parameter slTop1Parameter = newinstance.LookupParameter("SL_Top1");
                    Parameter slBot1Parameter = newinstance.LookupParameter("SL_Bottom1");
                    slTop1Parameter.Set(slTop1List[i]);
                    slBot1Parameter.Set(slBot1List[i]);

                    Parameter slTop2Parameter = newinstance.LookupParameter("SL_Top2");
                    Parameter slBot2Parameter = newinstance.LookupParameter("SL_Bottom2");
                    slTop2Parameter.Set(slTop2List[i]);
                    slBot2Parameter.Set(slBot2List[i]);

                    Parameter dai3nhanhdaiParameter = newinstance.LookupParameter("Thep_Dai3Nhanh");
                    Parameter dai4nhanhdaiParameter = newinstance.LookupParameter("Thep_Dai4Nhanh");
                    if (slnhanhdaiList[i] == 2) { dai3nhanhdaiParameter.Set(0); dai4nhanhdaiParameter.Set(0); }
                    else if (slnhanhdaiList[i] == 3) { dai3nhanhdaiParameter.Set(1); dai4nhanhdaiParameter.Set(0); }
                    else if (slnhanhdaiList[i] == 4) { dai3nhanhdaiParameter.Set(0); dai4nhanhdaiParameter.Set(1); }

                    Parameter thepgiaParameter = newinstance.LookupParameter("SL_LopThepGia");
                    if (hlist[i] < 700) { thepgiaParameter.Set(0); }
                    else if (hlist[i] == 700) { thepgiaParameter.Set(1); }
                    else if (hlist[i] >= 700) { thepgiaParameter.Set(Math.Ceiling((hlist[i] - 700) / 350)); }

                    ReferenceArray referenceArrayX = new ReferenceArray();
                    ReferenceArray referenceArrayY = new ReferenceArray();
                    referenceArrayY.Append(newinstance.GetReferenceByName("Top"));
                    referenceArrayY.Append(newinstance.GetReferenceByName("Bottom"));
                    referenceArrayX.Append(newinstance.GetReferenceByName("Left"));
                    referenceArrayX.Append(newinstance.GetReferenceByName("Right"));
                    Line lineY = Line.CreateUnbound(insertPointX, XYZ.BasisY);
                    Line lineX = Line.CreateUnbound(insertPointX, XYZ.BasisX);
                    Dimension dimensionX = doc.Create.NewDimension(doc.ActiveView, lineX, referenceArrayX);
                    Dimension dimensionY = doc.Create.NewDimension(doc.ActiveView, lineY, referenceArrayY);

                    XYZ translationX = new XYZ(0, -DhhUnitUtils.MmToFeet(hlist[i] / 2 + 150), 0);
                    XYZ translationXmm = new XYZ(0, -(hlist[i] / 2 + 150), 0);
                    ElementTransformUtils.MoveElement(doc, dimensionX.Id, translationX);

                    XYZ translationY = new XYZ(DhhUnitUtils.MmToFeet(blist[i] / 2 + 150), 0, 0);
                    XYZ translationYmm = new XYZ((blist[i] / 2 + 150), 0, 0);
                    ElementTransformUtils.MoveElement(doc, dimensionY.Id, translationY);
                }
                tran.Commit();
            }
        }
        #endregion
    }
}



