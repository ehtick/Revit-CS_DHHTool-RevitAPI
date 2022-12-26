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
    public partial class SheetDuplicatesWindow : Window
    {
        // ReSharper disable once FieldCanBeMadeReadOnly.Local
        // ReSharper disable once InconsistentNaming
        // ReSharper disable once FieldCanBeMadeReadOnly.Local
        //private DhhConstraint _dhhConstraint = new DhhConstraint();
        public SheetDuplicatesViewModel _viewModel;
        public UIDocument UiDoc;
        public Document Doc;
        public Element ColumnElement;
        public SheetDuplicatesHandler eventHandler;
        public ExternalEvent MyExternalEvent;

        public SheetDuplicatesWindow(SheetDuplicatesViewModel viewModel)
        {
            //InitializeComponent();
            _viewModel = viewModel;
            DataContext = _viewModel;
            Doc = _viewModel.Doc;
            eventHandler = new SheetDuplicatesHandler();
            MyExternalEvent = ExternalEvent.Create(eventHandler);
            //Icon = _dhhConstraint.IconWindow;
        }

        public void btnPick_P1(object sender, RoutedEventArgs e)
        {
            Hide();
            _viewModel.PickPoint1();
            Show();
        }
        public void btnPick_P2(object sender, RoutedEventArgs e)
        {
            Hide();
            _viewModel.PickPoint2();
            Show();
        }
        public void btnSelectPiles_Click(object sender, RoutedEventArgs e)
        {
            Hide();
            _viewModel.SelectPile();
            Show();
        }
        public void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            Hide();
            _viewModel.CoornPile();
        }
    }
}
