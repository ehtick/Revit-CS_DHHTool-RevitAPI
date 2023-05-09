using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Shapes;
using LineSegment = System.Windows.Media.LineSegment;

namespace DHHTools.Library
{
    class RebarFootingLibrary
    {
        public static PathGeometry GetSingleFootingPath(double Width, double Height, double WPedestal, double HPedestal, double WDiferrent, double HDiferrent)
        {
            PathGeometry footPath = new PathGeometry();
            
            PathFigure pthFigure = new PathFigure();
            pthFigure.StartPoint = new System.Windows.Point(0, 0);// starting cordinates of arcs
            LineSegment lineSegA = new LineSegment();
            lineSegA.Point = new System.Windows.Point((WDiferrent - WPedestal/2), (HDiferrent - HPedestal/2));
            LineSegment lineSegAB = new LineSegment();
            lineSegAB.Point = new System.Windows.Point((WDiferrent + WPedestal / 2) , (HDiferrent - HPedestal / 2));
            LineSegment lineSegBConer = new LineSegment();
            lineSegBConer.Point = new System.Windows.Point(Width  , 0) ;
            LineSegment lineSegCConer = new LineSegment();
            lineSegCConer.Point = new System.Windows.Point( Width, Height);
            LineSegment lineSegC = new LineSegment();
            lineSegC.Point = new System.Windows.Point((WDiferrent + WPedestal / 2), (HDiferrent + HPedestal / 2));
            LineSegment lineSegCB = new LineSegment();
            lineSegCB.Point = new System.Windows.Point((WDiferrent + WPedestal / 2), (HDiferrent - HPedestal / 2));
            LineSegment lineSegBA = new LineSegment();
            lineSegBA.Point = new System.Windows.Point((WDiferrent - WPedestal / 2), (HDiferrent - HPedestal / 2));
            LineSegment lineSegD = new LineSegment();
            lineSegD.Point = new System.Windows.Point((WDiferrent - WPedestal / 2), (HDiferrent + HPedestal / 2));
            LineSegment lineSegDConer = new LineSegment();
            lineSegDConer.Point = new System.Windows.Point(0, Height);
            LineSegment lineSegCD = new LineSegment();
            lineSegCD.Point = new System.Windows.Point((WDiferrent - WPedestal / 2), (HDiferrent + HPedestal / 2));
            PathSegmentCollection myPathSegmentCollection = new PathSegmentCollection();
            myPathSegmentCollection.Add(lineSegA);
            myPathSegmentCollection.Add(lineSegAB);
            myPathSegmentCollection.Add(lineSegBConer);
            myPathSegmentCollection.Add(lineSegCConer);
            myPathSegmentCollection.Add(lineSegC);
            myPathSegmentCollection.Add(lineSegCB);
            myPathSegmentCollection.Add(lineSegBA);
            myPathSegmentCollection.Add(lineSegD);
            myPathSegmentCollection.Add(lineSegDConer);
            myPathSegmentCollection.Add(lineSegCConer);
            myPathSegmentCollection.Add(lineSegC);
            myPathSegmentCollection.Add(lineSegCD);
            pthFigure.Segments = myPathSegmentCollection;
            PathFigureCollection pthFigureCollection = new PathFigureCollection();
            pthFigureCollection.Add(pthFigure);
            footPath.Figures = pthFigureCollection;
           
            return footPath;
        }

        public static Path GetFootPathFromGeometry(PathGeometry opthGeometry, Element element, double Width, double Height)
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


            if ((element as FamilyInstance).FacingFlipped == true && (element as FamilyInstance).HandFlipped == false)
            { path.RenderTransform = htransform; }
            else if ((element as FamilyInstance).FacingFlipped == false && (element as FamilyInstance).HandFlipped == true)
            { path.RenderTransform = vtransform; }
            else if ((element as FamilyInstance).FacingFlipped == true && (element as FamilyInstance).HandFlipped == true)
            { path.RenderTransform = vhtransform; }
            return path;
        }
    }
}
