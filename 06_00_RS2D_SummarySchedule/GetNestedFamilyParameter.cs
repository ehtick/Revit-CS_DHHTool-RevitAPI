using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;


namespace _06_00_RS2D_SummarySchedule
{
    public class SharedNestedFamilyUtils
    {
        /// <summary>
        /// Get the value of a parameter from a shared nested family by its name.
        /// </summary>
        /// <param name="doc">The current Revit document.</param>
        /// <param name="sharedFamilyName">The name of the shared nested family.</param>
        /// <param name="parameterName">The name of the parameter to retrieve.</param>
        /// <returns>The value of the parameter as a string, or a message if not found.</returns>
        public static string GetSharedNestedFamilyParameterValue(Document doc, string sharedFamilyName, string parameterName)
        {
            // Use FilteredElementCollector to find all family instances of the shared family
            FilteredElementCollector collector = new FilteredElementCollector(doc)
                .OfClass(typeof(FamilyInstance)) // Only family instances
                .WhereElementIsNotElementType(); // Exclude family types

            // Filter family instances by the family name
            FamilyInstance sharedFamilyInstance = collector
                .Cast<FamilyInstance>()
                .FirstOrDefault(fi => fi.Symbol.Family.Name == sharedFamilyName);

            if (sharedFamilyInstance != null)
            {
                // Get the desired parameter
                Parameter param = sharedFamilyInstance.LookupParameter(parameterName);
                if (param != null)
                {
                    return param.AsString() ?? param.AsValueString() ?? "No Value";
                }
                else
                {
                    return $"Parameter '{parameterName}' not found in family '{sharedFamilyName}'.";
                }
            }
            else
            {
                return $"Shared family '{sharedFamilyName}' not found in the document.";
            }
        }
    }
    public class GetNestedFamilyParameter
    {
        /// <summary>
        /// Gets a parameter value from a nested family instance within a host family instance.
        /// </summary>
        /// <param name="doc">The current Revit document.</param>
        /// <param name="hostFamilyInstance">The host FamilyInstance element.</param>
        /// <param name="nestedFamilyName">The name of the nested family.</param>
        /// <param name="parameterName">The name of the parameter to retrieve.</param>
        /// <returns>The parameter value as a string, or a message if not found.</returns>
        public static string GetNestedFamilyParameterValue(Document doc, FamilyInstance hostFamilyInstance, string nestedFamilyName, string parameterName)
        {
            // Get all nested family instances (subcomponents) from the host family
            ICollection<ElementId> nestedFamilyIds = hostFamilyInstance.GetSubComponentIds();

            foreach (ElementId nestedId in nestedFamilyIds)
            {
                // Get the nested family instance
                FamilyInstance nestedFamilyInstance = doc.GetElement(nestedId) as FamilyInstance;
                if (nestedFamilyInstance == null) continue;

                // Check if the nested family matches the target family name
                if (nestedFamilyInstance.Symbol.Family.Name == nestedFamilyName)
                {
                    // Access the desired parameter from the nested family
                    Parameter param = nestedFamilyInstance.LookupParameter(parameterName);
                    if (param != null)
                    {
                        // Return the parameter value as string
                        return param.AsString() ?? param.AsValueString() ?? "No Value";
                    }
                    else
                    {
                        return $"Parameter '{parameterName}' not found in nested family '{nestedFamilyName}'.";
                    }
                }
            }

            return $"Nested family '{nestedFamilyName}' not found in the host family instance.";
        }
    }
}
