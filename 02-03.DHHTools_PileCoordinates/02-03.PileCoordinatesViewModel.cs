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
    public class PileCoordinatesViewModel: ViewModelBase
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
        private XYZ rv_1PointPick_VM;
        private XYZ rv_2PointPick_VM;
        private double x_pointpick1_VM;
        private double y_pointpick1_VM;
        private double x_pointpick2_VM;
        private double y_pointpick2_VM;
        private XYZ vn_1Point_VM;
        private XYZ vn_2Point_VM;
        private XYZ vn_12Vecto_VM;
        private XYZ rv_12VectoPick_VM;
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
            get => Convert.ToDouble(sVN_X1Point)*1000;
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
            get => Convert.ToDouble(sVN_Y1Point) * 1000;
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
            get => Convert.ToDouble(sVN_X2Point) * 1000;
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
            get => Convert.ToDouble(sVN_Y2Point) * 1000;
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
        public double x_pointpick1
        {
            get => DhhUnitUtils.FeetToMm(pointpick1.X);
            set
            {
                if (x_pointpick1_VM != value)
                {
                    x_pointpick1_VM = value;
                    OnPropertyChanged("x_pointpick1");
                    OnPropertyChanged("pointpick1");
                }
            }
        }
        public double y_pointpick1
        {
            get => DhhUnitUtils.FeetToMm(pointpick1.Y);
            set
            {
                if (y_pointpick1_VM != value)
                {
                    y_pointpick1_VM = value;
                    OnPropertyChanged("y_pointpick1");
                    OnPropertyChanged("pointpick1");
                }
            }
        }
        public double x_pointpick2
        {
            get => DhhUnitUtils.FeetToMm(pointpick2.X);
            set
            {
                if (x_pointpick2_VM != value)
                {
                    x_pointpick2_VM = value;
                    OnPropertyChanged("x_pointpick2");
                    OnPropertyChanged("pointpick2");
                }
            }
        }
        public double y_pointpick2
        {
            get => DhhUnitUtils.FeetToMm(pointpick2.Y);
            set
            {
                if (y_pointpick2_VM != value)
                {
                    y_pointpick2_VM = value;
                    OnPropertyChanged("y_pointpick2");
                    OnPropertyChanged("pointpick2");
                }
            }
        }
        public XYZ vn_1Point
        {
            get => new XYZ(vn_Y1Point, vn_X1Point, 0);
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
            get => new XYZ(vn_Y2Point, vn_X2Point, 0);
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
        public XYZ rv_1PointPick
        {
            get => new XYZ(x_pointpick1, y_pointpick1, 0);
            set
            {
                if (rv_1PointPick_VM != value)
                {
                    rv_1PointPick_VM = value;
                    OnPropertyChanged("rv_1PointPick");
                    OnPropertyChanged("x_pointpick1");
                    OnPropertyChanged("y_pointpick1");
                }
            }
        }
        public XYZ rv_2PointPick
        {
            get => new XYZ(x_pointpick2, y_pointpick2, 0);
            set
            {
                if (rv_2PointPick_VM != value)
                {
                    rv_2PointPick_VM = value;
                    OnPropertyChanged("rv_2PointPick");
                    OnPropertyChanged("x_pointpick2");
                    OnPropertyChanged("y_pointpick2");
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
        public XYZ rv_12VectoPick
        {
            get => rv_2PointPick - rv_1PointPick;
            set
            {
                if (rv_12VectoPick_VM != value)
                {
                    rv_12VectoPick_VM = value;
                    OnPropertyChanged("rv_12VectoPick");
                    OnPropertyChanged("rv_2PointPick");
                    OnPropertyChanged("rv_1PointPick");
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
            XYZ translation = vn_1Point - rv_1PointPick;
            Transform transformmove = Transform.CreateTranslation(translation);
            XYZ rv_1Point = transformmove.OfPoint(rv_1PointPick);
            XYZ rv_2Point = transformmove.OfPoint(rv_2PointPick);
            XYZ rv_12Vecto = rv_2Point - rv_1Point;
            //cần lấy góc quay giữa vecto Revit và Vecto VN2000
            double vn_rv_angle = vn_12Vecto.AngleTo(rv_12Vecto);
            double vnAngleOX = vn_12Vecto.AngleTo(XYZ.BasisX);
            double vnAngleOY = vn_12Vecto.AngleTo(XYZ.BasisY);
            double rvAngleOX = rv_12Vecto.AngleTo(XYZ.BasisX);
            double rvAngleOY = rv_12Vecto.AngleTo(XYZ.BasisY);
            
            
            double angle = 0;
            if (vn_12Vecto.Y > 0 && rv_12Vecto.Y > 0)
            {
                if (vnAngleOX < rvAngleOX)
                { angle = -vn_rv_angle; }
                else if (vnAngleOX >= rvAngleOX)
                { angle = vn_rv_angle; }
            }
            else if (vn_12Vecto.Y < 0 && rv_12Vecto.Y < 0)
            {
                if (vnAngleOX < rvAngleOX)
                { angle = vn_rv_angle; }
                else if (vnAngleOX >= rvAngleOX)
                { angle = -vn_rv_angle; }
            }
            else if (vn_12Vecto.Y < 0 && rv_12Vecto.Y < 0)
             {
                if (vnAngleOX < rvAngleOX)
                { angle = -vn_rv_angle; }
                else if (vnAngleOX >= rvAngleOX)
                { angle = vn_rv_angle; }
            }
            else if (vn_12Vecto.Y < 0 && rv_12Vecto.Y < 0)
             {
                if (vnAngleOX < rvAngleOX)
                { angle = -vn_rv_angle; }
                else if (vnAngleOX >= rvAngleOX)
                { angle = vn_rv_angle; }
            }
            double anglede = DhhUnitUtils.RadiansToDegrees(angle);
            Transform transformrotate = Transform.CreateRotationAtPoint(XYZ.BasisZ, angle, vn_1Point);
            foreach (Element onePile in allPile)
            { 
                Parameter x_parameter = onePile.LookupParameter("X_Coordinates");
                Parameter y_parameter = onePile.LookupParameter("Y_Coordinates");
                LocationPoint location = onePile.Location as LocationPoint;
                XYZ pointPile = location.Point;
                double x_pointPile = DhhUnitUtils.FeetToMm(pointPile.X);
                double y_pointPile = DhhUnitUtils.FeetToMm(pointPile.Y);
                XYZ depointPile = new XYZ(x_pointPile, y_pointPile, 0);
                XYZ tranpointPile = transformmove.OfPoint(depointPile);
                XYZ rotatePointPile = transformrotate.OfPoint(tranpointPile);
                double x_roPointPile = Math.Round((rotatePointPile.X/1000), 3);
                double y_roPointPile = Math.Round((rotatePointPile.Y/1000), 3);
                x_parameter.Set(y_roPointPile.ToString());
                y_parameter.Set(x_roPointPile.ToString());
            }
        }
        #endregion
    }
}



