// ReSharper disable All
#region import
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Forms;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Windows.Markup;
// ReSharper disable All

#endregion

namespace DHHTools
{
    public partial class RebarFoundationWindow : Window
    {
        // ReSharper disable once FieldCanBeMadeReadOnly.Local
        // ReSharper disable once InconsistentNaming
        // ReSharper disable once FieldCanBeMadeReadOnly.Local
        //private DhhConstraint _dhhConstraint = new DhhConstraint();
        public RebarFoundationViewModel _viewModel;
        public UIDocument UiDoc;
        public Document Doc;
        public Element ColumnElement;
        public ExternalEvent MyExternalEvent;

        public RebarFoundationWindow(RebarFoundationViewModel viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel;
            DataContext = _viewModel;
            Doc = _viewModel.Doc;
            
            //Icon = _dhhConstraint.IconWindow;
        }


        public void Btn_Select(object sender, RoutedEventArgs e)
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

        }
    }
}
