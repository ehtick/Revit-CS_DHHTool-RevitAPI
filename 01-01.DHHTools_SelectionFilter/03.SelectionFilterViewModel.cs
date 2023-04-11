#region Namespaces
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Structure;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Documents;
using Application = Autodesk.Revit.ApplicationServices.Application;
using Curve = Autodesk.Revit.DB.Curve;
using PlanarFace = Autodesk.Revit.DB.PlanarFace;
// ReSharper disable All
#endregion

namespace DHHTools
{
    public class SelectionFilterViewModel : ViewModelBase
    {
        #region 01. Private Property
        protected internal ExternalEvent ExEvent;
        private UIApplication uiapp;
        private UIDocument uidoc;
        private Application app;
        private Document doc;
        private bool _isEntireProject;
        private bool _isCurrentSelection;
        #endregion
        #region 02. Public Property
        public UIDocument UiDoc;
        public Document Doc;
        public bool IsEntireProject
        {
            get => _isEntireProject;
            set
            {
                _isEntireProject = value;
                //UpdateAllElementSelection();
            }
        }
        public bool IsCurrentSelection
        {
            get => _isCurrentSelection;
            set
            {
                _isCurrentSelection = value;
                //UpdateAllElementSelection();
            }
        }
        public ObservableCollection<ElementExtension> AllElementSelection { get; set; }
            = new ObservableCollection<ElementExtension>();
        public List<Element> SeElements = new List<Element>();
        #endregion
        #region 03. View Model
        public SelectionFilterViewModel(ExternalCommandData commandData)
        {
            // Lưu trữ data từ Revit vào 2 Field Doc, UiDoc
            uiapp = commandData.Application;
            uidoc = uiapp.ActiveUIDocument;
            app = uiapp.Application;
            doc = uidoc.Document;
            UiDoc = uidoc;
            Doc = UiDoc.Document;
            //Khởi tạo data cho WPF
            if (UiDoc.Selection.GetElementIds().Count>0)
            {
                List<ElementId> elementIds = UiDoc.Selection.GetElementIds().ToList();
                foreach (var elementId in elementIds)
                {
                    Element e = doc.GetElement(elementId);
                    SeElements.Add(e);
                }
            }
            if (!SeElements.Any())
            {
                IsEntireProject = true;
            }
            else
            {
                IsCurrentSelection = true;
            }
        }
        #endregion
        #region 04. Update Element
        public void UpdateAllElementExtensions()
        {
            ElementExtension level1 = new ElementExtension("All");
            //List<Category> categories = DhhElementUtils.GetAllCategory(Doc, IsCurrentSelection, SeElements);

            //foreach (ViewType viewType in viewTypes)
            //{
            //    List<View> allViewsWithViewType
            //        = ViewUtils.GetAllViewsWithViewType(Doc,
            //            viewType,
            //            IsCurrentSelection, SelectedViews);

            //    if (!allViewsWithViewType.Any()) continue;

            //    ViewExtension level2 = new ViewExtension(viewType);

            //    level1.ViewItems.Add(level2);

            //    foreach (var view in allViewsWithViewType)
            //    {
            //        ViewExtension level3
            //            = new ViewExtension(view);
            //        level2.ViewItems.Add(level3);
            //    }
            //}

            //AllViewsExtension = new ObservableCollection<ViewExtension>();
            //AllViewsExtension.Add(level1);

            //OnPropertyChanged("AllViewsExtension");
        }
        #endregion
    }
}



