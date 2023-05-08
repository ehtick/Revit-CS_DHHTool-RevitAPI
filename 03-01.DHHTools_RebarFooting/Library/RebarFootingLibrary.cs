using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace DHHTools.Library
{
    class RebarFootingLibrary
    {
        public static PathGeometry GetSingleFootingPath(double Width, double Height, double WPedestal, double HPedestal, double WDiferrent, double HDiferrent, double scale)
        {
            PathGeometry stirrupPath = new PathGeometry();
            PathFigure pthFigure = new PathFigure();
            pthFigure.StartPoint = new System.Windows.Point(0, 0);// starting cordinates of arcs
            LineSegment lineSegA = new LineSegment();
            lineSegA.Point = new System.Windows.Point((WDiferrent - WPedestal/2) * scale, (HDiferrent - HPedestal/2)*scale);
            LineSegment lineSegAB = new LineSegment();
            lineSegAB.Point = new System.Windows.Point((WDiferrent + WPedestal / 2) * scale, (HDiferrent - HPedestal / 2) * scale);
            LineSegment lineSegB = new LineSegment();
            lineSegB.Point = new System.Windows.Point(Width * scale, 0) ;
            PathSegmentCollection myPathSegmentCollection = new PathSegmentCollection();
            myPathSegmentCollection.Add(lineSegA);
            myPathSegmentCollection.Add(arcSegA);
            myPathSegmentCollection.Add(lineSeg1);
            myPathSegmentCollection.Add(arcSegB);
            myPathSegmentCollection.Add(lineSeg2);
            myPathSegmentCollection.Add(arcSegC);
            myPathSegmentCollection.Add(lineSeg3);
            myPathSegmentCollection.Add(arcSegD);
            myPathSegmentCollection.Add(lineSeg4);
            myPathSegmentCollection.Add(arcSegA);
            myPathSegmentCollection.Add(lineSegB);
            pthFigure.Segments = myPathSegmentCollection;
            PathFigureCollection pthFigureCollection = new PathFigureCollection();
            pthFigureCollection.Add(pthFigure);
            stirrupPath.Figures = pthFigureCollection;

            return stirrupPath;
        }
    }
}
