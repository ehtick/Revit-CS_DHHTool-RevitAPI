using Microsoft.Xaml.Behaviors.Core;
using Autodesk.Revit.UI;
using BIMSoftLib.MVVM;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Autodesk.Revit.DB;
using System.Collections.Generic;
//using DHHTools.MVVM.Model;
using Application = Autodesk.Revit.ApplicationServices.Application;
using System;
using System.IO;
using _04_02_ModelColumnsFromCAD.MVVM.View;

namespace _04_02_ModelColumnsFromCAD.MVVM.ViewModel
{
    public class vmMain: PropertyChangedBase
    {
        private static vmMain _dcMain = new vmMain();
        public static vmMain DcMain { get { return _dcMain; } }
        public static UIApplication RevitApp;
        public static Application RevitAppService;
        private double _topOffset;
        public double TopOffset
        {
            get => _topOffset;
            set
            {
                _topOffset = value;
                OnPropertyChanged(nameof(TopOffset));
            }
        }
        private double _botOffset;
        public double BotOffset
        {
            get => _botOffset;
            set
            {
                _topOffset = value;
                OnPropertyChanged(nameof(BotOffset));
            }
        }

        private ActionCommand _btnCancel;

        public ICommand BtnCancel
        {
            get
            {
                if (_btnCancel == null)
                {
                    _btnCancel = new ActionCommand(PerformBtnCancel);
                }

                return _btnCancel;
            }
        }

        private void PerformBtnCancel(object par)
        {
            (par as vMain).Close();
        }

        private ActionCommand _btnSelectCAD;

        public ICommand BtnSelectCAD
        {
            get
            {
                if (_btnSelectCAD == null)
                {
                    _btnSelectCAD = new ActionCommand(PerformBtnSelectCAD);
                }

                return _btnSelectCAD;
            }
        }

        private void PerformBtnSelectCAD(object par)
        {
            (par as vMain).Hide();
            (par as vMain).Show();
        }
    }
}
