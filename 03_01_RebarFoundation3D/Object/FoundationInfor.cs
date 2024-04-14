using Autodesk.Revit.DB;
using BIMSoftLib.MVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DHHTools.Object
{
    public class FoundationInfor: PropertyChangedBase
    {
        private Element _foundation;
        public Element Foundation
        {
            get => _foundation;
            set
            {
                _foundation = value;
                OnPropertyChanged(nameof(Foundation));
            }
        }
        private string _foundationName;
        public string FoundationName
        {
            get => _foundationName;
            set
            {
                _foundationName = value;
                OnPropertyChanged(nameof(FoundationName));
            }
        }
        private double _foundationWidth;
        public double FoundationWidth
        {
            get => _foundationWidth;
            set
            {
                _foundationWidth = value;
                OnPropertyChanged(nameof(FoundationWidth));
            }
        }
        private double _foundationLength;
        public double FoundationLength
        {
            get => _foundationLength;
            set
            {
                _foundationLength = value;
                OnPropertyChanged(nameof(FoundationLength));
            }
        }
        private double _foundationHeight;
        public double FoundationHeight
        {
            get => _foundationHeight;
            set
            {
                _foundationHeight = value;
                OnPropertyChanged(nameof(FoundationHeight));
            }
        }
        private double _foundationHeightSub;
        public double FoundationHeightSub
        {
            get => _foundationHeightSub;
            set
            {
                _foundationHeightSub = value;
                OnPropertyChanged(nameof(FoundationHeightSub));
            }
        }
        #region Thép lớp dưới
        private double _diaRebarBotX;
        public double DiaRebarBotX
        {
            get => _diaRebarBotX;
            set
            {
                _diaRebarBotX = value;
                OnPropertyChanged(nameof(DiaRebarBotX));
            }
        }
        private double _spaRebarBotX;
        public double SpaRebarBotX
        {
            get => _spaRebarBotX;
            set
            {
                _spaRebarBotX = value;
                OnPropertyChanged(nameof(SpaRebarBotX));
            }
        }
        private double _diarebarBotY;
        public double DiaRebarBotY
        {
            get => _diarebarBotY;
            set
            {
                _diarebarBotY = value;
                OnPropertyChanged(nameof(DiaRebarBotY));
            }
        }
        private double _spaRebarBotY;
        public double SpaRebarBotY
        {
            get => _spaRebarBotY;
            set
            {
                _spaRebarBotY = value;
                OnPropertyChanged(nameof(SpaRebarBotY));
            }
        }
        #endregion
        #region Thép lớp trên
        private bool _isRebarTop;
        public bool IsRebarTop
        {
            get => _isRebarTop;
            set
            {
                _isRebarTop = value;
                OnPropertyChanged(nameof(IsRebarTop));
            }
        }
        private double _diarebarTopX;
        public double DiaRebarTopX
        {
            get => _diarebarTopX;
            set
            {
                _diarebarTopX = value;
                OnPropertyChanged(nameof(DiaRebarTopX));
            }
        }
        private double _sparebarTopX;
        public double SpaRebarTopX
        {
            get => _sparebarTopX;
            set
            {
                _sparebarTopX = value;
                OnPropertyChanged(nameof(SpaRebarTopX));
            }
        }
        private double _diarebarTopY;
        public double DiaRebarTopY
        {
            get => _diarebarTopY;
            set
            {
                _diarebarTopY = value;
                OnPropertyChanged(nameof(DiaRebarTopY));
            }
        }
        private double _sparebarTopY;
        public double SpaRebarTopY
        {
            get => _sparebarTopY;
            set
            {
                _sparebarTopY = value;
                OnPropertyChanged(nameof(SpaRebarTopY));
            }
        }
        #endregion
        public FoundationInfor(FamilyInstance element)
        {
            Foundation = element;
            Parameter name_par = element.LookupParameter("Mark");
            FoundationName = name_par.AsString();
            Parameter b_par = element.LookupParameter("ChieuRong_Mong");
            FoundationWidth = DhhUnitUtils.FeetToMm(b_par.AsDouble());
            Parameter l_par = element.LookupParameter("ChieuDai_Mong");
            FoundationLength = DhhUnitUtils.FeetToMm(l_par.AsDouble());
            Parameter h_par = element.LookupParameter("Dinh_Mong_Cao");
            FoundationHeight = DhhUnitUtils.FeetToMm(h_par.AsDouble());
            Parameter hSub_par = element.LookupParameter("Dai_Cao");
            FoundationHeightSub = DhhUnitUtils.FeetToMm(hSub_par.AsDouble());
            DiaRebarBotX = 12;
            SpaRebarBotX = 150;
            DiaRebarBotY = 12;
            SpaRebarBotY = 150;
            IsRebarTop = false;
            DiaRebarTopX = 10;
            SpaRebarTopX = 300;
            DiaRebarTopY = 10;
            SpaRebarTopY = 300;

        }
    }
}
