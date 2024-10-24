using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.UI;
using Autodesk.Revit.DB;
using static _06_04_RS2D_RebarSlab2D.MVVM.ViewModel.vmMainRebarSlab2DSchedule;
using _06_04_RS2D_RebarSlab2D.MVVM.View;

namespace _06_04_RS2D_RebarSlab2D
{
    [Transaction(TransactionMode.Manual)]
    public class RS2D_RebarSlab2D : IExternalCommand
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
                transGroup.Start("Rebar Slab 2D Schedule");
                vMainRebarSlab2DSchedule win = new vMainRebarSlab2DSchedule();
                bool? dialog = win.ShowDialog();
                if (dialog != false)
                    return Result.Succeeded;
                transGroup.Commit();
            }
            return Result.Succeeded;
        }
    }
}
