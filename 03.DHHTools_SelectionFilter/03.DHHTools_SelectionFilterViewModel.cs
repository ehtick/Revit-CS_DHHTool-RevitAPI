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
                UpdateAllElementSelection();
            }
        }
        public bool IsCurrentSelection
        {
            get => _isCurrentSelection;
            set
            {
                _isCurrentSelection = value;
                UpdateAllElementSelection();
            }
        }
        #endregion
        #region 03. View Model
        public SelectionFilterViewModel(ExternalCommandData commandData)
        {
            uiapp = commandData.Application;
            uidoc = uiapp.ActiveUIDocument;
            app = uiapp.Application;
            doc = uidoc.Document;
            UiDoc = uidoc;
            Doc = UiDoc.Document;

        }
        #endregion
    }
}



