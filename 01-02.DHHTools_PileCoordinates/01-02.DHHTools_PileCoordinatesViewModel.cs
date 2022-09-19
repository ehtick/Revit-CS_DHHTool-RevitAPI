#region Namespaces
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Structure;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using Application = Autodesk.Revit.ApplicationServices.Application;
using Curve = Autodesk.Revit.DB.Curve;
using PlanarFace = Autodesk.Revit.DB.PlanarFace;
using Point = Autodesk.Revit.DB.Point;

// ReSharper disable All
#endregion

namespace DHHTools
{
    public class PileCoordinatesViewModel : ViewModelBase
    {
        #region 01. Private Property
        protected internal ExternalEvent ExEvent;
        private UIApplication uiapp;
        private UIDocument uidoc;
        private Application app;
        private Document doc;
        private string sVN_X1Point_VM;
        private string sVN_Y1Point_VM;
        private string sVN_X2Point_VM;
        private string sVN_Y2Point_VM;
        private double vn_X1Point_VM;
        private double vn_Y1Point_VM;
        private double vn_X2Point_VM;
        private double vn_Y2Point_VM;
        private XYZ pointpick1_VM;
        private XYZ pointpick2_VM;
        private XYZ vn_1Point_VM;
        private XYZ vn_2Point_VM;
        private XYZ vn_12Vecto_VM;

        #endregion
        #region 02. Public Property
        public UIDocument UiDoc;
        public Document Doc;
        public string sVN_X1Point
        {
            get => sVN_X1Point_VM;
            set
            {
                if (sVN_X1Point_VM != value)
                {
                    sVN_X1Point_VM = value;
                    OnPropertyChanged("sVN_X1Point");
                }
            }
        }
        public string sVN_Y1Point
        {
            get => sVN_Y1Point_VM;
            set
            {
                if (sVN_Y1Point_VM != value)
                {
                    sVN_Y1Point_VM = value;
                    OnPropertyChanged("sVN_Y1Point");
                }
            }
        }
        public string sVN_X2Point
        {
            get => sVN_X2Point_VM;
            set
            {
                if (sVN_X2Point_VM != value)
                {
                    sVN_X2Point_VM = value;
                    OnPropertyChanged("sVN_X2Point");
                }
            }
        }
        public string sVN_Y2Point
        {
            get => sVN_Y2Point_VM;
            set
            {
                if (sVN_Y2Point_VM != value)
                {
                    sVN_Y2Point_VM = value;
                    OnPropertyChanged("sVN_Y2Point");
                }
            }
        }
        public double vn_X1Point
        {
            get => DhhUnitUtils.MeterToFeet(Convert.ToDouble(sVN_X1Point));
            set
            {
                if (vn_X1Point_VM != value)
                {
                    vn_X1Point_VM = value;
                    OnPropertyChanged("vn_X1Point");
                    OnPropertyChanged("sVN_X1Point");
                }
            }
        }
        public double vn_Y1Point
        {
            get => DhhUnitUtils.MeterToFeet(Convert.ToDouble(sVN_Y1Point));
            set
            {
                if (vn_Y1Point_VM != value)
                {
                    vn_Y1Point_VM = value;
                    OnPropertyChanged("vn_Y1Point");
                    OnPropertyChanged("sVN_Y1Point");
                }
            }
        }
        public double vn_X2Point
        {
            get => DhhUnitUtils.MeterToFeet(Convert.ToDouble(sVN_X2Point));
            set
            {
                if (vn_X2Point_VM != value)
                {
                    vn_X2Point_VM = value;
                    OnPropertyChanged("vn_X2Point");
                    OnPropertyChanged("sVN_X2Point");
                }
            }
        }
        public double vn_Y2Point
        {
            get => DhhUnitUtils.MeterToFeet(Convert.ToDouble(sVN_Y2Point));
            set
            {
                if (vn_Y2Point_VM != value)
                {
                    vn_Y2Point_VM = value;
                    OnPropertyChanged("vn_Y2Point");
                    OnPropertyChanged("sVN_Y2Point");
                }
            }
        }
        public XYZ pointpick1
        {
            get => pointpick1_VM;
            set
            {
                if (pointpick1_VM != value)
                {
                    pointpick1_VM = value;
                    OnPropertyChanged("point1");
                }
            }
        }
        public XYZ pointpick2
        {
            get => pointpick2_VM;
            set
            {
                if (pointpick2_VM != value)
                {
                    pointpick2_VM = value;
                    OnPropertyChanged("pointpick2");
                }
            }
        }
        public XYZ vn_1Point
        {
            get => new XYZ(vn_X1Point, vn_Y1Point, 0);
            set
            {
                if (vn_1Point_VM != value)
                {
                    vn_1Point_VM = value;
                    OnPropertyChanged("vn_1Point");
                    OnPropertyChanged("vn_X1Point");
                    OnPropertyChanged("vn_Y1Point");
                }
            }
        }
        public XYZ vn_2Point
        {
            get => new XYZ(vn_X2Point, vn_Y2Point, 0);
            set
            {
                if (vn_2Point_VM != value)
                {
                    vn_2Point_VM = value;
                    OnPropertyChanged("vn_2Point");
                    OnPropertyChanged("vn_X2Point");
                    OnPropertyChanged("vn_Y2Point");
                }
            }
        }
        public XYZ vn_12Vecto
        {
            get => vn_2Point - vn_1Point;
            set
            {
                if (vn_12Vecto_VM != value)
                {
                    vn_12Vecto_VM = value;
                    OnPropertyChanged("vn_12Vecto");
                    OnPropertyChanged("vn_1Point");
                    OnPropertyChanged("vn_2Point");
                }
            }
        }

        public List<Element> allPile = new List<Element>();
        #endregion
        #region 03. View Model
        public PileCoordinatesViewModel(ExternalCommandData commandData)
        {
            uiapp = commandData.Application;
            uidoc = uiapp.ActiveUIDocument;
            app = uiapp.Application;
            doc = uidoc.Document;
            UiDoc = uidoc;
            Doc = UiDoc.Document;
            sVN_X1Point = "1148811.797";
            sVN_Y1Point = "575339.780";
            sVN_X2Point = "1148800.088";
            sVN_Y2Point = "575347.053";
            pointpick1 = new XYZ();
            pointpick2 = new XYZ();           
        }
        #endregion
        #region 04. Pick Point
        public void PickPoint1()
        {
            pointpick1 = UiDoc.Selection.PickPoint("Chọn điểm gốc 1");
        }
        public void PickPoint2()
        {
            pointpick2 = UiDoc.Selection.PickPoint("Chọn điểm gốc 2");
        }
        #endregion  
        #region 05. Select Pile
        public void SelectPile()
        {
            allPile = (List<Element>) uidoc.Selection.PickElementsByRectangle(new PileSelectionFilter(), "Chọn cọc");
        }
        #endregion
        #region 06. Get-Set Coordinates
        public void CoornPile()
        {
            XYZ translation = vn_1Point - pointpick1;
            Transform transformmove = Transform.CreateTranslation(translation);
            XYZ rv_1Point = transformmove.OfPoint(pointpick1);
            XYZ rv_2Point = transformmove.OfPoint(pointpick2);
            XYZ rv_12Vecto = rv_2Point - rv_1Point;
            //cần lấy góc quay giữa vecto Revit và Vecto VN2000
            double angle = vn_12Vecto.AngleTo(rv_12Vecto);
            double anglede = DhhUnitUtils.RadiansToDegrees(angle);
            Transform transformrotate = Transform.CreateRotationAtPoint(XYZ.BasisZ, angle, vn_1Point);
            foreach (Element onePile in allPile)
            {
                
                Parameter x_parameter = onePile.LookupParameter("X_Coordinates");
                Parameter y_parameter = onePile.LookupParameter("Y_Coordinates");
                LocationPoint location = onePile.Location as LocationPoint;
                XYZ pointPile = location.Point;
                XYZ tranpointPile = transformmove.OfPoint(pointPile);
                XYZ rotatePointPile = transformrotate.OfPoint(tranpointPile);
                x_parameter.Set((Math.Round(DhhUnitUtils.FeetToMeter(rotatePointPile.X), 3)).ToString());
                y_parameter.Set((Math.Round(DhhUnitUtils.FeetToMeter(rotatePointPile.Y), 3)).ToString());
                
            }
        }
        #endregion
    }
}



