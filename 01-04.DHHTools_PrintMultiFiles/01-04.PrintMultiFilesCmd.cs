
#region Namespaces

//using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
#endregion

namespace DHHTools
{
    [Transaction(TransactionMode.Manual)]
    public class PrintMultiFilesCmd : IExternalCommand
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
                transGr.Start("Print Multi Files");
                PrintMultiFilesViewModel printMultiFilesViewModel 
                    = new PrintMultiFilesViewModel(commandData);
                PrintMultiFilesWindow window
                    = new PrintMultiFilesWindow(printMultiFilesViewModel);
                window.ShowDialog();
                transGr.Commit();
            }
            return Result.Succeeded;
            #endregion
        }

    }
}
