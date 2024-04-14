using Autodesk.Revit.DB;
using Autodesk.Revit.UI.Selection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DHHTools.Object
{
    public class FoundationFilter : ISelectionFilter
    {
        public bool AllowElement(Element elem)
        {
            string name = elem.Category.Name;
            return name.Equals("Structural Foundations");
        }

        public bool AllowReference(Reference reference, XYZ position)
        {
            return true;
        }
    }
    
}
