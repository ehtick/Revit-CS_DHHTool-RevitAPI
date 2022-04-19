#region Namespaces

using System.Collections.Generic;
using System.Linq;
using Autodesk.Revit.DB;
using View = Autodesk.Revit.DB.View;

#endregion

namespace DHHTools
{
    public static class ElementUtils
    {
        /// <summary>
        /// Lấy về tất cả ViewType của các View có trên doc hoặc của selectedViews nếu isCurentSelected = true
        /// </summary>
        /// <param name="doc"></param>
        /// <param name="iscurentSelected"></param>
        /// <param name="selectedViews"></param>
        /// <returns></returns>
        public static List<Category> GetAllCategory(Document doc, bool iscurentSelected, List<Element> selectedElements)
        {
            List<Category> listCategories = new List<Category>();
            List<Element> listElements;
            if (iscurentSelected)
            {
                foreach (Element selectedElement in selectedElements)
                {
                    listCategories.Add(selectedElement.Category);
                }
            }
            else
            {
                listElements = new FilteredElementCollector(doc)
                            .WhereElementIsNotElementType()
                            .Cast<Element>()
                            .ToList();
                foreach (Element eElement in listElements)
                {
                    listCategories.Add(eElement.Category);
                }
            }
            listCategories.Distinct();
            listCategories.Sort();
            return listCategories ;
        }
        public static List<string> GetAllCategoryWithName(Document doc, bool iscurentSelected, List<Element> selectedElements)
        {
            List<string> listnameCategories = new List<string>();
            List<Element> listElements;
            if (iscurentSelected)
            {
                foreach (Element selectedElement in selectedElements)
                {
                    listnameCategories.Add(selectedElement.Category.Name);
                }
            }
            else
            {
                listElements = new FilteredElementCollector(doc)
                            .WhereElementIsNotElementType()
                            .Cast<Element>()
                            .ToList();
                foreach (Element eElement in listElements)
                {
                    listnameCategories.Add(eElement.Category.Name);
                }
            }
            listnameCategories.Distinct();
            listnameCategories.Sort();
            return listnameCategories;
        }
    }
}
