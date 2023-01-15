#region Namespaces

using Autodesk.Revit.DB;
using Autodesk.Revit.UI.Selection;

#endregion

namespace DHHTools
{
    public class ViewSheetPlus : ViewModelBase
    {
        private bool _isSelected;
        public ViewSheet ViewSheet { get; set; }
        public string Name { get; set; }
        public string SheetNumber { get; set; }
        public bool IsSelected
        {
            get => _isSelected;
            set
            {
                _isSelected = value;
                OnPropertyChanged();
            }
        }

        public ViewSheetPlus(ViewSheet v)
        {
            ViewSheet = v;
            Name = v.Name;
            SheetNumber = v.SheetNumber;
            IsSelected = true;
        }
    }
}