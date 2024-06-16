using _05_01_ModelEtabsFromRevit.MVVM.View;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static _05_01_ModelEtabsFromRevit.MVVM.ViewModel.vmMainEtabsFrRevit;

namespace _05_01_ModelEtabsFromRevit
{
    [Transaction(TransactionMode.Manual)]
    public class ModelEtabsFromRevit : IExternalCommand
    {
        Result IExternalCommand.Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIApplication uIApp = commandData.Application;
            RevitUIApp = uIApp;
            //mHandlerEvent = ExternalEvent.Create(new mHandler());
            RevitApp = RevitUIApp.Application;
            UIDocument uIDoc = RevitUIApp.ActiveUIDocument;
            Document document = uIDoc.Document;
            using (TransactionGroup transGroup = new TransactionGroup(document))
            {
                transGroup.Start("Model ETABS From Revit");
                vMainEtabsFrRevit win = new vMainEtabsFrRevit();
                bool? dialog = win.ShowDialog();
                if (dialog != false)
                    return Result.Succeeded;
                transGroup.Commit();
            }
            return Result.Succeeded;
        }
    }
}
