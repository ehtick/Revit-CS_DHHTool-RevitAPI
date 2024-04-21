using Microsoft.Xaml.Behaviors.Core;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Autodesk.Revit.DB;
using System.Collections.Generic;
using Application = Autodesk.Revit.ApplicationServices.Application;
using System;
using System.IO;
using System.Windows;
using System.Linq;
using Autodesk.Revit.UI;
using BIMSoftLib.MVVM;
using System.Collections.ObjectModel;
using DHHTools.MVVM.Model;
using DHHTools.MVVM.View;
using System.Windows.Interop;
using DHHTools.Object;

namespace DHHTools.MVVM.ViewModel
{
    public class vmMain: PropertyChangedBase
    {
        private static vmMain _dcMain = new vmMain();
        public static vmMain DcMain { get { return _dcMain; } }
        public static UIApplication RevitApp;
        public static Application RevitAppService;
        public mRevit mFoundation = new mRevit();

        private ObservableRangeCollection<FoundationInfor> _dgFoundation = new ObservableRangeCollection<FoundationInfor>();
        public ObservableRangeCollection<FoundationInfor> DgFoundation
        {
            get
            {
              return _dgFoundation;
            }
            set
            {
                _dgFoundation = value;
                OnPropertyChanged(nameof(DgFoundation));
            }
        }
        private ObservableRangeCollection<FamilySymbol> _dgFouFamilyList = new ObservableRangeCollection<FamilySymbol>();
        public ObservableRangeCollection<FamilySymbol> DgFouFamilyList
        {
            get
            {
                List<FamilySymbol> _allLevelList = new FilteredElementCollector(RevitApp.ActiveUIDocument.Document)
                        .OfCategory(BuiltInCategory.OST_StructuralFoundation)
                        .OfClass(typeof(FamilySymbol))
                        .Cast<FamilySymbol>().ToList();
                foreach (FamilySymbol level in _allLevelList)
                {
                    _dgFouFamilyList.Add(level);
                }
                _dgFouFamilyList = new ObservableRangeCollection<FamilySymbol>(_dgFouFamilyList.Distinct());
                return _dgFouFamilyList;
            }
            set
            {
                _dgFouFamilyList = value;
                OnPropertyChanged(nameof(DgFouFamilyList));
            }
        }
        private FamilySymbol _dgFouFamilySelect;
        public FamilySymbol DgFouFamilySelect
        {
            get
            {
                _dgFouFamilySelect = DgFouFamilyList.FirstOrDefault(x => x.Name.Contains("MongDon")|| x.Name.Contains("SingleFooting")|| x.Name.Contains("SingleFoundation"));
                return _dgFouFamilySelect;
            }
            set
            {
                _dgFouFamilySelect = value; 
                OnPropertyChanged(nameof(DgFouFamilySelect));
            }
        }

        private ActionCommand selectFoundation;

        public ICommand SelectFoundation
        {
            get
            {
                if (selectFoundation == null)
                {
                    selectFoundation = new ActionCommand(PerformSelectFoundation);
                }

                return selectFoundation;
            }
        }

        private void PerformSelectFoundation(object par)
        {
            try
            {
                (par as vMain).Hide();
                mFoundation.SelectFoundations(DgFouFamilySelect);
                MessageBox.Show(DgFoundation.Count.ToString());
                (par as vMain).ShowDialog();
                
            }
            catch { }
        }

        private ActionCommand createRebarFoundation;

        public ICommand CreateRebarFoundation
        {
            get
            {
                if (createRebarFoundation == null)
                {
                    createRebarFoundation = new ActionCommand(PerformCreateRebarFoundation);
                }

                return createRebarFoundation;
            }
        }

        private void PerformCreateRebarFoundation(object par)
        {
            try
            {
                (par as vMain).Close();
                mFoundation.FoundationDetail(DgFoundation);
            }
            catch { }
        }

        private ActionCommand cancelRebarFoundation;

        public ICommand CancelRebarFoundation
        {
            get
            {
                if (cancelRebarFoundation == null)
                {
                    cancelRebarFoundation = new ActionCommand(PerformCancelRebarFoundation);
                }

                return cancelRebarFoundation;
            }
        }

        private void PerformCancelRebarFoundation(object par)
        {
            try
            {
                (par as vMain).Close();
            }
            catch { }
        }
    }
}
