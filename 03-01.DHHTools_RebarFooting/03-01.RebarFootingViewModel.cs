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
using DHHTools.Library;
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
        private double _bPedestal;
        private double _hPedestal;
        private double _bDifferent;
        private double _hDifferent;
        private RebarBarType _DiameterXBottom;
        private RebarBarType _DiameterYBottom;
        private RebarBarType _DiameterXTop;
        private RebarBarType _DiameterYTop;

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
                return Math.Round(DhhUnitUtils.FeetToMm((doc.GetElement(SelectedElement.GetTypeId()) as FamilySymbol).LookupParameter("Width").AsDouble()));
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
                return Math.Round(DhhUnitUtils.FeetToMm((doc.GetElement(SelectedElement.GetTypeId()) as FamilySymbol).LookupParameter("Length").AsDouble()));
            }
            set
            {
                _hFooting = value;
                OnPropertyChanged("hFooting");
            }
        }
        public double bPedestal
        {
            get
            {
                if (SelectedElement == null)
                {
                    return 0;
                }
                return Math.Round(DhhUnitUtils.FeetToMm((doc.GetElement(SelectedElement.GetTypeId()) as FamilySymbol).LookupParameter("Co_Cot_RongTD").AsDouble()));
            }
            set
            {
                _bPedestal = value;
                OnPropertyChanged("bPedestal");
            }
        }
        public double hPedestal
        {
            get
            {
                if (SelectedElement == null)
                {
                    return 0;
                }
                return Math.Round(DhhUnitUtils.FeetToMm((doc.GetElement(SelectedElement.GetTypeId()) as FamilySymbol).LookupParameter("Co_Cot_DaiTD").AsDouble()));
            }
            set
            {
                _hPedestal = value;
                OnPropertyChanged("hPedestal");
            }
        }
        public double bDifferent
        {
            get
            {
                if (SelectedElement == null)
                {
                    return 0;
                }
                return Math.Round(DhhUnitUtils.FeetToMm((doc.GetElement(SelectedElement.GetTypeId()) as FamilySymbol).LookupParameter("Lech_Phuong_Rong").AsDouble()));
            }
            set
            {
                _bDifferent = value;
                OnPropertyChanged("bDifferent");
            }
        }
        public double hDifferent
        {
            get
            {
                if (SelectedElement == null)
                {
                    return 0;
                }
                return Math.Round(DhhUnitUtils.FeetToMm((doc.GetElement(SelectedElement.GetTypeId()) as FamilySymbol).LookupParameter("Lech_Phuong_Dai").AsDouble()));
            }
            set
            {
                _hDifferent = value;
                OnPropertyChanged("hDifferent");
            }
        }
        public RebarBarType DiameterXBottom
        {
            get => _DiameterXBottom;
            set
            {
                if (_DiameterXBottom != value)
                {
                    _DiameterXBottom = value;
                    OnPropertyChanged("DiameterXBottom");
                }
            }
        }
        public RebarBarType DiameterYBottom
        {
            get => _DiameterYBottom;
            set
            {
                if (_DiameterYBottom != value)
                {
                    _DiameterYBottom = value;
                    OnPropertyChanged("DiameterYBottom");
                }
            }
        }
        public RebarBarType DiameterXTop
        {
            get => _DiameterXTop;
            set
            {
                if (_DiameterXTop != value)
                {
                    _DiameterXTop = value;
                    OnPropertyChanged("DiameterXTop");
                }
            }
        }
        public RebarBarType DiameterYTop
        {
            get => _DiameterYTop;
            set
            {
                if (_DiameterYTop != value)
                {
                    _DiameterYTop = value;
                    OnPropertyChanged("DiameterYTop");
                }
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
        public double bColumn;
        public double hColumn;
        public double bColumnLeft;
        public double hColumnTop;
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

            DiameterXBottom = AllRebarBarTypes[3];
            DiameterYBottom = AllRebarBarTypes[3];
            DiameterXTop = AllRebarBarTypes[3];
            DiameterYTop = AllRebarBarTypes[3];
        }
        #endregion
        #region 04. Select Element
        public void SelectElementBtn()
        {
            Reference pickObject = UiDoc.Selection.PickObject(ObjectType.Element, "Chọn móng");
            SelectedElement = Doc.GetElement(pickObject);
        }
        #endregion
        #region 05. Draw Canvas
        public void DrawFootingPlanCanvas(Canvas viewCanvas, double scale)
        {
            if (bFooting != 0 && hFooting != 0)
            {
                double topFoot = viewCanvas.ActualHeight / 4 - hFooting * scale / 2;
                Rectangle recFoot = new Rectangle
                {
                    Width = bFooting * scale,
                    Height = hFooting * scale,
                    StrokeThickness = 1,
                    Stroke = Brushes.Black,
                    
                };
                Rectangle recLean = new Rectangle
                {
                    Width = (bFooting + 100) * scale,
                    Height = (hFooting + 100) * scale,
                    StrokeThickness = 0.5,
                    Stroke = Brushes.Black,
                    StrokeDashArray = new DoubleCollection() { 20 },
                };
                bool facingFlipped = (SelectedElement as FamilyInstance).FacingFlipped;
                bool handingFlipped = (SelectedElement as FamilyInstance).HandFlipped;
                if (Math.Round(bDifferent) <= Math.Round(bPedestal/2)) 
                {
                    bColumn = bPedestal - 50;
                    bColumnLeft = viewCanvas.ActualWidth / 2 - bColumn * scale / 2 - (bFooting / 2 - bDifferent + 25)*scale;
                }
                else 
                { 
                    bColumn = bPedestal - 100;
                    bColumnLeft = viewCanvas.ActualWidth / 2 - bColumn * scale / 2 - (bFooting / 2 - bDifferent) * scale;
                }
                if (Math.Round(hDifferent) <= Math.Round(hPedestal/2)) 
                { 
                    hColumn = hPedestal - 50;
                    hColumnTop = viewCanvas.ActualHeight / 4 - hColumn * scale / 2 - (hFooting / 2 - hDifferent + 25)*scale;
                }
                else 
                { 
                    hColumn = hPedestal - 100;
                    hColumnTop = viewCanvas.ActualHeight / 4 - hColumn * scale / 2 - (hFooting / 2 - hDifferent) * scale;
                }
                Rectangle recColumn = new Rectangle
                {
                    Width = bColumn * scale,
                    Height = hColumn * scale,
                    StrokeThickness = 2,
                    Stroke = Brushes.Black,
                    Fill = Brushes.LightGray,
                };
                
                PathGeometry pthGeometry = RebarFootingLibrary
                    .GetSingleFootingPath(bFooting*scale, hFooting * scale, bPedestal * scale, hPedestal * scale, bDifferent * scale, hDifferent * scale);
                Path footPath = RebarFootingLibrary.GetFootPathFromGeometry(pthGeometry,SelectedElement,bFooting*scale,hFooting*scale);
                footPath.Stroke = new SolidColorBrush(Colors.Black);
                footPath.StrokeThickness = 1;

                Canvas.SetLeft(recLean, viewCanvas.ActualWidth / 2 - (bFooting + 100) * scale / 2);
                Canvas.SetTop(recLean, viewCanvas.ActualHeight / 4 - (hFooting + 100) * scale / 2);
                Canvas.SetLeft(recFoot, viewCanvas.ActualWidth / 2 - bFooting * scale / 2);
                Canvas.SetTop(recFoot, viewCanvas.ActualHeight / 4 - hFooting * scale / 2);
                Canvas.SetLeft(footPath, viewCanvas.ActualWidth / 2 - bFooting * scale / 2);
                Canvas.SetTop(footPath, viewCanvas.ActualHeight / 4 - hFooting * scale / 2);

                if (handingFlipped == false)
                { Canvas.SetLeft(recColumn, bColumnLeft );}
                else if (handingFlipped == true)
                { Canvas.SetRight(recColumn, bColumnLeft); }

                double tranfer = hFooting*scale - hColumn*scale - 2 * ( hColumnTop - topFoot);
                if (facingFlipped == false)
                { Canvas.SetTop(recColumn, hColumnTop); }
                else if (facingFlipped == true)
                { Canvas.SetTop(recColumn, hColumnTop + tranfer); }
                
                viewCanvas.Children.Add(recLean);
                viewCanvas.Children.Add(recFoot);
                viewCanvas.Children.Add(footPath);
                viewCanvas.Children.Add(recColumn);
                
            }    

        }
        #endregion
    }
}



