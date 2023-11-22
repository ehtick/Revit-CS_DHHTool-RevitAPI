using Microsoft.Xaml.Behaviors.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _02_01_DrawSectionBeam_Detail2D.MVVM.Model;
using BIMSoftLib.MVVM;
using System.Windows.Input;

namespace _02_01_DrawSectionBeam_Detail2D.MVVM.ViewModel
{
    public class vmMain: PropertyChangedBase
    {
        private static vmMain _dcMain = new vmMain();

        public static vmMain DcMain {get { return _dcMain; }}

        private ObservableRangeCollection<mSectionBeam> _dgSectionBeam = new ObservableRangeCollection<mSectionBeam>();

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
