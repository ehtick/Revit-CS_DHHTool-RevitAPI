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
            _viewModel = viewModel;
            DataContext = _viewModel;
            Doc = _viewModel.Doc;
            eventHandler = new CreateSectionBeam2DHandler();
            MyExternalEvent = ExternalEvent.Create(eventHandler);
            //Icon = _dhhConstraint.IconWindow;
        }

        public void Btn_ClickImportExcel(object sender, RoutedEventArgs e)
        {
            Hide();
            _viewModel.SelectExcelFile();
            Show();
        }

        private void Btn_ClickCancel(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();

        }

        private void Btn_ClickOK(object sender, RoutedEventArgs e)
        {
            Close();
            eventHandler.ViewModel = _viewModel;
            MyExternalEvent.Raise();
        }
    }
}
