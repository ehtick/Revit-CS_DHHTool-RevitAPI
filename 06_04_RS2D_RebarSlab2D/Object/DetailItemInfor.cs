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

        private double _d1;
        public double D1
        {
            get => _d1;
            set
            {
                _d1 = value;
                OnPropertyChanged(nameof(D1));
            }
        }


        private string _hdThep;
        public string HDThep
        {
            get => _hdThep;
            set
            {
                _hdThep = value;
                OnPropertyChanged(nameof(HDThep));
            }
        }
        #endregion


        public DetailItemInfor(FamilyInstance element)
        {
            DetailItem = element; 
            Parameter len_Para = element.LookupParameter("Length");
            D1 = DhhUnitUtils.FeetToMm(len_Para.AsDouble());
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
            RaiThepTrai = DhhUnitUtils.FeetToMm(raiTrai_par.AsDouble());  
            
            Parameter TsTop_par = element.LookupParameter("TS_Top");
            int TsTop = TsTop_par.AsInteger();
            Parameter TsAdd_par = element.LookupParameter("TS_Add");
            int TsAdd = TsAdd_par.AsInteger();
            Parameter TsAdd1Side_par = element.LookupParameter("TS_Add1Side");
            int TsAdd1Side = TsAdd1Side_par.AsInteger();
            Parameter TsBotUni8_par = element.LookupParameter("TS_Bot_Uni8");
            int TsBotUni8 = TsBotUni8_par.AsInteger();
            string tag = TsTop.ToString() + TsAdd.ToString() + TsAdd1Side.ToString() + TsBotUni8.ToString();
            switch(tag)
            {
                //Top
                case "1000": //Top Uniform
                    HDThep = "TT_TK1";
                    break;
                case "1100": //Top Add 
                    HDThep = "TT_TK2";
                    break;
                case "1010": //Top Add 1 Side
                    HDThep = "TT_TK1";
                    break;

                //Bottom
                case "0000": //Bot Uniform
                    HDThep = "TT_TK1";
                    break;
                case "0001": //Bot Uniform 8
                    HDThep = "TT_TK4";
                    break;
                case "0100": //Bot Add
                    HDThep = "TT_TK2";
                    break;
                case "0010": //Bot Uniform
                    HDThep = "TT_TK1";
                    break;
                case "0011": //Bot Uniform 8
                    HDThep = "TT_TK1";
                    break;
                case "0110": //Bot Add
                    HDThep = "TT_TK2";
                    break;
                default:
                    HDThep = "TT_TK2";
                    break;
            }    

        }
    }
}
