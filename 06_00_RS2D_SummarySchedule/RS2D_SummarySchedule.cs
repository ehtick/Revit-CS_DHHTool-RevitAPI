using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.UI;
using Autodesk.Revit.DB;

namespace _06_00_RS2D_SummarySchedule
{
    [Transaction(TransactionMode.Manual)]
    public class RS2D_SummarySchedule : IExternalCommand
    {
        Result IExternalCommand.Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIApplication uIApp = commandData.Application;
            UIDocument uIDoc = uIApp.ActiveUIDocument;
            Document document = uIDoc.Document;
            using (TransactionGroup transGroup = new TransactionGroup(document))
            {

                transGroup.Commit();
            }
            return Result.Succeeded;
        }
    }
}
