using BIMSoftLib.MVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
using _02_01_DrawSectionBeam_Detail2D.MVVM.Model;
using Microsoft.Office.Interop.Excel;

namespace _02_01_DrawSectionBeam_Detail2D.MVVM.Model
{
    public class mExcel : PropertyChangedBase
    {

        public static Excel._Application xlsApp { get; set; } = new Excel.Application();
        public static Excel.Workbook xlsworkbook { get; set; }
        public static Excel.Range xlRange { get; set; }
        public static Excel.Worksheet xlSheet { get; set; }
        public static int lastrow { get; set; }
        public int OpenExcelFile()
        {
            OpenFileDialog file = new OpenFileDialog();
            {
                file.InitialDirectory = "C:\\";
                file.DefaultExt = "xls";
                file.Filter = "Excel File(*.xls; *.xlsx; *.xlsm)|*.xls; *.xlsx" + "|All Files (*.*)|*.*";
                file.FilterIndex = 1;
                file.RestoreDirectory = true;
            }
            if (file.ShowDialog() == DialogResult.OK)
            {
                xlsworkbook = xlsApp.Workbooks.Open(file.FileName);
                xlSheet = xlsworkbook.Worksheets["Beam"];
                Excel.Range xllast = xlSheet.Cells.SpecialCells(XlCellType.xlCellTypeLastCell, Type.Missing);
                //xlRange = xlSheet.Range["A1", xllast];
                lastrow = xlSheet.Cells.Find("*", System.Reflection.Missing.Value, System.Reflection.Missing.Value, System.Reflection.Missing.Value, Microsoft.Office.Interop.Excel.XlSearchOrder.xlByRows, Microsoft.Office.Interop.Excel.XlSearchDirection.xlPrevious, false, System.Reflection.Missing.Value, System.Reflection.Missing.Value).Row;
                //xlRange = xlSheet.Cells[2,rowIndex];
                //xlRange = xlSheet.UsedRange;
                //xlRange = xlSheet.Range["A1", xlSheet.Cells[lastrow, 50]];
            }
            #region
            //if (file.ShowDialog() == DialogResult.OK) //if there is a file chosen by the user
            //{

            //    Excel.Workbook xlsworkbook = xlsApp.Workbooks.Open(file.FileName);
            //    Excel.Range xlRange = xlsworkbook.Worksheets["Beam"].UsedRange;
            //    int rowCnt = 0;
            //    for (rowCnt = 36; rowCnt <= xlRange.Rows.Count; rowCnt++)
            //    {
            //        #region Thông tin dầm
            //        string beamName = "";
            //        if ((xlRange.Cells[rowCnt, 2] as Microsoft.Office.Interop.Excel.Range).Value == null)
            //        {
            //            beamName = (xlRange.Cells[rowCnt - 1, 2] as Microsoft.Office.Interop.Excel.Range).Value;

            //        }
            //        else
            //        {
            //            beamName = (xlRange.Cells[rowCnt, 2] as Microsoft.Office.Interop.Excel.Range).Value;
            //        }
            //        string Location = (xlRange.Cells[rowCnt, 3] as Microsoft.Office.Interop.Excel.Range).Value;
            //        double b = (xlRange.Cells[rowCnt, 6] as Microsoft.Office.Interop.Excel.Range).Value;
            //        double h = (xlRange.Cells[rowCnt, 7] as Microsoft.Office.Interop.Excel.Range).Value;
            //        string section = b.ToString() + "x" + h.ToString();
            //        #endregion

            //        #region Thép chủ
            //        #region Biến phụ
            //        double slThepTrenL1_P1 = 0;
            //        double slThepTrenL1_P2 = 0;
            //        double dkThepTrenL1_P1 = 0;
            //        double dkThepTrenL1_P2 = 0;

            //        double slThepTrenL2_P1 = 0;
            //        double slThepTrenL2_P2 = 0;
            //        double dkThepTrenL2_P1 = 0;
            //        double dkThepTrenL2_P2 = 0;

            //        double slThepDuoiL1_P1 = 0;
            //        double slThepDuoiL1_P2 = 0;
            //        double dkThepDuoiL1_P1 = 0;
            //        double dkThepDuoiL1_P2 = 0;

            //        double slThepDuoiL2_P1 = 0;
            //        double slThepDuoiL2_P2 = 0;
            //        double dkThepDuoiL2_P1 = 0;
            //        double dkThepDuoiL2_P2 = 0;
            //        #endregion
            //        #region Biến chính
            //        double slThepTrenL1 = 0;
            //        double dkThepTrenL1 = 0;
            //        double slThepTrenL2 = 0;
            //        double dkThepTrenL2 = 0;
            //        double slThepDuoiL1 = 0;
            //        double dkThepDuoiL1 = 0;
            //        double slThepDuoiL2 = 0;
            //        double dkThepDuoiL2 = 0;
            //        #endregion
            //        #region Gán biến
            //        if (Location.Contains("END") | Location.Contains("GỐI"))
            //        {
            //            slThepTrenL1_P1 = (xlRange.Cells[rowCnt, 18] as Microsoft.Office.Interop.Excel.Range).Value;
            //            dkThepTrenL1_P1 = (xlRange.Cells[rowCnt, 19] as Microsoft.Office.Interop.Excel.Range).Value;

            //            if ((xlRange.Cells[rowCnt, 20] as Microsoft.Office.Interop.Excel.Range).Value == null)
            //            { slThepTrenL1_P2 = 0; }
            //            else { slThepTrenL1_P2 = (xlRange.Cells[rowCnt, 20] as Microsoft.Office.Interop.Excel.Range).Value; }

            //            if ((xlRange.Cells[rowCnt, 21] as Microsoft.Office.Interop.Excel.Range).Value == null)
            //            { dkThepTrenL1_P2 = 0; }
            //            else { dkThepTrenL1_P2 = (xlRange.Cells[rowCnt, 21] as Microsoft.Office.Interop.Excel.Range).Value; }

            //            if ((xlRange.Cells[rowCnt, 22] as Microsoft.Office.Interop.Excel.Range).Value == null)
            //            { slThepTrenL2_P1 = 0; }
            //            else { slThepTrenL2_P1 = (xlRange.Cells[rowCnt, 22] as Microsoft.Office.Interop.Excel.Range).Value; }

            //            if ((xlRange.Cells[rowCnt, 23] as Microsoft.Office.Interop.Excel.Range).Value == null)
            //            { dkThepTrenL2_P1 = 0; }
            //            else { dkThepTrenL2_P1 = (xlRange.Cells[rowCnt, 23] as Microsoft.Office.Interop.Excel.Range).Value; }

            //            if ((xlRange.Cells[rowCnt, 24] as Microsoft.Office.Interop.Excel.Range).Value == null)
            //            { slThepTrenL2_P2 = 0; }
            //            else { slThepTrenL2_P2 = (xlRange.Cells[rowCnt, 24] as Microsoft.Office.Interop.Excel.Range).Value; }

            //            if ((xlRange.Cells[rowCnt, 25] as Microsoft.Office.Interop.Excel.Range).Value == null)
            //            { dkThepTrenL2_P2 = 0; }
            //            else { dkThepTrenL2_P2 = (xlRange.Cells[rowCnt, 25] as Microsoft.Office.Interop.Excel.Range).Value; }

            //            slThepDuoiL1 = (xlRange.Cells[rowCnt + 1, 18] as Microsoft.Office.Interop.Excel.Range).Value;
            //            dkThepDuoiL1 = (xlRange.Cells[rowCnt + 1, 19] as Microsoft.Office.Interop.Excel.Range).Value;

            //            if (dkThepTrenL1_P1 == dkThepTrenL1_P2)
            //            {
            //                slThepTrenL1 = slThepTrenL1_P1 + slThepTrenL1_P2;

            //            }

            //        }
            //        else if (Location.Contains("CEN") | Location.Contains("NHỊP"))
            //        {
            //            slThepTrenL1 = (xlRange.Cells[rowCnt - 1, 18] as Microsoft.Office.Interop.Excel.Range).Value;
            //            dkThepTrenL1 = (xlRange.Cells[rowCnt - 1, 19] as Microsoft.Office.Interop.Excel.Range).Value;

            //            slThepDuoiL1_P1 = (xlRange.Cells[rowCnt, 18] as Microsoft.Office.Interop.Excel.Range).Value;
            //            dkThepDuoiL1_P1 = (xlRange.Cells[rowCnt, 19] as Microsoft.Office.Interop.Excel.Range).Value;

            //            if ((xlRange.Cells[rowCnt, 20] as Microsoft.Office.Interop.Excel.Range).Value == null)
            //            { slThepDuoiL1_P2 = 0; }
            //            else { slThepDuoiL1_P2 = (xlRange.Cells[rowCnt, 20] as Microsoft.Office.Interop.Excel.Range).Value; }

            //            if ((xlRange.Cells[rowCnt, 21] as Microsoft.Office.Interop.Excel.Range).Value == null)
            //            { dkThepDuoiL1_P2 = 0; }
            //            else { dkThepDuoiL1_P2 = (xlRange.Cells[rowCnt, 21] as Microsoft.Office.Interop.Excel.Range).Value; }

            //            if ((xlRange.Cells[rowCnt, 22] as Microsoft.Office.Interop.Excel.Range).Value == null)
            //            { dkThepDuoiL2_P1 = 0; }
            //            else { slThepDuoiL2_P1 = (xlRange.Cells[rowCnt, 22] as Microsoft.Office.Interop.Excel.Range).Value; }

            //            if ((xlRange.Cells[rowCnt, 23] as Microsoft.Office.Interop.Excel.Range).Value == null)
            //            { dkThepDuoiL2_P1 = 0; }
            //            else { dkThepDuoiL2_P1 = (xlRange.Cells[rowCnt, 23] as Microsoft.Office.Interop.Excel.Range).Value; }

            //            if ((xlRange.Cells[rowCnt, 24] as Microsoft.Office.Interop.Excel.Range).Value == null)
            //            { dkThepDuoiL2_P2 = 0; }
            //            else { slThepDuoiL2_P2 = (xlRange.Cells[rowCnt, 24] as Microsoft.Office.Interop.Excel.Range).Value; }

            //            if ((xlRange.Cells[rowCnt, 25] as Microsoft.Office.Interop.Excel.Range).Value == null)
            //            { dkThepDuoiL2_P2 = 0; }
            //            else { dkThepDuoiL2_P2 = (xlRange.Cells[rowCnt, 25] as Microsoft.Office.Interop.Excel.Range).Value; }
            //        }
            //        #endregion
            //        #endregion

            //        #region Thép giá
            //        double dkThepDai = (xlRange.Cells[rowCnt, 8] as Microsoft.Office.Interop.Excel.Range).Value;
            //        double sLThepDai = (xlRange.Cells[rowCnt, 9] as Microsoft.Office.Interop.Excel.Range).Value;
            //        double kcThepDai = (xlRange.Cells[rowCnt, 10] as Microsoft.Office.Interop.Excel.Range).Value;
            //        #endregion

            //        #region Thép giá
            //        double sLThepGia = 0;
            //        double dkThepGia = 0;
            //        if (h <= 700)
            //        {
            //            sLThepGia = 0;
            //            dkThepGia = 0;
            //        }
            //        else if (h < 1000)
            //        {
            //            sLThepGia = 2;
            //            dkThepGia = 12;
            //        }
            //        else
            //        {
            //            sLThepGia = Math.Ceiling((h - 1000) / 400) * 2 + 2;
            //            dkThepGia = 12;
            //        }
            //        #endregion

            //        mSectionBeam mSectionBeam = new mSectionBeam()
            //        {
            //            BeamName = beamName,
            //            SectionLocation = Location,
            //            //b = b,
            //            //h = h,
            //        };


            //    }
            //    xlsworkbook.Close(true, null, null);
            //    xlsApp.Quit();
            //}
            #endregion
            return lastrow;
        }

        public mSectionBeam SectionBeam(int i)
        {
            #region Thông tin dầm
            string beamName = "";
            if ((xlSheet.Cells[i, 2] as Microsoft.Office.Interop.Excel.Range).Value == null)
            {
                beamName = (xlSheet.Cells[i - 1, 2] as Microsoft.Office.Interop.Excel.Range).Value;

            }
            else
            {
                beamName = (xlSheet.Cells[i, 2] as Microsoft.Office.Interop.Excel.Range).Value;
            }
            string Location = (xlSheet.Cells[i, 3] as Microsoft.Office.Interop.Excel.Range).Value;
            double b = (xlSheet.Cells[i, 6] as Microsoft.Office.Interop.Excel.Range).Value;
            double h = (xlSheet.Cells[i, 7] as Microsoft.Office.Interop.Excel.Range).Value;
            #endregion

            #region Thép giá
            double dkThepDai = (xlSheet.Cells[i, 8] as Microsoft.Office.Interop.Excel.Range).Value;
            double sLThepDai = (xlSheet.Cells[i, 9] as Microsoft.Office.Interop.Excel.Range).Value;
            double kcThepDai = (xlSheet.Cells[i, 10] as Microsoft.Office.Interop.Excel.Range).Value;
            #endregion

            #region Thép chủ

            #region Biến phụ cho mSection
            //Top 
            double L1_n1T = 0;
            double L1_n2T = 0;
            double L1_d1T = 0;
            double L1_d2T = 0;
            double L2_n1T = 0;
            double L2_n2T = 0;
            double L2_d1T = 0;
            double L2_d2T = 0;

            // Bottom 
            double L1_n1B = 0;
            double L1_n2B = 0;
            double L1_d1B = 0;
            double L1_d2B = 0;
            double L2_n1B = 0;
            double L2_n2B = 0;
            double L2_d1B = 0;
            double L2_d2B = 0;
            #endregion

            #region Biến phụ lấy dữ liệu Excel
            #region Dữ liệu dòng trước
            double Excel18_N1_DongTruoc = 0;
            if ((xlSheet.Cells[i - 1, 18] as Microsoft.Office.Interop.Excel.Range).Value != null)
            { Excel18_N1_DongTruoc = (xlSheet.Cells[i - 1, 18] as Microsoft.Office.Interop.Excel.Range).Value; }

            double Excel19_D1_DongTruoc = 0;
            if ((xlSheet.Cells[i - 1, 19] as Microsoft.Office.Interop.Excel.Range).Value != null)
            { Excel19_D1_DongTruoc = (xlSheet.Cells[i - 1, 19] as Microsoft.Office.Interop.Excel.Range).Value; }
            #endregion

            #region Dữ liệu dòng chính
            // LAYER 1
            double Excel18_L1_N1 = 0;
            if ((xlSheet.Cells[i, 18] as Microsoft.Office.Interop.Excel.Range).Value != null) 
            { Excel18_L1_N1 = (xlSheet.Cells[i, 18] as Microsoft.Office.Interop.Excel.Range).Value;}

            double Excel19_L1_D1 = 0;
            if ((xlSheet.Cells[i, 19] as Microsoft.Office.Interop.Excel.Range).Value != null)
            { Excel19_L1_D1 = (xlSheet.Cells[i, 19] as Microsoft.Office.Interop.Excel.Range).Value; }

            double Excel20_L1_N2 = 0;
            if ((xlSheet.Cells[i, 20] as Microsoft.Office.Interop.Excel.Range).Value != null)
            { Excel20_L1_N2 = (xlSheet.Cells[i, 20] as Microsoft.Office.Interop.Excel.Range).Value; }

            double Excel21_L1_D2 = 0;
            if ((xlSheet.Cells[i, 21] as Microsoft.Office.Interop.Excel.Range).Value != null)
            { Excel21_L1_D2 = (xlSheet.Cells[i, 21] as Microsoft.Office.Interop.Excel.Range).Value; }

            // LAYER 2
            double Excel22_L2_N1 = 0;
            if ((xlSheet.Cells[i, 22] as Microsoft.Office.Interop.Excel.Range).Value != null)
            { Excel22_L2_N1 = (xlSheet.Cells[i, 22] as Microsoft.Office.Interop.Excel.Range).Value; }

            double Excel23_L2_D1 = 0;
            if ((xlSheet.Cells[i, 23] as Microsoft.Office.Interop.Excel.Range).Value != null)
            { Excel23_L2_D1 = (xlSheet.Cells[i, 23] as Microsoft.Office.Interop.Excel.Range).Value; }

            double Excel24_L2_N2 = 0;
            if ((xlSheet.Cells[i, 24] as Microsoft.Office.Interop.Excel.Range).Value != null)
            { Excel24_L2_N2 = (xlSheet.Cells[i, 24] as Microsoft.Office.Interop.Excel.Range).Value; }

            double Excel25_L2_D2 = 0;
            if ((xlSheet.Cells[i, 25] as Microsoft.Office.Interop.Excel.Range).Value != null)
            { Excel25_L2_D2 = (xlSheet.Cells[i, 25] as Microsoft.Office.Interop.Excel.Range).Value; }
            #endregion

            #region Dữ liệu dòng sau
            double Excel18_N1_DongSau = 0;
            if ((xlSheet.Cells[i+1, 18] as Microsoft.Office.Interop.Excel.Range).Value != null)
            { Excel18_N1_DongSau = (xlSheet.Cells[i+1, 18] as Microsoft.Office.Interop.Excel.Range).Value; }

            double Excel19_D1_DongSau = 0;
            if ((xlSheet.Cells[i+1, 19] as Microsoft.Office.Interop.Excel.Range).Value != null)
            { Excel19_D1_DongSau = (xlSheet.Cells[i+1, 19] as Microsoft.Office.Interop.Excel.Range).Value; }
            #endregion
            #endregion

            #region Gán biến
            if (Location.Contains("END") | Location.Contains("GỐI"))
            {
                L1_n1T = Excel18_L1_N1;
                L1_d1T = Excel19_L1_D1;
                L1_n2T = Excel20_L1_N2;
                L1_d2T = Excel21_L1_D2;

                L2_n1T = Excel22_L2_N1;
                L2_d1T = Excel23_L2_D1;
                L2_n2T = Excel24_L2_N2;
                L2_d2T = Excel25_L2_D2;

                L1_n1B = Excel18_N1_DongSau;
                L1_d1B = Excel19_D1_DongSau;
            }
            else if (Location.Contains("CENTER") | Location.Contains("NHỊP"))
            {
                L1_n1B = Excel18_L1_N1;
                L1_d1B = Excel19_L1_D1;
                L1_n2B = Excel20_L1_N2;
                L1_d2B = Excel21_L1_D2;

                L2_n1B = Excel22_L2_N1;
                L2_d1B = Excel23_L2_D1;
                L2_n2B = Excel24_L2_N2;
                L2_d2B = Excel25_L2_D2;

                L1_n1T = Excel18_N1_DongTruoc;
                L1_d1T = Excel19_D1_DongTruoc;
            }
            #endregion


            #endregion

            #region Setup to View
            string Layer1_top1View;
            if(L1_n1T==0) { Layer1_top1View = null;}
            else { Layer1_top1View = "[" + L1_n1T.ToString() + "T" + L1_d1T.ToString(); }

            string Layer1_top2View;
            if (L1_n2T == 0){ Layer1_top2View = "]";}
            else { Layer1_top2View = " + " + L1_n2T.ToString() + "T" + L1_d2T.ToString() + "]"; }

            string Layer2_top1View;
            if (L2_n1T == 0) { Layer2_top1View = null; }
            else { Layer2_top1View = " + [" + L2_n1T.ToString() + "T" + L2_d1T.ToString(); }

            string Layer2_top2View;
            if (L2_n1T == 0) { Layer2_top2View = null; }
            else
            {
                if (L2_n2T == 0) { Layer2_top2View = "]"; }
                else { Layer2_top2View = " + " + L2_n2T.ToString() + "T" + L2_d2T.ToString() + "]"; }

            }

            string Layer1_bot1View;
            if (L1_n1B == 0) { Layer1_bot1View = null; }
            else { Layer1_bot1View = "[" + L1_n1B.ToString() + "T" + L1_d1B.ToString(); }

            string Layer1_bot2View;
            if (L1_n2B == 0) { Layer1_bot2View = "]"; }
            else { Layer1_bot2View = " + " + L1_n2B.ToString() + "T" + L1_d2B.ToString() + "]"; }

            string Layer2_bot1View;
            if (L2_n1B == 0) { Layer2_bot1View = null; }
            else { Layer2_bot1View = " + [" + L2_n1B.ToString() + "T" + L2_d1B.ToString(); }

            string Layer2_bot2View;
            if(L2_n1B ==0) { Layer2_bot2View = null; }
            else
            {
                if (L2_n2B == 0) { Layer2_bot2View = "]"; }
                else { Layer2_bot2View = " + " + L2_n2B.ToString() + "T" + L2_d2B.ToString() + "]"; }
            }

            #endregion

            return new mSectionBeam()
            {
                //Beam Information
                BeamName = beamName,
                SectionLocation = Location,
                B = b,
                H = h,
                DiaStirrup = dkThepDai,
                NStirrup = sLThepDai,
                DisStirrup = kcThepDai,
                //Layer 1
                nTop1_Layer1 = L1_n1T,
                DiaTop1_Layer1 = L1_d1T,
                nTop2_Layer1 = L1_n2T,
                DiaTop2_Layer1 = L1_d2T,
                nBot1_Layer1 = L1_n1B,
                DiaBot1_Layer1 = L1_d1B,
                nBot2_Layer1 = L1_n2B,
                DiaBot2_Layer1 = L1_d2B,
                //Layer 2
                nTop1_Layer2 = L2_n1T,
                DiaTop1_Layer2 = L2_d1T,
                nTop2_Layer2 = L2_n2T,
                DiaTop2_Layer2 = L2_d2T,
                nBot1_Layer2 = L2_n1B,
                DiaBot1_Layer2 = L2_d1B,
                nBot2_Layer2 = L2_n2B,
                DiaBot2_Layer2 = L2_d2B,
                //Set to View
                Stirrup = sLThepDai.ToString() + "d" + dkThepDai.ToString() + "a" + kcThepDai.ToString(),
                TopView =  Layer1_top1View + Layer1_top2View  + Layer2_top1View + Layer2_top2View,
                BotView =  Layer1_bot1View + Layer1_bot2View  + Layer2_bot1View + Layer2_bot2View,

            };
        }
    }
}