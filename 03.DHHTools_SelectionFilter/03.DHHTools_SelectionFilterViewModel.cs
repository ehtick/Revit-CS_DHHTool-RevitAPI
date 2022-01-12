#region Namespaces
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Structure;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using System;
using System.Collections.Generic;
using System.Linq;
using Application = Autodesk.Revit.ApplicationServices.Application;
using Curve = Autodesk.Revit.DB.Curve;
using PlanarFace = Autodesk.Revit.DB.PlanarFace;
// ReSharper disable All
#endregion

namespace DHHTools
{
    public class SelectionFilterViewModel : ViewModelBase
    {
        #region 01. Private Property
        protected internal ExternalEvent ExEvent;
        private UIApplication uiapp;
        private UIDocument uidoc;
        private Application app;
        private Document doc;
        private string columnInformation_VM;
        private string RebarTypeNameMain_VM;
        private string RebarTypeNameStirrup_VM;
        private RebarBarType RebarTypeMain_VM;
        private RebarBarType RebarTypeStirrup_VM;
        private string hamLuongThep_VM;
        private double barDiaMain_VM;
        private double barDiaStrip_VM;
        private double SoluongH_VM;
        private double SoluongB_VM;
        private Parameter bPara_VM;
        private Parameter hPara_VM;
        private double b_VM;
        private double h_VM;
        private Element selectedElement_VM;
        private double LChan_VM;
        private double LGiua_VM;
        private double LDinh_VM;
        #endregion
        #region 02. Public Property
        public UIDocument UiDoc;
        public Document Doc;
        public string ColumnInformation
        {
            get
            {
                if (SelectedElement == null)
                {
                    return null;
                }
                return $"{bPara.AsValueString()}×{hPara.AsValueString()}" + "   " + SelectedElement.Id.ToString();

            }
            set
            {
                columnInformation_VM = value;
                OnPropertyChanged();
            }
        }
        public List<string> allRebarTypeName { get; set; }
        public string RebarTypeNameMain
        {
            get => RebarTypeNameMain_VM;
            set
            {
                if (RebarTypeNameMain_VM != value)
                {
                    RebarTypeNameMain_VM = value;
                    OnPropertyChanged("RebarTypeNameMain");
                    OnPropertyChanged("HamLuongThep");
                }
            }
        }
        public RebarBarType RebarTypeMain
        {
            get => DhhElementUtils.GetBarTypeByName(Doc, RebarTypeNameMain);
            set
            {
                if (RebarTypeMain_VM != value)
                {
                    RebarTypeMain_VM = value;
                    OnPropertyChanged("RebarTypeMain");
                    OnPropertyChanged("HamLuongThep");
                }
            }
        }
        public string RebarTypeNameStirrup
        {
            get => RebarTypeNameStirrup_VM;
            set
            {
                if (RebarTypeNameStirrup_VM != value)
                {
                    RebarTypeNameStirrup_VM = value;
                    OnPropertyChanged("RebarTypeNameStirrup");
                }
            }
        }
        public RebarBarType RebarTypeStirup
        {
            get => DhhElementUtils.GetBarTypeByName(Doc, RebarTypeNameStirrup);
            set
            {
                if (RebarTypeStirrup_VM != value)
                {
                    RebarTypeStirrup_VM = value;
                    OnPropertyChanged("RebarTypeStirrup");
                    OnPropertyChanged("HamLuongThep");
                }
            }
        }
        public string HamLuongThep
        {
            get
            {
                if (SelectedElement == null)
                {
                    return "0.00%";
                }
                return (Math.Round((SoluongH - 2 + SoluongB) * 2 * DhhUnitUtils.FeetToMm(RebarTypeMain.BarDiameter) / (b * h) * 100, 2)).ToString() + " %";
            }
            set
            {
                if (hamLuongThep_VM != value)
                {
                    hamLuongThep_VM = value;
                    OnPropertyChanged("HamLuongThep");
                }
            }
        }
        public double SoluongH
        {
            get => SoluongH_VM;
            set
            {
                if (SoluongH_VM != value)
                {
                    SoluongH_VM = value;
                    OnPropertyChanged("SoluongH");
                    OnPropertyChanged("HamLuongThep");
                }
            }
        }
        public double SoluongB
        {
            get => SoluongB_VM;
            set
            {
                if (SoluongB_VM != value)
                {
                    SoluongB_VM = value;
                    OnPropertyChanged("SoluongH");
                    OnPropertyChanged("HamLuongThep");
                }
            }
        }
        public double barDiaMain
        {
            get => Math.Round(DhhUnitUtils.FeetToMm(RebarTypeMain.BarDiameter));
            set
            {
                if (barDiaMain_VM != value)
                {
                    barDiaMain_VM = value;
                    OnPropertyChanged("barDiaMain");
                    OnPropertyChanged("HamLuongThep");
                }
            }
        }
        public double barDiaStirrup
        {
            get => Math.Round(DhhUnitUtils.FeetToMm(RebarTypeStirup.BarDiameter));
            set
            {
                if (barDiaStrip_VM != value)
                {
                    barDiaStrip_VM = value;
                    OnPropertyChanged("barDiaStirrup");
                }
            }
        }
        public double b
        {
            get
            {
                if (SelectedElement == null)
                {
                    return 0;
                }
                return Math.Round(DhhUnitUtils.FeetToMm(bPara.AsDouble()));
            }
            set
            {
                if (b_VM != value)
                {
                    b_VM = value;
                    OnPropertyChanged("b");
                    OnPropertyChanged("HamLuongThep");
                }
            }
        }
        public double h
        {
            get
            {
                if (SelectedElement == null)
                {
                    return 0;
                }
                return Math.Round(DhhUnitUtils.FeetToMm(hPara.AsDouble()));
            }
            set
            {
                if (h_VM != value)
                {
                    h_VM = value;
                    OnPropertyChanged("h");
                    OnPropertyChanged("HamLuongThep");
                }
            }
        }
        public Parameter bPara
        {
            get
            {
                if (SelectedElement == null)
                {
                    return null;
                }
                return (doc.GetElement(SelectedElement.GetTypeId()) as FamilySymbol).LookupParameter("b");
            }
            set
            {
                if (bPara_VM != value)
                {
                    bPara_VM = value;
                    OnPropertyChanged("bPara");
                    OnPropertyChanged("ColumnInformation");
                }
            }
        }
        public Parameter hPara
        {
            get
            {
                if (SelectedElement == null)
                {
                    return null;
                }
                return (doc.GetElement(SelectedElement.GetTypeId()) as FamilySymbol).LookupParameter("h");
            }
            set
            {
                if (hPara_VM != value)
                {
                    hPara_VM = value;
                    OnPropertyChanged("hPara");
                    OnPropertyChanged("ColumnInformation");
                }

            }
        }
        public Element SelectedElement
        {
            get => selectedElement_VM;
            set
            {
                if (selectedElement_VM != value)
                {
                    selectedElement_VM = value;
                    OnPropertyChanged("SelectedElement");
                    OnPropertyChanged("HamLuongThep");
                    OnPropertyChanged("ColumnInformation");
                }
            }
        }
        public double LChan
        {
            get => LChan_VM;
            set
            {
                if (LChan_VM != value)
                {
                    LChan_VM = value;
                    OnPropertyChanged("LChan");
                }
            }
        }
        public double LGiua
        {
            get => LGiua_VM;
            set
            {
                if (LGiua_VM != value)
                {
                    LGiua_VM = value;
                    OnPropertyChanged("LGiua");
                }
            }
        }
        public double LDinh
        {
            get => LDinh_VM;
            set
            {
                if (LDinh_VM != value)
                {
                    LDinh_VM = value;
                    OnPropertyChanged("LDinh");
                }
            }
        }
        #endregion
        #region 03. View Model
        public SelectionFilterViewModel(ExternalCommandData commandData)
        {
            uiapp = commandData.Application;
            uidoc = uiapp.ActiveUIDocument;
            app = uiapp.Application;
            doc = uidoc.Document;
            UiDoc = uidoc;
            Doc = UiDoc.Document;
            allRebarTypeName = new List<string>();
            ElementClassFilter rebarClassFilter = new ElementClassFilter(typeof(RebarBarType));
            FilteredElementCollector collector = new FilteredElementCollector(Doc);
            FilteredElementCollector rebarClass = collector.WherePasses(rebarClassFilter);
            foreach (Element rebarType in rebarClass)
            {
                allRebarTypeName.Add((rebarType as RebarBarType)?.Name);
            }
            allRebarTypeName.Sort();
            SoluongH = 2;
            SoluongB = 2;
            LChan = 1000;
            LGiua = 2000;
            LDinh = 1000;
            RebarTypeNameMain = allRebarTypeName[5];
            RebarTypeNameStirrup = allRebarTypeName[0];
            SelectionFilterWindow rebarColumnWindow = new SelectionFilterWindow(this);
            rebarColumnWindow.Show();
        }
        #endregion
        #region 04. Select Element
        public void SelectElementBtn()
        {
            Reference pickObject = UiDoc.Selection.PickObject(ObjectType.Element, "Chọn cột");
            SelectedElement = Doc.GetElement(pickObject);
        }
        #endregion
        #region 05. Main Rebar
        public void CreateMainRebar()
        {
            double lengthColumn = Math.Round(DhhUnitUtils.FeetToMm(SelectedElement.LookupParameter("Length").AsDouble()));
            Solid solidColumn = DhhGeometryUtils.GetSolids(SelectedElement);
            List<Face> faceSide = DhhGeometryUtils.GetSideFaceFromSolid(solidColumn);
            List<Face> facesBottom = DhhGeometryUtils.GetBottomFaceFromSolid(solidColumn);
            List<Face> hFaces = new List<Face>();
            Face hFace = null;
            List<Face> bFaces = new List<Face>();
            Face bFace = null;
            List<Line> listLines = new List<Line>();
            if (h == b)
            {
                hFace = faceSide[0];
                foreach (Face eFace in faceSide.GetRange(1, faceSide.Count - 1))
                {
                    if (DhhGeometryUtils.IsVectorParallel((hFace as PlanarFace).FaceNormal,
                            (eFace as PlanarFace).FaceNormal) == false)
                    {
                        bFaces.Add(eFace);
                    }
                }

                bFace = bFaces[0];
            }
            else
            {
                List<Face> faces = faceSide.GroupBy(ex => Math.Round(ex.Area)).Select(g => g.First()).ToList();
                foreach (Face eFace in faces)
                {
                    bool equals =
                        (Math.Round(DhhUnitUtils.SquareFeetToSquareMeter(eFace.Area) * 1000000) / lengthColumn).Equals(b);
                    if (equals == true)
                    {
                        bFaces.Add(eFace);
                    }
                    else
                    {
                        hFaces.Add(eFace);
                    }
                }

                bFace = bFaces[0];
                hFace = hFaces[0];
            }
            #region
            Face botFace = facesBottom.FirstOrDefault(x => Math.Abs(DhhUnitUtils.SquareFeetToSquareMeter(x.Area) - h * b * 0.000001) < 0.01);
            Face hFaceOffset = DhhGeometryUtils.FaceOffset(hFace, (hFace as PlanarFace).FaceNormal.Negate(),
                DhhUnitUtils.MmToFeet(DhhElementUtils.GetElementCover(Doc, SelectedElement) + barDiaMain / 2 + barDiaStirrup));
            Face bFaceOffset = DhhGeometryUtils.FaceOffset(bFace, (bFace as PlanarFace).FaceNormal.Negate(),
                DhhUnitUtils.MmToFeet(DhhElementUtils.GetElementCover(Doc, SelectedElement) + barDiaMain / 2 + barDiaStirrup));
            Face botFaceOffset = DhhGeometryUtils.FaceOffset(botFace, (botFace as PlanarFace).FaceNormal.Negate(),
                DhhUnitUtils.MmToFeet(DhhElementUtils.GetElementCover(Doc, SelectedElement) + barDiaStirrup / 2));
            IList<CurveLoop> botFaceCurveLoops = botFaceOffset.GetEdgesAsCurveLoops();
            FaceIntersectionFaceResult faceIntersectionFaceResult = botFaceOffset.Intersect(bFaceOffset, out Curve mainRebarCurve);
            RebarHookType hookType = null;
            RebarShape rebarShapeMain = new FilteredElementCollector(Doc)
                .OfClass(typeof(RebarShape))
                .Cast<RebarShape>()
                .First(x => x.Name == "t");
            RebarShape rebarShapeStirrup = new FilteredElementCollector(Doc)
                .OfClass(typeof(RebarShape))
                .Cast<RebarShape>()
                .First(x => x.Name == "d");
            List<Curve> listCurveMain = new List<Curve>();
            new CurveByPointsArray();
            double kcgiua2ThanhThep = DhhUnitUtils.MmToFeet(b - 2 * (barDiaMain / 2 + barDiaStirrup + DhhElementUtils.GetElementCover(Doc, SelectedElement)));
            listCurveMain.Add(mainRebarCurve);
            if (SoluongB <= 2)
            {
                Curve offset = mainRebarCurve.CreateOffset(kcgiua2ThanhThep, (bFaceOffset as PlanarFace).FaceNormal.Negate());
                listCurveMain.Add(offset);
            }
            else
            {
                for (int i = 1; i < SoluongB; i++)
                {
                    double kcOffset = kcgiua2ThanhThep / (SoluongB - 1) * i;
                    Curve offset = mainRebarCurve.CreateOffset(kcOffset, (bFaceOffset as PlanarFace).FaceNormal.Negate());
                    listCurveMain.Add(offset);
                }
            }
            RebarHookOrientation rebarHook = RebarHookOrientation.Left;
            #endregion
            using (Transaction trans = new Transaction(Doc, "Create Rebar Column"))
            {
                trans.Start();
                foreach (Curve eCurve in listCurveMain)
                {
                    List<Curve> MainCurves = new List<Curve>();
                    MainCurves.Add(eCurve);
                    Rebar rebarMain = Rebar.CreateFromCurvesAndShape(Doc, rebarShapeMain, RebarTypeMain,
                        hookType, hookType, SelectedElement, (bFaceOffset as PlanarFace).FaceNormal.Negate(), MainCurves, rebarHook, rebarHook);
                    Rebar.CreateFreeForm(Doc, RebarTypeStirup, SelectedElement, botFaceCurveLoops,
                        out RebarFreeFormValidationResult error);
                    RebarShapeDrivenAccessor drivenAccessor = rebarMain.GetShapeDrivenAccessor();
                    bool barsOnNormalSide = false;
                    bool includeFirstBar = true;
                    bool includeLastBar = true;
                    double arrayLength =
                        DhhUnitUtils.MmToFeet(h - 2 * (barDiaMain / 2 + barDiaStirrup + DhhElementUtils.GetElementCover(Doc, SelectedElement)));
                    int numberofBar = 0;
                    if (eCurve == listCurveMain.ElementAt(0) || eCurve == listCurveMain.ElementAt(listCurveMain.Count - 1))
                    {
                        numberofBar = Convert.ToInt32(SoluongH);
                    }
                    else
                    {
                        numberofBar = 2;
                    }
                    drivenAccessor.SetLayoutAsFixedNumber(numberofBar, arrayLength, barsOnNormalSide, includeFirstBar,
                        includeLastBar);
                    rebarMain.SetSolidInView(Doc.ActiveView as View3D, true);
                    rebarMain.SetUnobscuredInView(Doc.ActiveView as View3D, true);
                }
                trans.Commit();
            }
        }
        #endregion
        #region 06.Stirrup Rebar

        #endregion
    }
}



