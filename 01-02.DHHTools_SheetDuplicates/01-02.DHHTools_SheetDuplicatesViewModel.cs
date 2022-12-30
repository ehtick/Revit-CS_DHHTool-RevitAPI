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
    public class SheetDuplicatesViewModel : ViewModelBase
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

        public List<ViewSheet> AllViewsSheet { get; set; }
            = new List<ViewSheet>();
        public List<ViewSheet> SelectedViewsSheet{ get; set; }
            = new List<ViewSheet>();
        #endregion
        #region 03. View Model
        public SheetDuplicatesViewModel(ExternalCommandData commandData)
        {
            uiapp = commandData.Application;
            uidoc = uiapp.ActiveUIDocument;
            app = uiapp.Application;
            doc = uidoc.Document;
            UiDoc = uidoc;
            Doc = UiDoc.Document;
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



