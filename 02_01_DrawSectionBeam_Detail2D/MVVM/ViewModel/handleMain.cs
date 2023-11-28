using _02_01_DrawSectionBeam_Detail2D.MVVM.Model;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace _02_01_DrawSectionBeam_Detail2D.MVVM.ViewModel
{
    public class handleMain : IExternalEventHandler
    {



        //method
        /// <summary>
        /// Thực hiện các lệnh khi được Raise() lên
        /// </summary>
        /// <param name="app"></param>
        public void Execute(UIApplication app)
        {
            try
            {
                mRevit.CreateSectionBeam2D(app.ActiveUIDocument.Document,vmMain.DcMain.DgSectionBeam);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }
        //method
        public string GetName()
        {
            return "Q'Apps Solutions External Event";
        }
    }
}
