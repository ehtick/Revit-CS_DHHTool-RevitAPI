using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.Attributes;
using static DHHTools.MVVM.View.vMain;
using static DHHTools.MVVM.ViewModel.vmMain;
using DHHTools.MVVM.View;

namespace DHHTools
{
    [Transaction(TransactionMode.Manual)]
    public class RebarBeam3D: IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIApplication uIApplication = commandData.Application;
            RevitApp = uIApplication;
            RevitAppService = RevitApp.Application;
            UIDocument uIDocument = RevitApp.ActiveUIDocument;
            Document document = uIDocument.Document;
            using (TransactionGroup transGroup = new TransactionGroup(document))
            {
                transGroup.Start("Rebar Beams");
                vMain win = new vMain();
                bool? dialog = win.ShowDialog();
                if (dialog != false)
                    return Result.Succeeded;
                transGroup.Commit();
            }
            return Result.Succeeded;
        }
    }
}
