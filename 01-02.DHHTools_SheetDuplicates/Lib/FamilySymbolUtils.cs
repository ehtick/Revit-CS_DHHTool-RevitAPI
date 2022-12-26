#region Namespaces

using Autodesk.Revit.DB;
using System.Collections.Generic;
using System.Linq;
// ReSharper disable All

#endregion

namespace DHHTools
{
    public static class CreateCurveStirrup
    {
        internal static List<Parameter> GetAllParameter(this Element element,
            bool isInclueTypePara = true, bool isInclueParameterReadOnly = false)
        {
            List<Parameter> allParameter = null;
            if (isInclueParameterReadOnly)
            {
                allParameter = (from Parameter p in element.Parameters
                                    //where p.IsReadOnly == false
                                where p.UserModifiable == true
                                select p).ToList();
            }
            else
            {
                allParameter =
                    (from Parameter p in element.Parameters
                     where p.IsReadOnly == false
                     where p.UserModifiable == true
                     select p).ToList();
            }

            if (isInclueTypePara)
            {
                ElementId typeId = element.GetTypeId();
                ElementType elementType = element.Document.GetElement(typeId) as ElementType;

                allParameter.AddRange(elementType.GetAllParameter(true, isInclueParameterReadOnly));
            }

            allParameter.Sort();
            return allParameter;
        }
    }
}