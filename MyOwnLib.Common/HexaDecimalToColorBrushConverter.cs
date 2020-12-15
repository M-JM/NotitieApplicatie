using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace MyOwnLib.Common
{
    public class HexaDecimalToColorBrushConverter: IValueConverter
    {


      
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var hexString = (value as string);


            try
            {
                var x = (SolidColorBrush)(new BrushConverter().ConvertFrom(hexString));
                return x;
            }
            catch
            {
                throw new FormatException();
            }
        }

  

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
