
#region Namespaces

//using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
// ReSharper disable All

#endregion

namespace DHHTools
{
    [Transaction(TransactionMode.Manual)]
    public class CreateSectionBeam2DCmd : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Application app = uiapp.Application;
            Document doc = uidoc.Document;
            CreateSectionBeam2DViewModel viewModel = new CreateSectionBeam2DViewModel(commandData);
            
            #region Lưu lại Transaction
            using (TransactionGroup transGr = new TransactionGroup(doc))
            {
                transGr.Start("Set Elevation at Top of Beams");
                CreateSectionBeam2DWindow window = new CreateSectionBeam2DWindow(viewModel);
                bool? dialog = window.ShowDialog();
                if (dialog == false) return Result.Succeeded;
                transGr.Commit();
            }
            return Result.Succeeded;
            #endregion
        }
    }
}
