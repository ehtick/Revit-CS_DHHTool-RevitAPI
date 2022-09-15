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
        private XYZ point1_VM;
        private XYZ point2_VM;
        //private string RV_X1Point_VM;
        //private string RV_Y1Point_VM;
        //private string RV_X2Point_VM;
        //private string RV_Y2Point_VM;
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
        public XYZ point1
        {
            get => point1_VM;
            set
            {
                if (point1_VM != value)
                {
                    point1_VM = value;
                    OnPropertyChanged("point1");
                }
            }
        }
        public XYZ point2
        {
            get => point2_VM;
            set
            {
                if (point2_VM != value)
                {
                    point2_VM = value;
                    OnPropertyChanged("point2");
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

            double vn_X1Point = Convert.ToDouble(sVN_X1Point);
            double vn_Y1Point = Convert.ToDouble(sVN_Y1Point);
            double vn_X2Point = Convert.ToDouble(sVN_X2Point);
            double vn_Y2Point = Convert.ToDouble(sVN_Y2Point);
            //double rv_X1Point = point1.X;
            //double rv_Y1Point = point1.Y;
            //double rv_X2Point = point2.X;
            //double rv_Y2Point = point2.Y;
            PileCoordinatesWindow pileCoordinateWindow = new PileCoordinatesWindow(this);
            pileCoordinateWindow.Show();
        }
        #endregion
        #region 04. Pick Point
        public void PickPoint1()
        {
            point1 = UiDoc.Selection.PickPoint("Chọn điểm gốc 1");
        }
        public void PickPoint2()
        {
            point2 = UiDoc.Selection.PickPoint("Chọn điểm gốc 2");
        }
        #endregion
        #region 05. Select Pile
        public void SelectPile()
        {
            allPile = new FilteredElementCollector(doc, doc.ActiveView.Id)
                .OfCategory(BuiltInCategory.OST_StructuralColumns)
                .ToElements()
                .ToList();
            uidoc.Selection.SetElementIds(allPile.Select(e => e.Id).ToList());
        }
        #endregion

    }
}



