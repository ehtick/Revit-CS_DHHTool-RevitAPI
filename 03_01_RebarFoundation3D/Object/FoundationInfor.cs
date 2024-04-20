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
        #region Thông tin móng
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
        #endregion
        #region Thép Cạnh X
        private string _rebarBotX;
        public string RebarBotX
        {
            get => _rebarBotX;
            set
            {
                _rebarBotX = value;
                OnPropertyChanged(nameof(RebarBotX));
            }
        }
        private string _rebarTopX;
        public string RebarTopX
        {
            get => _rebarTopX;
            set
            {
                _rebarTopX = value;
                OnPropertyChanged(nameof(RebarTopX));
            }
        }
        #endregion
        #region Thép Cạnh Y
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
        private string _rebarBotY;
        public string RebarBotY
        {
            get => _rebarBotY;
            set
            {
                _rebarBotY = value;
                OnPropertyChanged(nameof(RebarBotY));
            }
        }
        private string _rebarTopY;
        public string RebarTopY
        {
            get => _rebarTopY;
            set
            {
                _rebarTopY = value;
                OnPropertyChanged(nameof(RebarTopY));
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
            RebarBotX = "12a150";
            RebarTopX = "10a300";
            IsRebarTop = false;
            RebarBotY = "12a150";
            RebarTopY = "10a300";
            
            

        }
    }
}
