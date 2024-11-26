using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Application = Autodesk.Revit.ApplicationServices.Application;
using BIMSoftLib.MVVM;
using System.IO;
using System.Windows.Controls;
using View = Autodesk.Revit.DB.View;
using System.Windows.Shapes;
using Autodesk.Revit.Creation;
using Document = Autodesk.Revit.DB.Document;
using Autodesk.Revit.UI.Selection;
using System.Windows;
using System.Xml.Linq;
using Autodesk.Revit.DB.Electrical;
using _06_04_RS2D_RebarSlab2D.MVVM.ViewModel;
using System.Collections.ObjectModel;
using static System.Net.WebRequestMethods;
using DHHTools;
using System;
using System.Windows.Media;
using _06_04_RS2D_RebarSlab2D.Object;


namespace _06_04_RS2D_RebarSlab2D.MVVM.Model
{
    public class mRebarSchedule2D : PropertyChangedBase
    {
        private Application _revitApp;
        public Application RevitApp
        {
            get
            {
                _revitApp = vmMainRebarSlab2DSchedule.RevitApp;
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
                _uiDocument = vmMainRebarSlab2DSchedule.RevitUIApp.ActiveUIDocument; return _uiDocument;
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

        public void RebarSchedule2D(ViewPlan viewPlan)
        {
            FamilySymbol fsymbol_RebarSlab = (FamilySymbol)new FilteredElementCollector(Document)
                .WhereElementIsElementType()
                .OfCategory(BuiltInCategory.OST_DetailComponents)
                .OfClass(typeof(FamilySymbol))
                .Cast<FamilySymbol>()
                .FirstOrDefault(s => s.Name.Contains("DHH_KC_ThepSan"));

            FamilySymbol fsymbol_RebarScheduleTitle = (FamilySymbol)new FilteredElementCollector(Document)
                .WhereElementIsElementType()
                .OfCategory(BuiltInCategory.OST_DetailComponents)
                .OfClass(typeof(FamilySymbol))
                .Cast<FamilySymbol>()
                .FirstOrDefault(s => s.Name.Contains("DHH_KC_DT_TKT-Title"));

            FamilySymbol fsymbol_RebarSchedule = (FamilySymbol)new FilteredElementCollector(Document)
                .WhereElementIsElementType()
                .OfCategory(BuiltInCategory.OST_DetailComponents)
                .OfClass(typeof(FamilySymbol))
                .Cast<FamilySymbol>()
                .FirstOrDefault(s => s.Name.Contains("DHH_KC_DetailItem_ThongKeThep"));
            FamilyInstanceFilter filter = new FamilyInstanceFilter(Document, fsymbol_RebarSlab.Id);
            FilteredElementCollector elementsFilter = new FilteredElementCollector(Document, viewPlan.Id);
            List<Element> familyInstances = elementsFilter.WherePasses(filter).ToElements().ToList();
            ViewType viewType = Document.ActiveView.ViewType;
            List<DetailItemInfor> Listelement = new List<DetailItemInfor>();
            foreach(Element element in familyInstances)
            {
                DetailItemInfor detailItemInfor = new DetailItemInfor(element as FamilyInstance);
                Listelement.Add(detailItemInfor);
            }
            List<DetailItemInfor> ListSort = Listelement.OrderBy(x => x.SoHieu, new NaturalStringComparer()).ToList();
            using (Transaction transaction = new Transaction(Document, "Thống kê thép sàn"))
            {
                transaction.Start();
                if (viewType != ViewType.DraftingView)
                {
                    MessageBox.Show("You must Active Draftting View");
                }
                else
                {
                    XYZ insPointStart = new XYZ(0, DhhUnitUtils.MmToFeet(12), 0);
                    FamilyInstance titleFamily = Document.Create.NewFamilyInstance(insPointStart, fsymbol_RebarScheduleTitle, Document.ActiveView);
                    for (int i = 0; i < Listelement.Count; i++)
                    {
                        FamilySymbol fsymbol_TK = (FamilySymbol)new FilteredElementCollector(Document)
                                .WhereElementIsElementType()
                                .OfCategory(BuiltInCategory.OST_GenericAnnotation)
                                .OfClass(typeof(FamilySymbol))
                                .Cast<FamilySymbol>()
                                .FirstOrDefault(s => s.Name.Contains(ListSort[i].HDThep));
                        XYZ insPointStartSection = new XYZ(0, DhhUnitUtils.MmToFeet(-i * 8), 0);
                        XYZ insPoint = insPointStartSection;
                        FamilyInstance sectionFamily = Document.Create.NewFamilyInstance(insPoint, fsymbol_RebarSchedule, Document.ActiveView);
                        Parameter HDKTpara = sectionFamily.LookupParameter("HDKT");
                        HDKTpara.Set(fsymbol_TK.Id);
                        Parameter SHpara = sectionFamily.LookupParameter("SH");
                        SHpara.Set(ListSort[i].SoHieu);
                        Parameter DKpara = sectionFamily.LookupParameter("DK");
                        DKpara.Set(ListSort[i].DuongKinh);
                        Parameter SLPara = sectionFamily.LookupParameter("SL");
                        SLPara.Set(ListSort[i].Soluong);
                        Parameter D1Para = sectionFamily.LookupParameter("D1");
                        D1Para.Set(ListSort[i].D1);
                    }
                }
                transaction.Commit();
            }
        }
    }
}
