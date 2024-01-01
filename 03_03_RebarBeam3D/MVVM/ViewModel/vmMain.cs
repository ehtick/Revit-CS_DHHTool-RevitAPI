using Microsoft.Xaml.Behaviors.Core;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Autodesk.Revit.DB;
using System.Collections.Generic;
using Application = Autodesk.Revit.ApplicationServices.Application;
using Rectangle = System.Windows.Shapes.Rectangle;
using Brushes = System.Windows.Media.Brushes;
using System;
using System.IO;
using DHHTools.MVVM.View;
using System.Windows;
using System.Windows.Controls;
using System.Linq;
using Autodesk.Revit.UI;
using BIMSoftLib.MVVM;
using System.Collections.ObjectModel;
using DHHTools.Object;
using DHHTools.MVVM.Model;
using System.Windows.Documents;

namespace DHHTools.MVVM.ViewModel
{
    public class vmMain: PropertyChangedBase
    {
        private static vmMain _dcMain = new vmMain();
        public static vmMain DcMain { get { return _dcMain; } }
        public static UIApplication RevitApp;
        public static Application RevitAppService;
        readonly mRevit mRevitObject = new mRevit();
        private Element _selectedBeam;
        public Element SelectedBeam
        {
            get => _selectedBeam;
            set
            {
                _selectedBeam = value;
                OnPropertyChanged(nameof(SelectedBeam));
            }
        }
        private ObservableRangeCollection<PathDetail> _ItemsToShowInCanvas = new ObservableRangeCollection<PathDetail>
        {
            new PathDetail
            {
                //Geometry = PathGeometryLibrary.GetFootPath(300,200,100,100,50,50),
                //Geometry = PathGeometryLibrary.GetBeamPath(1200,200,0.3),
                Geometry = PathGeometryLibrary.GetMiddleColumnPath(200,400,0.5),
                TopSet = 10,
                LeftSet = 10,
            }
        };
        public ObservableRangeCollection<PathDetail> ItemsToShowInCanvas
        {
            get
            {
                return _ItemsToShowInCanvas;
            }
            set
            {
                _ItemsToShowInCanvas = value; OnPropertyChanged(nameof(ItemsToShowInCanvas));
            }
        }
        private ObservableRangeCollection<Element> _listColumn;
        public ObservableRangeCollection<Element> ListColumn
        {
            get
            {
                return _listColumn;
            }
            set
            {
                _listColumn = value; OnPropertyChanged(nameof(ListColumn));
            }
        }


        #region Method
        private ActionCommand _cancelRebarBeam;
        public ICommand CancelRebarBeam
        {
            get
            {
                if (_cancelRebarBeam == null)
                {
                    _cancelRebarBeam = new ActionCommand(PerformCancelRebarBeam);
                }

                return _cancelRebarBeam;
            }
        }
        private void PerformCancelRebarBeam(object par)
        {
            (par as vMain).Close();
        }

        private ActionCommand _createRebarBeam;
        public ICommand CreateRebarBeam
        {
            get
            {
                if (_createRebarBeam == null)
                {
                    _createRebarBeam = new ActionCommand(PerformCreateRebarBeam);
                }

                return _createRebarBeam;
            }
        }
        private void PerformCreateRebarBeam(object par)
        {
            (par as vMain).Close();
        }

        private ActionCommand _selectBeam;
        public ICommand SelectBeam
        {
            get
            {
                if (_selectBeam == null)
                {
                    _selectBeam = new ActionCommand(PerformSelectBeam);
                }

                return _selectBeam;
            }
        }
        private void PerformSelectBeam(object par)
        {
            (par as vMain).Hide();
            List<double> listColumnWidth = new List<double>();
            SelectedBeam = mRevitObject.SelectBeam();
            ListColumn = mRevitObject.CheckIntersectElements(SelectedBeam);
            List<Solid> listPathDetail = mRevitObject.ListPathDetail(SelectedBeam, ListColumn, 20, 20);
            foreach (Element column in ListColumn)
            {
                listColumnWidth.Add(mRevitObject.CheckWidthColumn(SelectedBeam, column));
            }
            (par as vMain).Show();
            string allitems = string.Empty;
            foreach (double item in listColumnWidth)
            {
                if (listColumnWidth.Count > 0)
                {
                    allitems += "\n";
                }
                else { } //todo nothing

                allitems += item;
            }
            MessageBox.Show($"my items:\n{allitems}", "items");
        }
        #endregion


    }
}
