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
            IList<Element>  FoudationList_Selected = uiDocument.Selection.PickElementsByRectangle(new FoundationFilter(), "Chọn Móng:");
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
            using (Transaction transaction = new Transaction(Document, "Vẽ chi tiết móng"))
            {
                transaction.Start();
                foreach (FoundationInfor element in foundationInfor)
                {
                    ViewPlan viewPlan = ViewPlan.Create(Document, viewTypeId, element.Foundation.LevelId);
                    BoundingBoxXYZ bBoxFoun = element.Foundation.get_BoundingBox(viewPlan);
                    BoundingBoxXYZ bBoxView = new BoundingBoxXYZ();
                    bBoxView.Min = bBoxFoun.Min.Add(new XYZ(DhhUnitUtils.MmToFeet(-200), DhhUnitUtils.MmToFeet(-200), 0));
                    bBoxView.Max = bBoxFoun.Max.Add(new XYZ(DhhUnitUtils.MmToFeet(200), DhhUnitUtils.MmToFeet(200), 0));
                    viewPlan.CropBoxActive = true;
                    viewPlan.CropBoxVisible = false;
                    viewPlan.CropBox = bBoxView;
                    Outline outline = new Outline
                        (
                            new XYZ(bBoxView.Min.X, bBoxView.Min.Y, -100), 
                            new XYZ(bBoxView.Max.X, bBoxView.Max.Y, 100)
                        );
                    BoundingBoxIntersectsFilter filter = new BoundingBoxIntersectsFilter(outline);
                    BoundingBoxXYZ boundingBoxXYZ = new BoundingBoxXYZ();
                    boundingBoxXYZ.Min = outline.MinimumPoint;
                    boundingBoxXYZ.Max = outline.MaximumPoint;
                    Solid OutlineSolid = DhhGeometryUtils.CreateSolidFromBoundingBox(boundingBoxXYZ);
                    List<Element> beaminView = new FilteredElementCollector(Document, viewPlan.Id) 
                        .OfCategory(BuiltInCategory.OST_StructuralFraming)
                        .WherePasses(filter)
                        .Cast<Element>()
                        .ToList();
                    foreach (Element eBeam in beaminView)
                    {
                        List<Autodesk.Revit.DB.Line> intersectline = DhhGeometryUtils.GetIntersectLineBetweenSolidAndElementInView(OutlineSolid, eBeam, viewPlan); 
                        MessageBox.Show(intersectline.Count().ToString());
                    }    
                }
                transaction.Commit();
            }

            //using (Transaction tran = new Transaction(Document))
            //{
            //    tran.Start("Foundation Detail");
            //    ViewPlan viewPlan = ViewPlan.Create(Document, viewTypeId, foundationInfor.Foundation.LevelId);
            //    tran.Commit();
            //}
        }
    }
}
