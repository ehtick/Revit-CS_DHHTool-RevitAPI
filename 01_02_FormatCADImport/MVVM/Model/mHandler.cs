using Autodesk.Revit.UI;
using BIMSoftLib.MVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace _01_02_FormatCADImport.MVVM.Model
{
    public class mHandler : IExternalEventHandler
    {
        public UIApplication RevitApp { get; private set; }
        public ObservableRangeCollection<mImportInstancePlus> DgSelectedImportCAD { get; private set; }
        public ObservableRangeCollection<mCategoryPlus> DgCategory { get; private set; }

        public void Execute(UIApplication app)
        {
            try
            {
                methodAddCADFile.SetGraphicStyleForLayerCAD(RevitApp.ActiveUIDocument.Document, DgSelectedImportCAD, DgCategory);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }

        public string GetName()
        {
            throw new NotImplementedException();
        }
    }
}
