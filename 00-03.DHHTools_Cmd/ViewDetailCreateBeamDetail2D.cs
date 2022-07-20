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
    public class ViewDetailCreateBeamDetail2D : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Application app = uiapp.Application;
            Document doc = uidoc.Document;
            List<double> blist = new List<double>() { 500, 600, 700, 800, 600, 900 };
            List<double> hlist = new List<double>() { 600, 750, 900, 1000, 700, 1200 };
            List<int> slTop1List = new List<int>() {5, 4, 5, 7, 6, 9};
            List<int> slBot1List = new List<int>() {5, 4, 5, 7, 6, 9};
            List<int> slTop2List = new List<int>() { 2, 0, 0, 0, 3, 5 };
            List<int> slBot2List = new List<int>() { 0, 0, 0, 0, 2, 2 };
            List<int> slnhanhdaiList = new List<int>() { 2, 3, 4, 4, 3, 4 };

            double bkhung = blist[0];
            double hkhung = hlist[0];
            for (int i =0; i<blist.Count; i++)
            {
                if (bkhung < blist[i])
                    bkhung = blist[i];
                if (hkhung < hlist[i])
                    hkhung = hlist[i];
            }
            bkhung = bkhung + 700;
            hkhung = hkhung + 700;
           
            FamilySymbol fselement = (FamilySymbol)new FilteredElementCollector(doc)
               .WhereElementIsElementType()
               .OfCategory(BuiltInCategory.OST_DetailComponents)
               .OfClass(typeof(FamilySymbol))
               .Cast<FamilySymbol>()
               .FirstOrDefault(s => s.Name.Equals("ICIC_KC_ThepDamV2"));
            using (Transaction tran = new Transaction(doc))
            {
                tran.Start("Create Detail Beam 2D");
                for(int i = 0; i<blist.Count;i++)
                {
                    XYZ xyz  = XYZ.Zero;
                    XYZ insertPointX = new XYZ(DhhUnitUtils.MmToFeet(bkhung)*i, 0, 0);
                    XYZ insertPointXmm = new XYZ(bkhung * i, 0, 0);
                    xyz = XYZ.Zero.Add(insertPointX);
                    FamilyInstance newinstance = doc.Create.NewFamilyInstance(xyz, fselement, doc.ActiveView);

                    Parameter b_khungParameter = newinstance.LookupParameter("b_khung");
                    Parameter h_khungParameter = newinstance.LookupParameter("h_khung");
                    b_khungParameter.Set(DhhUnitUtils.MmToFeet(bkhung));
                    h_khungParameter.Set(DhhUnitUtils.MmToFeet(hkhung));

                    Parameter bParameter = newinstance.LookupParameter("b");
                    Parameter hParameter = newinstance.LookupParameter("h");
                    bParameter.Set(DhhUnitUtils.MmToFeet(blist[i]));
                    hParameter.Set(DhhUnitUtils.MmToFeet(hlist[i]));

                    Parameter slTop1Parameter = newinstance.LookupParameter("SL_Top1");
                    Parameter slBot1Parameter = newinstance.LookupParameter("SL_Bottom1");
                    slTop1Parameter.Set(slTop1List[i]);
                    slBot1Parameter.Set(slBot1List[i]);

                    Parameter slTop2Parameter = newinstance.LookupParameter("SL_Top2");
                    Parameter slBot2Parameter = newinstance.LookupParameter("SL_Bottom2");
                    slTop2Parameter.Set(slTop2List[i]);
                    slBot2Parameter.Set(slBot2List[i]);

                    Parameter dai3nhanhdaiParameter = newinstance.LookupParameter("Thep_Dai3Nhanh");
                    Parameter dai4nhanhdaiParameter = newinstance.LookupParameter("Thep_Dai4Nhanh");
                    if(slnhanhdaiList[i] == 2){dai3nhanhdaiParameter.Set(0);dai4nhanhdaiParameter.Set(0);}  
                    else if (slnhanhdaiList[i] ==3){dai3nhanhdaiParameter.Set(1);dai4nhanhdaiParameter.Set(0);}
                    else if (slnhanhdaiList[i] == 4){dai3nhanhdaiParameter.Set(0);dai4nhanhdaiParameter.Set(1);}

                    Parameter thepgiaParameter = newinstance.LookupParameter("SL_LopThepGia");
                    if(hlist[i]<700){thepgiaParameter.Set(0);}  
                    else if (hlist[i] == 700) { thepgiaParameter.Set(1); }
                    else if (hlist[i]>=700){thepgiaParameter.Set(Math.Ceiling((hlist[i]-700)/350));}

                    ReferenceArray referenceArrayX = new ReferenceArray();
                    ReferenceArray referenceArrayY = new ReferenceArray();
                    referenceArrayY.Append(newinstance.GetReferenceByName("Top"));
                    referenceArrayY.Append(newinstance.GetReferenceByName("Bottom"));
                    referenceArrayX.Append(newinstance.GetReferenceByName("Left"));
                    referenceArrayX.Append(newinstance.GetReferenceByName("Right"));
                    Line lineY = Line.CreateUnbound(insertPointX,XYZ.BasisY);
                    Line lineX = Line.CreateUnbound(insertPointX,XYZ.BasisX);
                    Dimension dimensionX = doc.Create.NewDimension(doc.ActiveView, lineX, referenceArrayX);
                    Dimension dimensionY = doc.Create.NewDimension(doc.ActiveView, lineY, referenceArrayY);

                    XYZ translationX = new XYZ(0, -DhhUnitUtils.MmToFeet(hlist[i]/2+150), 0);
                    XYZ translationXmm = new XYZ(0, -(hlist[i]/2+150), 0);
                    ElementTransformUtils.MoveElement(doc, dimensionX.Id, translationX);

                    XYZ translationY = new XYZ(DhhUnitUtils.MmToFeet(blist[i]/2+150), 0, 0);
                    XYZ translationYmm = new XYZ((blist[i]/2+150), 0, 0);
                    ElementTransformUtils.MoveElement(doc, dimensionY.Id, translationY);
                }
                tran.Commit();
            }
            
            return Result.Succeeded;
        }
    }
}
