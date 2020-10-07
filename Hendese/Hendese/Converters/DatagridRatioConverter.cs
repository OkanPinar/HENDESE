using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Hendese.Converters
{
    public class DatagridRatioConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            StructuralBase.Section.SectionBase section = value as StructuralBase.Section.SectionBase;

            if (section == null)
                return null;

            return (System.Convert.ToDouble(parameter) / section.I33).ToString("0.00");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
