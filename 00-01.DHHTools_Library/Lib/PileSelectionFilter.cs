#region Namespaces

using Autodesk.Revit.DB;
using Autodesk.Revit.UI.Selection;

#endregion

namespace DHHTools
{
    public class PileSelectionFilter : ISelectionFilter
    {
        public bool AllowElement(Element elem)
        {
            return elem.Category.Name.Equals("Structural Foundations");
        }

        public bool AllowReference(Reference reference, XYZ position)
        {
            return false;
        }
    }
}