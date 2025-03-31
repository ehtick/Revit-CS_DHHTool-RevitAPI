using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.ApplicationServices;

namespace _00_02.ChatbotAI
{
    public class DrawCommands
    {
        public static void DrawWall(ExternalCommandData commandData, double width, double length)
        {
            UIApplication uiApp = commandData.Application;
            UIDocument uidoc = uiApp.ActiveUIDocument;
            Document doc = uidoc.Document;

            using (Transaction trans = new Transaction(doc, "Vẽ tường"))
            {
                trans.Start();

                // Tạo đường tham chiếu để vẽ tường
                XYZ start = new XYZ(0, 0, 0);
                XYZ end = new XYZ(length / 304.8, 0, 0); // Chuyển từ mm sang feet
                Line wallLine = Line.CreateBound(start, end);

                // Chọn loại tường mặc định
                WallType wallType = new FilteredElementCollector(doc)
                    .OfClass(typeof(WallType))
                    .FirstElement() as WallType;

                // Tạo tường mới
                Wall newWall = Wall.Create(doc, (IList<Curve>)wallLine, wallType.Id, doc.ActiveView.GenLevel.Id, false);

                // Đặt chiều rộng
                Parameter widthParam = newWall.get_Parameter(BuiltInParameter.WALL_ATTR_WIDTH_PARAM);
                widthParam.Set(width / 304.8);

                trans.Commit();
            }
        }
    }
}
