// ReSharper disable All
#region import
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Linq;
using System.Windows;
// ReSharper disable All

#endregion

namespace DHHTools
{
    public partial class SheetDuplicatesWindow : Window
    {
        // ReSharper disable once FieldCanBeMadeReadOnly.Local
        // ReSharper disable once InconsistentNaming
        // ReSharper disable once FieldCanBeMadeReadOnly.Local
        //private DhhConstraint _dhhConstraint = new DhhConstraint();
        public SheetDuplicatesViewModel _viewModel;
        public SheetDuplicatesHandler eventHandler;
        public UIDocument UiDoc;
        public Document Doc;
        public TransactionGroup transG;
        public ExternalEvent MyExternalEvent;


        public SheetDuplicatesWindow(SheetDuplicatesViewModel viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel;
            DataContext = _viewModel;
            Doc = _viewModel.Doc;
            eventHandler = new SheetDuplicatesHandler();
            MyExternalEvent = ExternalEvent.Create(eventHandler);
            transG = new TransactionGroup(_viewModel.Doc);
        }

        private void Button_OK(object sender, RoutedEventArgs e)
        {
            Close();
            eventHandler.ViewModel = _viewModel;
            MyExternalEvent.Raise();
        }

        private void Button_Cancel(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void CheckedAll(object sender, RoutedEventArgs e)
        {
            foreach (ViewSheetPlus viewSheet in _viewModel.AllViewsSheetList)
            {
                viewSheet.IsSelected = true;
            }
        }

        private void CheckedNone(object sender, RoutedEventArgs e)
        {
            foreach (ViewSheetPlus viewSheet in _viewModel.AllViewsSheetList)
            {
                viewSheet.IsSelected = false;
            }
        }

        private void CheckBoxClick(object sender, RoutedEventArgs e)
        {
            ViewSheetPlus first = _viewModel.SelectedViewsSheet
                .FirstOrDefault();

            bool selected = first.IsSelected;
            foreach (var vs in _viewModel.SelectedViewsSheet)
                vs.IsSelected = !selected;
        }

        private void ViewDuplicate(object sender, RoutedEventArgs e)
        {
            _viewModel.duplicateOption = ViewDuplicateOption.Duplicate;
        }

        private void ViewDuplicateDetailing(object sender, RoutedEventArgs e)
        {
            _viewModel.duplicateOption = ViewDuplicateOption.WithDetailing;
        }

        private void ViewDuplicateDependence(object sender, RoutedEventArgs e)
        {
            _viewModel.duplicateOption = ViewDuplicateOption.AsDependent;
        }
    }
}
