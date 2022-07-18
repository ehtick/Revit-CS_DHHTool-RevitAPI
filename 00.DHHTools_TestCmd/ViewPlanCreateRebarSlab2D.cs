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
           
           
            FamilySymbol fselement = (FamilySymbol)new FilteredElementCollector(doc)
               .WhereElementIsElementType()
               .OfCategory(BuiltInCategory.OST_DetailComponents)
               .OfClass(typeof(FamilySymbol))
               .Cast<FamilySymbol>()
               .FirstOrDefault(s => s.Name.Equals("DHH_KC_ThepSan_1/100"));
            XYZ p1 = uidoc.Selection.PickPoint(ObjectSnapTypes.Nearest,"Point 1");
            XYZ p2 = uidoc.Selection.PickPoint(ObjectSnapTypes.Nearest, "Point 2");
            XYZ p3 = uidoc.Selection.PickPoint(ObjectSnapTypes.Nearest, "Point 3");
            XYZ p4 = uidoc.Selection.PickPoint(ObjectSnapTypes.Nearest, "Point 4");
            Line lineX = Line.CreateUnbound(p1,XYZ.BasisX);
            double xp2 = p2.X;
            XYZ pX2 = new XYZ(xp2, p1.Y, p1.Z);
            Line line1 = Line.CreateBound(p1, pX2);
            Line line2 = Line.CreateUnbound(p3, XYZ.BasisY);
            double phai = line1.Distance(p3);
            double trai = line1.Distance(p4);
            double distance = line2.Distance(p1);

            using (Transaction tran = new Transaction(doc))
            {
                tran.Start("Create Detail Rebar 2D");
                FamilyInstance newinstance = doc.Create.NewFamilyInstance(line1, fselement, doc.ActiveView);
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
