using Microsoft.Xaml.Behaviors.Core;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Autodesk.Revit.DB;
using System.Collections.Generic;
using Application = Autodesk.Revit.ApplicationServices.Application;
using System;
using System.IO;
using DHHTools.MVVM.View;
using System.Windows;
using System.Windows.Controls;
using System.Linq;
using Autodesk.Revit.UI;
using BIMSoftLib.MVVM;

namespace DHHTools.MVVM.ViewModel
{
    public class vmMain: PropertyChangedBase
    {
        private static vmMain _dcMain = new vmMain();
        public static vmMain DcMain { get { return _dcMain; } }
        public static UIApplication RevitApp;
        public static Application RevitAppService;

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

                return _createRebarBeam;
            }
        }
        private void PerformSelectBeam(object par)
        {
            (par as vMain).Hide();
            (par as vMain).Show();
        }
    }
}
