using System.Collections.Generic;
using System.Linq;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.UI;
using Autodesk.Revit.DB;
using DHHTools;
using Autodesk.Revit.ApplicationServices;

namespace _06_00_RS2D_SummarySchedule
{
    [Transaction(TransactionMode.Manual)]
    public class RS2D_SummarySchedule : IExternalCommand
    {
        Result IExternalCommand.Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Application app = uiapp.Application;
            Document document = uidoc.Document;
            using (TransactionGroup transGroup = new TransactionGroup(document))
            {
                transGroup.Start("Rebar Summary");
                List<Element> pickelements = uidoc.Selection.PickElementsByRectangle(new DhhRebarScheduleFilter(), "Chọn bảng thống kê").ToList();
                transGroup.Commit();
            }
            return Result.Succeeded;
        }
    }
}
