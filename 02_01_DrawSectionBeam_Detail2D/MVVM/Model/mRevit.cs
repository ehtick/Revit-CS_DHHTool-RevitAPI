using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.DB.Mechanical;
using Autodesk.Revit.UI;
using Autodesk.Revit.DB;
using BIMSoftLib.MVVM;
using System.Windows.Controls;

namespace _02_01_DrawSectionBeam_Detail2D.MVVM.Model
{
    class mRevit: PropertyChangedBase
    {
        private UIApplication _application;
        public UIApplication Application
        {
            get { return _application; }
            set 
            { 
                _application = value;
                OnPropertyChanged(nameof(Application));
            }
        }
        private UIDocument _uiDocument;
        public UIDocument uiDocument
        { 
            get => _uiDocument; 
            set
            {
                _uiDocument = value;
               OnPropertyChanged(nameof(uiDocument));
            }
        }
        private Document _document;
        public Document Document
        {
            get => _document;
            set
            {
                _document = value;
                OnPropertyChanged(nameof(_document));
            }
        }

        public void CreateSectionBeam2D(Document document, ObservableRangeCollection<mSectionBeam> mSectionBeams)
        {
            using (Transaction tran = new Transaction(document))
            {
                tran.Start("Export DWF");

                tran.Commit();
            }    
        }
    }
}
