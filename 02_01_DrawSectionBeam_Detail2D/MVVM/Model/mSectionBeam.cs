using BIMSoftLib.MVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _02_01_DrawSectionBeam_Detail2D.MVVM.Model
{
    public class mSectionBeam: PropertyChangedBase
    {
        #region Beam Information
        private string _beamName;
        public string BeamName
        {
            get => _beamName;
            set
            {
                _beamName = value;
                OnPropertyChanged(nameof(BeamName));
            }
        }
        private string _sectionLocation;   
        public string SectionLocation
        {
            get => _sectionLocation;
            set
            {
                _sectionLocation = value;
                OnPropertyChanged(nameof(SectionLocation));
            }
        }
        private double _b;
        public double B
        {
            get => _b;
            set
            {
                _b = value;
                OnPropertyChanged(nameof(B));
            }
        }
        private double _h;
        public double H
        {
            get => _h;
            set
            {
                _h = value;
                OnPropertyChanged(nameof(H));
            }
        }
        #endregion

        #region Layer 1 Top 1
        private double _diaTop1_Layer1;
        public double DiaTop1_Layer1
        {
            get => _diaTop1_Layer1;
            set
            {
                _diaTop1_Layer1 = value;
                OnPropertyChanged(nameof(DiaTop1_Layer1));
            }
        }
        private double _nTop1_Layer1;
        public double nTop1_Layer1
        {
            get => _nTop1_Layer1;
            set
            {
                _nTop1_Layer1 = value;
                OnPropertyChanged(nameof(nTop1_Layer1));
            }
        }
        #endregion

        #region Layer 1 Top 2
        private double _nTop2_Layer1;
        public double nTop2_Layer1
        {
            get => _nTop2_Layer1;
            set
            {
                _nTop2_Layer1 = value;
                OnPropertyChanged(nameof(nTop2_Layer1));
            }
        }
        private double _diaTop2_Layer1;
        public double DiaTop2_Layer1
        {
            get => _diaTop2_Layer1;
            set
            {
                _diaTop2_Layer1 = value;
                OnPropertyChanged(nameof(DiaTop2_Layer1));
            }
        }
        #endregion

        #region Layer 2 Top 1
        private double _diaTop1_Layer2;
        public double DiaTop1_Layer2
        {
            get => _diaTop1_Layer2;
            set
            {
                _diaTop1_Layer2 = value;
                OnPropertyChanged(nameof(DiaTop1_Layer2));
            }
        }
        private double _nTop1_Layer2;
        public double nTop1_Layer2
        {
            get => _nTop1_Layer2;
            set
            {
                _nTop1_Layer2 = value;
                OnPropertyChanged(nameof(nTop1_Layer2));
            }
        }
        #endregion

        #region Layer 2 Top 2
        private double _nTop2_Layer2;
        public double nTop2_Layer2
        {
            get => _nTop2_Layer2;
            set
            {
                _nTop2_Layer2 = value;
                OnPropertyChanged(nameof(nTop2_Layer2));
            }
        }
        private double _diaTop2_Layer2;
        public double DiaTop2_Layer2
        {
            get => _diaTop2_Layer2;
            set
            {
                _diaTop2_Layer2 = value;
                OnPropertyChanged(nameof(DiaTop2_Layer2));
            }
        }
        #endregion

        #region Layer 1 Bottom 1
        private double _diaBot1_Layer1;
        public double DiaBot1_Layer1
        {
            get => _diaBot1_Layer1;
            set
            {
                _diaBot1_Layer1 = value;
                OnPropertyChanged(nameof(DiaBot1_Layer1));
            }
        }
        private double _nBot1_Layer1;
        public double nBot1_Layer1
        {
            get => _nBot1_Layer1;
            set
            {
                _nBot1_Layer1 = value;
                OnPropertyChanged(nameof(nBot1_Layer1));
            }
        }
        #endregion

        #region Layer 1 Bottom 2 
        private double _nBot2_Layer1;
        public double nBot2_Layer1
        {
            get => _nBot2_Layer1;
            set
            {
                _nBot2_Layer1 = value;
                OnPropertyChanged(nameof(nBot2_Layer1));
            }
        }
        private double _diaBot2_Layer1;
        public double DiaBot2_Layer1
        {
            get => _diaBot2_Layer1;
            set
            {
                _diaBot2_Layer1 = value;
                OnPropertyChanged(nameof(DiaBot2_Layer1));
            }
        }
        #endregion

        #region Layer 2 Bottom 1
        private double _diaBot1_Layer2;
        public double DiaBot1_Layer2
        {
            get => _diaBot1_Layer2;
            set
            {
                _diaBot1_Layer2 = value;
                OnPropertyChanged(nameof(DiaBot1_Layer2));
            }
        }
        private double _nBot1_Layer2;
        public double nBot1_Layer2
        {
            get => _nBot1_Layer2;
            set
            {
                _nBot1_Layer2 = value;
                OnPropertyChanged(nameof(nBot1_Layer2));
            }
        }
        #endregion

        #region Layer 2 Bottom 2 
        private double _nBot2_Layer2;
        public double nBot2_Layer2
        {
            get => _nBot2_Layer2;
            set
            {
                _nBot2_Layer2 = value;
                OnPropertyChanged(nameof(nBot2_Layer2));
            }
        }
        private double _diaBot2_Layer2;
        public double DiaBot2_Layer2
        {
            get => _diaBot2_Layer2;
            set
            {
                _diaBot2_Layer2 = value;
                OnPropertyChanged(nameof(DiaBot2_Layer2));
            }
        }
        #endregion

        #region Stirrup
        private double _diaStirrup;
        public double DiaStirrup
        {
            get => _diaStirrup;
            set
            {
                _diaStirrup = value;
                OnPropertyChanged(nameof(DiaStirrup));
            }
        }
        private double _nStirrup;
        public double NStirrup
        {
            get => _nStirrup;
            set
            {
                _nStirrup = value;
                OnPropertyChanged(nameof(NStirrup));
            }
        }
        private double _disStirrup;
        public double DisStirrup
        {
            get => _disStirrup;
            set
            {
                _disStirrup = value;
                OnPropertyChanged(nameof(DisStirrup));
            }
        }
        private string _stirrup;
        public string Stirrup
        {
            get => _stirrup;
            set
            {
                _stirrup = value;
                OnPropertyChanged(nameof(Stirrup)); 
            }
        }
        private string _topView;
        public string TopView
        {
            get => _topView;
            set
            {
                _topView = value;
                OnPropertyChanged(nameof(TopView));
            }
        }
        private string _botView;
        public string BotView
        {
            get => _botView;
            set
            {
                _botView = value;
                OnPropertyChanged(nameof(BotView));
            }
        }
        #endregion
    }
}
