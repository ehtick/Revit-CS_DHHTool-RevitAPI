using Autodesk.Revit.DB;
using Autodesk.Revit.UI.Selection;

public class DhhRebarScheduleFilter : ISelectionFilter
{
    public bool AllowElement(Element element)
    {
        if (element.Category != null && element.Category.Id.IntegerValue == (int)BuiltInCategory.OST_DetailComponents)
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

