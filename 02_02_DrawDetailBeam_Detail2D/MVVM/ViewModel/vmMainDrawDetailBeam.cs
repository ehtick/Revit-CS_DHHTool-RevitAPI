using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.UI;
using BIMSoftLib;
using BIMSoftLib.MVVM;

namespace _02_02_DrawDetailBeam_Detail2D.MVVM.ViewModel
{
    public class vmMainDrawDetailBeam: PropertyChangedBase
    {
        private static vmMainDrawDetailBeam _dcMainDrawDetailBeam = new vmMainDrawDetailBeam();
        public static vmMainDrawDetailBeam DcMainDrawDetailBeam { get { return _dcMainDrawDetailBeam; } }
        public static UIApplication RevitApp;
    }
}
