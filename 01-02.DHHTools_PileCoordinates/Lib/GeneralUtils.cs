#region Namespaces

using Autodesk.Revit.DB;

#endregion

namespace DHHTools
{
    public static class GeneralUtils
    {
        public static void SetWorkPlane(Document doc)
        {
            Plane workPlane =
                Plane.CreateByNormalAndOrigin(doc.ActiveView.ViewDirection, doc.ActiveView.Origin);
            SketchPlane sketchPlane = SketchPlane.Create(doc, workPlane);

            doc.ActiveView.SketchPlane = sketchPlane;
        }
    }
}
