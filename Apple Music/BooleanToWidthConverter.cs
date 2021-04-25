using System;
using System.Diagnostics;
using System.Windows.Data;

namespace Apple_Music
{
    [ValueConversion(typeof(bool), typeof(double))]
    public class BooleanToWidthConverter : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
            if (value is not bool)
                throw new InvalidOperationException("yo i need a boolean");

            return (bool) value! ? 300 : 0;
        }
            
        public object ConvertBack(object value, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException();
        }
            
        #endregion
    }
}