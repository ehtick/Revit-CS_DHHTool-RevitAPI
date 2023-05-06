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
// ReSharper disable All

#endregion

namespace DHHTools
{
    public partial class PrintMultiFilesWindow : Window
    {
        // ReSharper disable once FieldCanBeMadeReadOnly.Local
        // ReSharper disable once InconsistentNaming
        // ReSharper disable once FieldCanBeMadeReadOnly.Local
        //private DhhConstraint _dhhConstraint = new DhhConstraint();
        public PrintMultiFilesViewModel _viewModel;
        public UIDocument UiDoc;
        public Document Doc;
        public TransactionGroup transG;
        //public ExternalEvent MyExternalEvent;


        public PrintMultiFilesWindow(PrintMultiFilesViewModel viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel;
            DataContext = _viewModel;
            Doc = _viewModel.Doc;
            transG = new TransactionGroup(_viewModel.Doc);
        }
        private void Btn_SaveLocation(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            // shows the path to the selected folder in the folder dialog
            //System.Windows.MessageBox.Show(fbd.SelectedPath);
            _viewModel.SelectFolder = fbd.SelectedPath;
        }

        private void Btn_OK(object sender, RoutedEventArgs e)
        {
            Close();
            _viewModel.createFolder();
            _viewModel.exportDWF();
            _viewModel.exportDWG();
            _viewModel.deletePCPFile();
            _viewModel.exportPDF();
        }

        private void Btn_Cancel(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Btn_ListChange(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            _viewModel.updateViewSheet();
        }

        private void Btn_AddFile(object sender, RoutedEventArgs e)
        {
            _viewModel.addFile();
            //_viewModel.UpdateAllDocumentList();
        }
    }
}
