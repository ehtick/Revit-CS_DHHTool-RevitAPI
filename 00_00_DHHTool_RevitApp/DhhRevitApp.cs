using Autodesk.Revit.UI;
using DHHTools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _00_00_DHHTool_RevitApp
{
    public class DhhRevitApp : IExternalApplication
    {
        public Result OnShutdown(UIControlledApplication app)
        {
            return Result.Succeeded;
        }

        public Result OnStartup(UIControlledApplication app)
        {
            try
            {
                string versionName = app.ControlledApplication.VersionName;
                string versionNumber = app.ControlledApplication.VersionNumber;
                string versionBuild = app.ControlledApplication.VersionBuild;
                string subVersionNumber = app.ControlledApplication.SubVersionNumber;
                MessageBox.Show("VersionName: " + versionName + "\n" +
                "VersionNumber: " + versionNumber + "\n" +
                "VersionBuild: " + versionBuild + "\n" +
                "SubVersionNumber: " + subVersionNumber + "\n");
                CreateRibbonPanel(app);
                return Result.Succeeded;
            }
            catch
            {
                return Result.Failed;
            }
        }

        private void CreateRibbonPanel(UIControlledApplication a)
        {
            DhhConstraint dhhConstraint = new DhhConstraint();
            DhhRibbonUtils ribbonUtils = new DhhRibbonUtils(a.ControlledApplication);

            // Tạo Ribbon tab
            string ribbonName = "DHH Tools";
            a.CreateRibbonTab(ribbonName);

            // Tạo Ribbon Panel
            string panelName = "General";
            RibbonPanel panel1 = a.CreateRibbonPanel(ribbonName, panelName);

            // Add button vào Panel

            #region Create Push Button 

            string path = @"D:\WORKING\STUDY\Programming\C-Sharp\C-Sharp_DHHTools-DinhHoangHoa\00.DHHTools_TestCmd\bin\Debug\DHHTools_TestCmd.dll";

            PushButtonData pushButtonData
                = ribbonUtils.CreatePushButtonData("Loai1Cmd",
                "Hello\nWorld", path,
                "DHHTools.TestCmd", "TestImage.png",
                "This is tooltip", "Đây là giúp đỡ",
                "This is long description", "TestImage.png", "");

            panel1.AddItem(pushButtonData);

            #endregion

            #region Create Pulldown Button

            PulldownButton pulldownButton =
                ribbonUtils.CreatePulldownButton(panel1,
                "PulldownButton",
                "Selection\r\n& Filtering",
                "PulldownButton sample",
                "PulldownButton_32px.png");

            // SelectionCmd button
            pushButtonData = ribbonUtils.CreatePushButtonData(
                "SelectionCmd",
                "Chọn đối tượng", "Lesson 02_PICK TEXT to ELEMENT.dll",
                "QApps.SelectionCmd", "DungFiltersChonDoiTuong_32px.png",
                "This is tooltip", dhhConstraint.HelperPath,
                "This is long description", "DungFiltersChonDoiTuong_100px.png",
                "https://youtu.be/O8FKjaD5V80");
            pulldownButton.AddPushButton(pushButtonData);

            // PickTextToElementCmd button
            pushButtonData = ribbonUtils.CreatePushButtonData(
                "PickTextToElementCmd",
                "Pick text to Element", "Lesson 02_PICK TEXT to ELEMENT.dll",
                "QApps.PickTextToElementCmd", "PickTextToElement_32px.png",
                "This is tooltip", dhhConstraint.HelperPath,
                "This is long description", "PickTextToElement_100px.png",
                "https://youtu.be/O8FKjaD5V80");
            pulldownButton.AddPushButton(pushButtonData);

            // TotalWallLengthCmd button
            pushButtonData = ribbonUtils.CreatePushButtonData(
                "TotalWallLengthCmd",
                "Total Wall Length", "Lesson 02_PICK TEXT to ELEMENT.dll",
                "QApps.TotalWallLengthCmd", "TotalWallLength_32px.png",
                "This is tooltip", dhhConstraint.HelperPath,
                "This is long description", "TotalWallLength_100px.png",
                "https://youtu.be/O8FKjaD5V80");
            pulldownButton.AddPushButton(pushButtonData);

            #endregion Create Pulldown Button

        }
    }
}
