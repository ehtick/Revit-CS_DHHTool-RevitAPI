using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _00_00_DHHTool_RevitApp
{
    public class RevitApp : IExternalApplication
    {
        public Result OnShutdown(UIControlledApplication application)
        {
            return Result.Succeeded;
        }

        public Result OnStartup(UIControlledApplication application)
        {
            try
            {
                RevitApp = application;
            }
            catch
            {
                return Result.Failed;
            }
            return Result.Succeeded;
        }
    }
}
