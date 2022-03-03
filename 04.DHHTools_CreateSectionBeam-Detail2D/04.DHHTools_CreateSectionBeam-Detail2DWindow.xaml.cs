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
    public partial class CreateSectionBeam2DWindow : Window
    {
        // ReSharper disable once FieldCanBeMadeReadOnly.Local
        // ReSharper disable once InconsistentNaming
        // ReSharper disable once FieldCanBeMadeReadOnly.Local
        //private DhhConstraint _dhhConstraint = new DhhConstraint();
        public CreateSectionBeam2DViewModel _viewModel;
        public UIDocument UiDoc;
        public Document Doc;
        public Element ColumnElement;
        public CreateSectionBeam2DHandler eventHandler;
        public ExternalEvent MyExternalEvent;

        public CreateSectionBeam2DWindow(CreateSectionBeam2DViewModel viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel;
            DataContext = _viewModel;
            Doc = _viewModel.Doc;
            eventHandler = new CreateSectionBeam2DHandler();
            MyExternalEvent = ExternalEvent.Create(eventHandler);
            //Icon = _dhhConstraint.IconWindow;
        }

        private void InitializeComponent()
        {
            throw new NotImplementedException();
        }

        public void Btn_SelectFilter(object sender, RoutedEventArgs e)
        {
            Hide();
            _viewModel.SelectElementBtn();
            Show();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();

        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            Close();
            eventHandler.ViewModel = _viewModel;
            MyExternalEvent.Raise();
        }
    }
}
