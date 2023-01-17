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
    public partial class PrintSheetWindow : Window
    {
        // ReSharper disable once FieldCanBeMadeReadOnly.Local
        // ReSharper disable once InconsistentNaming
        // ReSharper disable once FieldCanBeMadeReadOnly.Local
        //private DhhConstraint _dhhConstraint = new DhhConstraint();
        public PrintSheetViewModel _viewModel;
        public PrintSheetHandler eventHandler;
        public UIDocument UiDoc;
        public Document Doc;
        public TransactionGroup transG;
        public ExternalEvent MyExternalEvent;


        public PrintSheetWindow(PrintSheetViewModel viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel;
            DataContext = _viewModel;
            Doc = _viewModel.Doc;
            eventHandler = new PrintSheetHandler();
            MyExternalEvent = ExternalEvent.Create(eventHandler);
            transG = new TransactionGroup(_viewModel.Doc);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
            _viewModel.exportDWF();
            _viewModel.exportDWG();
            _viewModel.exportPDF();
        }
    }
}
