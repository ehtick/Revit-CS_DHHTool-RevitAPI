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
using DHHTools.Method;

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
            FamilySymbol fsymbol_BreakDetail = (FamilySymbol)new FilteredElementCollector(Document)
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
                    BoundingBoxXYZ bBoxFoun = element.Foundation.get_BoundingBox(viewPlan);
                    BoundingBoxXYZ bBoxView = new BoundingBoxXYZ();
                    bBoxView.Min = bBoxFoun.Min.Add(new XYZ(DhhUnitUtils.MmToFeet(-400), DhhUnitUtils.MmToFeet(-400), 0));
                    bBoxView.Max = bBoxFoun.Max.Add(new XYZ(DhhUnitUtils.MmToFeet(400), DhhUnitUtils.MmToFeet(400), 0));
                    viewPlan.CropBoxActive = true;
                    viewPlan.CropBoxVisible = false;
                    viewPlan.CropBox = bBoxView;
                    FoundationRebarPlanMethod.InsertBreakDetailItem(Document,viewPlan,fsymbol_BreakDetail,element);
                    FoundationRebarPlanMethod.InSertDimention(Document,viewPlan, element);
                    FoundationRebarPlanMethod.SetGridLine(Document,viewPlan, element);
                }
                transaction.Commit();

            }
        }
    }
}
