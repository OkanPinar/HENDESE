using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StructuralBase.Section;

using AdvInputOutput3;

namespace Hendese.Models
{
    public class Purlings : BaseModel
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

        private double _deadLoad;
        /// <summary>
        /// kg/m^2
        /// </summary>
        [Input(Label = "g [kg/m^2]", ControlType = ControlTypes.Input)]
        public double DeadLoad
        {
            get { return _deadLoad; }
            set
            {
                _deadLoad = value;
                OnPropertyChanged("DeadLoad");
            }
        }

        private double _roofAngle;
        /// <summary>
        /// Degree
        /// </summary>
        [Input(Label = "\u03B1 [degree]", ControlType = ControlTypes.Input)]
        public double RoofAngle
        {
            get { return _roofAngle; }
            set
            {
                _roofAngle = value;
                OnPropertyChanged("DeadLoad");
            }
        }

        private double _purlingSpan;
        /// <summary>
        /// m
        /// </summary>
        [Input(ControlType = ControlTypes.Input)]
        public double PurlingSpan
        {
            get { return _purlingSpan; }
            set
            {
                _purlingSpan = value;
                OnPropertyChanged("PurlingSpan");
            }
        }

        private double _purlingLength;
        /// <summary>
        /// m
        /// </summary>
        [Input(ControlType = ControlTypes.Input)]
        public double PurlingLength
        {
            get { return _purlingLength; }
            set
            {
                _purlingLength = value;
                OnPropertyChanged("PurlingLength");
            }
        }

        private double _numberOfGergi;
        /// <summary>
        /// 
        /// </summary>
        [Input(ControlType = ControlTypes.Input)]
        public double NumberOfGergi
        {
            get { return _numberOfGergi; }
            set
            {
                _numberOfGergi = value;
                OnPropertyChanged("NumberOfGergi");
            }
        }

        private double _gergiSpan;
        /// <summary>
        /// m
        /// </summary>
        [Input(ControlType = ControlTypes.Input)]
        public double GergiSpan
        {
            get { return _gergiSpan; }
            set
            {
                _gergiSpan = value;
                OnPropertyChanged("GergiSpan");
            }
        }

        private double _wx;
        /// <summary>
        /// cm^3
        /// </summary>
        [Input(ControlType = ControlTypes.Input)]
        public double Wx
        {
            get { return _wx; }
            set
            {
                _wx = value;
                OnPropertyChanged("Wx");
            }
        }

        private double _wy;
        /// <summary>
        /// cm^3
        /// </summary>
        [Input(ControlType = ControlTypes.Input)]
        public double Wy
        {
            get { return _wy; }
            set
            {
                _wy = value;
                OnPropertyChanged("Wy");
            }
        }

        private double _i;
        /// <summary>
        /// cm^4
        /// </summary>
        [Input(ControlType = ControlTypes.Input)]
        public double I
        {
            get { return _i; }
            set
            {
                _i = value;
                OnPropertyChanged("I");
            }
        }

        private double _mx;
        /// <summary>
        /// ton.m
        /// </summary>
        [Input(ControlType = ControlTypes.Output)]
        public double Mx
        {
            get { return _mx; }
            set
            {
                _mx = value;
                OnPropertyChanged("Mx");
            }
        }

        private double _my;
        /// <summary>
        /// ton.m
        /// </summary>
        [Input(ControlType = ControlTypes.Output)]
        public double My
        {
            get { return _my; }
            set
            {
                _my = value;
                OnPropertyChanged("My");
            }
        }

        private double _alfa;
        /// <summary>
        /// kg/cm^2
        /// </summary>
        [Input(ControlType = ControlTypes.Output)]
        public double Alfa
        {
            get { return _alfa; }
            set
            {
                _alfa = value;
                OnPropertyChanged("Alfa");
            }
        }

        private double _deflection;
        /// <summary>
        /// mm
        /// </summary>
        [Input(ControlType = ControlTypes.Output)]
        public double Deflection
        {
            get { return _deflection; }
            set
            {
                _deflection = value;
                OnPropertyChanged("Deflection");
            }
        }

        public Purlings()
        {
            this.DistributedLoad = 75;
            this.DeadLoad = 25;
            this.RoofAngle = 20;
            this.PurlingSpan = 1.6;
            this.PurlingLength = 4.4;
            this.NumberOfGergi = 2;
            this.GergiSpan = 2.2;
            this.Wx = 41.2;
            this.Wy = 8.49;
            this.I = 3.91;
        }

        public override void Calculate()
        {
            double distributedLoad = DistributedLoad * 10.0 * 1e-6;// N/mm^2
            double deadLoad = DeadLoad * 10.0 * 1e-6; // N/mm^2
            double roofAngle = RoofAngle * Math.PI / 180; // radian
            double purlingSpan = PurlingSpan * 1e3; // mm
            double purlingLength = PurlingLength * 1e3; // mm
            double gergiSpan = GergiSpan * 1e3; // mm
            double wx = Wx * 1e3; // mm^3
            double wy = Wy * 1e3; // mm^3
            double i = I * 1e4; // mm^4
            double e = 210000.0; // N/mm^2


            double mx = (distributedLoad * purlingSpan * Math.Cos(roofAngle) + deadLoad * purlingSpan)
                * Math.Cos(roofAngle) * Math.Pow(purlingLength, 2) / 8; // N.mm
            double my = (distributedLoad * purlingSpan * Math.Cos(roofAngle) + deadLoad * purlingSpan)
                * Math.Sin(roofAngle) * Math.Pow(gergiSpan, 2) / 8; // N.mm
            double alfa = mx / wx + my / wy; // N/mm^2
            double deflection = (distributedLoad * purlingSpan * Math.Cos(roofAngle) + deadLoad * purlingSpan)
                * Math.Cos(roofAngle) * 5 / 384 * Math.Pow(purlingLength, 4) / e / i; // mm

            this._mx = mx * 1e-7; // ton.m
            this._my = my * 1e-7; // ton.m
            this._alfa = alfa; // N/mm^2
            this._deflection = deflection; // mm

        }

        public override bool CheckSection(SectionBase Section)
        {
            return base.CheckSection(Section);
        }
    }
}
