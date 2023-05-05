#region Namespaces

using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;

#endregion

namespace DHHTools
{
    public static class FamilySymbolUtils
    {
        public static FamilySymbol GetFamilySymbolFraming(Family family, double b, double h)
        {
            List<FamilySymbol> allFamilySymbol = family.GetAllFamilySymbol();

            foreach (var familySymbol in allFamilySymbol)
            {
                Parameter bParameter = familySymbol.LookupParameter("b");
                Parameter hParameter = familySymbol.LookupParameter("h");
                double bvalue = bParameter.AsDouble();
                double hvalue = hParameter.AsDouble();

                if (bvalue == b && hvalue == h)
                {
                    return familySymbol;
                }
            }


            // làm tròn đến hàng đơn vị, ví dụ: 2995.5 -> 2996
            double sectionX = Math.Round(DLQUnitUtils.FeetToMm(b), 0);
            double sectionY = Math.Round(DLQUnitUtils.FeetToMm(h), 0);
            string name = string.Concat(sectionX, "x", sectionY);

            FamilySymbol result = null;
            using (Transaction tx = new Transaction(family.Document))
            {
                tx.Start("Create new Framing Type");
                ElementType s1 = allFamilySymbol[0].Duplicate(name);
                s1.LookupParameter("b").Set(b);
                s1.LookupParameter("h").Set(h);

                result = s1 as FamilySymbol;
                tx.Commit();
            }
            return result;
        }
        public static List<FamilySymbol> GetAllFamilySymbol(this Family family)
        {
            List<FamilySymbol> familySymbols = new List<FamilySymbol>();

            foreach (ElementId familySymbolId in family.GetFamilySymbolIds())
            {
                FamilySymbol familySymbol = family.Document.GetElement(familySymbolId) as FamilySymbol;
                familySymbols.Add(familySymbol);
            }

            return familySymbols;
        }
    }
}