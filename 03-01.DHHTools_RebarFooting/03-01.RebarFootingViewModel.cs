#region Namespaces
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Structure;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Controls;
using Application = Autodesk.Revit.ApplicationServices.Application;
using Curve = Autodesk.Revit.DB.Curve;
using PlanarFace = Autodesk.Revit.DB.PlanarFace;
using Rectangle = System.Windows.Shapes.Rectangle;
using System.Windows.Shapes;
using System.Windows.Media;
// ReSharper disable All
#endregion

namespace DHHTools
{
    public class RebarFootingViewModel : ViewModelBase
    {
        #region 01. Private Property
        protected internal ExternalEvent ExEvent;
        private UIApplication uiapp;
        private UIDocument uidoc;
        private Application app;
        private Document doc;
        private bool _IsEnableTopLayer;
        private bool _IsEnableMiddleLayer;
        private Element _SelectedElement;
        private double _DistanceBottom;
        private double _DistanceTop;
        private double _bFooting;
        private double _hFooting;
        private RebarBarType _RebarType;


        #endregion
        #region 02. Public Property
        public UIDocument UiDoc;
        public Document Doc;
        public bool IsEnableTopLayer
        {
            get => _IsEnableTopLayer;
            set
            {
                if (_IsEnableTopLayer != value)
                {
                    _IsEnableTopLayer = value;
                    OnPropertyChanged("IsEnableTopLayer");
                }
            }
        }
        public bool IsEnableMiddleLayer
        {
            get => _IsEnableMiddleLayer;
            set
            {
                if (_IsEnableMiddleLayer != value)
                {
                    _IsEnableMiddleLayer = value;
                    OnPropertyChanged("IsEnableMiddleLayer");
                }
            }
        }
        public double DistanceBottom
        {
            get => _DistanceBottom;
            set
            {
                _DistanceBottom = value;
                OnPropertyChanged("DistanceBottom");
            }
        }
        public double DistanceTop
        {
            get => _DistanceTop;
            set
            {
                _DistanceTop = value;
                OnPropertyChanged("DistanceTop");
            }
        }
        public double bFooting
        {
            get
            {
                if (SelectedElement == null)
                {
                    return 0;
                }
                return Math.Round(DhhUnitUtils.FeetToMm((doc.GetElement(SelectedElement.GetTypeId()) as FamilySymbol).LookupParameter("ChieuRong_Mong").AsDouble()));
            }
            set
            {
                _bFooting = value;
                OnPropertyChanged("bFooting");
            }
        }
        public double hFooting
        {
            get
            {
                if (SelectedElement == null)
                {
                    return 0;
                }
                return Math.Round(DhhUnitUtils.FeetToMm((doc.GetElement(SelectedElement.GetTypeId()) as FamilySymbol).LookupParameter("ChieuDai_Mong").AsDouble()));
            }
            set
            {
                _hFooting = value;
                OnPropertyChanged("hFooting");
            }
        }
        public ObservableCollection<RebarBarType> AllRebarBarTypes { get; set; } = new ObservableCollection<RebarBarType>();
        public Element SelectedElement
        {
            get => _SelectedElement;
            set
            {
                if (_SelectedElement != value)
                {
                    _SelectedElement = value;
                    OnPropertyChanged("SelectedElement");
                }
            }
        }


        #endregion
        #region 03. View Model
        public RebarFootingViewModel(ExternalCommandData commandData)
        {
            uiapp = commandData.Application;
            uidoc = uiapp.ActiveUIDocument;
            app = uiapp.Application;
            doc = uidoc.Document;
            UiDoc = uidoc;
            Doc = UiDoc.Document;
            #region Lấy tất cả các đường kính thép

            ElementClassFilter rebarClassFilter = new ElementClassFilter(typeof(RebarBarType));
            FilteredElementCollector collector = new FilteredElementCollector(Doc);
            FilteredElementCollector rebarClass = collector.WherePasses(rebarClassFilter);
            foreach (Element rebarType in rebarClass)
            {
                {
                    AllRebarBarTypes.Add((rebarType as RebarBarType));
                }
            }
            #endregion

        }
        #endregion
        #region 04. Select Element
        public void SelectElementBtn()
        {
            Reference pickObject = UiDoc.Selection.PickObject(ObjectType.Element, "Chọn móng");
            SelectedElement = Doc.GetElement(pickObject);
        }
        #endregion
        #region Draw Canvas
        public void DrawFootingPlanCanvas(Canvas viewCanvas, double scale)
        {
            if (bFooting != 0 && hFooting != 0)
            {
                Rectangle rectangle = new Rectangle
                {
                    Width = bFooting * scale,
                    Height = hFooting * scale,
                    StrokeThickness = 2,
                    Stroke = Brushes.Black,
                    
                };
                Rectangle rectangleLean = new Rectangle
                {
                    Width = (bFooting + 100) * scale,
                    Height = (hFooting + 100) * scale,
                    StrokeThickness = 1,
                    Stroke = Brushes.Black,
                    StrokeDashArray = new DoubleCollection() { 10 },
                   

                };
                Canvas.SetLeft(rectangle, viewCanvas.ActualWidth / 2 - bFooting * scale / 2);
                Canvas.SetTop(rectangle, viewCanvas.ActualHeight / 2 - hFooting * scale / 2);
                Canvas.SetLeft(rectangleLean, viewCanvas.ActualWidth / 2 - (bFooting+100) * scale / 2 );
                Canvas.SetTop(rectangleLean, viewCanvas.ActualHeight / 2 - (hFooting+100) * scale / 2 );

                viewCanvas.Children.Add(rectangle);
                viewCanvas.Children.Add(rectangleLean);
            }    

        }
        #endregion
    }
}



