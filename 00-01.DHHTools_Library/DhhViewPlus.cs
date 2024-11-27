#region Namespaces

using Autodesk.Revit.DB;
using Autodesk.Revit.UI.Selection;

#endregion

namespace DHHTools
{
    public class ViewPlus : DhhViewModelBase
    {
        private bool _isSelected;
        public View View { get; set; }
        public string Name { get; set; }

        public bool IsSelected
        {
            get => _isSelected;
            set
            {
                _isSelected = value;
                OnPropertyChanged();
            }
        }

        public ViewPlus(View v)
        {
            View = v;
            Name = v.Name;
            IsSelected = true;
        }
    }
}