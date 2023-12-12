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
using System.Windows;
using _04_02_ModelColumnsFromCAD.MVVM.Model;
using System.Windows.Controls;
using System.Linq;

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
        readonly mAutoCAD mAutoCAD = new mAutoCAD();
        private ImportInstance _cadLink;
        public ImportInstance CadLink
        {
            get=> _cadLink;
            set
            {
                _cadLink = value;
                OnPropertyChanged(nameof(CadLink));
            }
        }

        private ObservableRangeCollection<string> _allLayer = new ObservableRangeCollection<string>();
        public ObservableRangeCollection<string> AllLayer
        {
            get
            {
                return _allLayer;
            }
            set
            {
                _allLayer = value; OnPropertyChanged(nameof(AllLayer));
            }
        }
        private ObservableRangeCollection<Level> _allLevel = new ObservableRangeCollection<Level>();
        public ObservableRangeCollection<Level> AllLevel
        {
            get
            {
                List <Level> _allLevelList = new FilteredElementCollector(RevitApp.ActiveUIDocument.Document)
                        .OfClass(typeof(Level))
                        .Cast<Level>().ToList();
                _allLevelList.OrderBy(x => x.Elevation);
                _allLevel.Clear();
                foreach (Level level in _allLevelList)
                {
                    _allLevel.Add(level);
                }
                _allLevel= new ObservableRangeCollection<Level>(_allLevel.Distinct());
                _allLevel = new ObservableRangeCollection<Level>(_allLevel.OrderBy(x=>x.Elevation));
                return _allLevel;
            }
            set
            {
                _allLevel = value;
                OnPropertyChanged(nameof(AllLevel));
            }
        }
        private Level _bottomLevel;
        public Level BotLevel
        {
            get
            {
                _bottomLevel = _allLevel[0];
                return _bottomLevel;
            }
            set
            {
                _bottomLevel = value;
                OnPropertyChanged(nameof(BotLevel));
            }
        }

        private Level _topLevel;
        public Level TopLevel
        {
            get
            {
                _topLevel = _allLevel[1];
                return _topLevel;
            }
            set
            {
                _topLevel = value;
                OnPropertyChanged(nameof(TopLevel));
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
            CadLink = mAutoCAD.SelectCADLink();
            AllLayer = mAutoCAD.GetAllLayer(CadLink);
            (par as vMain).Show();
        }
    }
}
