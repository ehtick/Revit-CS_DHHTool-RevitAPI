using Autodesk.Revit.UI;
using BIMSoftLib.MVVM;
using Microsoft.Xaml.Behaviors.Core;
using System;
using System.Collections.Generic;
using Application = Autodesk.Revit.ApplicationServices.Application;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace _05_01_ModelEtabsFromRevit.MVVM.ViewModel
{
    public class vmMainEtabsFrRevit: PropertyChangedBase
    {
        #region vmMainEtabsFrRevit
        private static vmMainEtabsFrRevit _dcMainEtabsFrRevit = new vmMainEtabsFrRevit();
        public static vmMainEtabsFrRevit DcMainEtabsFrRevit { get { return _dcMainEtabsFrRevit; } }
        public static UIApplication RevitUIApp;
        public static Application RevitApp;

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
        {
        }
        #endregion
    }
}
