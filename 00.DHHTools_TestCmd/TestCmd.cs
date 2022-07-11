#region Namespaces
//using System.Linq;
//using System.Windows.Documents;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using static DHHTools.DhhUnitUtils;
using Application = Autodesk.Revit.ApplicationServices.Application;

// ReSharper disable All
#endregion


namespace DHHTools
{
    [Transaction(TransactionMode.Manual)]
    public class TestCmd : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
            {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Application app = uiapp.Application;
            Document doc = uidoc.Document;
            #region Get Parameter Framing
            List<ElementId> selectColumsElements = new List<ElementId>();
            Reference refer = uidoc.Selection.PickObject(ObjectType.Face);
            Element element = doc.GetElement(refer);
            ElementType elementType = doc.GetElement(element.GetTypeId()) as ElementType;
            double elementCover = DhhElementUtils.GetElementCover(doc, element);
            Parameter bParameter = elementType.LookupParameter("b");
            Parameter hParameter = elementType.LookupParameter("h");
            double bFeet = bParameter.AsDouble();
            double hFeet = hParameter.AsDouble();
            #endregion Get Parameter Framing


            #region Get Column Interect
            PlanarFace face = element.GetGeometryObjectFromReference(refer) as PlanarFace;
            XYZ faceNormalline = face.FaceNormal;
            XYZ faceNormal = new XYZ(Math.Round(faceNormalline.X), Math.Round(faceNormalline.Y), Math.Round(faceNormalline.Z));
            LocationCurve elementLocation = element.Location as LocationCurve;
            Line line = elementLocation.Curve as Line;
            XYZ lineDirection = line.Direction;
            Line xLine = DhhGeometryUtils.LineToXLine(line);
            IList<CurveLoop> curveLoops = face.GetEdgesAsCurveLoops();
            CurveLoop curveLoop = curveLoops[0];
            double rectangularHeight =
                FeetToMm(curveLoop.GetRectangularHeight(Plane.CreateByNormalAndOrigin(faceNormal, face.Origin)));
            double rectangularWidth =
                FeetToMm(curveLoop.GetRectangularHeight(Plane.CreateByNormalAndOrigin(faceNormal, face.Origin)));
            double lengthBeam;
            if (rectangularHeight == FeetToMm(bFeet) || rectangularHeight == FeetToMm(hFeet))
            {
                lengthBeam = Math.Round(rectangularWidth);
            }
            else
            {
                lengthBeam = Math.Round(rectangularHeight);
            }

            double angle = RadiansToDegrees(faceNormal.AngleTo(XYZ.BasisZ));
            double distanceOffset;
            if (angle == 0)
            {
                distanceOffset = hFeet;
            }
            else
            {
                distanceOffset = bFeet;
            }
            Solid solid = GeometryCreationUtilities.CreateExtrusionGeometry(curveLoops, faceNormal.Negate(), distanceOffset);

            BoundingBoxUV boundingBox = face.GetBoundingBox();
            UV midPointFaceUV = boundingBox.Max.Add(boundingBox.Min).Multiply(0.5);
            XYZ midPointFaceXYZ = face.Evaluate(midPointFaceUV);
            XYZ vectorAdd = faceNormal.Negate().Multiply(distanceOffset / 2);
            XYZ midPointSolid = midPointFaceXYZ.Add(vectorAdd);
            Line line2 = Line.CreateUnbound(midPointSolid, lineDirection);
            XYZ line2Direction = line2.Direction;
            ElementCategoryFilter filter = new ElementCategoryFilter(BuiltInCategory.OST_StructuralColumns);
            ReferenceIntersector refIntersector =
                new ReferenceIntersector(filter, FindReferenceTarget.Element, doc.ActiveView as View3D);
            ReferenceWithContext referenceWithContext_No1 = refIntersector.FindNearest(midPointSolid, line2Direction);
            ReferenceWithContext referenceWithContext_No2 = refIntersector.FindNearest(midPointSolid, line2Direction.Negate());
            Reference reference1 = referenceWithContext_No1.GetReference();
            Reference reference2 = referenceWithContext_No2.GetReference();
            ElementId elementId1 = doc.GetElement(reference1).Id;
            ElementId elementId2 = doc.GetElement(reference2).Id;
            selectColumsElements.Add(elementId1);
            selectColumsElements.Add(elementId2);
            //uidoc.Selection.SetElementIds(selectColumsElements);
            #endregion Get Column Interect

            #region Get Face Element
            List<Face> topFaceFromSolid = DhhGeometryUtils.GetTopFaceFromSolid(solid);
            List<Face> bottomFaceFromSolid = DhhGeometryUtils.GetBottomFaceFromSolid(solid);
            List<Face> sideFaceFromSolid = DhhGeometryUtils.GetSideFaceFromSolid(solid);
            #endregion

            #region Create Curve Rebar
            Face topFace = topFaceFromSolid[0];
            Face topFaceOffset = DhhGeometryUtils.FaceOffset(topFace, XYZ.BasisZ.Negate(), MmToFeet(elementCover));
            List<Face> besideFaces = new List<Face>();
            List<Face> frontBackFaces = new List<Face>();
            //string isVectorParallel = DhhGeometryUtils.IsVectorParallel((topFace as PlanarFace).FaceNormal, lineDirection).ToString(); ;

            foreach (Face sideFace in sideFaceFromSolid)
            {
                XYZ sideFaceNormal = (sideFace as PlanarFace).FaceNormal;
                bool isVectorParallel = DhhGeometryUtils.IsVectorParallel(sideFaceNormal, lineDirection);
                if (isVectorParallel == false)
                {
                    besideFaces.Add(sideFace);
                }
            }

            foreach (Face frontBackFace in sideFaceFromSolid)
            {
                XYZ sideFaceNormal = (frontBackFace as PlanarFace).FaceNormal;
                bool isVectorParallel = DhhGeometryUtils.IsVectorParallel(sideFaceNormal, lineDirection);
                if (isVectorParallel == true)
                {
                    frontBackFaces.Add(frontBackFace);
                }
            }

            Curve curveRebar;
            //IntersectionResultArray result = null;
            Face onebesideFace = besideFaces[0];
            Face besideFaceOffset = DhhGeometryUtils.FaceOffset(onebesideFace, (onebesideFace as PlanarFace).FaceNormal.Negate(), MmToFeet(elementCover));
            FaceIntersectionFaceResult faceResult = topFaceOffset.Intersect(besideFaceOffset, out curveRebar);
            Line xlineRebar = DhhGeometryUtils.LineToXLine(curveRebar as Line);
            //foreach (Face frbaFace in frontBackFaces)
            //{
            //    Face frbaFaceOffset = DhhGeometryUtils.FaceOffset(frbaFace, (frbaFace as PlanarFace).FaceNormal.Negate(), MmToFeet(lengthBeam / 6));
            //    SetComparisonResult setComparisonResult = frbaFaceOffset.Intersect(xlineRebar, out result);
            //}

            //int resultSize = result.Size;

            #endregion

            #region Check tọa độ các face của solid
            String messagetext = String.Empty;
            string xPoint1Line = Math.Round(FeetToMm(xlineRebar.GetEndPoint(0).X)).ToString();
            string yPoint1Line = Math.Round(FeetToMm(xlineRebar.GetEndPoint(0).Y)).ToString();
            string zPoint1Line = Math.Round(FeetToMm(xlineRebar.GetEndPoint(0).Z)).ToString();
            string xPoint2Line = Math.Round(FeetToMm(xlineRebar.GetEndPoint(1).X)).ToString();
            string yPoint2Line = Math.Round(FeetToMm(xlineRebar.GetEndPoint(1).Y)).ToString();
            string zPoint2Line = Math.Round(FeetToMm(xlineRebar.GetEndPoint(1).Z)).ToString();
            XYZ vectorExtruc = faceNormal.Negate();
            double lenghExtruc = faceNormal.Negate().GetLength();
            string xSolid = Math.Round(FeetToMm(midPointSolid.X)).ToString();
            string ySolid = Math.Round(FeetToMm(midPointSolid.Y)).ToString();
            string zSolid = Math.Round(FeetToMm(midPointSolid.Z)).ToString();
            string xSelectFaceNormal = Math.Round(vectorExtruc.X / faceNormal.GetLength()).ToString();
            string ySelectFaceNormal = Math.Round(vectorExtruc.Y / faceNormal.GetLength()).ToString();
            string zSelectFaceNormal = Math.Round(vectorExtruc.Z / faceNormal.GetLength()).ToString();
            BoundingBoxUV boundingBoxUvselectFace = face.GetBoundingBox();
            UV midPointselectFaceUV = boundingBoxUvselectFace.Max.Add(boundingBoxUvselectFace.Min).Multiply(0.5);
            XYZ midPointselectFaceXYZ = face.Evaluate(midPointselectFaceUV);
            string xOringinselectFace = Math.Round(FeetToMm(midPointselectFaceXYZ.X)).ToString();
            string yOringinselectFace = Math.Round(FeetToMm(midPointselectFaceXYZ.Y)).ToString();
            string zOringinselectFace = Math.Round(FeetToMm(midPointselectFaceXYZ.Z)).ToString();

            messagetext = "Point Solid: " + "(" + xSolid + "," + ySolid + "," + zSolid + ")   " + "\n" +
                          "Point 1: " + "(" + xPoint1Line + "," + yPoint1Line + "," + zPoint1Line + ")   " + "\n" +
                          "Point 2: " + "(" + xPoint2Line + "," + yPoint2Line + "," + zPoint2Line + ")" + "\n" +
                          "Point Face: " + "(" + xOringinselectFace + "," + yOringinselectFace + "," + zOringinselectFace + ")";

            #endregion

            MessageBox.Show(messagetext, "Kết quả", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);

            #region MyRegion
            //Options options = new Options();
            //FilteredElementCollector collector = new FilteredElementCollector(doc)
            //    .OfCategory(BuiltInCategory.OST_StructuralColumns);
            //List<Face> facesColumn = new List<Face>();
            //List<Solid> allSolids = new List<Solid>();
            //List<ElementId> idsColumnFileter = new List<ElementId>();
            //foreach (Element columnElement in collector)
            //{
            //    idsColumnFileter.Add(columnElement.Id);
            //    GeometryElement geometry = columnElement.get_Geometry(options);
            //    foreach (GeometryObject geometryObj in geometry)
            //    {
            //        if (geometryObj is Solid)
            //        {
            //            Solid solidColumn = geometryObj as Solid;
            //            if (solidColumn.SurfaceArea > 0)
            //            {
            //                allSolids.Add(solidColumn);
            //            }
            //        }
            //    }
            //}

            //IEnumerable<Solid> allSolidsColumn = allSolids.Distinct().ToList();
            //foreach (Solid solidColumn in allSolidsColumn)
            //{
            //    FaceArray columnFacesArray = solidColumn.Faces;
            //    foreach (object columnFace in columnFacesArray)
            //    {
            //        PlanarFace planarFaceColumn = columnFace as PlanarFace;
            //        foreach (PlanarFace planarFaceFraming in planarFaces)
            //        {
            //            FaceIntersectionFaceResult intersectionFaceResult = planarFaceFraming.Intersect(planarFaceColumn);
            //            if (intersectionFaceResult == FaceIntersectionFaceResult.Intersecting)
            //            {
            //                facesColumn.Add(planarFaceColumn);
            //            }
            //        }
            //    }
            //}

            //List<Face> list = facesColumn.Distinct().ToList();
            //List<ElementId> elementColumnId = new List<ElementId>();
            ////foreach (Face faceColumnInter in list)
            ////{
            ////    if (faceColumnInter is PlanarFace)
            ////    {
            ////        PlanarFace columnInter = faceColumnInter as PlanarFace;
            ////        Reference faceReference = columnInter.Reference;
            ////        Element ele = doc.GetElement();
            ////        elementColumnId.Add(ele.Id);
            ////    }

            ////}

            //uidoc.Selection.SetElementIds(elementColumnId);
            #endregion

            #region Lưu lại

            //SolidOptions solidOptions = new SolidOptions();
            //GeometryCreationUtilities.CreateExtrusionGeometry()
            ////Reference rElement = uidoc.Selection.PickObject(ObjectType.Element, "Chọn 1 cái dầm");
            //Element element = doc.GetElement(pickFaces);
            //GeometryElement geomElement = element.get_Geometry(new Options());
            //List<Solid> allSolids = new List<Solid>();
            //foreach (GeometryObject geomObj in geomElement)
            //{
            //    if (geomObj is Solid)
            //    {
            //        Solid solid = geomObj as Solid;
            //        if (solid.SurfaceArea > 0)
            //        {
            //            allSolids.Add(solid);
            //        }
            //    }
            //}

            //Solid solid0 = allSolids[0];
            //List<Solid> spSolids = SolidUtils.SplitVolumes(solid0).ToList();

            ////FilteredElementCollector collector = new FilteredElementCollector(doc);
            ////collector.OfClass(typeof(FamilyInstance));
            ////collector.WherePasses(new ElementIntersectsSolidFilter(solid0));

            //List<ElementId> idsSolid = SolidSolidCutUtils.GetCuttingSolids(element).ToList();


            //uidoc.Selection.SetElementIds(idsSolid);

            //RebarBarType rebarBarType = RebarBarType.Create(doc);
            //RebarStyle style = RebarStyle.Standard;
            //RebarHookType rebarHookType = RebarHookType.Create(doc, 90, 1);
            //Rebar.CreateFromCurves(doc, style,rebarBarType,rebarHookType, rebarHookType,element,);

            #endregion

            #region Code vẽ thép

            //Location elementLocation = element.Location;
            //LocationCurve locationCurve = elementLocation as LocationCurve;
            //double lengthLine = Math.Round(DHHUnitUtils.FeetToMm(locationCurve.Curve.Length), 0);

            //RebarBarType rebarBarType = new FilteredElementCollector(doc)
            //    .OfClass(typeof(RebarBarType))
            //    .Cast<RebarBarType>()
            //    .First(x => x.Name == "10M");

            //RebarShape rebarShape = new FilteredElementCollector(doc)
            //    .OfClass(typeof(RebarShape))
            //    .Cast<RebarShape>()
            //    .First(x => x.Name == "M_00");
            //Line line = locationCurve.Curve as Line;
            //XYZ origin = line.Origin;
            //XYZ xVector = line.Direction;
            //XYZ yVector = new XYZ(0, 0, -50);

            //using (Transaction trans = new Transaction(doc, "Test Code API"))
            //{
            //    trans.Start();
            //    Rebar createrebar = Rebar.CreateFromRebarShape(doc, rebarShape, rebarBarType, element, origin, xVector, yVector);
            //    RebarShapeDrivenAccessor drivenAccessor = createrebar.GetShapeDrivenAccessor();
            //    bool barsOnNormalSide = true;
            //    bool includeFirstBar = true;
            //    bool includeLastBar = true;
            //    drivenAccessor.SetLayoutAsFixedNumber(2, DHHUnitUtils.MmToFeet(200), barsOnNormalSide, includeFirstBar, includeLastBar);
            //    createrebar.SetSolidInView(doc.ActiveView as View3D, true);
            //    createrebar.SetUnobscuredInView(doc.ActiveView as View3D, true);
            //    trans.Commit();
            //}

            #endregion

            //MessageBox.Show(FeetToMm(curveLoops[0].GetRectangularWidth(Plane.CreateByNormalAndOrigin(faceNormal, midPointSolid))).ToString(), "Kết quả", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            //MessageBox.Show(faces.ToString(), "Kết quả", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            return Result.Succeeded;
        }
    }
}
