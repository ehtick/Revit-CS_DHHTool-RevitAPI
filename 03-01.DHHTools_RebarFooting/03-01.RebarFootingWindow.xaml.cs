// ReSharper disable All
#region import
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Drawing;
using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;

// ReSharper disable All

#endregion

namespace DHHTools
{
    public partial class RebarFootingWindow : Window
    {
        // ReSharper disable once FieldCanBeMadeReadOnly.Local
        // ReSharper disable once InconsistentNaming
        // ReSharper disable once FieldCanBeMadeReadOnly.Local
        //private DhhConstraint _dhhConstraint = new DhhConstraint();
        public RebarFootingViewModel _viewModel;
        public UIDocument UiDoc;
        public Document Doc;
        public Element ColumnElement;
        public RebarFootingHandler eventHandler;
        public ExternalEvent MyExternalEvent;
        private object _dhhConstraint;

        public RebarFootingWindow(RebarFootingViewModel viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel;
            DataContext = _viewModel;
            Doc = _viewModel.Doc;
            eventHandler = new RebarFootingHandler();
            MyExternalEvent = ExternalEvent.Create(eventHandler);
            string iconPath = Path.Combine("D:\\STUDY\\Programming\\C-Sharp\\04.CS_DHHTool_RevitAPI\\03-01.DHHTools_RebarFooting\\Image", "Footing.ico");
            Uri iconUri = new Uri(iconPath, UriKind.Absolute);
            BitmapImage icconWin = new BitmapImage(iconUri);
            Icon = icconWin;
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
            eventHandler.ViewModel = _viewModel;
            MyExternalEvent.Raise();
        }
    }
}
