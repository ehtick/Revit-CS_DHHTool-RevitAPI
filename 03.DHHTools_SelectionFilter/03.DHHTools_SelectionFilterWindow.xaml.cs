// ReSharper disable All
#region import

using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Windows;
// ReSharper disable All

#endregion

namespace DHHTools
{
    public partial class SelectionFilterWindow : Window
    {
        // ReSharper disable once FieldCanBeMadeReadOnly.Local
        // ReSharper disable once InconsistentNaming
        // ReSharper disable once FieldCanBeMadeReadOnly.Local
        //private DhhConstraint _dhhConstraint = new DhhConstraint();
        public SelectionFilterViewModel _viewModel;
        public UIDocument UiDoc;
        public Document Doc;
        public Element ColumnElement;
        public ExternalEvent MyExternalEvent;

        public SelectionFilterWindow(SelectionFilterViewModel viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel;
            DataContext = _viewModel;
            Doc = _viewModel.Doc;

            //Icon = _dhhConstraint.IconWindow;
        }

        private void InitializeComponent()
        {
            throw new NotImplementedException();
        }

        public void Btn_SelectFilter(object sender, RoutedEventArgs e)
        {
            Hide();
            Show();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();

        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            Close();
            MyExternalEvent.Raise();
        }
    }
}
