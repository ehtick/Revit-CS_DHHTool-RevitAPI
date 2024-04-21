// ReSharper disable All
using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DHHTools
{
    public static class DhhGeometryUtils
    {
        #region GetSolidFromElement
        public static Solid GetSolids(Element element)
        {
            List<Solid> allSolids = new List<Solid>();
            // Defaults to medium detail, no references and no view.
            Options option = new Options();
                option.ComputeReferences = true;
                option.DetailLevel = ViewDetailLevel.Fine;


            GeometryElement geoElement = element.get_Geometry(option);
            foreach (GeometryObject geomObj in geoElement)
            {
                if (geomObj is Solid)
                {
                    Solid solid = geomObj as Solid;
                    if (Math.Round(solid.SurfaceArea) > 0)
                    {
                        allSolids.Add(solid);
                    }
                }
            }
            return allSolids[0];
        }
        #endregion
        #region GetAllSolidFromElement
        public static List<Solid> GetAllSolids(Element element)
        {
            List<Solid> allSolids = new List<Solid>();
            // Defaults to medium detail, no references and no view.
            Options option = new Options();
            option.ComputeReferences = true;
            option.DetailLevel = ViewDetailLevel.Fine;
            GeometryElement geoElement = element.get_Geometry(option);
            foreach (GeometryObject geomObj in geoElement)
            {
                if (geomObj is Solid)
                {
                    Solid solid = geomObj as Solid;
                    if (Math.Round(solid.SurfaceArea) > 0)
                    {
                        allSolids.Add(solid);
                    }
                }
            }
            return allSolids;
        }
        #endregion
        #region LineToXline
        /// <summary>
        ///     Convert Line to XLine.
        /// </summary>
        public static Line LineToXLine(Line line)
        {
            Curve curve = line as Curve;
            XYZ endPoint1 = line.GetEndPoint(0);
            double xEndPoint1 = Math.Round(endPoint1.X);
            double yEndPoint1 = Math.Round(endPoint1.Y);
            double zEndPoint1 = Math.Round(endPoint1.Z);
            XYZ newendPoint1 = new XYZ(xEndPoint1, yEndPoint1, zEndPoint1);
            XYZ endPoint2 = line.GetEndPoint(1);
            double xEndPoint2 = Math.Round(endPoint2.X);
            double yEndPoint2 = Math.Round(endPoint2.Y);
            double zEndPoint2 = Math.Round(endPoint2.Z);
            XYZ newendPoint2 = new XYZ(xEndPoint2, yEndPoint2, zEndPoint2);
            XYZ direction = Line.CreateBound(newendPoint1, newendPoint2).Direction;
            XYZ scaleVector = direction.Multiply(100000);
            XYZ scaleEndPoint1 = newendPoint1.Add(scaleVector);
            XYZ scaleEndPoint2 = newendPoint2.Subtract(scaleVector);
            Line xLine = Line.CreateBound(scaleEndPoint1, scaleEndPoint2);
            return xLine;
        }
        #endregion
        #region GetTopFaceSolid
        /// <summary>
        ///     Get top Faces from Solid.
        /// </summary>
        public static List<Face> GetTopFaceFromSolid(Solid solid)
        {
            List<Face> topPlanarFaces = new List<Face>();
            foreach (object solidFace in solid.Faces)
            {
                PlanarFace planarFace = solidFace as PlanarFace;
                if (planarFace != null)
                {
                    double normalX = Math.Round(planarFace.FaceNormal.X);
                    double normalY = Math.Round(planarFace.FaceNormal.Y);
                    double normalZ = Math.Round(planarFace.FaceNormal.Z);
                    if (normalX.ToString() == "0" &&
                        normalY.ToString() == "0" &&
                        normalZ.ToString() == "1")
                    {
                        topPlanarFaces.Add(planarFace as Face);
                    }
                }
            }
            return topPlanarFaces;
        }
        #endregion
        #region GetBottomFaceSolid
        /// <summary>
        ///     Get bottom Faces from Solid.
        /// </summary>
        public static List<Face> GetBottomFaceFromSolid(Solid solid)
        {
            List<Face> bottomPlanarFaces = new List<Face>();
            foreach (object solidFace in solid.Faces)
            {
                PlanarFace planarFace = solidFace as PlanarFace;
                if (planarFace != null)
                {
                    double normalX = Math.Round(planarFace.FaceNormal.X);
                    double normalY = Math.Round(planarFace.FaceNormal.Y);
                    double normalZ = Math.Round(planarFace.FaceNormal.Z);
                    if (normalX.ToString() == "0" &&
                        normalY.ToString() == "0" &&
                        normalZ.ToString() == "-1")
                    {
                        bottomPlanarFaces.Add(planarFace as Face);
                    }
                }
            }
            return bottomPlanarFaces;
        }
        #endregion
        #region GetSideFaceSolid
        /// <summary>
        ///     Get side Faces from Solid.
        /// </summary>
        public static List<Face> GetSideFaceFromSolid(Solid solid)
        {
            List<Face> planarFaces = new List<Face>();
            FaceArray array = solid.Faces;
            foreach (object solidFace in array)
            {
                PlanarFace planar = solidFace as PlanarFace;
                if (planar != null)
                {
                    double normalZ = Math.Round(planar.FaceNormal.Z);
                    if (normalZ.ToString() == "0")
                    {
                        planarFaces.Add(planar as Face);
                    }
                }
            }

            List<Face> SideFaces = planarFaces.Distinct().ToList();
            return SideFaces;
        }
        #endregion
        #region Offset Face by Vector
        /// <summary>
        ///     Offset face by vector.
        /// </summary>
        public static Face FaceOffset(Face face, XYZ vector, double distanceOffset)
        {
            IList<Face> iFaces = new List<Face>();
            IList<CurveLoop> curveLoops = face.GetEdgesAsCurveLoops();
            Solid solid = GeometryCreationUtilities.CreateExtrusionGeometry(curveLoops, vector, distanceOffset);
            foreach (object eachface in solid.Faces)
            {
                PlanarFace planarFace = eachface as PlanarFace;
                double faceNormalX;
                if (Math.Round(planarFace.FaceNormal.X) == 0)
                {
                    faceNormalX = 0;
                }
                else
                {
                    faceNormalX = Math.Round(planarFace.FaceNormal.X) / Math.Abs(Math.Round(planarFace.FaceNormal.X));
                }
                double faceNormalY;
                if (Math.Round(planarFace.FaceNormal.Y) == 0)
                {
                    faceNormalY = 0;
                }
                else
                {
                    faceNormalY = Math.Round(planarFace.FaceNormal.Y) / Math.Abs(Math.Round(planarFace.FaceNormal.Y));
                }
                double faceNormalZ;
                if (Math.Round(planarFace.FaceNormal.Z) == 0)
                {
                    faceNormalZ = 0;
                }
                else
                {
                    faceNormalZ = Math.Round(planarFace.FaceNormal.Z) / Math.Abs(Math.Round(planarFace.FaceNormal.Z));
                }
                double vectorX;
                if (Math.Round(vector.X) == 0)
                {
                    vectorX = 0;
                }
                else
                {
                    vectorX = Math.Round(vector.X) / Math.Abs(Math.Round(vector.X));
                }
                double vectorY;
                if (Math.Round(vector.Y) == 0)
                {
                    vectorY = 0;
                }
                else
                {
                    vectorY = Math.Round(vector.Y) / Math.Abs(Math.Round(vector.Y));
                }
                double vectorZ;
                if (Math.Round(vector.Z) == 0)
                {
                    vectorZ = 0;
                }
                else
                {
                    vectorZ = Math.Round(vector.Z) / Math.Abs(Math.Round(vector.Z));
                }
                if (faceNormalX == vectorX && faceNormalY == vectorY && faceNormalZ == vectorZ)
                {
                    Face faceOffset = eachface as Face;
                    iFaces.Add(faceOffset);
                }
            }
            return iFaces[0];
        }
        #endregion
        #region Check Vector isParallel
        /// <summary>
        ///     Check Vector is parallel other Vector
        /// </summary>
        public static bool IsVectorParallel(XYZ vector1, XYZ vector2)
        {
            double vector1X;
            if (Math.Round(vector1.X) == 0)
            {
                vector1X = 0;
            }
            else
            {
                vector1X = Math.Round(vector1.X) / Math.Abs(Math.Round(vector1.X));
            }
            double vector1Y;
            if (Math.Round(vector1.Y) == 0)
            {
                vector1Y = 0;
            }
            else
            {
                vector1Y = Math.Round(vector1.Y) / Math.Abs(Math.Round(vector1.Y));
            }
            double vector1Z;
            if (Math.Round(vector1.Z) == 0)
            {
                vector1Z = 0;
            }
            else
            {
                vector1Z = Math.Round(vector1.Z) / Math.Abs(Math.Round(vector1.Z));
            }
            XYZ vector1Uv = new XYZ(vector1X, vector1Y, vector1Z);
            double vector2X;
            if (Math.Round(vector2.X) == 0)
            {
                vector2X = 0;
            }
            else
            {
                vector2X = Math.Round(vector2.X) / Math.Abs(Math.Round(vector2.X));
            }
            double vector2Y;
            if (Math.Round(vector2.Y) == 0)
            {
                vector2Y = 0;
            }
            else
            {
                vector2Y = Math.Round(vector2.Y) / Math.Abs(Math.Round(vector2.Y));
            }
            double vector2Z;
            if (Math.Round(vector2.Z) == 0)
            {
                vector2Z = 0;
            }
            else
            {
                vector2Z = Math.Round(vector2.Z) / Math.Abs(Math.Round(vector2.Z));
            }
            XYZ vector2Uv = new XYZ(vector2X, vector2Y, vector2Z);
            double round = Math.Round(vector1Uv.DotProduct(vector2Uv));
            if (round == 1 || round == -1)
            {
                return true;
            }
            return false;
        }
        #endregion
        #region Create Line Stirrup
        /// <summary>
        ///     Convert Line to XLine.
        /// </summary>
        public static List<Curve> CreateCurveFromFaceInterest(List<Face> listFaces, Face interFace)
        {
            List<Curve> listCurve = new List<Curve>();
            foreach (Face eFace in listFaces)
            {
                eFace.Intersect(interFace, out Curve inteCurve);
            }
            return listCurve;
        }
        #endregion
        #region CreateSolidFromBoundingBox
        public static Solid CreateSolidFromBoundingBox(BoundingBoxXYZ bbActiveView)
        {

            XYZ pt0 = new XYZ(bbActiveView.Min.X, bbActiveView.Min.Y, bbActiveView.Min.Z);
            XYZ pt1 = new XYZ(bbActiveView.Max.X, bbActiveView.Min.Y, bbActiveView.Min.Z);
            XYZ pt2 = new XYZ(bbActiveView.Max.X, bbActiveView.Max.Y, bbActiveView.Min.Z);
            XYZ pt3 = new XYZ(bbActiveView.Min.X, bbActiveView.Max.Y, bbActiveView.Min.Z);

            Line edge00 = Line.CreateBound(pt0, pt1);
            Line edge11 = Line.CreateBound(pt1, pt2);
            Line edge22 = Line.CreateBound(pt2, pt3);
            Line edge33 = Line.CreateBound(pt3, pt0);

            List<Curve> edges0 = new List<Curve>();
            edges0.Add(edge00);
            edges0.Add(edge11);
            edges0.Add(edge22);
            edges0.Add(edge33);

            CurveLoop baseLoop0 = CurveLoop.Create(edges0);
            List<CurveLoop> loopList0 = new List<CurveLoop>();
            loopList0.Add(baseLoop0);
            Solid preTransformSolid = GeometryCreationUtilities.CreateExtrusionGeometry(loopList0, XYZ.BasisZ, bbActiveView.Max.Z - bbActiveView.Min.Z);
            return preTransformSolid;
            //return preTransformSolid;

        }
        #endregion
        #region Get Intersect Line Between Solid and Solid in View
        public static List<Line> GetIntersectLineBetweenSolidAndElementInView(Solid solid, Element element, ViewPlan viewPlan)
        {
            // xem lại link này https://www.youtube.com/watch?v=VkH6QboUKkw
            List<Line> Listline = new List<Line>();
            List<Face> sideFace = DhhGeometryUtils.GetSideFaceFromSolid(solid);
            Solid SolidElement = DhhGeometryUtils.GetSolids(element);
            List<Face> TopFaceSolidelement = DhhGeometryUtils.GetTopFaceFromSolid(SolidElement);
            foreach (Face face in sideFace)
            {
                foreach (Face face1 in TopFaceSolidelement) 
                {
                    SetComparisonResult result = (SetComparisonResult)face1.Intersect(face, out Curve intersection);
                    if(result == SetComparisonResult.Disjoint)
                    {
                        continue;
                    }
                    Listline.Add(intersection as Line); 
                }
            }
            return Listline;
        }
        #endregion
    }
}