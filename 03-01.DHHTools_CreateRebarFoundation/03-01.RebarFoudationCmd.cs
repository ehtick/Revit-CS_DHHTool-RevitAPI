
#region Namespaces

//using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
//using DHHTools.RebarFoundation;

#endregion

namespace DHHTools.RebarFoundation
{
    [Transaction(TransactionMode.Manual)]
    public class CreateRebarFounationCmd : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData,
            ref string message, ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            //Application app = uiapp.Application;
            Document doc = uidoc.Document;
            RebarFoundationViewModel FoundationViewModel = new RebarFoundationViewModel(commandData);
            return Result.Cancelled;
            #region Lưu lại Transaction

            //using (TransactionGroup transGr = new TransactionGroup(doc))
            //{
            //    transGr.Start("Set Elevation at Top of Beams");

            //    RebarFounationViewModel viewModel
            //        = new RebarFounationViewModel();
            //    CreateRebarFounationWindow window
            //        = new CreateRebarFounationWindow(viewModel);
            //    bool? dialog = window.ShowDialog();
            //    if (dialog == false) return Result.Cancelled;

            //    transGr.Assimilate();
            //}
            //return Result.Succeeded;
            #endregion
        }
    }
}
