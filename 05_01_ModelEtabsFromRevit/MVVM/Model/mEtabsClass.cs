using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using CSiAPIv1;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using DHHTools;

namespace _05_01_ModelEtabsFromRevit.MVVM.Model
{
    public class mEtabsClass
    {
        public static cOAPI MyEtabsObject { get; set; }
        public static cSapModel MySapModel { get; set; }
        public static cHelper MyHelper { get; set; }



        public void SelectETABS()
        {
            try
            {
                MyHelper = new Helper();
                MyEtabsObject = (CSiAPIv1.cOAPI)System.Runtime.InteropServices.Marshal.GetActiveObject("CSI.ETABS.API.ETABSObject");
                MySapModel = MyEtabsObject.SapModel;
                MessageBox.Show("Connected ETABS Complete");
            }
            catch
            {
                MessageBox.Show("Open Etabs");
            }
        }

        public void DrawGridAndLevelETABS(UIDocument uIDocument)
        {
            try
            {
                XYZ originPoint = uIDocument.Selection.PickPoint();
                FilteredElementCollector elements = new FilteredElementCollector(uIDocument.Document);
                List<Grid> grids = elements.OfCategory(BuiltInCategory.OST_Grids).OfClass(typeof(Grid)).Cast<Grid>().ToList();
                int NumberFields = grids.Count;
                string[] TableDataGrid = new string[NumberFields * 11];
                List<string> tabledata_Draft = new List<string>();
                foreach (Grid grid in grids)
                {
                    tabledata_Draft.Add("G1");
                    string name = grid.Name;
                    Curve curve = grid.Curve;
                    XYZ origin = (curve as Line).Origin;
                    XYZ direction = (curve as Line).Direction;
                    //double check = direction.AngleTo(XYZ.BasisX);
                    double check = direction.DotProduct(XYZ.BasisX);
                    if (Math.Round(check) == 0) { tabledata_Draft.Add("X (Cartesian)"); }
                    else { tabledata_Draft.Add("Y (Cartesian)"); }
                    tabledata_Draft.Add(name);
                    double v = (curve as Line).Distance(originPoint);
                    double distance = Math.Round(DhhUnitUtils.FeetToMm(v), 0);
                    tabledata_Draft.Add((distance / 1000).ToString());
                    tabledata_Draft.Add("");
                    tabledata_Draft.Add("");
                    tabledata_Draft.Add("");
                    tabledata_Draft.Add("");
                    tabledata_Draft.Add("");
                    if (Math.Round(check) == 0) { tabledata_Draft.Add("End"); }
                    else { tabledata_Draft.Add("Start"); }
                    tabledata_Draft.Add("Yes");
                }
                TableDataGrid = tabledata_Draft.ToArray();
                MessageBox.Show(grids.Count.ToString());
                #region ETABS
                MySapModel.InitializeNewModel(eUnits.kN_m_C);
                MySapModel.File.NewBlank();
                string GridSysName = "G1";
                MySapModel.GridSys.SetGridSys(GridSysName, 0, 0, 0);

                ////int NumberTables = 0;
                ////string[] TableKey = null;
                ////string[] TableName = null;
                ////int[] ImportType = null;
                ////bool[] IsEmpty = null;
                ////string TableKeyGet = null;

                ////int NumberFields = 0;
                ////string[] FieldKey = null;
                ////string[] FieldName = null;
                ////string[] Description = null;
                ////string[] UnitsString = null;
                ////bool[] IsImporttable = null;

                int TableVersion = 0;
                string[] FieldsKeysIncluded = { "Name", "Grid Line Type", "ID", "Ordinate", "Angle", "X1", "Y1", "X2", "Y2", "Bubble Location", "Visible" };
                int NumberRecord = 4;

                bool FillImportLog = true;
                int NumFatalErrors = 0;
                int NumErrorMsgs = 0;
                int NumWarnMsgs = 0;
                int NumInfoMsgs = 0;
                string ImportLog = null;
                //string[] FieldKeysIncludedLevel = { "Tower", "Name", "Height", "Master Story", "Similar To", "Splice Story", "Color", "GUID" };
                //string[] TableDataGrid =
                //{
                //    "G1", "X (Cartesian)", "A","0","","","","","","End","Yes",
                //    "G1", "X (Cartesian)", "B","5","","","","","","End","Yes",
                //    "G1", "Y (Cartesian)", "1","0","","","","","","Start","Yes",
                //    "G1", "Y (Cartesian)", "2","10","","","","","","Start","Yes",

                //};
                FilteredElementCollector elementsLevel = new FilteredElementCollector(uIDocument.Document);
                List<Level> levels = elements.OfCategory(BuiltInCategory.OST_Levels).OfClass(typeof(Level)).Cast<Level>().ToList();
                List<Level> levelSort = (List<Level>)levels.OrderBy(x => x.Elevation);
                int NumberLevel = levelSort.Count;
                List<string> StoryNameList = new List<string>();
                List<double> StoryHeightList = new List<double>();
                List<bool> IsMaterStoryList = new List<bool>();
                List<string> IsSimilarToStoryList = new List<string>();
                List<bool> SpliceAboveList = new List<bool>();
                List<double> SpliceHeightList = new List<double>();
                List<int> ColorList = new List<int>();

                string[] StoryName = StoryNameList.ToArray();
                double[] StoryHeight = StoryHeightList.ToArray();
                bool[] isMaterStory = IsMaterStoryList.ToArray();
                string[] IsSimilarToStory = IsSimilarToStoryList.ToArray();
                bool[] SpliceAbove = SpliceAboveList.ToArray();
                double[] SpliceHeight = SpliceHeightList.ToArray();
                int[] Color = ColorList.ToArray();


                MySapModel.DatabaseTables.SetTableForEditingArray("Grid Definitions - Grid Lines", ref TableVersion, ref FieldsKeysIncluded, NumberRecord, ref TableDataGrid);
                MySapModel.DatabaseTables.ApplyEditedTables(FillImportLog, ref NumFatalErrors, ref NumErrorMsgs, ref NumWarnMsgs, ref NumInfoMsgs, ref ImportLog);
                MySapModel.Story.SetStories_2(-2, 4, ref StoryName, ref StoryHeight, ref isMaterStory, ref IsSimilarToStory, ref SpliceAbove, ref SpliceHeight, ref Color);
                MySapModel.View.RefreshWindow();
                MySapModel.View.RefreshView();
                
                #endregion

            }
            catch
            {
                MessageBox.Show("Open Etabs");
            }
        }
    }
}
