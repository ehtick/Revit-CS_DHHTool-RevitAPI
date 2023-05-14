using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Structure;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using BIMSoftLib.MVVM;

namespace DHHTools
{
    public static class DhhDocumentUtil
    {
        #region 

        /// <summary>
        ///     Get all View Sheet Set
        /// </summary>

        public static ObservableCollection<ViewSheetSet> GetAllSheetSet(Document doc)
        {
            ObservableRangeCollection<ViewSheetSet> allViewSheetSet = new ObservableRangeCollection<ViewSheetSet>();

            FilteredElementCollector colec = new FilteredElementCollector(doc);
            List<Element> allsheetset = colec.OfClass(typeof(ViewSheetSet)).ToElements().ToList();
            foreach (Element item in allsheetset)
            {
                string name = (item as ViewSheetSet).Name;
                //DocPlus.DocAllSheetSetName.Add(name);
                allViewSheetSet.Add((item as ViewSheetSet));
            }
            return allViewSheetSet;
        }
        public static ObservableCollection<string> GetNameAllSheetSet(Document doc)
        {
            ObservableCollection<string> allNameViewSheetSet = new ObservableCollection<string>();

            FilteredElementCollector colec = new FilteredElementCollector(doc);
            List<Element> allsheetset = colec.OfClass(typeof(ViewSheetSet)).ToElements().ToList();
            foreach (Element item in allsheetset)
            {
                string name = (item as ViewSheetSet).Name;
                //DocPlus.DocAllSheetSetName.Add(name);
                allNameViewSheetSet.Add(name);
            }
            return allNameViewSheetSet;
        }

        #endregion
    }
}