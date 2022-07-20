// ReSharper disable RedundantUsingDirective
#region Namespaces
using System.Linq;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System.Collections.Generic;
using System;
using System.Windows.Forms;
using static DHHTools.DhhUnitUtils;
using Application = Autodesk.Revit.ApplicationServices.Application;
// ReSharper disable All
#pragma warning disable 184

#endregion

namespace DHHTools
{
    [Transaction(TransactionMode.Manual)]
    class ViewPlanDimensionPlanColumnCmd : IExternalCommand
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
            List<Solid> listSolids2 = new List<Solid>();
            List<PlanarFace> listPlanarFaces = new List<PlanarFace>();
            List<GeometryElement> listgeo = new List<GeometryElement>();
            if (element.Name != "Structural Plan")
            {
                MessageBox.Show("Phải sử dụng trên Structure Plan", "Tên Level", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                Parameter par = uidoc.ActiveView.LookupParameter("Associated Level");
                MessageBox.Show(par.AsString(), "Tên Level", MessageBoxButtons.OK, MessageBoxIcon.Information);
                IList<Element> sColumnInView = new FilteredElementCollector(doc, doc.ActiveView.Id)
                        .OfCategory(BuiltInCategory.OST_StructuralColumns)
                        .ToElements()
                        .ToList();
                Options options = new Options();
                foreach (var eSColumn in sColumnInView)
                {
                    GeometryElement geoColumn = eSColumn.get_Geometry(options);
                    foreach (GeometryObject egeoColumn in geoColumn)
                    {
                        //Xử lý cho cột thép
                        if (egeoColumn is GeometryInstance egeoInstance)
                        {
                           GeometryElement geometry = egeoInstance.GetSymbolGeometry();
                           foreach (GeometryObject egeometry in geometry)
                           {
                               if (egeometry is Solid)
                               {
                                   Solid eSolid = egeometry as Solid;
                                   if (eSolid.SurfaceArea > 0) listSolids.Add(eSolid);
                               }
                           }
                        }
                        // Xử lý cho cột BT
                       else
                        {
                           if (egeoColumn is Solid)
                           {
                               Solid eSolid = egeoColumn as Solid;
                               if (eSolid.SurfaceArea > 0) listSolids2.Add(eSolid);
                               FaceArray faceArray = eSolid.Faces;
                               foreach (var efaceArray in faceArray)
                               {
                                   PlanarFace eplannFace = efaceArray as PlanarFace;
                                   if (Math.Round(eplannFace.FaceNormal.DotProduct(XYZ.BasisZ))==0)
                                   {
                                       List<PlanarFace> eFaces = new List<PlanarFace>();
                                       eFaces.Add(eplannFace);
                                       for (int i = 0; i < eFaces.Count-1; i++)
                                       {
                                           eFaces[i].FaceNormal.DotProduct(eFaces[i + 1].FaceNormal);
                                       }
                                   }
                               }
                           }
                        }
                    }
                }
                MessageBox.Show(listSolids.Count.ToString(), "Cột thép", MessageBoxButtons.OK, MessageBoxIcon.Information);
                MessageBox.Show(listSolids2.Count.ToString()+"+"+listPlanarFaces.Count.ToString(), "Cột bê tông", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            return Result.Succeeded;
        }
    }
}
