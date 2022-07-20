using Autodesk.Revit.DB;
using Autodesk.Revit.UI.Selection;

namespace DHHTools
{
    public class ColumnsAndWallsFilter : ISelectionFilter
    {
        public bool AllowElement(Element element)
        {
            if (element.Category.Name == "Structural Columns" || element.Category.Name == "Walls")
            {
                return true;
            }
            return false;
        }

        public bool AllowReference(Reference reference, XYZ position)
        {
            throw new System.NotImplementedException();
        }
    }
}