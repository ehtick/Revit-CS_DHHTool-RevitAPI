#region Namespaces

using Autodesk.Revit.DB;
using System.Collections.Generic;
using System.Linq;
// ReSharper disable All

#endregion

namespace DHHTools
{
    public class ExcelDataExtension : ViewModelBase
    {
        #region 01. Private Property
        private string BeamName_VM;
        private string Location_VM;
        private string Section_VM;
        private double b_VM;
        private double h_VM;
        private double DkThepTrenL1_VM;
        private double SLThepTrenL1_VM;
        private double DkThepTrenL2_VM;
        private double SLThepTrenL2_VM;
        private double DkThepDuoiL1_VM;
        private double SLThepDuoiL1_VM;
        private double DkThepDuoiL2_VM;
        private double SLThepDuoiL2_VM;
        private double DkThepDai_VM;
        private double SLThepDai_VM;
        private double KCThepDai_VM;
        private double DkThepGia_VM;
        private double SLThepGia_VM;
        #endregion

        #region 02. Public Property
        public string BeamName
        {
            get => BeamName_VM;
            set
            {
                if (BeamName_VM != value)
                {
                    BeamName_VM = value;
                    OnPropertyChanged("Location");
                }
            }
        }
        public string Location
        {
            get => Location_VM;
            set
            {
                if (Location_VM != value)
                {
                    Location_VM = value;
                    OnPropertyChanged("Location");
                }
            }
        }
        public double b
        {
            get => b_VM;
            set
            {
                if (b_VM != value)
                {
                    b_VM = value;
                    OnPropertyChanged("b");
                }
            }
        }
        public double h
        {
            get => h_VM;
            set
            {
                if (h_VM != value)
                {
                    h_VM = value;
                    OnPropertyChanged("h");
                }
            }
        }
        public double DkThepTrenL1
        {
            get => DkThepTrenL1_VM;
            set
            {
                if (DkThepTrenL1_VM != value)
                {
                    DkThepTrenL1_VM = value;
                    OnPropertyChanged("DkThepTrenL1");
                }
            }
        }
        public double SLThepTrenL1
        {
            get => SLThepTrenL1_VM;
            set
            {
                if (SLThepTrenL1_VM != value)
                {
                    SLThepTrenL1_VM = value;
                    OnPropertyChanged("SLThepTrenL1");
                }
            }
        }
        public double DkThepTrenL2
        {
            get => DkThepTrenL2_VM;
            set
            {
                if (DkThepTrenL2_VM != value)
                {
                    DkThepTrenL2_VM = value;
                    OnPropertyChanged("DkThepTrenL2");
                }
            }
        }
        public double SLThepTrenL2
        {
            get => SLThepTrenL2_VM;
            set
            {
                if (SLThepTrenL2_VM != value)
                {
                    SLThepTrenL2_VM = value;
                    OnPropertyChanged("SLThepTrenL2");
                }
            }
        }
        public double DkThepDuoiL1
        {
            get => DkThepDuoiL1_VM;
            set
            {
                if (DkThepDuoiL1_VM != value)
                {
                    DkThepDuoiL1_VM = value;
                    OnPropertyChanged("DkThepDuoiL1");
                }
            }
        }
        public double SLThepDuoiL1
        {
            get => SLThepDuoiL1_VM;
            set
            {
                if (SLThepDuoiL1_VM != value)
                {
                    SLThepDuoiL1_VM = value;
                    OnPropertyChanged("SLThepDuoiL1");
                }
            }
        }
        public double DkThepDuoiL2
        {
            get => DkThepDuoiL2_VM;
            set
            {
                if (DkThepDuoiL2_VM != value)
                {
                    DkThepDuoiL2_VM = value;
                    OnPropertyChanged("DkThepDuoiL2");
                }
            }
        }
        public double SLThepDuoiL2
        {
            get => SLThepDuoiL2_VM;
            set
            {
                if (SLThepDuoiL2_VM != value)
                {
                    SLThepDuoiL2_VM = value;
                    OnPropertyChanged("SLThepDuoiL2");
                }
            }
        }
        public double DkThepDai
        {
            get => DkThepDai_VM;
            set
            {
                if (DkThepDai_VM != value)
                {
                    DkThepDai_VM = value;
                    OnPropertyChanged("DkThepDai");
                }
            }
        }
        public double SLThepDai
        {
            get => SLThepDai_VM;
            set
            {
                if (SLThepDai_VM != value)
                {
                    SLThepDai_VM = value;
                    OnPropertyChanged("SLThepDai");
                }
            }
        }
        public double KCThepDai
        {
            get => KCThepDai_VM;
            set
            {
                if (KCThepDai_VM != value)
                {
                    KCThepDai_VM = value;
                    OnPropertyChanged("KCThepDai");
                }
            }
        }
        public string Section
        {
            get
            {
                return b.ToString() + "x" + h.ToString();
            }
            set
            {
                if (Section_VM != value)
                {
                    Section_VM = value;
                    OnPropertyChanged("Section");
                }
            }
        }
        public double DkThepGia
        {
            get => DkThepGia_VM;
            set
            {
                if (DkThepGia_VM != value)
                {
                    DkThepGia_VM = value;
                    OnPropertyChanged("DkThepGia");
                }
            }
        }
        public double SLThepGia
        {
            get => SLThepGia_VM;
            set
            {
                if (SLThepGia_VM != value)
                {
                    SLThepGia_VM = value;
                    OnPropertyChanged("SLThepGia");
                }
            }
        }

        #endregion

    }
}