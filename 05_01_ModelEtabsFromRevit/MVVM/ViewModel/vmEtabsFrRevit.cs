using Autodesk.Revit.UI;
using BIMSoftLib.MVVM;
using Microsoft.Xaml.Behaviors.Core;
using System;
using System.Collections.Generic;
using Application = Autodesk.Revit.ApplicationServices.Application;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using _05_01_ModelEtabsFromRevit.MVVM.Model;
using _05_01_ModelEtabsFromRevit.MVVM.View;

namespace _05_01_ModelEtabsFromRevit.MVVM.ViewModel
{
    public class vmMainEtabsFrRevit : PropertyChangedBase
    {
        #region vmMainEtabsFrRevit
        private static vmMainEtabsFrRevit _dcMainEtabsFrRevit = new vmMainEtabsFrRevit();
        public static vmMainEtabsFrRevit DcMainEtabsFrRevit { get { return _dcMainEtabsFrRevit; } }
        public static UIApplication RevitUIApp;
        public static Application RevitApp;

        mEtabsClass ETABsClass { get; set; } = new mEtabsClass();

        #endregion

        #region vmMainEtabsFrRevit Command
        private ActionCommand connectETABS;
        public ICommand ConnectETABS
        {
            get
            {
                if (connectETABS == null)
                {
                    connectETABS = new ActionCommand(PerformConnectETABS);
                }

                return connectETABS;
            }
        }
        private void PerformConnectETABS()
        { ETABsClass.SelectETABS(); }

        private ActionCommand drawGrid;
        public ICommand DrawGrid
        {
            get
            {
                if (drawGrid == null)
                {
                    drawGrid = new ActionCommand(PerformDrawGrid);
                }

                return drawGrid;
            }
        }
        private void PerformDrawGrid(object par)
        {
            try
            {
                (par as vMainEtabsFromRevit).Hide();
                ETABsClass.DrawGridAndLevelETABS(RevitUIApp.ActiveUIDocument);
            }
            catch
            {

            }


        }

        private System.Windows.Visibility _visibleWindow;
        public System.Windows.Visibility VisibleWindow
        {
            get => _visibleWindow;
            set
            {
                _visibleWindow = value;
                OnPropertyChanged(nameof(VisibleWindow));
            }

        }
        #endregion
    }
}
