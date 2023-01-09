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
        
        public ExternalEvent MyExternalEvent;

        public SheetDuplicatesWindow(SheetDuplicatesViewModel viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel;
            DataContext = _viewModel;
            Doc = _viewModel.Doc;

            //Icon = _dhhConstraint.IconWindow;
        }
    }
}
