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

namespace DHHTools.MVVM.ViewModel
{
    public class vmMain: PropertyChangedBase
    {
        private static vmMain _dcMain = new vmMain();
        public static vmMain DcMain { get { return _dcMain; } }
        public static UIApplication RevitApp;
        public static Application RevitAppService;
    }
}
