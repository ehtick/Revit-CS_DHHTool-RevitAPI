using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BIMSoftLib.MVVM;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using DHHTools;

namespace _06_04_RS2D_RebarSlab2D.Object
{
    public class DetailItemInfor: PropertyChangedBase
    {
        #region Thông tin Family Thép sàn
        private Element _detailItem;
        public Element DetailItem
        {
            get => _detailItem;
            set
            {
                _detailItem = value;
                OnPropertyChanged(nameof(DetailItem));
            }
        }
        
        private int _duongKinh;
        public int DuongKinh
        {
            get => _duongKinh;
            set
            {
                _duongKinh = value;
                OnPropertyChanged(nameof(DuongKinh));
            }
        }
        
        private string _soHieu;
        public string SoHieu
        {
            get => _soHieu;
            set
            {
                _soHieu = value;
                OnPropertyChanged(nameof(SoHieu));
            }
        }

        private string _khoangCach;
        public string KhoangCach
        {
            get => _khoangCach;
            set
            {
                _khoangCach = value;
                OnPropertyChanged(nameof(KhoangCach));
            }
        }

        private int _soLuong;
        public int Soluong
        {
            get => _soLuong;
            set
            {
                _soLuong = value;
                OnPropertyChanged(nameof(Soluong));
            }
        }

        private double _raithepTrai;
        public double RaiThepTrai
        {
            get => _raithepTrai;
            set
            {
                _raithepTrai = value;
                OnPropertyChanged(nameof(RaiThepTrai));
            }
        }

        private double _raithepPhai;
        public double RaiThepPhai
        {
            get => _raithepPhai;
            set
            {
                _raithepPhai = value;
                OnPropertyChanged(nameof(RaiThepPhai));
            }
        }

        private double _tongKhoangRai;
        public double TongkhoangRai
        {
            get => _tongKhoangRai;
            set
            {
                _tongKhoangRai = value;
                OnPropertyChanged(nameof(TongkhoangRai));
            }
        }

        #endregion

        public DetailItemInfor(FamilyInstance element)
        {
            DetailItem = element;
            Parameter dk_par = element.LookupParameter("TS_DuongKinh");
            DuongKinh = (int)dk_par.AsInteger();
            Parameter sh_par = element.LookupParameter("TS_SoHieu");
            SoHieu = sh_par.AsString();
            Parameter kc_par = element.LookupParameter("TS_KhoangCach");
            KhoangCach = kc_par.AsString();
            Parameter raiTrai_par = element.LookupParameter("CR_RaiThep_Trai");
            RaiThepTrai = DhhUnitUtils.FeetToMm(raiTrai_par.AsDouble());
            Parameter raiPhai_par = element.LookupParameter("CR_RaiThep_Phai");
            RaiThepTrai = DhhUnitUtils.FeetToMm(raiPhai_par.AsDouble());
            TongkhoangRai = RaiThepPhai + RaiThepTrai;
            Soluong = (int)Math.Floor(TongkhoangRai / (Convert.ToDouble(KhoangCach))) + 1;
        }
    }
}
