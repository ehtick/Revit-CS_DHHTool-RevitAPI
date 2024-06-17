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

                XYZ startPoint = uIDocument.Selection.PickPoint();
                FilteredElementCollector elements = new FilteredElementCollector(uIDocument.Document);
                List<Grid> grids = elements.OfCategory(BuiltInCategory.OST_Grids).OfClass(typeof(Grid)).Cast<Grid>().ToList();
                foreach (Grid grid in grids)
                {
                    string name = grid.Name;
                    Curve curve = grid.Curve;
                    XYZ origin = (curve as Line).Origin;
                    XYZ direction = (curve as Line).Direction;
                    bool check = DhhGeometryUtils.IsVectorParallel(direction, XYZ.BasisX);
                }    
                MessageBox.Show(grids.Count.ToString());
                #region ETABS
                //MySapModel.InitializeNewModel(eUnits.kN_m_C);
                //MySapModel.File.NewBlank();
                //string GridSysName = "G1";
                //MySapModel.GridSys.SetGridSys(GridSysName, 0, 0, 0);

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

                //int TableVersion = 0;
                //int NumberRecord = 4;
                //string[] FieldsKeysIncluded = { "Name", "Grid Line Type", "ID", "Ordinate", "Angle", "X1", "Y1", "X2", "Y2", "Bubble Location", "Visible" };
                //string[] FieldKeysIncludedLevel = { "Tower", "Name", "Height", "Master Story", "Similar To", "Splice Story", "Color", "GUID" };
                //bool FillImportLog = true;
                //int NumFatalErrors = 0;
                //int NumErrorMsgs = 0;
                //int NumWarnMsgs = 0;
                //int NumInfoMsgs = 0;
                //string ImportLog = null;
                //string[] TableDataGrid =
                //{
                //    "G1", "X (Cartesian)", "A","0","","","","","","End","Yes",
                //    "G1", "X (Cartesian)", "B","5","","","","","","End","Yes",
                //    "G1", "Y (Cartesian)", "1","0","","","","","","Start","Yes",
                //    "G1", "Y (Cartesian)", "2","10","","","","","","Start","Yes",

                //};
                //string[] StoryName = { "L1", "L2", "L3", "L4" };
                //double[] StoryHeight = { 3, 4, 5, 6 };
                //bool[] isMaterStory = { false, false, true, false };
                //string[] IsSimilarToStory = { "None", "None", "None", "None" };
                //bool[] SpliceAbove = { false, false, false, false };
                //double[] SpliceHeight = { 0, 0, 0, 0 };
                //int[] ColorList = { 0, 0, 0, 0 };
                ////Story Definitions {Key: "Tower", "Story", "Height", "IsMaster", "SimilarTo", "IsSpliced", "Color", "GUID" }
                ////Story Definitions {Name: "Tower", "Name", "Height", "Master Story", "Similar To", "Splice Story", "Color", "GUID" }
                ////MySapModel.DatabaseTables.GetAllFieldsInTable("Story Definitions", ref TableVersion, ref NumberFields, ref FieldKey, ref FieldName, ref Description, ref UnitsString, ref IsImporttable);
                ////MySapModel.DatabaseTables.GetAllTables(ref NumberTables, ref TableKey, ref TableName, ref ImportType, ref IsEmpty);
                //MySapModel.DatabaseTables.SetTableForEditingArray("Grid Definitions - Grid Lines", ref TableVersion, ref FieldsKeysIncluded, NumberRecord, ref TableDataGrid);
                //MySapModel.DatabaseTables.ApplyEditedTables(FillImportLog, ref NumFatalErrors, ref NumErrorMsgs, ref NumWarnMsgs, ref NumInfoMsgs, ref ImportLog);
                //MySapModel.Story.SetStories_2(-2, 4, ref StoryName, ref StoryHeight, ref isMaterStory, ref IsSimilarToStory, ref SpliceAbove, ref SpliceHeight, ref ColorList);
                #endregion

            }
            catch
            {
                MessageBox.Show("Open Etabs");
            }
        }
    }
}
