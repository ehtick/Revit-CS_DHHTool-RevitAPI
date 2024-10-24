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


namespace _06_04_RS2D_RebarSlab2D.MVVM.Model
{
    public class mViewPlan : PropertyChangedBase
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


        private List<FamilyInstance> GetNestedInstances(FamilyInstance familyInstance)
        {
            List<FamilyInstance> nestedInstances = new List<FamilyInstance>();
            foreach (ElementId id in familyInstance.GetSubComponentIds())
            {
                Element subComponent = familyInstance.Document.GetElement(id);
                if (subComponent is FamilyInstance nestedInstance)
                {
                    nestedInstances.Add(nestedInstance);
                }
            }
            return nestedInstances;
        }
        public void RebarSchedule2D(ViewPlan viewPlan)
        {
            FamilySymbol familyType = new FilteredElementCollector(Document)
                .OfClass(typeof(FamilySymbol))
                .Cast<FamilySymbol>()
                .FirstOrDefault(fs => fs.Name.Equals("TK_TK2", StringComparison.InvariantCultureIgnoreCase));
            FamilySymbol fsymbol_RebarSlab = (FamilySymbol)new FilteredElementCollector(Document)
                .WhereElementIsElementType()
                .OfCategory(BuiltInCategory.OST_DetailComponents)
                .OfClass(typeof(FamilySymbol))
                .Cast<FamilySymbol>()
                .FirstOrDefault(s => s.Name.Contains("DHH_KC_ThepSan"));
            FamilySymbol fsymbol_RebarSchedule = (FamilySymbol)new FilteredElementCollector(Document)
                .WhereElementIsElementType()
                .OfCategory(BuiltInCategory.OST_DetailComponents)
                .OfClass(typeof(FamilySymbol))
                .Cast<FamilySymbol>()
                .FirstOrDefault(s => s.Name.Contains("DHH_KC_DetailItem_ThongKeThep"));

            FamilyInstanceFilter filter = new FamilyInstanceFilter(Document, fsymbol_RebarSlab.Id);
            FilteredElementCollector elementsFilter = new FilteredElementCollector(Document, viewPlan.Id);
            List<Element> familyInstances = elementsFilter.WherePasses(filter).ToElements().ToList();
            MessageBox.Show(familyInstances.Count.ToString());
            ViewType viewType = Document.ActiveView.ViewType;
            int scale = Document.ActiveView.Scale;
            using (Transaction transaction = new Transaction(Document, "Thống kê thép sàn"))
            {
                transaction.Start();
                if (viewType != ViewType.DraftingView)
                {
                    MessageBox.Show("You must Active Draftting View");
                }
                else
                {
                    for (int i = 0; i < familyInstances.Count; i++)
                    {
                        XYZ insPointStartSection = new XYZ(0, DhhUnitUtils.MmToFeet(i * 8 * scale), 0);
                        //XYZ insPointContSection = new XYZ(DhhUnitUtils.MmToFeet(B / 2), 0, 0);
                        XYZ insPoint = insPointStartSection; //+ insPointContSection;
                        FamilyInstance sectionFamily = Document.Create.NewFamilyInstance(insPoint, fsymbol_RebarSchedule, Document.ActiveView);

                        var nestedInstances = GetNestedInstances(sectionFamily);
                        IList<Subelement> subelements = sectionFamily.GetSubelements();

                        Parameter HDKTpara = sectionFamily.LookupParameter("HDKT");
                        HDKTpara.AsElementId();

                    }

                }
                transaction.Commit();
            }
        }
    }
}
