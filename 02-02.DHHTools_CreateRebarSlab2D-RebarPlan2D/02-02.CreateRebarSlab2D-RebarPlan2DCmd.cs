
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
            CreateRebarSlab2DViewModel viewModel = new CreateRebarSlab2DViewModel(commandData);

            #region Lưu lại Transaction
            using (TransactionGroup transGr = new TransactionGroup(doc))
            {
                transGr.Start("Create Slab Rebar");
                CreateRebarSlab2DWindow window = new CreateRebarSlab2DWindow(viewModel);
                bool? dialog = window.ShowDialog();
                if (dialog == true) return Result.Succeeded;
                transGr.Commit();
            }
            return Result.Succeeded;
            #endregion
        }
    }
}
