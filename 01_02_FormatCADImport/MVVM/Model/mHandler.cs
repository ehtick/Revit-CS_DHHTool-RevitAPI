using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using BIMSoftLib.MVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace _01_02_FormatCADImport.MVVM.Model
{
    public class mHandler : IExternalEventHandler
    {
        public static ObservableRangeCollection<mImportInstancePlus> DgSelectedImportCAD = new ObservableRangeCollection<mImportInstancePlus>();
        public static ObservableRangeCollection<mCategoryPlus> DgCategory = new ObservableRangeCollection<mCategoryPlus>();
        public static ExternalEvent mHandlerEvent = null;
        public static int LoadValue = 0;


        void IExternalEventHandler.Execute(UIApplication app)
        {
            try
            {
                UIDocument uiDoc = app.ActiveUIDocument;
                Document document = uiDoc.Document;
                ObservableRangeCollection<mCategoryPlus> CategoryPlusListChange = new ObservableRangeCollection<mCategoryPlus>();
                ObservableRangeCollection<string> CategoryListChange = new ObservableRangeCollection<string>();
                //Lấy ra các Layer có thay đổi trên Tất cả các Layer
                foreach (mCategoryPlus category in DgCategory)
                {
                    if (category.LinePatternSelect != "<No Override>" || category.LineWeightSelect != "<No Override>")
                    {
                        CategoryPlusListChange.Add(category);
                        CategoryListChange.Add(category.Category.Name);
                    }
                }
                using (Transaction tran = new Transaction(document))
                {
                    tran.Start("Set AutoCAD Layer");
                    //Kiểm tra trong file CAD, Đối chiếu với list Layer ở trên
                    foreach (mImportInstancePlus instancePlus in DgSelectedImportCAD)
                    {
                        ImportInstance importInstance = instancePlus.ImportInstance;
                        CategoryNameMap layers = importInstance.Category.SubCategories;
                        foreach (var layer in layers)
                        {
                            LoadValue += 1;
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
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }

        string IExternalEventHandler.GetName()
        {
            return "mHandlerEvent";
        }

    }
}
