using AdvInputOutput3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hendese
{
    public class InputAttribute : Attribute
    {
        public string Label { get; set; }
        public string Tooltip { get; set; }

        private int _floating = -1;
        public int Floating
        {
            get { return _floating; }
            set { _floating = value; }
        }

        public double UnitConversion { get; set; }

        public ControlTypes ControlType { get; set; }

        public InputAttribute()
        {

        }
    }
}
