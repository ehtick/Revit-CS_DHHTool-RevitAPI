
using Autodesk.Revit.ApplicationServices;
using System;
using System.IO;
using System.Windows.Media.Imaging;

namespace DHHTools
{
    public class DhhConstraint
    {
        #region Khai báo biến

        public string RibbonName;

        /// <summary>
        /// Excemple: C:\ProgramData\Autodesk\ApplicationPlugins\Q'Apps.bundle\Contents
        /// </summary>
        public string ContentsFolder;

        /// <summary>
        /// Excemple: C:\ProgramData\Autodesk\ApplicationPlugins\Q'Apps.bundle\Contents\Resources
        /// </summary>
        public string ResourcesFolder;

        /// <summary>
        /// Excemple: "C:\ProgramData\Autodesk\ApplicationPlugins\Q'Apps.bundle\Contents\Resources\Help"
        /// </summary>
        public string HelpFolder;

        /// <summary>
        /// C:\ProgramData\Autodesk\ApplicationPlugins\Q'Apps.bundle\Contents\Resources\Image
        /// </summary>
        public string ImageFolder;

        /// <summary>
        /// Excemple: C:\ProgramData\Autodesk\ApplicationPlugins\Q'Apps.bundle\Setting
        /// </summary>
        public string SettingFolder;

        /// <summary>
        /// Excemple: C:\ProgramData\Autodesk\ApplicationPlugins\Q'Apps.bundle\Contents\2017\dll
        /// </summary>
        public string DllFolder;

        /// <summary>
        /// Caption hiện lên
        /// </summary>
        public string MessageBoxCaption;

        /// <summary>
        /// tên file icon 16x16, ví dụ: QApps16x16.png
        /// </summary>
        public string Icon16X16;

        /// <summary>
        /// tên file icon 32x32, ví dụ: QApps32x32.png
        /// </summary>
        public string Icon32X32;

        /// <summary>
        /// tên file ảnh 32x32 Other
        /// </summary>
        public string Other32X32;

        /// <summary>
        /// tên file ảnh 16x16 Other
        /// </summary>
        public string Other16X16;

        /// <summary>
        /// tên file icon .ico, ví dụ: About.ico
        /// </summary>
        public string IconWindowIco;

        /// <summary>
        /// Đường dẫn tới file icon của các window WPF hiện ra
        /// </summary>
        public string IconWindowPath;
        public Uri IconWindowUri;
        /// <summary>
        /// Icon của các window
        /// </summary>
        public BitmapImage IconWindow;
        //public BitmapImage IconWindow16x16;

        /// <summary>
        /// Example: "C:\ProgramData\Autodesk\ApplicationPlugins\Q'Apps.bundle\Contents\Resources\Help\Q'AppsHelper.pdf"
        /// </summary>
        public string HelperPath;

        /// <summary>
        /// Tên của file Share parameter, ví dụ: "Q'Apps_SharedParameter.txt"
        /// </summary>
        public string SharedParamsPath;

        /// <summary>
        /// Tên của group parameter kết cấu, ví dụ: "Q'Apps_Structure"
        /// </summary>
        public string SharedParamsGroupStr;
        public string SharedParamsGroupArc;
        public string SharedParamsGroupMep;


        #endregion Khai báo biến

        public DhhConstraint(ControlledApplication a = null)
        {
            #region   Khai báo các biến
            RibbonName = "Q'Apps Training";
            Icon16X16 = "QApps16x16.png";
            Icon32X32 = "QApps32x32.png";
            IconWindowIco = "About.ico";
            Other32X32 = "Other32x32.png";
            Other16X16 = "Other16x16.png";
            ContentsFolder = "C:\\ProgramData\\Autodesk\\ApplicationPlugins\\Q'AppsTraining.bundle\\Contents";
            SettingFolder = "C:\\ProgramData\\Autodesk\\ApplicationPlugins" +
                            "\\Q'AppsTraining.bundle\\Contents\\Resources\\Setting";
            HelperPath = Path.Combine(ContentsFolder,
                "Resources", "Help", "Q'AppsHelper.pdf");
            MessageBoxCaption = string.Concat(RibbonName, " - Effective Add-ins for Autodesk Revit");


            ResourcesFolder = Path.Combine(ContentsFolder, "Resources");
            HelpFolder = Path.Combine(ResourcesFolder, "Help");
            ImageFolder = Path.Combine(ResourcesFolder, "Image");

            IconWindowPath = Path.Combine(ImageFolder, IconWindowIco);
            IconWindowUri = new Uri(IconWindowPath, UriKind.Absolute);

            IconWindow = new BitmapImage(IconWindowUri);

            if (a != null)
            {
                switch (a.VersionNumber)
                {
                    case "2017":
                        DllFolder = Path.Combine(ContentsFolder, "2017", "dll");
                        break;
                    case "2018":
                        DllFolder = Path.Combine(ContentsFolder, "2018", "dll");
                        break;
                    case "2019":
                        DllFolder = Path.Combine(ContentsFolder, "2019", "dll");
                        break;
                    case "2020":
                        DllFolder = Path.Combine(ContentsFolder, "2020", "dll");
                        break;
                    case "2021":
                        DllFolder = Path.Combine(ContentsFolder, "2021", "dll");
                        break;
                    case "2022":
                        DllFolder = Path.Combine(ContentsFolder, "2022", "dll");
                        break;
                    case "2023":
                        DllFolder = Path.Combine(ContentsFolder, "2023", "dll");
                        break;
                    case "2024":
                        DllFolder = Path.Combine(ContentsFolder, "2024", "dll");
                        break;
                    case "2025":
                        DllFolder = Path.Combine(ContentsFolder, "2025", "dll");
                        break;
                }
            }

            #endregion Khai báo các biến
        }
    }
}