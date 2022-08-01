
#region Namespaces

//using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

#endregion

namespace DHHTools
{
    [Transaction(TransactionMode.Manual)]
    public class CreateRebarSlab2DCmd : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData,
            ref string message, ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            // ReSharper disable once UnusedVariable
            Application app = uiapp.Application;
            Document doc = uidoc.Document;
            
            #region Lưu lại Transaction
            using (TransactionGroup transGr = new TransactionGroup(doc))
            {
                transGr.Start("Create Slab Rebar");
                CreateRebarSlab2DViewModel viewModel = new CreateRebarSlab2DViewModel(commandData);
                CreateRebarSlab2DWindow window = new CreateRebarSlab2DWindow(viewModel);
                bool? dialog = window.ShowDialog();
                if (dialog == false) return Result.Cancelled;
                transGr.Assimilate();
            }
            return Result.Succeeded;
            #endregion
        }
    }
}
