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
            XYZ xyz1 = new XYZ(800, 0, 0);
            //XYZ xyz2 = new XYZ(-DhhUnitUtils.MmToFeet(800), -DhhUnitUtils.MmToFeet(900), 0);
            //Line line = Line.CreateBound(xyz1, xyz2);
            using (Transaction tran = new Transaction(doc))
            {
                tran.Start("Create Detail Beam 2D");
                FamilyInstance newinstance = doc.Create.NewFamilyInstance(xyz,fselement,doc.ActiveView);
                //referenceArray.Append(newinstance.GetReferenceByName("Top"));
                //referenceArray.Append(newinstance.GetReferenceByName("Bottom"));
                //Dimension dimension = doc.Create.NewDimension(doc.ActiveView, line, referenceArray);
                tran.Commit();
            }
            MessageBox.Show(fselement.Name.ToString(), "Kết quả", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            return Result.Succeeded;
        }
    }
}
