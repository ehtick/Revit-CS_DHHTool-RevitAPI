using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Application = Autodesk.Revit.ApplicationServices.Application;
using DHHTools.MVVM.ViewModel;
using BIMSoftLib.MVVM;
using System.IO;
using System.Windows.Controls;
using View = Autodesk.Revit.DB.View;
using System.Windows.Shapes;
using Autodesk.Revit.Creation;
using Document = Autodesk.Revit.DB.Document;
using Autodesk.Revit.UI.Selection;
using DHHTools.Object;
using System.Windows;
using System.Xml.Linq;
using Autodesk.Revit.DB.Electrical;

namespace DHHTools.MVVM.Model
{
    public class mRevit : PropertyChangedBase
    {
        private Application _revitApp;
        public Application RevitApp
        {
            get
            {
                _revitApp = vmMain.RevitAppService;
                return _revitApp;
            }
            set
            {
                _revitApp = value;
                OnPropertyChanged(nameof(RevitApp));
            }
        }
        private UIDocument _uiDocument;
        public UIDocument uiDocument
        {
            get
            {
                _uiDocument = vmMain.RevitApp.ActiveUIDocument; return _uiDocument;
            }
            set
            {
                _uiDocument = value;
                OnPropertyChanged(nameof(uiDocument));
            }
        }
        private Document _document;
        public Document Document
        {
            get
            {
                _document = uiDocument.Document;
                return _document;
            }
            set
            {
                _document = value;
                OnPropertyChanged(nameof(Document));
            }
        }
        private ObservableRangeCollection<FoundationInfor> _foudationList;
        public ObservableRangeCollection<FoundationInfor> FoundationList
        {
            get
            {
                _foudationList = vmMain.DcMain.DgFoundation;
                return _foudationList;
            }
            set
            {
                _foudationList = value;
                OnPropertyChanged(nameof(FoundationList));
            }
        }
        public ObservableRangeCollection<FoundationInfor> SelectFoundations(FamilySymbol familySymbol)
        {
            FoundationList.Clear();
            IList<Element> FoudationList_Selected = uiDocument.Selection.PickElementsByRectangle(new FoundationFilter(), "Chọn Móng:");
            IList<Element> Fou_Temp = FoudationList_Selected.Where(x => x.GetTypeId() == familySymbol.Id).ToList();
            IList<Element> Temp2 = Fou_Temp.GroupBy(t => t.LookupParameter("Mark").AsString()).Select(x => x.First()).ToList();
            IList<Element> Temp3 = Temp2.OrderBy(x => x.LookupParameter("Mark").AsString()).ToList();
            foreach (FamilyInstance element in Temp3)
            {
                FoundationInfor foundationInfor = new FoundationInfor(element);
                FoundationList.Add(foundationInfor);
            }
            return FoundationList;
        }

        public void FoundationDetail(ObservableRangeCollection<FoundationInfor> foundationInfor)
        {
            IEnumerable<ViewFamilyType> viewFamilyTypes = new FilteredElementCollector(Document)
                .OfClass(typeof(ViewFamilyType))
                .Cast<ViewFamilyType>()
                .Where(x => x.ViewFamily == ViewFamily.StructuralPlan);
            ElementId viewTypeId = viewFamilyTypes.Where(x => x.Name == "02.MB-ChiTiet-Mong").First().Id;
            FamilySymbol fselement = (FamilySymbol)new FilteredElementCollector(Document)
                .WhereElementIsElementType()
                .OfCategory(BuiltInCategory.OST_DetailComponents)
                .OfClass(typeof(FamilySymbol))
                .Cast<FamilySymbol>()
                .FirstOrDefault(s => s.Name.Equals("DHH_KH_KC_VetCat"));
            using (Transaction transaction = new Transaction(Document, "Vẽ chi tiết móng"))
            {
                transaction.Start();
                foreach (FoundationInfor element in foundationInfor)
                {
                    ViewPlan viewPlan = ViewPlan.Create(Document, viewTypeId, element.Foundation.LevelId);
                    List<XYZ> xYZs = new List<XYZ>();
                    BoundingBoxXYZ bBoxFoun = element.Foundation.get_BoundingBox(viewPlan);
                    BoundingBoxXYZ bBoxView = new BoundingBoxXYZ();
                    bBoxView.Min = bBoxFoun.Min.Add(new XYZ(DhhUnitUtils.MmToFeet(-200), DhhUnitUtils.MmToFeet(-200), 0));
                    bBoxView.Max = bBoxFoun.Max.Add(new XYZ(DhhUnitUtils.MmToFeet(200), DhhUnitUtils.MmToFeet(200), 0));
                    viewPlan.CropBoxActive = true;
                    viewPlan.CropBoxVisible = false;
                    viewPlan.CropBox = bBoxView;
                    Outline outline = new Outline
                        (
                            new XYZ(bBoxView.Min.X, bBoxView.Min.Y, -100),
                            new XYZ(bBoxView.Max.X, bBoxView.Max.Y, 100)
                        );
                    BoundingBoxIntersectsFilter filter = new BoundingBoxIntersectsFilter(outline);
                    BoundingBoxXYZ boundingBoxXYZ = new BoundingBoxXYZ();
                    boundingBoxXYZ.Min = outline.MinimumPoint;
                    boundingBoxXYZ.Max = outline.MaximumPoint;
                    Solid OutlineSolid = DhhGeometryUtils.CreateSolidFromBoundingBox(boundingBoxXYZ);
                    #region Lấy vị trí đặt break vào 100 so với View Boudary
                    BoundingBoxXYZ bBoxTemp = new BoundingBoxXYZ();
                    bBoxTemp.Min = bBoxFoun.Min.Add(new XYZ(DhhUnitUtils.MmToFeet(-100), DhhUnitUtils.MmToFeet(-100), 0));
                    bBoxTemp.Max = bBoxFoun.Max.Add(new XYZ(DhhUnitUtils.MmToFeet(100), DhhUnitUtils.MmToFeet(100), 0));
                    Outline outlineTemp = new Outline
                        (
                            new XYZ(bBoxTemp.Min.X, bBoxTemp.Min.Y, -100),
                            new XYZ(bBoxTemp.Max.X, bBoxTemp.Max.Y, 100)
                        );
                    BoundingBoxXYZ boundingBoxXYZTemp = new BoundingBoxXYZ();
                    boundingBoxXYZTemp.Min = outlineTemp.MinimumPoint;
                    boundingBoxXYZTemp.Max = outlineTemp.MaximumPoint;
                    Solid OutlineSolidTemp = DhhGeometryUtils.CreateSolidFromBoundingBox(boundingBoxXYZTemp);
                    List<Face> sideFace = DhhGeometryUtils.GetSideFaceFromSolid(OutlineSolidTemp);
                    List<Face> orderSideFace = new List<Face>();
                    List<Element> orderElement = new List<Element>();
                    #endregion
                    List<Element> beaminView = new FilteredElementCollector(Document, viewPlan.Id)
                       .OfCategory(BuiltInCategory.OST_StructuralFraming)
                       .WherePasses(filter)
                       .Cast<Element>()
                       .ToList();
                    #region Kiểm tra Dầm có cắt solid của view hay không
                    //Đoạn này chỉ dùng được cho dầm thẳng, dầm cong chưa biết làm.
                    foreach (Element eBeam in beaminView)
                    { 
                        LocationCurve locationCurve = eBeam.Location as LocationCurve;

                        Curve beamCurve = locationCurve.Curve;
                        Autodesk.Revit.DB.Line beamLocationLine = beamCurve as Autodesk.Revit.DB.Line;
                        XYZ startPoint = beamLocationLine.GetEndPoint(0);
                        XYZ endPoint = beamLocationLine.GetEndPoint(1);
                        XYZ MidPoint = (startPoint + endPoint) / 2;

                        Solid eBeamSolid = DhhGeometryUtils.GetSolids(eBeam);
                        BoundingBoxXYZ eBeamBBox = eBeam.get_BoundingBox(viewPlan);
                        XYZ xYZeBeam = eBeamSolid.ComputeCentroid();
                        ////Chuyển đường beamCurve về tâm của dầm để lấy điểm chèn giữa dầm)
                        Transform transform = Transform.CreateTranslation(xYZeBeam - MidPoint);
                        Curve beamCenterLine = beamLocationLine.CreateTransformed(transform);

                        foreach (Face eSideFace in sideFace)
                        {
                            SetComparisonResult intersects =  (SetComparisonResult)eSideFace.Intersect(beamCenterLine, out var intersections);
                            if (intersects == SetComparisonResult.Disjoint)
                            {
                                continue;
                            }
                            else
                            {
                                // Get the first intersection point
                                var intersection = intersections.Cast<IntersectionResult>().First();
                                var intersectionPoint = intersection.XYZPoint;
                                xYZs.Add(intersectionPoint);
                                orderSideFace.Add(eSideFace);
                                orderElement.Add(eBeam);
                            }  
                        }
                    }
                    #endregion

                    for (int i = 0; i< xYZs.Count; i++)
                    {
                        FamilyInstance titleFamily = Document.Create.NewFamilyInstance(new XYZ(xYZs[i].X, xYZs[i].Y, viewPlan.Origin.Z), fselement, viewPlan);
                        Autodesk.Revit.DB.Line axis = Autodesk.Revit.DB.Line.CreateUnbound(new XYZ(xYZs[i].X, xYZs[i].Y, viewPlan.Origin.Z), XYZ.BasisZ);
                        double angleRotation1 = 0;
                        double angleRotation2 = 0;
                        LocationCurve locationCurve = (LocationCurve)(orderElement[i].Location as Location);
                        Curve curve = locationCurve.Curve;
                        Autodesk.Revit.DB.Line lineBeam = curve as Autodesk.Revit.DB.Line;               
                        angleRotation1 = XYZ.BasisY.AngleOnPlaneTo((orderSideFace[i] as PlanarFace).FaceNormal, XYZ.BasisZ);
                        ElementTransformUtils.RotateElement(Document, titleFamily.Id, axis, angleRotation1);

                        angleRotation2 = lineBeam.Direction.AngleOnPlaneTo((orderSideFace[i] as PlanarFace).FaceNormal, XYZ.BasisZ);
                        ElementTransformUtils.RotateElement(Document, titleFamily.Id, axis, angleRotation2);
                    }   
                    
                }
                transaction.Commit();

            }
        }
    }
}
