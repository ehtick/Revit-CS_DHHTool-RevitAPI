#region Namespaces
//using System.Linq;
//using System.Windows.Documents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using static DHHTools.DhhUnitUtils;
using Application = Autodesk.Revit.ApplicationServices.Application;

// ReSharper disable All
#endregion


namespace DHHTools
{
    [Transaction(TransactionMode.Manual)]
    public class ViewPlanCreateRebarSlab2D : IExternalCommand
    {

        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Application app = uiapp.Application;
            Document doc = uidoc.Document;

            string phuongThep = "DirY";
            FamilySymbol fselement = (FamilySymbol)new FilteredElementCollector(doc)
               .WhereElementIsElementType()
               .OfCategory(BuiltInCategory.OST_DetailComponents)
               .OfClass(typeof(FamilySymbol))
               .Cast<FamilySymbol>()
               .FirstOrDefault(s => s.Name.Equals("DHH_KC_ThepSan_1/100"));
            XYZ p1 = uidoc.Selection.PickPoint("Point 1");
            XYZ p2 = uidoc.Selection.PickPoint("Point 2");
            XYZ p3 = uidoc.Selection.PickPoint("Point 3");
            XYZ p4 = uidoc.Selection.PickPoint("Point 4");
            Line lineX = Line.CreateUnbound(p1,XYZ.BasisX);
            Line lineY = Line.CreateUnbound(p1, XYZ.BasisY);
            double xp2 = p2.X;
            double yp2 = p2.Y;

            XYZ p2linethep = XYZ.Zero;
            XYZ pX2 = new XYZ(xp2, p1.Y, p1.Z);
            XYZ pY2 = new XYZ(p1.X, yp2, p1.Z);
            if(phuongThep=="DirX"){ p2linethep = pX2;}    else{ p2linethep = pY2;}    
            Line linethep = Line.CreateBound(p1, p2linethep);
            Line line2x = Line.CreateUnbound(p3, XYZ.BasisY);
            Line line3x = Line.CreateUnbound(p4, XYZ.BasisY);
            Line line2y = Line.CreateUnbound(p3, XYZ.BasisX);
            Line line3y = Line.CreateUnbound(p4, XYZ.BasisX);
            double phai = 0;
            double trai = 0;
            double distance = 0;
            if(phuongThep == "DirX")
            { 
                if (p3.Y >= p4.Y)
                {
                    phai = linethep.Distance(p3);
                    trai = linethep.Distance(p4);
                    distance = line2x.Distance(p1);
                }
                else
                {
                    phai = linethep.Distance(p4);
                    trai = linethep.Distance(p3);
                    distance = line3x.Distance(p1);
                }
            }
            else
            {
                if (p3.X <= p4.X)
                {
                    phai = linethep.Distance(p3);
                    trai = linethep.Distance(p4);
                    distance = line2y.Distance(p1);
                }
                else
                {
                    phai = linethep.Distance(p4);
                    trai = linethep.Distance(p3);
                    distance = line3y.Distance(p1);
                }
            }

            using (Transaction tran = new Transaction(doc))
            {
                tran.Start("Create Detail Rebar 2D");
                FamilyInstance newinstance = doc.Create.NewFamilyInstance(linethep, fselement, doc.ActiveView);
                Parameter phaiParameter = newinstance.LookupParameter("CR_RaiThep_Phai");
                Parameter traiParameter = newinstance.LookupParameter("CR_RaiThep_Trai");
                Parameter distanceParameter = newinstance.LookupParameter("KC_MuiTen.No1");
                phaiParameter.Set(phai);
                traiParameter.Set(trai);
                distanceParameter.Set(distance);
                tran.Commit();
            }
            //MessageBox.Show(fselement.Name.ToString());
            return Result.Succeeded;
        }
    }
}
