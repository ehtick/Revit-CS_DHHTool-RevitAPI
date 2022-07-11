// ReSharper disable RedundantUsingDirective
#region Namespacesusing Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.DB;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.UI.Selection;
using System.Windows.Forms;
using Application = Autodesk.Revit.ApplicationServices.Application;
#endregion

namespace DHHTools
{
    [Transaction(TransactionMode.Manual)]
    public class CreateDetailBeamCmd : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Application app = uiapp.Application;
            Document doc = uidoc.Document;
            XYZ originPoint = new XYZ(0, 0, 0);
            FilteredElementCollector detailitemSymbol = new FilteredElementCollector(doc);
            detailitemSymbol
                .OfClass(typeof(FamilySymbol))
                .WhereElementIsElementType()
                .Where(sym => sym.Category.Name.Equals("Detail Items"))
                .FirstOrDefault(s => s.Name.Equals("ICIC_KC_ThepDamV2"));
            MessageBox.Show(detailitemSymbol.Name.ToString(), "Kết quả", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            //using (Transaction trans2 = new Transaction(doc, "Create Detail Beam"))
            //{
            //    trans2.Start();
            //    doc.Create.NewFamilyInstance(originPoint, detailitemSymbol, doc.ActiveView);
            //    trans2.Commit();
            //}
            //List<Line> curvesb = new List<Line>();
            //List<Line> curvesb = new List<Line>();
            //List<Line> curvesh = new List<Line>();
            //ReferenceArray bRa = new ReferenceArray();
            //ReferenceArray hRa = new ReferenceArray();
            //listElements.Add(ebeamDetail);
            //Parameter bPara = ebeamDetail.LookupParameter("b");
            //Parameter hPara = ebeamDetail.LookupParameter("h");
            //double b = Math.Round(DhhUnitUtils.FeetToMm(bPara.AsDouble()));
            //double h = Math.Round(DhhUnitUtils.FeetToMm(hPara.AsDouble()));
            //LocationPoint eLp = ebeamDetail.Location as LocationPoint;
            //if (eLp != null)
            return Result.Succeeded;
        }
    }
}
