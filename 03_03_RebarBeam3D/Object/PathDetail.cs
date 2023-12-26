using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Shapes;
using BIMSoftLib.MVVM;

namespace DHHTools.Object
{
    public class PathDetail: PropertyChangedBase
    {
        private PathGeometry _geometry;
        public PathGeometry Geometry
        {
            get => _geometry;
            set
            {
                _geometry = value;
                OnPropertyChanged(nameof(Geometry));
            }
        }
        private double _TopSet;
        public double TopSet
        {
            get => _TopSet;
            set
            {
                _TopSet = value;
                OnPropertyChanged(nameof(TopSet));
            }
        }
        private double _LeftSet;
        public double LeftSet
        {
            get => _LeftSet;
            set
            {
                _LeftSet = value;
                OnPropertyChanged(nameof(LeftSet));
            }
        }
    }
}
