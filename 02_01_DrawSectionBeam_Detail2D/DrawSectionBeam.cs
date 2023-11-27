using _02_01_DrawSectionBeam_Detail2D.MVVM.View;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static _02_01_DrawSectionBeam_Detail2D.MVVM.ViewModel.vmMain;

namespace _02_01_DrawSectionBeam_Detail2D
{
    [Transaction(TransactionMode.Manual)]
    public class DrawSectionBeam: IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            RevitApp = commandData.Application;
            var win = new vMain();
            win.Show();
            return Result.Succeeded;
        }
        //public Result Execute(UIApplication application)
        //{
        //    var win = new vMain();
        //    win.Show();
        //    return Result.Succeeded;
        //}

    }
}
