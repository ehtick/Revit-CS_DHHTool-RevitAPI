using Autodesk.Revit.UI;
using System;
using System.Windows.Forms;

namespace DHHTools
{
    public class SelectionFilterHandler : IExternalEventHandler
    {
        //property
        public SelectionFilterViewModel ViewModel;
        //method
        /// <summary>
        /// Thực hiện các lệnh khi được Raise() lên
        /// </summary>
        /// <param name="app"></param>
     
        //public void Execute(UIApplication app)
        //{
        //    try
        //    {
        //        ViewModel.CreateMainRebar();
        //    }
        //    catch (Exception e)
        //    {
        //        MessageBox.Show(e.ToString());
        //    }
        //}
        
        //method
        public string GetName()
        {
            return "Q'Apps Solutions External Event";
        }
    }
}
