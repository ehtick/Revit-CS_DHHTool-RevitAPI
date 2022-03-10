#region Namespaces

using Autodesk.Revit.DB;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
// ReSharper disable All

#endregion

namespace DHHTools
{
    public class ElementExtension : ViewModelBase
    {
        private bool _isSelected;
        public Element Element { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public bool IsSelected
        {
            get => _isSelected;
            set
            {
                _isSelected = value;
                OnPropertyChanged();
            }
        }

        public ElementExtension(Element e)
        {
            Element = e;
            Name = e.Name;
            Category = e.Category.Name;
            IsSelected = true;
        }
    }
}