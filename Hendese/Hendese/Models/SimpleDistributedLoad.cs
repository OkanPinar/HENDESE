using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

using AdvInputOutput3;

namespace Hendese.Models
{
    public class SimpleDistributedLoad : BaseModel
    {
        
        private double _distributedLoad;
        /// <summary>
        /// kg/m^2
        /// </summary>
        [Input(Label = "q [kg/m^2]", ControlType = ControlTypes.Input)]
        public double DistributedLoad
        {
            get { return _distributedLoad; }
            set
            {
                _distributedLoad = value;
                OnPropertyChanged("DistributedLoad");
            }
        }

        private double _leftSpan;
        /// <summary>
        /// mm
        /// </summary>
        [Input(Label = "L\u2097 [m]", ControlType = ControlTypes.Input)]
        public double LeftSpan
        {
            get { return _leftSpan; }
            set
            {
                _leftSpan = value;
                OnPropertyChanged("LeftSpan");
            }
        }

        private double _rightSpan;
        /// <summary>
        /// mm
        /// </summary>
        [Input(Label = "L\u1D63 Span [m]", ControlType = ControlTypes.Input)]
        public double RightSpan
        {
            get { return _rightSpan; }
            set
            {
                _rightSpan = value;
                OnPropertyChanged("RightSpan");
            }
        }

        private double _l;
        /// <summary>
        /// m
        /// </summary>
        [Input(Label = "L [m]", ControlType = ControlTypes.Input)]
        public double L
        {
            get { return _l; }
            set
            {
                _l = value;
                OnPropertyChanged("L");
            }
        }

        private double _deflectionCriteria;
        /// <summary>
        /// 
        /// </summary>
        [Input(Label = "\u03B4c [unitless]", ControlType = ControlTypes.Input)]
        public double DeflectionCriteria
        {
            get { return _deflectionCriteria; }
            set
            {
                _deflectionCriteria = value;
                OnPropertyChanged("DeflectionCriteria");
            }
        }

        private double _fMax;
        /// <summary>
        /// mm
        /// </summary>
        [Input(Label = "\u03B4max [mm]", Floating = 2, ControlType = ControlTypes.Output)]
        public double FMax
        {
            get { return _fMax; }
            set
            {
                _fMax = value;
                OnPropertyChanged("FMax");
            }
        }

        private double _load;
        /// <summary>
        /// ton/m
        /// </summary>
        //[Output(Label = "Element Load [N/mm^2]")]
        public double Load
        {
            get { return _load; }
            set
            {
                _load = value;
                OnPropertyChanged("Load");
            }
        }

        private double _fRequired;
        /// <summary>
        /// mm
        /// </summary>
        //[Output(Label = "Required Deflection[mm]")]
        public double FRequired
        {
            get { return _fRequired; }
            set
            {
                _fRequired = value;
                OnPropertyChanged("FRequired");
            }
        }

        private double _ix;
        /// <summary>
        /// cm^4
        /// </summary>
        [Input(Label = "Imin [cm^4]", Floating = 2, ControlType = ControlTypes.Output)]
        public double Ix
        {
            get { return _ix; }
            set
            {
                _ix = value;
                OnPropertyChanged("Ix");
            }
        }

        private double _centeralMoment;
        /// <summary>
        /// ton.m
        /// </summary>
        [Input(Label = "M [ton.m]", Floating = 2, ControlType = ControlTypes.Output)]
        public double CenteralMoment
        {
            get { return _centeralMoment; }
            set
            {
                _centeralMoment = value;
                OnPropertyChanged("CenteralMoment");
            }
        }

        private double _maxStress;
        /// <summary>
        /// ton/cm^2
        /// </summary>
        [Input(Label = "\u03C3max [N/mm\u00B2]", ControlType = ControlTypes.Input)]
        public double MaxStress
        {
            get { return _maxStress; }
            set
            {
                _maxStress = value;
                OnPropertyChanged("MaxStress");
            }
        }

        private double _wMin;
        /// <summary>
        /// cm^3
        /// </summary>
        [Input(Label = "Wmin [cm^3]", Floating = 2, ControlType = ControlTypes.Output)]
        public double WMin
        {
            get { return _wMin; }
            set
            {
                _wMin = value;
                OnPropertyChanged("WMin");
            }
        }




        public SimpleDistributedLoad()
        {
            this.DistributedLoad = 200;
            this.LeftSpan = 1;
            this.RightSpan = 1;
            this.L = 7;
            this.DeflectionCriteria = 300;
            this.MaxStress = 355;
        }
        

        public override void Calculate()
        {
            double distributedLoad = DistributedLoad * 1e-5;// N/mm^2
            double l = L * 1e3; // mm
            double lSpan = LeftSpan * 1e3; // mm
            double rSpan = RightSpan * 1e3; // mm
            double load = distributedLoad * (lSpan + rSpan) / 2.0; // N
            double e = 210000.0; // N/mm^2
            double maxStress = MaxStress; // N/mm^2

            this.FMax = l / this.DeflectionCriteria;
            this._fRequired = this._fMax;
            double fr = this.FRequired; // mm

            double ix = 5.0 * load * Math.Pow(l, 4) / (384.0 * e * fr); // mm^4
            this.Ix = ix * 1e-4; // cm^4

            double centeralMoment = load * Math.Pow(l, 2) / 8; ; // N.mm
            double wMin = centeralMoment / maxStress; // mm^3
            this._centeralMoment = centeralMoment * 1e-8; // ton.m
            this._wMin = wMin * 1e-3; // cm^3
            this.MaxMoment = centeralMoment;
            this.l = l;

            this.parameters = new object[] { Ix, WMin };

        }

        public override bool CheckSection(StructuralBase.Section.SectionBase Section)
        {
            bool temp = true;
            StructuralBase.Section.SectionBase section = Section;

            if (section.I33 < this.Ix)
                temp = false;
            if (section.W33 < this.WMin)
                temp = false;

            if (!base.CheckSection(section))
                temp = false;

            return temp;
        }
    }
}
