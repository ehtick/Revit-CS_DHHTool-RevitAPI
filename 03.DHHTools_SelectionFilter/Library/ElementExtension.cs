using System.Collections.ObjectModel;
using Autodesk.Revit.DB;

namespace DHHTools
{
    public class ElementExtension : ViewModelBase
    {
        public Element Element { get; set; }
        public Category Category { get; set; }
        public ObservableCollection<ViewExtension> ElementItems { get; set; }
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged();
            }
        }
        public string NewName
        {
            get => _newName;
            set
            {
                _newName = value;
                OnPropertyChanged();
            }
        }
        public bool IsSelected
        {
            get => _isSelected;
            set
            {
                _isSelected = value;
                OnPropertyChanged();
            }
        }
        //public ViewExtension(View view)
        //{
        //    ViewItems = new ObservableCollection<ViewExtension>();
        //    View = view;
        //    Name = view.get_Parameter(BuiltInParameter.VIEW_NAME).AsString();
        //    ViewType = view.ViewType;
        //    IsSelected = false;
        //}
        //public ViewExtension(string name)
        //{
        //    ViewItems = new ObservableCollection<ViewExtension>();
        //    Name = name;
        //    IsSelected = false;
        //}
        //public ViewExtension(ViewType viewType)
        //{
        //    ViewItems = new ObservableCollection<ViewExtension>();
        //    Name = viewType.ToString();
        //    ViewType = viewType;
        //    IsSelected = false;
        //}
        private bool _isSelected;
        private string _name;
        private string _newName;
    }
}