// ReSharper disable RedundantUsingDirective
#region Namespaces
//using System.Linq;
//using System.Windows.Documents;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
//using System;
//using System.Collections.Generic;
using System.Windows.Forms;
//using static DHHTools.DhhUnitUtils;
using Application = Autodesk.Revit.ApplicationServices.Application;
// ReSharper disable All
#endregion

namespace DHHTools
{
    [Transaction(TransactionMode.Manual)]
    public class ViewDetailDimensionDetailDamCmd : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            //Application app = uiapp.Application;
            Document doc = uidoc.Document;
            List<Element> listElements = new List<Element>();
            List<string> str = new List<string>();
            List<string> strSection = new List<string>();
            IList<Element> beamDetail = new FilteredElementCollector(doc, doc.ActiveView.Id)
                .OfCategory(BuiltInCategory.OST_DetailComponents)
                .ToElements()
                .ToList();
            using (TransactionGroup transGr = new TransactionGroup(doc))
            {
                transGr.Start("Dimension for SectionBeams");
                foreach (var ebeamDetail in beamDetail)
                {
                    if (ebeamDetail.Name == "ICIC_KC_ThepDam")
                    {
                        List<Line> curvesb = new List<Line>();
                        List<Line> curvesh = new List<Line>();
                        ReferenceArray bRa = new ReferenceArray();
                        ReferenceArray hRa = new ReferenceArray();
                        listElements.Add(ebeamDetail);
                        Parameter bPara = ebeamDetail.LookupParameter("b");
                        Parameter hPara = ebeamDetail.LookupParameter("h");
                        double b = Math.Round(DhhUnitUtils.FeetToMm(bPara.AsDouble()));
                        double h = Math.Round(DhhUnitUtils.FeetToMm(hPara.AsDouble()));
                        LocationPoint eLp = ebeamDetail.Location as LocationPoint;
                        if (eLp != null)
                        {
                            XYZ eXyz = eLp.Point;
                            double xeXyz = Math.Round(DhhUnitUtils.FeetToMm(eXyz.X));
                            double yeXyz = Math.Round(DhhUnitUtils.FeetToMm(eXyz.Y));
                            double zeXyz = Math.Round(DhhUnitUtils.FeetToMm(eXyz.Z));
                            str.Add(xeXyz.ToString(CultureInfo.InvariantCulture) + "," +
                                    yeXyz.ToString(CultureInfo.InvariantCulture) + "," +
                                    zeXyz.ToString(CultureInfo.InvariantCulture));
                            strSection.Add(bPara.AsValueString() + "x" + hPara.AsValueString());
                            Options option = new Options();
                            option.ComputeReferences = true;
                            option.View = doc.ActiveView;
                            GeometryElement geoElement = ebeamDetail.get_Geometry(option);
                            foreach (GeometryObject geoObject in geoElement)
                            {
                                if (geoObject is GeometryInstance geoInstance)
                                {
                                    GeometryElement geoElement2 = geoInstance.GetSymbolGeometry();
                                    foreach (GeometryObject geoObject2 in geoElement2)
                                    {
                                        if (geoObject2 is Curve curve)
                                        {
                                            if (curve.IsCyclic == false)
                                            {
                                                Line lineDtComponent = curve as Line;
                                                XYZ direction = lineDtComponent?.Direction;
                                                if (!(lineDtComponent is null))
                                                {
                                                    double lelineDtComponent =
                                                        Math.Round(DhhUnitUtils.FeetToMm(lineDtComponent.Length));
                                                    if (direction != null &&
                                                        (Math.Abs(Math.Abs(lelineDtComponent) - b) < 0.0001
                                                         && (Math.Abs(direction.AngleTo(XYZ.BasisX) -
                                                                      DhhUnitUtils.DegreesToRadians(0)) < 0.0001
                                                             || Math.Abs(direction.AngleTo(XYZ.BasisX) -
                                                                         DhhUnitUtils.DegreesToRadians(180)) < 0.0001)))
                                                    {
                                                        curvesb.Add(lineDtComponent);
                                                        bRa.Append(lineDtComponent.Reference);
                                                    }

                                                    if (direction != null &&
                                                        (Math.Abs(Math.Abs(lelineDtComponent) - h) < 0.0001
                                                         && (Math.Abs(direction.AngleTo(XYZ.BasisY) -
                                                                      DhhUnitUtils.DegreesToRadians(0)) < 0.0001
                                                             || Math.Abs(direction.AngleTo(XYZ.BasisY) -
                                                                         DhhUnitUtils.DegreesToRadians(180)) < 0.0001)))
                                                    {
                                                        curvesh.Add(lineDtComponent);
                                                        hRa.Append(lineDtComponent.Reference);
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }

                            using (Transaction trans2 = new Transaction(doc, "Dim Detail Beam"))
                            {
                                trans2.Start();
                                Line lineb = Line.CreateBound(new XYZ(DhhUnitUtils.MmToFeet(xeXyz - b / 2 - 150), 0, 0),
                                    new XYZ(DhhUnitUtils.MmToFeet(xeXyz - b / 2 - 150), DhhUnitUtils.MmToFeet(200), 0));
                                Line lineh =
                                    Line.CreateBound(new XYZ(0, (DhhUnitUtils.MmToFeet(yeXyz - h / 2 - 150)), 0),
                                        new XYZ(DhhUnitUtils.MmToFeet(200), DhhUnitUtils.MmToFeet(yeXyz - h / 2 - 150),
                                            0));
                                doc.Create.NewDimension(doc.ActiveView, lineb, bRa);
                                doc.Create.NewDimension(doc.ActiveView, lineh, hRa);
                                trans2.Commit();
                            }
                        }
                    }
                }
                transGr.Assimilate();
            }
            return Result.Succeeded;
        }
    }
}
