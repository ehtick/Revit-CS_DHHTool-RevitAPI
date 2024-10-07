using Autodesk.Revit.UI;
using BIMSoftLib.MVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _02_02_DrawSectionSlab_Detail2D.MVVM.ViewModel
{
    public class viewmodel_SectionSlab2D : PropertyChangedBase
    {
        private static viewmodel_SectionSlab2D _dcSectionSlab2D = new viewmodel_SectionSlab2D();
        public static viewmodel_SectionSlab2D DcSectionSlab2D { get { return _dcSectionSlab2D; } }

        public static UIControlledApplication RevitCtrlApp;
        public static UIApplication RevitApp;
    }
}
