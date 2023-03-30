#region Namespaces

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI.Selection;

#endregion

namespace DHHTools
{

    public class DocumentPlus : ViewModelBase
    {
        private string modelPath_VM;
        private Document document_VM;
        private ViewSheetSet documentSelectSheetSet_VM;
        public Document Document
        {
            get => document_VM;
            set
            {
                document_VM = value;
                OnPropertyChanged("Document");
            }
        }
        public string ModelPath
        {
            get => modelPath_VM;
            set
            {
                modelPath_VM = value;
                OnPropertyChanged("ModelPath");
            }
        }
        public ViewSheetSet DocumentSelectSheetSet
        {
            get => documentSelectSheetSet_VM;
            set
            {
                documentSelectSheetSet_VM = value;
                OnPropertyChanged("DocumentSelectSheetSet");
                OnPropertyChanged("DocumentsAllSheetSet");
            }
        }
        public ObservableCollection<ViewSheetSet> DocumentsAllSheetSet { get; set; } 


        public DocumentPlus(Document doc)
        {
            Document = doc;
            ModelPath = doc.PathName;
            DocumentsAllSheetSet = new ObservableCollection<ViewSheetSet>();
            DocumentsAllSheetSet = DhhDocumentUtil.GetAllSheetSet(doc);

        }

    }
}