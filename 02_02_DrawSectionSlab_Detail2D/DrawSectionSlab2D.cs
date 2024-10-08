using _02_02_DrawSectionSlab_Detail2D.MVVM.View;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static _02_02_DrawSectionSlab_Detail2D.MVVM.ViewModel.viewmodel_SectionSlab2D;

namespace _02_02_DrawSectionSlab_Detail2D
{
    [Transaction(TransactionMode.Manual)]
    public class DrawSectionSlab2D : IExternalCommand
    {
        Result IExternalCommand.Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIApplication uIApplication = commandData.Application;
            RevitApp = uIApplication;
            UIDocument uIDocument = RevitApp.ActiveUIDocument;
            Document document = uIDocument.Document;
            using (TransactionGroup transGroup = new TransactionGroup(document))
            {
                transGroup.Start("Detail Beam 2D");
                view_SectionSlab2D win = new view_SectionSlab2D();
                bool? dialog = win.ShowDialog();
                if (dialog != false)
                    return Result.Succeeded;
                transGroup.Commit();
            }

            return Result.Succeeded;
        }
    }
}
