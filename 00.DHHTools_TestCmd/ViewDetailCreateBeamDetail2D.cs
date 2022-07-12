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

            #region Get Parameter Framing
            //List<ElementId> selectColumsElements = new List<ElementId>();
            //Reference refer = uidoc.Selection.PickObject(ObjectType.Face);
            //Element element = doc.GetElement(refer);
            //ElementType elementType = doc.GetElement(element.GetTypeId()) as ElementType;
            //double elementCover = DhhElementUtils.GetElementCover(doc, element);
            //Parameter bParameter = elementType.LookupParameter("b");
            //Parameter hParameter = elementType.LookupParameter("h");
            //double bFeet = bParameter.AsDouble();
            //double hFeet = hParameter.AsDouble();
            #endregion Get Parameter Framing
            List<string> liststring = new List<string>();

            XYZ xyz = new XYZ(0, 0, 0);
            FilteredElementCollector filterdetailItem = new FilteredElementCollector(doc)
               //.OfClass(typeof(FamilySymbol))
               .WhereElementIsElementType()
               .OfCategory(BuiltInCategory.OST_DetailComponents);
            List<FamilySymbol> listFamilySymbol = new List<FamilySymbol>();
            List<FamilySymbol> familySymbolDetailBeam = new List<FamilySymbol>();
            foreach (var e in filterdetailItem)
            {
                if(e.GetType().ToString()== "Autodesk.Revit.DB.FamilySymbol")
                {
                    listFamilySymbol.Add(e as FamilySymbol);
                }
            }    
            foreach (var e in listFamilySymbol)
            {
                if(e.Name == "ICIC_KC_ThepDamV2")
                {
                    familySymbolDetailBeam.Add(e);
                }    
            }
            FamilySymbol fselement = familySymbolDetailBeam[0];
            ReferenceArray referenceArray = new ReferenceArray();
            ReferenceArray referenceArray2 = new ReferenceArray();
            XYZ xyz1 = new XYZ(-DhhUnitUtils.MmToFeet(500), 0, 0);
            XYZ xyz2 = new XYZ(-DhhUnitUtils.MmToFeet(500), -DhhUnitUtils.MmToFeet(450), 0);
            Line line = Line.CreateBound(xyz1, xyz2);
            XYZ xyz3 = new XYZ(-DhhUnitUtils.MmToFeet(250), -DhhUnitUtils.MmToFeet(350), 0);
            XYZ xyz4 = new XYZ(-DhhUnitUtils.MmToFeet(-250), 0, 0);
            Line line2 = Line.CreateBound(xyz3, xyz4);
            using (Transaction tran = new Transaction(doc)) {
                tran.Start("Create Detail Beam 2D");
                FamilyInstance newinstance = doc.Create.NewFamilyInstance(xyz,fselement,doc.ActiveView);
                referenceArray.Append(newinstance.GetReferenceByName("Top"));
                referenceArray.Append(newinstance.GetReferenceByName("Bottom"));
                referenceArray2.Append(newinstance.GetReferenceByName("Left"));
                referenceArray2.Append(newinstance.GetReferenceByName("Right"));
                Dimension dimension = doc.Create.NewDimension(doc.ActiveView, line, referenceArray);
                Dimension dimension2 = doc.Create.NewDimension(doc.ActiveView, line2, referenceArray2);
                tran.Commit();
            }
            MessageBox.Show(fselement.Name.ToString(), "Kết quả", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            return Result.Succeeded;
        }
    }
}
