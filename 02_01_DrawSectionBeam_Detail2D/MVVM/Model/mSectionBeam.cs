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

        #region Top 1
        private double _diaTop1;
        public double DiaTop1
        {
            get => _diaTop1;
            set
            {
                _diaTop1 = value;
                OnPropertyChanged(nameof(DiaTop1));
            }
        }
        private double _nTop1;
        public double nTop1
        {
            get => _nTop1;
            set
            {
                _nTop1 = value;
                OnPropertyChanged(nameof(nTop1));
            }
        }
        #endregion

        #region Top 2
        private double _nTop2;
        public double nTop2
        {
            get => _nTop2;
            set
            {
                _nTop2 = value;
                OnPropertyChanged(nameof(nTop2));
            }
        }
        private double _diaTop2;
        public double DiaTop2
        {
            get => _diaTop2;
            set
            {
                _diaTop2 = value;
                OnPropertyChanged(nameof(DiaTop2));
            }
        }
        #endregion

        #region Bot 1
        private double _diaBot1;
        public double DiaBot1
        {
            get => _diaBot1;
            set
            {
                _diaBot1 = value;
                OnPropertyChanged(nameof(DiaBot1));
            }
        }
        private double _nBot1;
        public double nBot1
        {
            get => _nBot1;
            set
            {
                _nBot1 = value;
                OnPropertyChanged(nameof(nBot1));
            }
        }
        #endregion

        #region Bot 2
        private double _nBot2;
        public double nBot2
        {
            get => _nBot2;
            set
            {
                _nBot2 = value;
                OnPropertyChanged(nameof(nBot2));
            }
        }
        private double _diaBot2;
        public double DiaBot2
        {
            get => _diaBot2;
            set
            {
                _diaBot2 = value;
                OnPropertyChanged(nameof(DiaBot2));
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
