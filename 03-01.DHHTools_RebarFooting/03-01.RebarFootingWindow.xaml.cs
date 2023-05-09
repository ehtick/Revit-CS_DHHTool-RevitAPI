// ReSharper disable All
#region import
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Drawing;
using System.IO;
using System.Reflection;
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
        public double scale = 0.2;
        public string iconPath;
        public string userName;


        public RebarFootingWindow(RebarFootingViewModel viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel;
            DataContext = _viewModel;
            Doc = _viewModel.Doc;
            //eventHandler = new RebarFootingHandler();
            //MyExternalEvent = ExternalEvent.Create(eventHandler);
            userName = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
            if(userName=="ICIC\\Dinh Hoang Hoa")
            {
                iconPath = Path.Combine(@"D:\01. WORKING\00. C-Sharp\04.CS_DHHTool_RevitAPI\03-01.DHHTools_RebarFooting\Image", "Footing.ico");
            }  
            else if (userName=="Admintrastor\\Desktop")
            {
                iconPath = Path.Combine("D:\\STUDY\\Programming\\C-Sharp\\04.CS_DHHTool_RevitAPI\\03-01.DHHTools_RebarFooting\\Image", "Footing.ico");
            }    
             
            Uri iconUri = new Uri(iconPath, UriKind.Absolute);
            BitmapImage icconWin = new BitmapImage(iconUri);
            Icon = icconWin;
        }

        public void btnSelect_Click(object sender, RoutedEventArgs e)
        {
            Hide();
            _viewModel.SelectElementBtn();
            Show();
            viewCanvas.Children.Clear();
            _viewModel.DrawFootingPlanCanvas(viewCanvas,scale);
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            Close();
            eventHandler.ViewModel = _viewModel;
            MyExternalEvent.Raise();
        }

        private void DrawCanvas_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            double scaleX = viewCanvas.ActualWidth / _viewModel.bFooting;
            double scaleY = viewCanvas.ActualHeight / _viewModel.hFooting;
            viewCanvas.Children.Clear();
            _viewModel.DrawFootingPlanCanvas(viewCanvas, scale);
        }
    }
}
