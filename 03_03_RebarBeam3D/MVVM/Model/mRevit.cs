using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application = Autodesk.Revit.ApplicationServices.Application;
using BIMSoftLib.MVVM;
using Autodesk.Revit.UI;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI.Selection;
using DHHTools.MVVM.ViewModel;
using System.Windows.Controls;
using System.Collections.ObjectModel;
using DHHTools.Object;
using System.Xml.Linq;
using System.Net;
using DHHTools;


namespace DHHTools.MVVM.Model
{
    public class mRevit: PropertyChangedBase
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
                OnPropertyChanged(nameof(_document));
            }
        }
        public Element SelectBeam()
        {
            Reference r = uiDocument.Selection.PickObject(ObjectType.Element, new BeamSelectionFilter(), "Chọn Dầm");
            Element SelectedBeam = Document.GetElement(r) as Element;
            return SelectedBeam;
        }
        public ObservableRangeCollection<Element> CheckIntersectElements(Element SelectedBeam)
        {
            ObservableRangeCollection<Element> elements = new ObservableRangeCollection<Element>();
            List<Element> unorderList = new List<Element>();
            IList<ElementId> result = (IList<ElementId>)JoinGeometryUtils.GetJoinedElements(Document, SelectedBeam);
            foreach (var elementid in result)
            {
                Element element = Document.GetElement(elementid);
                if(element.Category.Name == "Structural Columns")
                {
                    unorderList.Add(element);
                }    
            }
            unorderList = unorderList.OrderBy(x => ((LocationPoint)x.Location).Point.X).ToList();
            for (int i=0; i < unorderList.Count; i++)
            {
                elements.Add(unorderList[i]);
            }    
            return elements;
        }
        public double CheckWidthColumn (Element SelectedBeam, Element Column)
        {
            double WidthColumn = 0;
            Parameter b_Para = null;
            Parameter h_Para = null;
            double b = 0;
            double h = 0;
            FamilyInstance familyInstance = Column as FamilyInstance;
            FamilySymbol familySymbol = familyInstance.Symbol;
            if (familySymbol != null)
            {
                b_Para = familySymbol.LookupParameter("b");
                h_Para = familySymbol.LookupParameter("h");
                b = b_Para.AsDouble();
                h = h_Para.AsDouble();
            }
            if (b == h) 
            {
                WidthColumn = Math.Round(DhhUnitUtils.FeetToMm(b)); 
            }
            else
            {
                XYZ directionBeam = null;
                List<PlanarFace> ParallelFace = new List<PlanarFace>();
                if (SelectedBeam != null)
                {
                    Location location = SelectedBeam.Location;
                    LocationCurve locationCurveBeam = location as LocationCurve;
                    Line curve = locationCurveBeam.Curve as Line;
                    directionBeam = curve.Direction;
                }
                Solid solid = DhhGeometryUtils.GetSolids(Column);
                List<Face> SideFace = new List<Face>();
                if (solid != null)
                {
                    SideFace = DhhGeometryUtils.GetSideFaceFromSolid(solid);
                }
                foreach (Face face in SideFace)
                {
                    if (face != null)
                    {
                        PlanarFace planarFace = face as PlanarFace;
                        XYZ faceNormal = planarFace.FaceNormal;
                        double v = Math.Round(faceNormal.DotProduct(directionBeam));
                        if (v == 0)
                        {
                            ParallelFace.Add(planarFace);
                        }

                    }
                }
                List<Curve> curves1 = new List<Curve>();
                foreach (PlanarFace plaface in ParallelFace)
                {
                    List<CurveLoop> list1 = plaface.GetEdgesAsCurveLoops().ToList();
                    
                    foreach (Curve c in list1[0])
                    {
                        Line line = c as Line;
                        bool check  = DhhGeometryUtils.IsVectorParallel(line.Direction, directionBeam);
                        if (check)
                        {
                            curves1.Add(c);
                        }    
                        
                    }
                   
                }
                foreach (Curve curve in curves1)
                {
                    Line lineWidth = curve as Line;
                    if (Math.Round(lineWidth.Length - b)==0)
                    {
                        WidthColumn = Math.Round(DhhUnitUtils.FeetToMm(b));
                        break;
                    }
                    else if (Math.Round(lineWidth.Length - h) == 0)
                    {
                        WidthColumn = Math.Round(DhhUnitUtils.FeetToMm(h));
                        break;
                    }
                }    
            }
            return WidthColumn;
        }

        public List<Solid> ListPathDetail(Element SelectedBeam, ObservableRangeCollection<Element> listColumn, 
                                                double SetTopBase, double SetLeftBase)
        {
            List<Solid> BeamSolid = new List<Solid>();
            //List<PathDetail> list = new List<PathDetail>();
            BeamSolid = DhhGeometryUtils.GetAllSolids(SelectedBeam);
            return BeamSolid;
        }

    }
}
