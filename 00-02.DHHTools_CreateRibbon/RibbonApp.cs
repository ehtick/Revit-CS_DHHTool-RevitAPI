using Autodesk.Revit.UI;

namespace DHHTools
{
    /// <summary>
    /// Tham khảo:
    /// 1. http://bit.ly/2l3Jsf6
    /// 2. https://autode.sk/2mtSaUb
    /// </summary>
    public class RibbonApp : IExternalApplication
    {
        public Result OnStartup(UIControlledApplication app)
        {
            string versionName = app.ControlledApplication.VersionName;
            string versionNumber = app.ControlledApplication.VersionNumber;
            string versionBuild = app.ControlledApplication.VersionBuild;
            string subVersionNumber = app.ControlledApplication.SubVersionNumber;
            //MessageBox.Show("VersionName: " + versionName + "\n" +
            //"VersionNumber: " + versionNumber + "\n" +
            //"VersionBuild: " + versionBuild + "\n" +
            //"SubVersionNumber: " + subVersionNumber + "\n");
            CreateRibbonPanel(app);
            return Result.Succeeded;
        }

        public Result OnShutdown(UIControlledApplication app)
        {
            return Result.Succeeded;
        }

        private void CreateRibbonPanel(UIControlledApplication a)
        {
            //DHHConstraint dhhConstraint = new DHHConstraint();
            RibbonUtils ribbonUtils = new RibbonUtils(a.ControlledApplication);

            // Tạo Ribbon tab
            string ribbonName = "DHH Tools";
            a.CreateRibbonTab(ribbonName);

            // Tạo Ribbon Panel
            string panelName = "Test Code Panel";
            RibbonPanel panel1 = a.CreateRibbonPanel(ribbonName, panelName);

            // Add button vào Panel

            #region Create Push Button 

            string path = @"D:\WORKING\STUDY\Programming\C-Sharp\C-Sharp_DHHTools-DinhHoangHoa\00.DHHTools_TestCmd\bin\Debug\DHHTools_TestCmd.dll";

            PushButtonData pushButtonData
                = ribbonUtils.CreatePushButtonData_Type2("Loai1Cmd",
                "Test\nCode", path,
                "DHHTools.TestCmd", "TestImage.png",
                "This is tooltip", "Đây là giúp đỡ",
                "This is long description", "", "");

            panel1.AddItem(pushButtonData);

            #endregion

        }

    }
}