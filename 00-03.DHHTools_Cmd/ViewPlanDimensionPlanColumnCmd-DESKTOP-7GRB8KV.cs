// ReSharper disable RedundantUsingDirective
#region Namespaces
using System.Linq;
//using System.Windows.Documents;
using System.Collections.Generic;
using System.Windows.Documents;
//using System.Linq;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
//using System;
//using System.Collections.Generic;
using System.Windows.Forms;
using static Autodesk.Revit.DB.View;
//using static DHHTools.DhhUnitUtils;
using Application = Autodesk.Revit.ApplicationServices.Application;

#endregion

namespace DHHTools
{
    [Transaction(TransactionMode.Manual)]
    class ViewPlanDimensionPlanColumnCmd: IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            //Application app = uiapp.Application;
            Document doc = uidoc.Document;
            ElementId elementId = doc.ActiveView.GetTypeId();
            Element element = doc.GetElement(elementId);
            List<Solid> listSolids = new List<Solid>();
            if (element.Name != "Structural Plan")
            {
                MessageBox.Show("Phải sử dụng trên Structure Plan", "Tên Level", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                //Parameter par = uidoc.ActiveView.LookupParameter("Associated Level");
                //MessageBox.Show(par.AsString(), "Tên Level", MessageBoxButtons.OK, MessageBoxIcon.Information);
                IList<Element> sColumnInView = new FilteredElementCollector(doc, doc.ActiveView.Id)
                        .OfCategory(BuiltInCategory.OST_StructuralColumns)
                        .ToElements()
                        .ToList();
                foreach (var eSColumn in sColumnInView)
                {
                    Solid solidsColumn = DhhGeometryUtils.GetSolids(eSColumn);
                    listSolids.Add(solidsColumn);
                }
                MessageBox.Show(listSolids.Count.ToString(), "Tên Level", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            return Result.Succeeded;
        }
    }
}
