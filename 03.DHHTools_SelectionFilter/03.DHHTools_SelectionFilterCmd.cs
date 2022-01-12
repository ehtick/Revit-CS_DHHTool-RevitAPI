
#region Namespaces

//using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

#endregion

namespace DHHTools
{
    [Transaction(TransactionMode.Manual)]
    public class SelectionFilterCmd : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData,
            ref string message, ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            //Application app = uiapp.Application;
            Document doc = uidoc.Document;
            SelectionFilterViewModel columnViewModel = new SelectionFilterViewModel(commandData);
            return Result.Cancelled;
            #region Lưu lại Transaction

            //using (TransactionGroup transGr = new TransactionGroup(doc))
            //{
            //    transGr.Start("Set Elevation at Top of Beams");

            //    RebarColumnViewModel viewModel
            //        = new RebarColumnViewModel();
            //    CreateRebarColumnWindow window
            //        = new CreateRebarColumnWindow(viewModel);
            //    bool? dialog = window.ShowDialog();
            //    if (dialog == false) return Result.Cancelled;

            //    transGr.Assimilate();
            //}
            //return Result.Succeeded;
            #endregion
        }
    }
}
