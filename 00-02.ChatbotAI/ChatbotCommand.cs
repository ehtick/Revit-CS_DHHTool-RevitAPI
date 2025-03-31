using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _00_02.ChatbotAI
{
    [Transaction(TransactionMode.Manual)]
    public class ChatbotCommand : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            System.Threading.Tasks.Task.Delay(5000).Wait();
            UIApplication uIApplication = commandData.Application;
            UIDocument uIDocument = uIApplication.ActiveUIDocument;
            Document document = uIDocument.Document;

            using (TransactionGroup transGroup = new TransactionGroup(document))
            {
                transGroup.Start("Vẽ tường tự động");
                ChatbotWindow win = new ChatbotWindow(commandData.Application);
                bool? dialog = win.ShowDialog();
                if (dialog != false)
                    return Result.Succeeded;
                transGroup.Commit();    
            }
            return Result.Succeeded;
        }
    }
}
