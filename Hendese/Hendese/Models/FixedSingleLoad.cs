using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AdvInputOutput3;

namespace Hendese.Models
{
    public class FixedSingleLoad : BaseModel
    {
        private double _p;
        /// <summary>
        /// kg/m^2
        /// </summary>
        [Input(Label = "P [ton]", ControlType = ControlTypes.Input)]
        public double P
        {
            get { return _p; }
            set
            {
                _p = value;
                OnPropertyChanged("P");
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
        [Input(Label = "Imin [mm]", Floating = 2, ControlType = ControlTypes.Output)]
        public double Ix
        {
            get { return _ix; }
            set
            {
                _ix = value;
                OnPropertyChanged("Ix");
            }
        }

        private double _momentAtSpan;
        /// <summary>
        /// ton.m
        /// </summary>
        [Input(Label = "Mspan [ton.m]", Floating = 2, ControlType = ControlTypes.Output)]
        public double MomentAtSpan
        {
            get { return _momentAtSpan; }
            set
            {
                _momentAtSpan = value;
                OnPropertyChanged("MomentAtSpan");
            }
        }

        private double _momentAtSupport;
        /// <summary>
        /// ton.m
        /// </summary>
        [Input(Label = "Msupport [ton.m]", Floating = 2, ControlType = ControlTypes.Output)]
        public double MomentAtSupport
        {
            get { return _momentAtSupport; }
            set
            {
                _momentAtSupport = value;
                OnPropertyChanged("MomentAtSupport");
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

        private double _wMinAtSpan;
        /// <summary>
        /// cm^3
        /// </summary>
        [Input(Label = "WminSpan [mm^3]", Floating = 2, ControlType = ControlTypes.Output)]
        public double WMinAtSpan
        {
            get { return _wMinAtSpan; }
            set
            {
                _wMinAtSpan = value;
                OnPropertyChanged("WMinAtSpan");
            }
        }

        private double _wMinAtSupport;
        /// <summary>
        /// cm^3
        /// </summary>
        [Input(Label = "WminSupport [mm^3]", Floating = 2, ControlType = ControlTypes.Output)]
        public double WMinAtSupport
        {
            get { return _wMinAtSupport; }
            set
            {
                _wMinAtSupport = value;
                OnPropertyChanged("WMinAtSupport");
            }
        }

        public FixedSingleLoad()
        {
            this.P = 100;
            this.L = 25;
            this.DeflectionCriteria = 200;
            this.MaxStress = 355;
        }

        public override void Calculate()
        {
            var load = P * 1e4; // N
            double l = L * 1e3; // mm
            double e = 210000.0; // N/mm^2
            double maxStress = MaxStress; // N/mm^2
            this.FMax = l / this.DeflectionCriteria;
            this._fRequired = this._fMax;
            double fr = this.FRequired; // mm

            double ix = load * Math.Pow(l, 3) / (192.0 * e * fr); // mm^4
            this.Ix = ix * 1e-4; // cm^4

            double momentAtSupport = load * l / 8.0; // N.mm
            double momentAtSpan = load * l / 8.0; // N.mm
            double wMinAtSupport = momentAtSupport / maxStress; // mm^3
            double wMinAtSpan = momentAtSpan / maxStress; // mm^3
            this.MomentAtSupport = momentAtSupport / 1e8; // ton.m
            this.MomentAtSpan = momentAtSpan / 1e8; // ton.m0,8125
            this.WMinAtSupport = wMinAtSupport * 1e-3; // cm^3
            this.WMinAtSpan = wMinAtSpan * 1e-3; // cm^3
            this.MaxMoment = Math.Max(momentAtSupport, momentAtSpan);
            this.l = l;

            this.parameters = new object[] { Ix, Math.Min(WMinAtSpan, WMinAtSupport) };

        }

        public override bool CheckSection(StructuralBase.Section.SectionBase Section)
        {
            bool temp = true;
            StructuralBase.Section.SectionBase section = Section;

            if (section.I33 < this.Ix)
                temp = false;
            if (section.W33 < this.WMinAtSpan)
                temp = false;
            if (section.W33 < this.WMinAtSupport)
                temp = false;

            if (!base.CheckSection(section))
                temp = false;

            return temp;
        }
    }
}
