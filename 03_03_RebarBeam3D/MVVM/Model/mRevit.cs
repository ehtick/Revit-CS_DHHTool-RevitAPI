using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application = Autodesk.Revit.ApplicationServices.Application;
using BIMSoftLib.MVVM;
using Autodesk.Revit.UI;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI.Selection;
using DHHTools.MVVM.ViewModel;
using System.Windows.Controls;
using System.Collections.ObjectModel;
using DHHTools.Object;
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
                OnPropertyChanged(nameof(_document));
            }
        }
        public Element SelectBeam()
        {
            Reference r = uiDocument.Selection.PickObject(ObjectType.Element, new BeamSelectionFilter(), "Chọn Dầm");
            Element SelectedBeam = Document.GetElement(r) as Element;
            return SelectedBeam;
        }
        public ObservableRangeCollection<Element> CheckIntersectElements(Element SelectedBeam)
        {
            ObservableRangeCollection<Element> elements = new ObservableRangeCollection<Element>();
            //var collector2 = new FilteredElementCollector(Document).OfCategory(BuiltInCategory.OST_StructuralColumns).OfClass(typeof(FamilyInstance));
            //var pipeIntersectFilter = new ElementIntersectsElementFilter(SelectedBeam);
            //IList<Element> result = collector2.WherePasses(pipeIntersectFilter).ToList().ConvertAll(x => x as Element);I
            IList<ElementId> result = (IList<ElementId>)JoinGeometryUtils.GetJoinedElements(Document, SelectedBeam);
            foreach (var elementid in result)
            {
                Element element = Document.GetElement(elementid);
                if(element.Category.Name == "Structural Columns")
                {
                    elements.Add(element);
                }    
            }
            return elements;
        }



    }
}
