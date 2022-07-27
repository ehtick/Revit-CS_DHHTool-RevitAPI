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
    public class CreateRebarSlab2DViewModel : ViewModelBase
    {
        #region 01. Private Property
        protected internal ExternalEvent ExEvent;
        private UIApplication uiapp;
        private UIDocument uidoc;
        private Application app;
        private Document doc;
        private Element selectedElement_VM;
        private bool IsThep2D_VM;
        private bool IsThep3D_VM;
        private bool IsThepLopTren_VM;
        private bool IsThepLopDuoi_VM;
        private double ValueDistanceTopBottom_VM;
        private double ValueSpacingTop_VM;
        private double ValueSpacingBot_VM;
        private bool IsThepPhanBo_VM;
        private bool IsThepGiaCuong_VM;
        private bool IsDirectionX_VM;
        private bool IsDirectionY_VM;
        private bool IsDirectionOther_VM;
        private bool IsGhiChu_VM;
        private string RebarTypeNameTop_VM;
        private string RebarTypeNameBot_VM;
        private RebarBarType RebarTypeTop_VM;
        private RebarBarType RebarTypeBot_VM;
        private double barDiaTop_VM;
        private double barDiaBot_VM;


        #endregion
        #region 02. Public Property
        public UIDocument UiDoc;
        public Document Doc;
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
        public List<string> AllRebarType { get; set; }
        public bool IsThep2D
        {
            get => IsThep2D_VM;
            set { if (IsThep2D_VM != value) { IsThep2D_VM = value; } }
        }
        public bool IsThep3D
        {
            get => IsThep3D_VM;
            set { if (IsThep3D_VM != value) { IsThep3D_VM = value; } }
        }
        public bool IsThepLopTren
        {
            get => IsThepLopTren_VM;
            set { if (IsThepLopTren_VM != value) { IsThepLopTren_VM = value; } }
        }
        public bool IsThepLopDuoi
        {
            get => IsThepLopDuoi_VM;
            set { if (IsThepLopDuoi_VM != value) { IsThepLopDuoi_VM = value; } }
        }
        public double ValueDistanceTopBottom
        {
            get => ValueDistanceTopBottom_VM;
            set { if (ValueDistanceTopBottom_VM != value) { ValueDistanceTopBottom_VM = value; } }
        }
        public double ValueSpacingTop
        {
            get => ValueSpacingTop_VM;
            set { if (ValueSpacingTop_VM != value) { ValueSpacingTop_VM = value; } }
        }
        public double ValueSpacingBot
        {
            get => ValueSpacingBot_VM;
            set { if (ValueSpacingBot_VM != value) { ValueSpacingBot_VM = value; } }
        }
        public bool IsThepPhanBo
        {
            get => IsThepPhanBo_VM;
            set { if (IsThepPhanBo_VM != value) { IsThepPhanBo_VM = value; } }
        }
        public bool IsThepGiaCuong
        {
            get => IsThepGiaCuong_VM;
            set { if (IsThepGiaCuong_VM != value) { IsThepGiaCuong_VM = value; } }
        }
        public bool IsDirectionX
        {
            get => IsDirectionX_VM;
            set { if (IsDirectionX_VM != value) { IsDirectionX_VM = value; } }
        }
        public bool IsDirectionY
        {
            get => IsDirectionY_VM;
            set { if (IsDirectionY_VM != value) { IsDirectionY_VM = value; } }
        }
        public bool IsDirectionOther
        {
            get => IsDirectionOther_VM;
            set { if (IsDirectionOther_VM != value) { IsDirectionOther_VM = value; } }
        }
        public bool IsGhiChu
        {
            get => IsGhiChu_VM;
            set { if (IsGhiChu_VM != value) { IsGhiChu_VM = value; } }
        }
        public string RebarTypeNameTop
        {
            get => RebarTypeNameTop_VM;
            set
            {
                if (RebarTypeNameTop_VM != value)
                {
                    RebarTypeNameTop_VM = value;
                    OnPropertyChanged("RebarTypeNameTop");
                }
            }
        }
        public RebarBarType RebarTypeTop
        {
            get => DhhElementUtils.GetBarTypeByName(Doc, RebarTypeNameTop);
            set
            {
                if (RebarTypeTop_VM != value)
                {
                    RebarTypeTop_VM = value;
                    OnPropertyChanged("RebarTypeTop");
                }
            }
        }
        public string RebarTypeNameBot
        {
            get => RebarTypeNameBot_VM;
            set
            {
                if (RebarTypeNameBot_VM != value)
                {
                    RebarTypeNameBot_VM = value;
                    OnPropertyChanged("RebarTypeNameBot");
                }
            }
        }
        public RebarBarType RebarTypeBot
        {
            get => DhhElementUtils.GetBarTypeByName(Doc, RebarTypeNameBot);
            set
            {
                if (RebarTypeBot_VM != value)
                {
                    RebarTypeBot_VM = value;
                    OnPropertyChanged("RebarTypeBot");
                }
            }
        }
        public double barDiaTop
        {
            get => Math.Round(DhhUnitUtils.FeetToMm(RebarTypeTop.BarDiameter));
            set
            {
                if (barDiaTop_VM != value)
                {
                    barDiaTop_VM = value;
                    OnPropertyChanged("barDiaTop");
                }
            }
        }
        public double barDiaBot
        {
            get => Math.Round(DhhUnitUtils.FeetToMm(RebarTypeBot.BarDiameter));
            set
            {
                if (barDiaBot_VM != value)
                {
                    barDiaBot_VM = value;
                    OnPropertyChanged("barDiaBot");
                }
            }
        }

        #endregion
        #region 03. View Model
        public CreateRebarSlab2DViewModel(ExternalCommandData commandData)
        {
            uiapp = commandData.Application;
            uidoc = uiapp.ActiveUIDocument;
            app = uiapp.Application;
            doc = uidoc.Document;
            UiDoc = uidoc;
            Doc = UiDoc.Document;
            AllRebarType = new List<string>();
            ElementClassFilter rebarClassFilter = new ElementClassFilter(typeof(RebarBarType));
            FilteredElementCollector collector = new FilteredElementCollector(Doc);
            FilteredElementCollector rebarClass = collector.WherePasses(rebarClassFilter);
            foreach (Element rebarType in rebarClass)
            {
                AllRebarType.Add((rebarType as RebarBarType)?.Name);
            }
            AllRebarType.Sort();
            RebarTypeNameTop = AllRebarType[5];
            RebarTypeNameBot = AllRebarType[0];
            IsThep2D = true;
            IsThep3D = true;
            IsThepLopTren = true;
            IsThepLopDuoi = true;
            ValueDistanceTopBottom = 150;
            ValueSpacingTop = 200;
            ValueSpacingBot = 200;
            IsThepPhanBo = true;
            IsThepGiaCuong = true;
            IsDirectionX = true;
            IsGhiChu = true;
            CreateRebarSlab2DWindow rebarSlab2DWindow = new CreateRebarSlab2DWindow(this);
            rebarSlab2DWindow.Show();
        }

        public CreateRebarSlab2DViewModel()
        {
        }
        #endregion
        #region 04. Select Element
        public void SelectElementBtn()
        {
            Reference pickObject = UiDoc.Selection.PickObject(ObjectType.Element,new FloorSelectionFilter(), "Chọn sàn");
            SelectedElement = Doc.GetElement(pickObject);
        }
        #endregion

        
        
    }
}



