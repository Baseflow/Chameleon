using System;
using System.Globalization;
using MvvmCross.Converters;

namespace Chameleon.Core.Helpers
{
    public class DoubleToTimeSpan : MvxValueConverter<double, TimeSpan>
    {
        protected override TimeSpan Convert(double value, Type targetType,object parameter,CultureInfo culture)
        {
            return TimeSpan.FromSeconds(value);

        }
    }

}
