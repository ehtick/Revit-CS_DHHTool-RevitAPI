﻿using BIMSoftLib.MVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _02_01_DrawSectionBeam_Detail2D.MVVM.Model
{
    public class mSectionBeam: PropertyChangedBase
    {
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
        private int _nMain1;
        public int nMain1
        {
            get => _nMain1;
            set
            {
                _nMain1 = value;
                OnPropertyChanged(nameof(nMain1));
            }
        }

        private int _diaMain1;
        public int DiaMain1
        {
            get => _diaMain1;
            set
            {
                _diaMain1 = value;
                OnPropertyChanged(nameof(DiaMain1));
            }
        }
        private int _nMain2;
        public int nMain2
        {
            get => _nMain2;
            set
            {
                _nMain2 = value;
                OnPropertyChanged(nameof(nMain2));
            }
        }
        private int _diaMain2;
        public int DiaMain2
        {
            get => _diaMain2;
            set
            {
                _diaMain2 = value;
                OnPropertyChanged(nameof(DiaMain2));
            }
        }
        private int _nSub;
        public int nSub
        {
            get => _nSub;
            set
            {
                _nSub = value;
                OnPropertyChanged(nameof(nSub));
            }
        }
        private int _diaSub;
        public int DiaSub
        {
            get => _diaSub;
            set
            {
                _diaSub = value;
                OnPropertyChanged(nameof(DiaSub));
            }
        }
    }
}