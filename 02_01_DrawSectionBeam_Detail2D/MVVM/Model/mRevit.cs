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
using System.Globalization;
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
        public static void CreateSectionBeam2D(Document document,ObservableRangeCollection<mSectionBeam> mSectionBeams, double B, double H)
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
                        #region Title Family
                        XYZ insPointStartTitle = new XYZ(DhhUnitUtils.MmToFeet(B) * i, DhhUnitUtils.MmToFeet(H+500), 0);
                        var listName = mSectionBeams.Where(x => x.BeamName == mSectionBeams[i].BeamName).ToList();
                        int countlist = listName.Count();
                        XYZ insPointContTitle = new XYZ(DhhUnitUtils.MmToFeet(B) * countlist / 2, 0, 0);
                        FamilyInstance titleFamily;
                        Parameter b_khungParameter;
                        if (i==0)
                        {
                           titleFamily = document.Create.NewFamilyInstance(insPointStartTitle + insPointContTitle, fTitle, document.ActiveView);
                           b_khungParameter = titleFamily.LookupParameter("b");
                           b_khungParameter.Set(DhhUnitUtils.MmToFeet(B * countlist));
                           Parameter beamName = titleFamily.LookupParameter("Beam_Name");
                            beamName.Set(mSectionBeams[i].BeamName);
                        }
                        else if (mSectionBeams[i].BeamName != mSectionBeams[i - 1].BeamName)
                        {
                            titleFamily = document.Create.NewFamilyInstance(insPointStartTitle + insPointContTitle, fTitle, document.ActiveView);
                            b_khungParameter = titleFamily.LookupParameter("b");
                            b_khungParameter.Set(DhhUnitUtils.MmToFeet(B * countlist));
                            Parameter beamName = titleFamily.LookupParameter("Beam_Name");
                            beamName.Set(mSectionBeams[i].BeamName);
                        }
                        #endregion
                        #region Section Family
                        XYZ insPointStartSection = new XYZ(DhhUnitUtils.MmToFeet(B) * i, DhhUnitUtils.MmToFeet(H/2 + 300), 0);
                        XYZ insPointContSection = new XYZ(DhhUnitUtils.MmToFeet(B/2), 0, 0);
                        FamilyInstance sectionFamily = document.Create.NewFamilyInstance(insPointStartSection + insPointContSection, fselement, document.ActiveView);
                        Parameter b_Frame = sectionFamily.LookupParameter("b_khung");
                        b_Frame.Set(DhhUnitUtils.MmToFeet(B));
                        Parameter h_Frame = sectionFamily.LookupParameter("h_khung");
                        h_Frame.Set(DhhUnitUtils.MmToFeet(H));
                        Parameter b_Section = sectionFamily.LookupParameter("b_Section");
                        double width = mSectionBeams[i].B;
                        b_Section.Set(DhhUnitUtils.MmToFeet(width));
                        Parameter h_Section = sectionFamily.LookupParameter("h_Section");
                        h_Section.Set(DhhUnitUtils.MmToFeet(mSectionBeams[i].H));
                        CreateDimensionBeam(document, sectionFamily);
                        #endregion
                    }


                }
                transaction.Commit();
            }


        }
        public static void CreateDimensionBeam(Document doc, FamilyInstance sectionBeam)
        {

                        List<Line> curvesb = new List<Line>();
                        List<Line> curvesh = new List<Line>();
                        ReferenceArray bRa = new ReferenceArray();
                        ReferenceArray hRa = new ReferenceArray();
                        //listElements.Add(ebeamDetail);
                        Parameter bPara = sectionBeam.LookupParameter("b_Section");
                        Parameter hPara = sectionBeam.LookupParameter("h_Section");
                        double b = Math.Round(DhhUnitUtils.FeetToMm(bPara.AsDouble()));
                        double h = Math.Round(DhhUnitUtils.FeetToMm(hPara.AsDouble()));
                        LocationPoint eLp = sectionBeam.Location as LocationPoint;
                        if (eLp != null)
                        {
                            XYZ eXyz = eLp.Point;
                            double xeXyz = Math.Round(DhhUnitUtils.FeetToMm(eXyz.X));
                            double yeXyz = Math.Round(DhhUnitUtils.FeetToMm(eXyz.Y));
                            double zeXyz = Math.Round(DhhUnitUtils.FeetToMm(eXyz.Z));
                            //str.Add(xeXyz.ToString(CultureInfo.InvariantCulture) + "," + yeXyz.ToString(CultureInfo.InvariantCulture) + "," + zeXyz.ToString(CultureInfo.InvariantCulture));
                            //strSection.Add(bPara.AsValueString() + "x" + hPara.AsValueString());
                            Options option = new Options();
                            option.ComputeReferences = true;
                            option.View = doc.ActiveView;
                            GeometryElement geoElement = sectionBeam.get_Geometry(option);
                            foreach (GeometryObject geoObject in geoElement)
                            {
                                if (geoObject is GeometryInstance geoInstance)
                                {
                                    GeometryElement geoElement2 = geoInstance.GetSymbolGeometry();
                                    foreach (GeometryObject geoObject2 in geoElement2)
                                    {
                                        if (geoObject2 is Curve curve)
                                        {
                                            if (curve.IsCyclic == false)
                                            {
                                                Line lineDtComponent = curve as Line;
                                                XYZ direction = lineDtComponent?.Direction;
                                                if (!(lineDtComponent is null))
                                                {
                                                    double lelineDtComponent =
                                                        Math.Round(DhhUnitUtils.FeetToMm(lineDtComponent.Length));
                                                    if (direction != null &&
                                                        (Math.Abs(Math.Abs(lelineDtComponent) - b) < 0.0001
                                                         && (Math.Abs(direction.AngleTo(XYZ.BasisX) -
                                                                      DhhUnitUtils.DegreesToRadians(0)) < 0.0001
                                                             || Math.Abs(direction.AngleTo(XYZ.BasisX) -
                                                                         DhhUnitUtils.DegreesToRadians(180)) < 0.0001)))
                                                    {
                                                        curvesb.Add(lineDtComponent);
                                                        bRa.Append(lineDtComponent.Reference);
                                                    }

                                                    if (direction != null &&
                                                        (Math.Abs(Math.Abs(lelineDtComponent) - h) < 0.0001
                                                         && (Math.Abs(direction.AngleTo(XYZ.BasisY) -
                                                                      DhhUnitUtils.DegreesToRadians(0)) < 0.0001
                                                             || Math.Abs(direction.AngleTo(XYZ.BasisY) -
                                                                         DhhUnitUtils.DegreesToRadians(180)) < 0.0001)))
                                                    {
                                                        curvesh.Add(lineDtComponent);
                                                        hRa.Append(lineDtComponent.Reference);
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }


                                Line lineb = Line.CreateBound(new XYZ(DhhUnitUtils.MmToFeet(xeXyz - b / 2 - 150), 0, 0),
                                    new XYZ(DhhUnitUtils.MmToFeet(xeXyz - b / 2 - 150), DhhUnitUtils.MmToFeet(200), 0));
                                Line lineh =
                                    Line.CreateBound(new XYZ(0, (DhhUnitUtils.MmToFeet(yeXyz - h / 2 - 150)), 0),
                                        new XYZ(DhhUnitUtils.MmToFeet(200), DhhUnitUtils.MmToFeet(yeXyz - h / 2 - 150),
                                            0));
                                doc.Create.NewDimension(doc.ActiveView, lineb, bRa);
                                doc.Create.NewDimension(doc.ActiveView, lineh, hRa);

                        }
                    
                }

            }
        
    
}
