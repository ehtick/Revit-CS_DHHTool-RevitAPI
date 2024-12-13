using Autodesk.Revit.DB;
using Autodesk.Revit.UI.Selection;

namespace DHHTools
{
    public class DhhRebarScheduleFilter : ISelectionFilter
    {
        public bool AllowElement(Element element)
        {
            if (element.Name == "DHH_KC_DetailItem_ThongKeThep")
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
