using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

using AdvInputOutput3;
using AdvInputOutput3.Controls;

namespace Hendese.Models
{
    public abstract class BaseModel : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string prop)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        public object[] parameters { get; set; }
        protected double MaxMoment;
        protected double l;

        public BaseModel()
        {
            this.PropertyChanged += BaseModel_PropertyChanged;
        }

        public abstract void Calculate();

        public virtual bool CheckSection(StructuralBase.Section.SectionBase Section)
        {
            // Design
            StructuralBase.Material.Steel mat = new StructuralBase.Material.Steel(210000, 355, 70000);
            StructuralBase.Design.Steel.SteelDesign newDesign = 
                    (StructuralBase.Design.Steel.SteelDesign)Section.DesignSection(mat);
            StructuralBase.Design.InternalForces newForce = new StructuralBase.Design.InternalForces();
            double moment = MaxMoment;
            newForce.M3 = moment;
            newForce.V2 = moment / (l / 2.0);
            newDesign.Lx = l;
            newDesign.Ly = l;
            newDesign.MA = moment / 2.0;
            newDesign.MB = moment;
            newDesign.MC = moment / 2.0;
            newDesign.MMax = moment;
            newDesign.K = 1;

            newDesign.CheckASD(newForce);

            //if (newDesign.Result == false)
            //    return false;
            //

            return true;
        }

        private void BaseModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            var attrs = this.GetType().GetProperty(e.PropertyName).GetCustomAttributes(true);
            foreach (var attr in attrs)
            {
                if (attr is InputAttribute)
                {
                    InputAttribute inputAttr = (InputAttribute)attr;
                    if (inputAttr.ControlType == ControlTypes.Input)
                    {
                        this.Calculate();
                        break;
                    }
                }
            }
           
        }
    }
}
