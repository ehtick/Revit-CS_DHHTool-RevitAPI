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

namespace _02_01_DrawSectionBeam_Detail2D.MVVM.ViewModel
{
    public class vmMain: PropertyChangedBase
    {
        private static vmMain _dcMain = new vmMain();

        public static vmMain DcMain {get { return _dcMain; }}

        private ObservableRangeCollection<mSectionBeam> _dgSectionBeam = new ObservableRangeCollection<mSectionBeam>();

        readonly mExcel mExcel = new mExcel();

        private Range _excelRange;
        public Range ExcelRange
        {
            get 
            {
                _excelRange = mExcel.xlRange;
                return _excelRange; }
            set
            {
                _excelRange = value;
                OnPropertyChanged(nameof(ExcelRange));
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
                for (int i =36;i< lastrow+1;i++)
                {
                    mSectionBeam mSectionBeam = mExcel.SectionBeam(i);
                    DgSectionBeam.Add(mSectionBeam);
                }
                //var itemsToRemove = DgSectionBeam.Where(x=>x.BeamName == null).ToList();

                //foreach (var itemToRemove in itemsToRemove)
                //{
                //    DgSectionBeam.Remove(itemToRemove);
                //}
                
                //MessageBox.Show(mExcel.xlRange.ToString());
                //MessageBox.Show(mExcel.xlRange.Count.ToString());
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
        }
    }
}
