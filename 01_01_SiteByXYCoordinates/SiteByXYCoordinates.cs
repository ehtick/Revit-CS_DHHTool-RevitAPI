using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.ApplicationServices;
using DHHTools;

namespace DHHTools
{
    [Transaction(TransactionMode.Manual)]
    public class SiteByXYCoordinates: IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Application app = uiapp.Application;
            Document document = uidoc.Document;
            using (TransactionGroup transGroup = new TransactionGroup(document))
            {
                transGroup.Start("Rebar Beams");
                View activeView = document.ActiveView;
                Parameter level = activeView.LookupParameter("Associated Level");
                FilteredElementCollector lvlCollector = new FilteredElementCollector(document);
                ICollection<Element> lvlCollection = lvlCollector.OfClass(typeof(Level)).ToElements();
                //Level lvl = lvlCollection[0] as Level;
                //TaskDialog.Show("test", lvl.Name + "\n" + lvl.Elevation.ToString());
                XYZ point1 = new XYZ(DhhUnitUtils.MeterToFeet(575339780*0.001), DhhUnitUtils.MeterToFeet(1148811797*0.001), 0);
                XYZ point2 = new XYZ(DhhUnitUtils.MeterToFeet(575347053*0.001), DhhUnitUtils.MeterToFeet(1148800088*0.001), 0);
                XYZ point3 = new XYZ(DhhUnitUtils.MeterToFeet(575347960*0.001), DhhUnitUtils.MeterToFeet(1148800417*0.001), 0);
                XYZ point4 = new XYZ(DhhUnitUtils.MeterToFeet(575353498*0.001), DhhUnitUtils.MeterToFeet(1148791187*0.001), 0);
                XYZ point5 = new XYZ(DhhUnitUtils.MeterToFeet(575357567*0.001), DhhUnitUtils.MeterToFeet(1148784406*0.001), 0);
                XYZ point6 = new XYZ(DhhUnitUtils.MeterToFeet(575368128*0.001), DhhUnitUtils.MeterToFeet(1148767800*0.001), 0);
                XYZ point7 = new XYZ(DhhUnitUtils.MeterToFeet(575372322*0.001), DhhUnitUtils.MeterToFeet(1148760990*0.001), 0);
                XYZ point8 = new XYZ(DhhUnitUtils.MeterToFeet(575373939*0.001), DhhUnitUtils.MeterToFeet(1148758285*0.001), 0);
                XYZ point9 = new XYZ(DhhUnitUtils.MeterToFeet(575373687*0.001), DhhUnitUtils.MeterToFeet(1148754549*0.001), 0);
                XYZ point10 = new XYZ(DhhUnitUtils.MeterToFeet(575325702*0.001), DhhUnitUtils.MeterToFeet(1148724910*0.001), 0);
                XYZ point11 = new XYZ(DhhUnitUtils.MeterToFeet(575321576*0.001), DhhUnitUtils.MeterToFeet(1148725892*0.001), 0);
                XYZ point12 = new XYZ(DhhUnitUtils.MeterToFeet(575298549*0.001), DhhUnitUtils.MeterToFeet(1148763212*0.001), 0);
                XYZ point13 = new XYZ(DhhUnitUtils.MeterToFeet(575288096*0.001), DhhUnitUtils.MeterToFeet(1148780439*0.001), 0);
                List<XYZ> ListXYZ = new List<XYZ>();
                ListXYZ.Add(point1);
                ListXYZ.Add(point2);
                ListXYZ.Add(point3);
                ListXYZ.Add(point4);
                ListXYZ.Add(point5);
                ListXYZ.Add(point6);
                ListXYZ.Add(point7);
                ListXYZ.Add(point8);
                ListXYZ.Add(point9);
                ListXYZ.Add(point10);
                ListXYZ.Add(point11);
                ListXYZ.Add(point12);
                ListXYZ.Add(point13);
                Level lvl = null;

                XYZ Grid1 = new XYZ(DhhUnitUtils.MeterToFeet(575309373 * 0.001), DhhUnitUtils.MeterToFeet(1148767225 * 0.001), 0);
                XYZ Grid2 = new XYZ(DhhUnitUtils.MeterToFeet(575327657 * 0.001), DhhUnitUtils.MeterToFeet(1148737029 * 0.001), 0);
                XYZ Grid3 = new XYZ(DhhUnitUtils.MeterToFeet(575346326 * 0.001), DhhUnitUtils.MeterToFeet(1148789600 * 0.001), 0);
                List<XYZ> ListGrid = new List<XYZ>();
                ListGrid.Add(Grid1);
                ListGrid.Add(Grid2);
                ListGrid.Add(Grid3);

                foreach (Element l in lvlCollection)
                {
                    Level lvle = l as Level;
                    if (lvle.Name == level.AsString())
                    {
                       lvl = lvle;
                       break;
                    }
                }
                using (Transaction trans = new Transaction(document, "Test Code API"))
                {
                    trans.Start();
                    for (int i = 0; i < ListXYZ.Count(); i++)
                    {
                        if (i < ListXYZ.Count() - 1)
                        {
                            XYZ startPoint = new XYZ(ListXYZ[i].X, ListXYZ[i].Y, lvl.Elevation);
                            XYZ endPoint = new XYZ(ListXYZ[i + 1].X, ListXYZ[i + 1].Y, lvl.Elevation);
                            Line line = Line.CreateBound(startPoint, endPoint);
                            document.Create.NewDetailCurve(activeView, line);
                        }
                        else
                        {
                            XYZ startPoint = new XYZ(ListXYZ[i].X, ListXYZ[i].Y, lvl.Elevation);
                            XYZ endPoint = new XYZ(ListXYZ[0].X, ListXYZ[0].Y, lvl.Elevation);
                            Line line = Line.CreateBound(startPoint, endPoint);
                            document.Create.NewDetailCurve(activeView, line);
                        }
                    }
                    trans.Commit();
                }

                using (Transaction trans = new Transaction(document, "Test Code API"))
                {
                    trans.Start();
                    for (int i = 0; i < ListGrid.Count(); i++)
                    {

                        if (i < ListGrid.Count() - 1)
                        {
                            XYZ startPoint = new XYZ(ListGrid[i].X, ListGrid[i].Y, lvl.Elevation);
                            XYZ endPoint = new XYZ(ListGrid[i + 1].X, ListGrid[i + 1].Y, lvl.Elevation);
                            Line line = Line.CreateBound(startPoint, endPoint);
                            document.Create.NewDetailCurve(activeView, line);
                        }
                        else
                        {
                            XYZ startPoint = new XYZ(ListGrid[i].X, ListGrid[i].Y, lvl.Elevation);
                            XYZ endPoint = new XYZ(ListGrid[0].X, ListGrid[0].Y, lvl.Elevation);
                            Line line = Line.CreateBound(startPoint, endPoint);
                            document.Create.NewDetailCurve(activeView, line);
                        }
                    }
                    trans.Commit();
                }
                transGroup.Commit();
            }
            return Result.Succeeded;
        }
    }
}
