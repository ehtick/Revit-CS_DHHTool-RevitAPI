using System.Collections.Generic;
using System.Linq;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.UI;
using Autodesk.Revit.DB;
using DHHTools;
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.UI.Events;
using System.Windows;
using System;

namespace _06_00_RS2D_SummarySchedule
{
    [Transaction(TransactionMode.Manual)]
    public class RS2D_SummarySchedule : IExternalCommand
    {
        Result IExternalCommand.Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            //Autodesk.Revit.ApplicationServices.Application app = uiapp.Application;
            Document document = uidoc.Document;
            try
            {
                using (TransactionGroup transGroup = new TransactionGroup(document))
                {
                    transGroup.Start("Rebar Summary");
                    List<Element> pickelements = uidoc.Selection.PickElementsByRectangle(new RebarScheduleFilter(), "Chọn bảng thống kê").ToList();
                    List<RSDetailInfor> List1 = new List<RSDetailInfor>();
                    foreach (Element pickelement in pickelements)
                    {
                        FamilyInstance familyInstance = pickelement as FamilyInstance;
                        Parameter parameterDK = familyInstance.LookupParameter("DK");
                        string DK_String = parameterDK.AsValueString();

                        Parameter parameterHDKT = familyInstance.LookupParameter("HDKT");
                        ElementId nestedelementId = parameterHDKT.AsElementId();
                        Element nestedelement = document.GetElement(nestedelementId);
                        string TCD_String = GetNestedFamilyParameter.GetNestedFamilyParameterValue(document, familyInstance, nestedelement.Name, "TCD");


                        MessageBox.Show(DK_String + " - " + TCD_String);




                    }
                    transGroup.Commit();
                }
            }
            catch { }
            return Result.Succeeded;
        }
    }
}
