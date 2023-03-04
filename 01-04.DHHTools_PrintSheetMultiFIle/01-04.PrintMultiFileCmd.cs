
#region Namespaces

//using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

#endregion

namespace DHHTools
{
    [Transaction(TransactionMode.Manual)]
    public class PrintMultiFileCmd : IExternalCommand
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
                transGr.Start("Print Multi File");
                PrintMultiFileViewModel printMultiFileViewModel 
                    = new PrintMultiFileViewModel(commandData);
                PrintMultiFileWindow window
                    = new PrintMultiFileWindow(printMultiFileViewModel);
                bool? dialog = window.ShowDialog();
                if (dialog != false) return Result.Succeeded;
                transGr.Assimilate();
            }
            return Result.Succeeded;
            #endregion
        }

    }
}
