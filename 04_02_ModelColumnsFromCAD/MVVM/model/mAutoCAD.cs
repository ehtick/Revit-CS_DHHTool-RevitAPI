using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _04_02_ModelColumnsFromCAD.MVVM.ViewModel;
using Application = Autodesk.Revit.ApplicationServices.Application;
using BIMSoftLib.MVVM;
using Autodesk.Revit.UI;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI.Selection;
using System.Windows.Controls;
using _04_02_ModelColumnsFromCAD.Object;
using System.Collections.ObjectModel;

namespace _04_02_ModelColumnsFromCAD.MVVM.Model
{
    public class mAutoCAD: PropertyChangedBase
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
        

        public ImportInstance SelectCADLink()
        {
            Reference r = uiDocument.Selection.PickObject(ObjectType.Element,new ImportInstanceSelectionFilter(), "CHỌN CAD LINK");
            ImportInstance SelectedCadLink = Document.GetElement(r) as ImportInstance;
            return SelectedCadLink;
        }
        public static ObservableRangeCollection<string> GetAllLayer(ImportInstance cadInstance)
        {
            ObservableRangeCollection<string> allLayers = new ObservableRangeCollection<string>();

            // Defaults to medium detail, no references and no view.
            Options option = new Options();
            option.IncludeNonVisibleObjects = true;
            option.ComputeReferences = true;
            option.DetailLevel = ViewDetailLevel.Fine;

            // option.View = cadInstance.Document.ActiveView;
            GeometryElement geoElement = cadInstance.get_Geometry(option);

            foreach (GeometryObject geoObject in geoElement)
            {
                if (geoObject is GeometryInstance)
                {
                    GeometryInstance geoInstance = geoObject as GeometryInstance;
                    GeometryElement geoElement2 = geoInstance.GetInstanceGeometry();

                    foreach (GeometryObject geoObject2 in geoElement2)
                    {
                        if (geoObject2 is Solid)
                        {
                            Solid solid = geoObject2 as Solid;
                            if (solid.Volume < 0.001) continue;

                            FaceArray faceArray = solid.Faces;
                            foreach (Face face in faceArray)
                            {
                                ElementId elementId = face.GraphicsStyleId;
                                GraphicsStyle graphicsStyle =
                                    cadInstance.Document.GetElement(elementId)
                                        as GraphicsStyle;
                                Category styleCategory = graphicsStyle.GraphicsStyleCategory;
                                allLayers.Add(styleCategory.Name);
                            }
                        }
                        else
                        {
                            ElementId elementId = geoObject2.GraphicsStyleId;
                            GraphicsStyle graphicsStyle =
                                cadInstance.Document.GetElement(elementId)
                                    as GraphicsStyle;
                            Category styleCategory = graphicsStyle.GraphicsStyleCategory;
                            allLayers.Add(styleCategory.Name);
                        }
                    }
                }
            }
            allLayers  = new ObservableRangeCollection<string>(allLayers.Distinct());
            return allLayers;
        }
    }
}
