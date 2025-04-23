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
using _01_02_FormatCADImport.MVVM.Model;
using static _01_02_FormatCADImport.MVVM.Model.mHandler;


namespace _01_02_FormatCADImport
{
    [Transaction(TransactionMode.Manual)]
    public class FormatCAD : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIApplication uIApp = commandData.Application;
            RevitApp = uIApp;
            mHandlerEvent = ExternalEvent.Create(new mHandler());
            RevitAppService = RevitApp.Application;
            UIDocument uIDoc = RevitApp.ActiveUIDocument;
            Document document = uIDoc.Document;
            using (TransactionGroup transGroup = new TransactionGroup(document))
            {
                transGroup.Start("Format CAD");
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
