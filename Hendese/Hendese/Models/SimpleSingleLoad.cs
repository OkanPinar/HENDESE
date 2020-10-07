using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AdvInputOutput3;

namespace Hendese.Models
{
    public class SimpleSingleLoad : BaseModel
    {

        private int _p;
        /// <summary>
        /// ton
        /// </summary>
        [Input(Label = "P [ton]", ControlType = ControlTypes.Input)]
        public int P
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
        /// N/mm^2
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

        private double _deflectionCriteria;
        /// <summary>
        /// unitless
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


        public SimpleSingleLoad()
        {
            this.P = 20;
            this.L = 30;
            this.MaxStress = 355;
            this.DeflectionCriteria = 200;
        }

        public override void Calculate()
        {
            this.FMax = this.L * 1000.0 / this.DeflectionCriteria;
            this._fRequired = this._fMax;
            double p = P * 10000.0;// N
            double l = L * 1000.0; // mm
            double e = 210000.0; // N/mm^2
            double fr = FRequired; // mm
            double maxStress = MaxStress; // N/mm^2

            double ix = p * Math.Pow(l, 3) / (48.0 * e * fr); // mm^4
            this.Ix = ix * 1e-4; // cm^4

            double centeralMoment = p * l / 4.0; // N.mm
            double wMin = centeralMoment / maxStress; // mm^3
            this._centeralMoment = centeralMoment * 1e-7; // ton.m
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
