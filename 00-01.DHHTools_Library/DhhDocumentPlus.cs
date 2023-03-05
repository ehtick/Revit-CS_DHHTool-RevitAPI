#region Namespaces

using System.Collections.Generic;
using System.Linq;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI.Selection;

#endregion

namespace DHHTools
{

    public class DocumentPlus : ViewModelBase
    {
        private string modelPath_VM;
        public Document Document { get; set; }
        public List<ViewSheetSet> AllSheetSetList { get; set; } = new List<ViewSheetSet>();
        public string ModelPath
        {
            get => modelPath_VM;
            set
            {
                modelPath_VM = value;
                OnPropertyChanged("ModelPath");
            }
        }
        
        public DocumentPlus(Document doc)
        {
            Document = doc;
            ModelPath = doc.PathName;
            FilteredElementCollector colec = new FilteredElementCollector(doc);
            List<Element> allsheetset = colec.OfClass(typeof(ViewSheetSet)).ToElements().ToList();
            foreach (Element item in allsheetset)
            {
                AllSheetSetList.Add(item as ViewSheetSet);
            }
            AllSheetSetList.Sort();
        }
    }
}