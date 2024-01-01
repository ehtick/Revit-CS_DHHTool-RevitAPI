using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Shapes;
using LineSegment = System.Windows.Media.LineSegment;

namespace DHHTools.Object
{ 
    class PathGeometryLibrary
    {

        public static PathGeometry GetBeamPath(double Length, double Height, double Scale)
        {
            PathGeometry BeamPath = new PathGeometry();
            PathFigure pthFigure = new PathFigure();
            pthFigure.StartPoint = new System.Windows.Point(0, 0);// starting cordinates of arcs => đi từ trên xuống, trái qua phải
            LineSegment lineSegTop = new LineSegment();
            lineSegTop.Point = new System.Windows.Point(Length*Scale, 0); // cạnh trên
            LineSegment lineSegRight = new LineSegment();
            lineSegRight.Point = new System.Windows.Point(Length * Scale, Height * Scale);// cạnh phải; - là đi lên, + là đi xuống
            lineSegRight.IsStroked = false;
            LineSegment lineSegBottom = new LineSegment();
            lineSegBottom.Point = new System.Windows.Point(0, Height * Scale);// cạnh dưới; - là đi lên, + là đi xuống
            LineSegment lineSegLeft = new LineSegment();
            lineSegLeft.Point = new System.Windows.Point(0, 0);// cạnh trái;
            lineSegLeft.IsStroked = false;
            PathSegmentCollection myPathSegmentCollection = new PathSegmentCollection();
            myPathSegmentCollection.Add(lineSegTop);
            myPathSegmentCollection.Add(lineSegRight);
            myPathSegmentCollection.Add(lineSegBottom);
            myPathSegmentCollection.Add(lineSegLeft);
            pthFigure.Segments = myPathSegmentCollection;
            PathFigureCollection pthFigureCollection = new PathFigureCollection();
            pthFigureCollection.Add(pthFigure);
            BeamPath.Figures = pthFigureCollection;
            return BeamPath;
        }

        public static PathGeometry GetStartColumnPath(double Width, double HeightBeam, double Scale)
        {
            PathGeometry startColumnPath = new PathGeometry();
            PathFigure pthFigure = new PathFigure();
            pthFigure.StartPoint = new System.Windows.Point(0, 0);// starting cordinates of arcs => đi từ trên xuống, trái qua phải

            LineSegment lineSegTop = new LineSegment();
            lineSegTop.Point = new System.Windows.Point(Width * Scale, 0); // cạnh trên

            LineSegment lineSegRightTop = new LineSegment();
            lineSegRightTop.Point = new System.Windows.Point(Width * Scale, HeightBeam*0.5 * Scale);// cạnh phải trên dầm

            LineSegment lineSegRightBeam = new LineSegment();
            lineSegRightBeam.Point = new System.Windows.Point(Width * Scale, (HeightBeam * 0.5 + HeightBeam) * Scale);// cạnh phải ngay dầm
            lineSegRightBeam.IsStroked = false;

            LineSegment lineSegRightBot = new LineSegment();
            lineSegRightBot.Point = new System.Windows.Point(Width * Scale, (2*HeightBeam * 0.5 + HeightBeam) * Scale);// cạnh phải dưới dầm

            LineSegment lineSegBottom = new LineSegment();
            lineSegBottom.Point = new System.Windows.Point(0, (2 * HeightBeam * 0.5 + HeightBeam) * Scale); // cạnh dưới

            LineSegment lineSegLeft = new LineSegment();
            lineSegLeft.Point = new System.Windows.Point(0, 0); // cạnh trái

            PathSegmentCollection myPathSegmentCollection = new PathSegmentCollection();
            myPathSegmentCollection.Add(lineSegTop);
            myPathSegmentCollection.Add(lineSegRightTop);
            myPathSegmentCollection.Add(lineSegRightBeam);
            myPathSegmentCollection.Add(lineSegRightBot);
            myPathSegmentCollection.Add(lineSegBottom);
            myPathSegmentCollection.Add(lineSegLeft);
            pthFigure.Segments = myPathSegmentCollection;
            PathFigureCollection pthFigureCollection = new PathFigureCollection();
            pthFigureCollection.Add(pthFigure);
            startColumnPath.Figures = pthFigureCollection;
            return startColumnPath;
        }

        public static PathGeometry GetMiddleColumnPath(double Width, double HeightBeam, double Scale)
        {
            PathGeometry middleColumnPath = new PathGeometry();
            PathFigure pthFigure = new PathFigure();
            pthFigure.StartPoint = new System.Windows.Point(0, 0);// starting cordinates of arcs => đi từ trên xuống, trái qua phải

            LineSegment lineSegTop = new LineSegment();
            lineSegTop.Point = new System.Windows.Point(Width * Scale, 0); // cạnh trên

            LineSegment lineSegRightTop = new LineSegment();
            lineSegRightTop.Point = new System.Windows.Point(Width * Scale, HeightBeam * 0.5 * Scale);// cạnh phải trên dầm

            LineSegment lineSegRightBeam = new LineSegment();
            lineSegRightBeam.Point = new System.Windows.Point(Width * Scale, (HeightBeam * 0.5 + HeightBeam) * Scale);// cạnh phải ngay dầm
            lineSegRightBeam.IsStroked = false;

            LineSegment lineSegRightBot = new LineSegment();
            lineSegRightBot.Point = new System.Windows.Point(Width * Scale, (2 * HeightBeam * 0.5 + HeightBeam) * Scale);// cạnh phải dưới dầm

            LineSegment lineSegBottom = new LineSegment();
            lineSegBottom.Point = new System.Windows.Point(0, (2 * HeightBeam * 0.5 + HeightBeam) * Scale); // cạnh dưới

            LineSegment lineSegLeftBot = new LineSegment();
            lineSegLeftBot.Point = new System.Windows.Point(0, (HeightBeam * 0.5 + HeightBeam) * Scale);// cạnh trái dưới dầm

            LineSegment lineSegLeftBeam = new LineSegment();
            lineSegLeftBeam.Point = new System.Windows.Point(0, (HeightBeam * 0.5) * Scale);// cạnh trái ngay dầm
            lineSegLeftBeam.IsStroked = false;

            LineSegment lineSegLeftTop = new LineSegment();
            lineSegLeftTop.Point = new System.Windows.Point(0, 0); // cạnh trái trên dầm

            PathSegmentCollection myPathSegmentCollection = new PathSegmentCollection();
            myPathSegmentCollection.Add(lineSegTop);
            myPathSegmentCollection.Add(lineSegRightTop);
            myPathSegmentCollection.Add(lineSegRightBeam);
            myPathSegmentCollection.Add(lineSegRightBot);
            myPathSegmentCollection.Add(lineSegBottom);
            myPathSegmentCollection.Add(lineSegLeftBot);
            myPathSegmentCollection.Add(lineSegLeftBeam);
            myPathSegmentCollection.Add(lineSegLeftTop);
            pthFigure.Segments = myPathSegmentCollection;
            PathFigureCollection pthFigureCollection = new PathFigureCollection();
            pthFigureCollection.Add(pthFigure);
            middleColumnPath.Figures = pthFigureCollection;
            return middleColumnPath;
        }

        public static PathGeometry GetEndColumnPath(double Width, double HeightBeam, double Scale)
        {
            PathGeometry endColumnPath = new PathGeometry();
            PathFigure pthFigure = new PathFigure();
            pthFigure.StartPoint = new System.Windows.Point(0, 0);// starting cordinates of arcs => đi từ trên xuống, trái qua phải

            LineSegment lineSegTop = new LineSegment();
            lineSegTop.Point = new System.Windows.Point(Width * Scale, 0); // cạnh trên

            LineSegment lineSegRight = new LineSegment();
            lineSegRight.Point = new System.Windows.Point(Width * Scale, (2 * HeightBeam * 0.5 + HeightBeam) * Scale);// cạnh phải

            LineSegment lineSegBottom = new LineSegment();
            lineSegBottom.Point = new System.Windows.Point(0, (2 * HeightBeam * 0.5 + HeightBeam) * Scale); // cạnh dưới

            LineSegment lineSegLeftBot = new LineSegment();
            lineSegLeftBot.Point = new System.Windows.Point(0, (HeightBeam * 0.5 + HeightBeam) * Scale);// cạnh trái dưới dầm

            LineSegment lineSegLeftBeam = new LineSegment();
            lineSegLeftBeam.Point = new System.Windows.Point(0, (HeightBeam * 0.5) * Scale);// cạnh trái ngay dầm
            lineSegLeftBeam.IsStroked = false;

            LineSegment lineSegLeftTop = new LineSegment();
            lineSegLeftTop.Point = new System.Windows.Point(0, 0); // cạnh trái trên dầm

            PathSegmentCollection myPathSegmentCollection = new PathSegmentCollection();
            myPathSegmentCollection.Add(lineSegTop);
            myPathSegmentCollection.Add(lineSegRight);
            myPathSegmentCollection.Add(lineSegBottom);
            myPathSegmentCollection.Add(lineSegLeftBot);
            myPathSegmentCollection.Add(lineSegLeftBeam);
            myPathSegmentCollection.Add(lineSegLeftTop);

            pthFigure.Segments = myPathSegmentCollection;
            PathFigureCollection pthFigureCollection = new PathFigureCollection();
            pthFigureCollection.Add(pthFigure);
            endColumnPath.Figures = pthFigureCollection;
            return endColumnPath;
        }
    }
}
