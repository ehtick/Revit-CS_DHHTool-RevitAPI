using Autodesk.Revit.DB;
using Autodesk.Revit.UI.Selection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _04_02_ModelColumnsFromCAD.Object
{
    public class ImportInstanceSelectionFilter : ISelectionFilter
    {
        public bool AllowElement(Element elem)
        {
            return elem is ImportInstance;
        }
        public bool AllowReference(Reference reference, XYZ position)
        {
            return false;
        }
    }
}
