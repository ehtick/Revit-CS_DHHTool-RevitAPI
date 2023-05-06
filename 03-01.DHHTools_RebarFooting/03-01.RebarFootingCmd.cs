
#region Namespaces

//using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

#endregion

namespace DHHTools
{
    [Transaction(TransactionMode.Manual)]
    public class RebarFootingCmd : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData,
            ref string message, ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            //Application app = uiapp.Application;
            Document doc = uidoc.Document;
            #region Lưu lại Transaction

            using (TransactionGroup transGr = new TransactionGroup(doc))
            {
                transGr.Start("Set Elevation at Top of Beams");

                RebarFootingViewModel viewModel
                    = new RebarFootingViewModel(commandData);
                RebarFootingWindow window
                    = new RebarFootingWindow(viewModel);
                bool? dialog = window.ShowDialog();
                if (dialog == false) return Result.Cancelled;

                transGr.Assimilate();
            }
            return Result.Succeeded;
            #endregion
        }
    }
}
