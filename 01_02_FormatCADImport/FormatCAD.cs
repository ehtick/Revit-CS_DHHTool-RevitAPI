using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static _01_02_FormatCADImport.MVVM.ViewModel.vmMain;
using static _01_02_FormatCADImport.MVVM.View.vMain;
using _01_02_FormatCADImport.MVVM.View;


namespace _01_02_FormatCADImport
{
    [Transaction(TransactionMode.Manual)]
    public class FormatCAD : IExternalCommand
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

                transGroup.Start("Foundation Details");
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
