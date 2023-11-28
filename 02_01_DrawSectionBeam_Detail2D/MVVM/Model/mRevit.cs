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
using System.Windows;
using DHHTools;

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

        public static void CreateSectionBeam2D(Document document,ObservableRangeCollection<mSectionBeam> mSectionBeams)
        {   

            FamilySymbol fselement = (FamilySymbol)new FilteredElementCollector(document)
                        .WhereElementIsElementType()
                        .OfCategory(BuiltInCategory.OST_DetailComponents)
                        .OfClass(typeof(FamilySymbol))
                        .Cast<FamilySymbol>()
                        .FirstOrDefault(s => s.Name.Equals("ICIC_KC_ThepDam"));
            FamilySymbol fTitle = (FamilySymbol)new FilteredElementCollector(document)
                            .WhereElementIsElementType()
                            .OfCategory(BuiltInCategory.OST_DetailComponents)
                            .OfClass(typeof(FamilySymbol))
                            .Cast<FamilySymbol>()
                            .FirstOrDefault(s => s.Name.Equals("ICIC_KH_Title-TenDam"));
            
            ViewType viewType = document.ActiveView.ViewType;
            using (Transaction transaction = new Transaction(document))
            {
                transaction.Start("Create Detail Beam");
                if (viewType != ViewType.DraftingView)
                {
                    MessageBox.Show("You must Active Draftting View");
                }
                else
                {
                    for(int i = 0; i < mSectionBeams.Count; i++)
                    {
                        if(i== 0)
                        {
                            document.Create.NewFamilyInstance(XYZ.Zero, fTitle, document.ActiveView);
                        }
                        else if (mSectionBeams[i].BeamName != mSectionBeams[i-1].BeamName) 
                        {
                            XYZ insertPointX = new XYZ(DhhUnitUtils.MmToFeet(1000) * i, 0, 0);
                            document.Create.NewFamilyInstance(XYZ.Zero + insertPointX, fTitle, document.ActiveView);
                        }
                        
                    }


                }
                transaction.Commit();
            }


        }
    }
}
