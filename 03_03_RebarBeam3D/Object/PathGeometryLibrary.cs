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
        public static PathGeometry GetFootPath(double Width, double Height, double WPedestal, double HPedestal, double WDiferrent, double HDiferrent)
        {
            PathGeometry footPath = new PathGeometry();

            PathFigure pthFigure = new PathFigure();
            pthFigure.StartPoint = new System.Windows.Point(0, 0);// starting cordinates of arcs
            LineSegment lineSegA = new LineSegment();
            lineSegA.Point = new System.Windows.Point((WDiferrent - WPedestal / 2), (HDiferrent - HPedestal / 2));
            LineSegment lineSegAB = new LineSegment();
            lineSegAB.Point = new System.Windows.Point((WDiferrent + WPedestal / 2), (HDiferrent - HPedestal / 2));
            //LineSegment lineSegBConer = new LineSegment();
            //lineSegBConer.Point = new System.Windows.Point(Width, 0);
            //LineSegment lineSegCConer = new LineSegment();
            //lineSegCConer.Point = new System.Windows.Point(Width, Height);
            //LineSegment lineSegC = new LineSegment();
            //lineSegC.Point = new System.Windows.Point((WDiferrent + WPedestal / 2), (HDiferrent + HPedestal / 2));
            //LineSegment lineSegCB = new LineSegment();
            //lineSegCB.Point = new System.Windows.Point((WDiferrent + WPedestal / 2), (HDiferrent - HPedestal / 2));
            //LineSegment lineSegBA = new LineSegment();
            //lineSegBA.Point = new System.Windows.Point((WDiferrent - WPedestal / 2), (HDiferrent - HPedestal / 2));
            //LineSegment lineSegD = new LineSegment();
            //lineSegD.Point = new System.Windows.Point((WDiferrent - WPedestal / 2), (HDiferrent + HPedestal / 2));
            //LineSegment lineSegDConer = new LineSegment();
            //lineSegDConer.Point = new System.Windows.Point(0, Height);
            //LineSegment lineSegCD = new LineSegment();
            //lineSegCD.Point = new System.Windows.Point((WDiferrent - WPedestal / 2), (HDiferrent + HPedestal / 2));
            PathSegmentCollection myPathSegmentCollection = new PathSegmentCollection();
            myPathSegmentCollection.Add(lineSegA);
            myPathSegmentCollection.Add(lineSegAB);
            //myPathSegmentCollection.Add(lineSegBConer);
            //myPathSegmentCollection.Add(lineSegCConer);
            //myPathSegmentCollection.Add(lineSegC);
            //myPathSegmentCollection.Add(lineSegCB);
            //myPathSegmentCollection.Add(lineSegBA);
            //myPathSegmentCollection.Add(lineSegD);
            //myPathSegmentCollection.Add(lineSegDConer);
            //myPathSegmentCollection.Add(lineSegCConer);
            //myPathSegmentCollection.Add(lineSegC);
            //myPathSegmentCollection.Add(lineSegCD);
            pthFigure.Segments = myPathSegmentCollection;
            PathFigureCollection pthFigureCollection = new PathFigureCollection();
            pthFigureCollection.Add(pthFigure);
            footPath.Figures = pthFigureCollection;

            return footPath;
        }

        public static Path GetFootPathFromGeometry(PathGeometry opthGeometry, double Width, double Height)
        {
            Path path = new Path();
            path.Data = opthGeometry;

            ScaleTransform hStransform = new ScaleTransform(1, -1);
            TranslateTransform hMtransform = new TranslateTransform(0, Height);
            TransformGroup htransform = new TransformGroup();
            htransform.Children.Add(hStransform);
            htransform.Children.Add(hMtransform);


            ScaleTransform vStransform = new ScaleTransform(-1, 1);
            TranslateTransform vMtransform = new TranslateTransform(Width, 0);
            TransformGroup vtransform = new TransformGroup();
            vtransform.Children.Add(vStransform);
            vtransform.Children.Add(vMtransform);

            PathGeometry vhpathGeometry = new PathGeometry();
            ScaleTransform vhStransform = new ScaleTransform(-1, -1);
            TranslateTransform vhMtransform = new TranslateTransform(Width, Height);
            TransformGroup vhtransform = new TransformGroup();
            vhtransform.Children.Add(vhStransform);
            vhtransform.Children.Add(vhMtransform);
            path.RenderTransform = htransform;

            //if ((element as FamilyInstance).FacingFlipped == true && (element as FamilyInstance).HandFlipped == false)
            //{ path.RenderTransform = htransform; }
            //else if ((element as FamilyInstance).FacingFlipped == false && (element as FamilyInstance).HandFlipped == true)
            //{ path.RenderTransform = vtransform; }
            //else if ((element as FamilyInstance).FacingFlipped == true && (element as FamilyInstance).HandFlipped == true)
            //{ path.RenderTransform = vhtransform; }
            return path;
        }

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
