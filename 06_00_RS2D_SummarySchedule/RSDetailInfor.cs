using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BIMSoftLib;
using BIMSoftLib.MVVM;

namespace _06_00_RS2D_SummarySchedule
{
    public class RSDetailInfor : PropertyChangedBase
    {
        public string DuongKinh { get; set; }
        public string TongChieuDai { get; set; }

    }
}
