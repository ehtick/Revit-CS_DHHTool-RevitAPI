using _01_02_FormatCADImport.MVVM.ViewModel;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Application = Autodesk.Revit.ApplicationServices.Application;
using BIMSoftLib.MVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;


namespace _01_02_FormatCADImport.MVVM.Model
{
    public class methodAddCADFile : PropertyChangedBase
    {
        public static ObservableRangeCollection<mImportInstancePlus> GetCADFile(Document doc)
        {
            ObservableRangeCollection<mImportInstancePlus> AllCADFile = new ObservableRangeCollection<mImportInstancePlus>();
            var importInstances = new FilteredElementCollector(doc)
                .OfClass(typeof(ImportInstance))
                .ToElements();
            if (importInstances.Count > 0)
            {
                foreach (var importinstance in importInstances)
                {
                    if (importinstance.Category.Name.Contains(".dwg") || importinstance.Category.Name.Contains(".DWG"))
                    {
                        mImportInstancePlus mImportInstancePlus = new mImportInstancePlus((ImportInstance)importinstance);
                        AllCADFile.Add(mImportInstancePlus);
                    }
                }
            }
            return AllCADFile;
        }
        public static ObservableRangeCollection<mCategoryPlus> GetcategoryUnique (ObservableRangeCollection<mImportInstancePlus> ListImportInstance, Document document)
        {
            ObservableRangeCollection<mCategoryPlus> AllCategory = new ObservableRangeCollection<mCategoryPlus>();
            List<mCategoryPlus> AllCategory_draft = new List<mCategoryPlus>();
            List<string> AllCategoryName_Draft = new List<string>();
            foreach (mImportInstancePlus instancePlus in ListImportInstance)
            {
                ImportInstance importInstance = instancePlus.ImportInstance;
                CategoryNameMap layers = importInstance.Category.SubCategories;
                foreach (var layer in layers)
                {
                    Category CategorylayerCAD = layer as Category;
                    mCategoryPlus layerCAD = new mCategoryPlus(CategorylayerCAD, document);
                    if (AllCategoryName_Draft.Contains(layerCAD.Name) == false)
                    {
                        AllCategoryName_Draft.Add(layerCAD.Name);
                        AllCategory_draft.Add(layerCAD); 
                    }
                    
                }
            }
            List<mCategoryPlus> AllCategory_DraftSort = AllCategory_draft.OrderBy(x => x.Name).ToList();
            foreach(mCategoryPlus category in AllCategory_DraftSort) { AllCategory.Add(category); }
            //MessageBox.Show(AllCategory.Count.ToString());
            return AllCategory;
        }
        public static ObservableRangeCollection<mCategoryPlus> Getcategory(ObservableRangeCollection<mImportInstancePlus> ListImportInstance, Document document)
        {
            ObservableRangeCollection<mCategoryPlus> AllCategory = new ObservableRangeCollection<mCategoryPlus>();
            foreach (mImportInstancePlus instancePlus in ListImportInstance)
            {
                ImportInstance importInstance = instancePlus.ImportInstance;
                CategoryNameMap layers = importInstance.Category.SubCategories;
                foreach (var layer in layers)
                {
                    Category CategorylayerCAD = layer as Category;
                    mCategoryPlus layerCAD = new mCategoryPlus(CategorylayerCAD, document);
                    AllCategory.Add(layerCAD);
                }
            }
            return AllCategory;
        }
        #region Backup
        public static void SetGraphicStyleForLayerCAD(Document document, ObservableRangeCollection<mImportInstancePlus> SelectedImportCADList, ObservableRangeCollection<mCategoryPlus> CategoryList)
        {
            ObservableRangeCollection<mCategoryPlus> CategoryPlusListChange = new ObservableRangeCollection<mCategoryPlus>();
            ObservableRangeCollection<string> CategoryListChange = new ObservableRangeCollection<string>();
            //Lấy ra các Layer có thay đổi trên Tất cả các Layer
            foreach (mCategoryPlus category in CategoryList)
            {
                if (category.LinePatternSelect != "<No Override>" || category.LineWeightSelect != "<No Override>")
                {
                    CategoryPlusListChange.Add(category);
                    CategoryListChange.Add(category.Category.Name);
                }
            }
            MessageBox.Show(CategoryListChange.Count.ToString());
            using (Transaction tran = new Transaction(document))
            {
                tran.Start("Set AutoCAD Layer");
                //Kiểm tra trong file CAD, Đối chiếu với list Layer ở trên
                foreach (mImportInstancePlus instancePlus in SelectedImportCADList)
                {
                    ImportInstance importInstance = instancePlus.ImportInstance;
                    CategoryNameMap layers = importInstance.Category.SubCategories;
                    foreach (var layer in layers)
                    {
                        Category CategorylayerCAD = layer as Category;

                        int indexofLayer = CategoryListChange.IndexOf(CategorylayerCAD.Name);
                        if (indexofLayer != -1)
                        {
                            string LineWeightSelect = CategoryPlusListChange[indexofLayer].LineWeightSelect;
                            string LinePatternSelect = CategoryPlusListChange[indexofLayer].LinePatternSelect;
                            //Set Line Weight
                            if (LineWeightSelect == "<No Override>") { continue; }
                            else { CategorylayerCAD.SetLineWeight(Int32.Parse(LineWeightSelect), GraphicsStyleType.Projection); }
                            //Set Line Pattern
                            if (LinePatternSelect == "<No Override>") { continue; }
                            else if (LinePatternSelect == "Solid")
                            {
                                ElementId solidLinePatternId = LinePatternElement.GetSolidPatternId();
                                CategorylayerCAD.SetLinePatternId(solidLinePatternId, GraphicsStyleType.Projection);
                            }
                            else
                            {
                                LinePatternElement linePatternElement = LinePatternElement.GetLinePatternElementByName(document, LinePatternSelect);
                                ElementId linePatternId = linePatternElement.Id;
                                CategorylayerCAD.SetLinePatternId(linePatternId, GraphicsStyleType.Projection);
                            }
                        }


                    }
                }
                tran.Commit();

            }
            MessageBox.Show($"Đã chỉnh sửa: {CategoryListChange.Count} layer");
        }
        #endregion

        public static void SelectAllCAD(ObservableRangeCollection<mImportInstancePlus> ListCADSelect)
        {
            foreach (mImportInstancePlus CadSelect in ListCADSelect)
            {
                CadSelect.IsCheck = true;
            }    
        }

        public static void SelectNoneCAD(ObservableRangeCollection<mImportInstancePlus> ListCADSelect)
        {
            foreach (mImportInstancePlus CadSelect in ListCADSelect)
            {
                CadSelect.IsCheck = false;
            }
        }
    }
}
