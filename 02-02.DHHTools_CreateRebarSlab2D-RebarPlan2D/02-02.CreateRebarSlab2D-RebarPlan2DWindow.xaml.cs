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
    public partial class CreateRebarSlab2DWindow : Window
    {
        // ReSharper disable once FieldCanBeMadeReadOnly.Local
        // ReSharper disable once InconsistentNaming
        // ReSharper disable once FieldCanBeMadeReadOnly.Local
        // private DhhConstraint _dhhConstraint = new DhhConstraint();
        public CreateRebarSlab2DViewModel _viewModel;
        public UIDocument UiDoc;
        public Document Doc;
        public Element ColumnElement;
        public CreateRebarSlab2DHandler eventHandler;
        public ExternalEvent MyExternalEvent;

        public CreateRebarSlab2DWindow(CreateRebarSlab2DViewModel viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel;
            DataContext = _viewModel;
            Doc = _viewModel.Doc;
            eventHandler = new CreateRebarSlab2DHandler();
            MyExternalEvent = ExternalEvent.Create(eventHandler);
            eventHandler.ViewModel = _viewModel;
            //Icon = _dhhConstraint.IconWindow;
        }
        public void Btn_ClickSelect(object sender, RoutedEventArgs e)
        {
            Hide();
            _viewModel.SelectElementBtn();
            Show();
        }
        public void Btn_ClickCancel(object sender, RoutedEventArgs e)
        {
            Close();
        }

        public void Btn_ClickOK(object sender, RoutedEventArgs e)
        {
            Hide();
            _viewModel.PickPointOnPlan();
            _viewModel.DrawRebar2D();
            _viewModel.TagRebar2D();
            //do
            //{
            //    _viewModel.PickPointOnPlan();
            //    _viewModel.DrawRebar2D();
            //}
            //while (ConsoleKey.Enter != (ConsoleKey) 0);
            //{
            //    _viewModel.DrawRebar2D();
            //}
            //while (ConsoleKey.Enter != (ConsoleKey) 0) ;

            //MyExternalEvent.Raise();
        }
    }
}
