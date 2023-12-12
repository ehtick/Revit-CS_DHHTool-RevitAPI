using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using _01_04_PrintMultiFiles.MVVM.View;
//using static _01_04_PrintMultiFiles.MVVM.ViewModel.vmMain;
using static _04_02_ModelColumnsFromCAD.MVVM.ViewModel.vmMain;
using _04_02_ModelColumnsFromCAD.MVVM.View;
using Autodesk.Revit.Attributes;

namespace DHHTools
{
    [Transaction(TransactionMode.Manual)]
    public class ModelColumnsFromCAD: IExternalCommand
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
                transGroup.Start("Model Columns From AutoCAD");
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
