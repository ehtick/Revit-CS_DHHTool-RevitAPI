using Autodesk.Revit.UI;
using DHHTools;
using System;
using System.Collections.Generic;
using System.IO;
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

            #region Create Format CAD Button 

            string nameFC = "FormatCAD";
            string displayNameFC = "Format\nCAD";
            string dllNameFC = "01_02_FormatCADImport.dll";
            string fullClassNameFC = "_01_02_FormatCADImport.FormatCAD";
            string imageFC = "FormatCADIcon32x32.png";
            string tooltipFC = "Đây là tooltip";
            string helperPathFC = null;
            string longDescriptionFC = null;
            string tooltipimageFC = null;
            string linkYoutubeFC = null;
            PushButtonData pushButtonDataFC
                = ribbonUtils.CreatePushButtonData(
                    nameFC, displayNameFC,
                    dllNameFC, fullClassNameFC,
                    imageFC, tooltipFC,
                    helperPathFC,
                    longDescriptionFC,
                    tooltipimageFC, linkYoutubeFC);

            panel1.AddItem(pushButtonDataFC);

            #endregion

            #region Create Print Multi File Revit Button 

            string namePMF = "PrintMultiFile";
            string displayNamePMF = "Print Files";
            string dllNamePMF = "PrintMultiFiles.dll";
            string fullClassNamePMF = "DHHTools.PrintMultiFiles";
            string imagePMF = "PrinterMultiFileIcon32x32.png";
            string tooltipPMF = "Đây là tooltip";
            string helperPathPMF = null;
            string longDescriptionPMF = null;
            string tooltipimagePMF = null;
            string linkYoutubePMF = null;
            PushButtonData pushButtonDataPrMulFile
                = ribbonUtils.CreatePushButtonData(
                    namePMF, displayNamePMF,
                    dllNamePMF, fullClassNamePMF,
                    imagePMF, tooltipPMF,
                    helperPathPMF,
                    longDescriptionPMF,
                    tooltipimagePMF, linkYoutubePMF);

            panel1.AddItem(pushButtonDataPrMulFile);

            #endregion

            #region Create Pulldown Button Backup

            //PulldownButton pulldownButton =
            //    ribbonUtils.CreatePulldownButton(panel1,
            //    "PulldownButton",
            //    "Selection\r\n& Filtering",
            //    "PulldownButton sample",
            //    "PulldownButton_32px.png");

            //// SelectionCmd button
            //pushButtonData = ribbonUtils.CreatePushButtonData(
            //    "SelectionCmd",
            //    "Chọn đối tượng", "Lesson 02_PICK TEXT to ELEMENT.dll",
            //    "QApps.SelectionCmd", "DungFiltersChonDoiTuong_32px.png",
            //    "This is tooltip", dhhConstraint.HelperPath,
            //    "This is long description", "DungFiltersChonDoiTuong_100px.png",
            //    "https://youtu.be/O8FKjaD5V80");
            //pulldownButton.AddPushButton(pushButtonData);

            //// PickTextToElementCmd button
            //pushButtonData = ribbonUtils.CreatePushButtonData(
            //    "PickTextToElementCmd",
            //    "Pick text to Element", "Lesson 02_PICK TEXT to ELEMENT.dll",
            //    "QApps.PickTextToElementCmd", "PickTextToElement_32px.png",
            //    "This is tooltip", dhhConstraint.HelperPath,
            //    "This is long description", "PickTextToElement_100px.png",
            //    "https://youtu.be/O8FKjaD5V80");
            //pulldownButton.AddPushButton(pushButtonData);

            //// TotalWallLengthCmd button
            //pushButtonData = ribbonUtils.CreatePushButtonData(
            //    "TotalWallLengthCmd",
            //    "Total Wall Length", "Lesson 02_PICK TEXT to ELEMENT.dll",
            //    "QApps.TotalWallLengthCmd", "TotalWallLength_32px.png",
            //    "This is tooltip", dhhConstraint.HelperPath,
            //    "This is long description", "TotalWallLength_100px.png",
            //    "https://youtu.be/O8FKjaD5V80");
            //pulldownButton.AddPushButton(pushButtonData);

            #endregion Create Pulldown Button

        }
    }
}
