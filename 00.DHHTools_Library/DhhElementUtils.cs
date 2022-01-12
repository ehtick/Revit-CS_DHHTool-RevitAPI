using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Structure;
using System.Collections.Generic;
using System.Linq;
using static DHHTools.DhhUnitUtils;

namespace DHHTools
{
    public static class DhhElementUtils
    {
        #region Unit Handling

        /// <summary>
        ///     Get Cover of Framing or Structural Column
        /// </summary>

        public static double GetElementCover(Document doc, Element element)
        {
            Parameter othercoverParameter = element.get_Parameter(BuiltInParameter.CLEAR_COVER_OTHER);
            Parameter topcoverParameter = element.get_Parameter(BuiltInParameter.CLEAR_COVER_TOP);
            Parameter bottomcoverParameter = element.get_Parameter(BuiltInParameter.CLEAR_COVER_BOTTOM);

            ElementId otherParaId = othercoverParameter.AsElementId();
            ElementId topParaId = topcoverParameter.AsElementId();
            ElementId botParaId = bottomcoverParameter.AsElementId();

            List<ElementId> listIds = new List<ElementId> { otherParaId, topParaId, botParaId };
            listIds.RemoveAll(x => x == null || x.ToString() == "-1");
            double cover = 0;
            if (listIds.Count > 0)
            {
                ElementId idCover = listIds[0];
                Element coverElement = doc.GetElement(idCover);
                if (coverElement is RebarCoverType coType) cover = FeetToMm(coType.CoverDistance);
            }
            else
            {
                cover = 25;
            }

            return cover;
        }

        public static RebarBarType GetBarTypeByName(Document doc, string rebarbartypename)
        {
            List<RebarBarType> allRebarBarTypes = new List<RebarBarType>();
            ElementClassFilter rebarClassFilter = new ElementClassFilter(typeof(RebarBarType));
            FilteredElementCollector rebarTypeCollector = new FilteredElementCollector(doc);
            RebarBarType rebarbartype = rebarTypeCollector
                .WherePasses(rebarClassFilter)
                .FirstOrDefault(e => e.Name.Equals(rebarbartypename)) as RebarBarType;
            return rebarbartype;
        }
        #endregion
    }
}