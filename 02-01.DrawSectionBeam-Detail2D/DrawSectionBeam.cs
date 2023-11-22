using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _02_01_DrawSectionBeam_Detail2D.MVVM.
using Autodesk.Revit.Attributes;

namespace _02_01_DrawSectionBeam_Detail2D
{
    [Transaction(TransactionMode.Manual)]
    public class DrawSectionBeam : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            return Execute(commandData.Application);
        }

        public Result Execute(UIApplication application)
        {
            var win = new MVVM.View.vMain();
            return Result.Succeeded;
        }
    }
}
