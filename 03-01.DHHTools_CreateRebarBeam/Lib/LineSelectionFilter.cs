#region Namespaces

using Autodesk.Revit.DB;

#endregion

namespace DHHTools
{
    public class LineSelectionFilter : ISelectionFilter
    {
        public bool AllowElement(Element elem)
        {
            return elem.Category.Name.Equals("Lines");
        }

        public bool AllowReference(Reference reference, XYZ position)
        {
            return false;
        }
    }
}