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
                XYZ pickPoint = uIDocument.Selection.PickPoint();
                FilteredElementCollector elements = new FilteredElementCollector(uIDocument.Document);
                #region Grid
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
                    double check = direction.DotProduct(XYZ.BasisX);
                    if (Math.Round(check) == 0) { tabledata_Draft.Add("X (Cartesian)"); }
                    else { tabledata_Draft.Add("Y (Cartesian)"); }
                    tabledata_Draft.Add(name);
                    //XYZ checkPoint = new XYZ(pickPoint.X, pickPoint.Y, 0);
                    //curve.MakeBound(0, 1);
                    //Line line = Line.CreateBound
                    //    (
                    //        new XYZ(curve.GetEndPoint(0).X, curve.GetEndPoint(0).Y, 0),
                    //        new XYZ(curve.GetEndPoint(1).X, curve.GetEndPoint(1).Y, 0)
                    //    );
                    //Line XLine = DhhGeometryUtils.LineToXLine(line);
                    double v = 0;
                    if (Math.Round(check) == 0) { v = origin.X - pickPoint.X; }
                    else { v = origin.Y - pickPoint.Y; }
                    tabledata_Draft.Add(DhhUnitUtils.FeetToMeter(v).ToString()); // Lưu ý xứ lý số liệu việc làm tròn
                    tabledata_Draft.Add("");
                    tabledata_Draft.Add("");
                    tabledata_Draft.Add("");
                    tabledata_Draft.Add("");
                    tabledata_Draft.Add("");
                    if (Math.Round(check) == 0) { tabledata_Draft.Add("End"); }
                    else { tabledata_Draft.Add("Start"); }
                    tabledata_Draft.Add("Yes");
                    //MessageBox.Show($"{name} + {check} + {origin.Y}");
                }
                TableDataGrid = tabledata_Draft.ToArray();
                MessageBox.Show(grids.Count.ToString());
                MySapModel.InitializeNewModel(eUnits.kN_m_C);
                MySapModel.File.NewBlank();
                string GridSysName = "G1";
                MySapModel.GridSys.SetGridSys(GridSysName, 0, 0, 0);
                int TableVersion = 0;
                string[] FieldsKeysIncluded = { "Name", "Grid Line Type", "ID", "Ordinate", "Angle", "X1", "Y1", "X2", "Y2", "Bubble Location", "Visible" };
                int NumberRecord = 0;
                bool FillImportLog = true;
                int NumFatalErrors = 0;
                int NumErrorMsgs = 0;
                int NumWarnMsgs = 0;
                int NumInfoMsgs = 0;
                string ImportLog = null;

                #endregion

                #region Level
                FilteredElementCollector elementsLevel = new FilteredElementCollector(uIDocument.Document);
                List<Level> levels = elementsLevel.OfCategory(BuiltInCategory.OST_Levels).OfClass(typeof(Level)).Cast<Level>().ToList();
                List<Level> levelSort = levels.OrderBy(x => x.Elevation).ToList();
                List<string> StoryNameList = new List<string>();
                List<double> StoryHeightList = new List<double>();
                List<bool> IsMaterStoryList = new List<bool>();
                List<string> IsSimilarToStoryList = new List<string>();
                List<bool> SpliceAboveList = new List<bool>();
                List<double> SpliceHeightList = new List<double>();
                List<int> ColorList = new List<int>();
                double firstHeight = 1;
                for (int i = 0; i < levelSort.Count; i++)
                {
                    StoryNameList.Add(levelSort[i].Name);
                    if (i == 0) { StoryHeightList.Add(firstHeight); }
                    else { StoryHeightList.Add(DhhUnitUtils.FeetToMeter(levelSort[i].Elevation - levelSort[i - 1].Elevation)); }
                    IsMaterStoryList.Add(false);
                    IsSimilarToStoryList.Add("None");
                    SpliceAboveList.Add(false);
                    SpliceHeightList.Add(0);
                    ColorList.Add(0);
                }
                string[] StoryName = StoryNameList.ToArray();
                double[] StoryHeight = StoryHeightList.ToArray();
                bool[] isMaterStory = IsMaterStoryList.ToArray();
                string[] IsSimilarToStory = IsSimilarToStoryList.ToArray();
                bool[] SpliceAbove = SpliceAboveList.ToArray();
                double[] SpliceHeight = SpliceHeightList.ToArray();
                int[] Color = ColorList.ToArray();
                MySapModel.Story.SetStories_2(DhhUnitUtils.FeetToMeter(levelSort[0].Elevation) - firstHeight, levelSort.Count, ref StoryName, ref StoryHeight, ref isMaterStory, ref IsSimilarToStory, ref SpliceAbove, ref SpliceHeight, ref Color);
                MySapModel.DatabaseTables.SetTableForEditingArray("Grid Definitions - Grid Lines", ref TableVersion, ref FieldsKeysIncluded, NumberRecord, ref TableDataGrid);
                MySapModel.DatabaseTables.ApplyEditedTables(FillImportLog, ref NumFatalErrors, ref NumErrorMsgs, ref NumWarnMsgs, ref NumInfoMsgs, ref ImportLog);
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
