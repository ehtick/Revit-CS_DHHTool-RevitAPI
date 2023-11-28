using Microsoft.Xaml.Behaviors.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _02_01_DrawSectionBeam_Detail2D.MVVM.Model;
using BIMSoftLib.MVVM;
using System.Windows.Input;
using System.Windows;
using Microsoft.Office.Interop.Excel;
using Autodesk.Revit.UI;
using Autodesk.Revit.DB;
using System.Reflection;
using Autodesk.Revit.Creation;
using Document = Autodesk.Revit.DB.Document;
using static _02_01_DrawSectionBeam_Detail2D.MVVM.Model.mRevit;

using _02_01_DrawSectionBeam_Detail2D.MVVM.View;
using System.Windows.Forms;

namespace _02_01_DrawSectionBeam_Detail2D.MVVM.ViewModel
{
    public class vmMain : PropertyChangedBase
    {
        private static vmMain _dcMain = new vmMain();

        public static vmMain DcMain { get { return _dcMain; } }

        public static string Apploc = Assembly.GetExecutingAssembly().Location;
        private ObservableRangeCollection<mSectionBeam> _dgSectionBeam = new ObservableRangeCollection<mSectionBeam>();


        public static UIControlledApplication RevitCtrlApp;
        public static UIApplication RevitApp;
        readonly mExcel mExcel = new mExcel();

        private Range _excelRange;
        public Range ExcelRange
        {
            get
            {
                _excelRange = mExcel.xlRange;
                return _excelRange;
            }
            set
            {
                _excelRange = value;
                OnPropertyChanged(nameof(ExcelRange));
            }
        }

        private double _widthFrame = 1000;
        public double WidthFrame
        {
            get => _widthFrame;
            set
            {
                _widthFrame = value;
                OnPropertyChanged(nameof(WidthFrame));
            }
        }

        private double _heightFrame = 1250;
        public double HeightFrame
        {
            get => _heightFrame;
            set
            {
                _heightFrame = value;
                OnPropertyChanged(nameof(HeightFrame));
            }
        }

        public ObservableRangeCollection<mSectionBeam> DgSectionBeam
        {
            get
            {

                return _dgSectionBeam;
            }
            set
            {
                _dgSectionBeam = value;
                OnPropertyChanged(nameof(DgSectionBeam));
            }
        }

        //Method
        private ActionCommand fileExcelOpen;

        public ICommand FileExcelOpen
        {
            get
            {
                if (fileExcelOpen == null)
                {
                    fileExcelOpen = new ActionCommand(PerformFileExcelOpen);
                }

                return fileExcelOpen;
            }
        }

        private void PerformFileExcelOpen()
        {
            try
            {
                int lastrow = mExcel.OpenExcelFile();
                for (int i = 36; i < lastrow + 1; i++)
                {
                    mSectionBeam mSectionBeam = mExcel.SectionBeam(i);
                    DgSectionBeam.Add(mSectionBeam);
                }
                var itemsToRemove = DgSectionBeam.Where(x => x.BeamName == null).ToList();

                foreach (var itemToRemove in itemsToRemove)
                {
                    DgSectionBeam.Remove(itemToRemove);
                }
                mExcel.xlsworkbook.Close();
                mExcel.xlsApp.Quit();
            }
            catch
            {

            }
        }

        private ActionCommand createSectionDetail;

        public ICommand CreateSectionDetail
        {
            get
            {
                if (createSectionDetail == null)
                {
                    createSectionDetail = new ActionCommand(PerformCreateSectionDetail);
                }

                return createSectionDetail;
            }
        }

        private void PerformCreateSectionDetail()
        {

            try
            {
                CreateSectionBeam2D(RevitApp.ActiveUIDocument.Document, DgSectionBeam);
            }

            catch { }

        }

    }
}
