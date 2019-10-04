using System;
using System.Globalization;
using MvvmCross.Converters;

namespace Chameleon.Core.Helpers
{
    public class TimeSpanToDoubleValueConverter : MvxValueConverter<TimeSpan, double>
    {
        protected override double Convert(TimeSpan value, Type targetType, object parameter, CultureInfo culture)
        {
            return value.TotalSeconds;
        }

        protected override TimeSpan ConvertBack(double value, Type targetType, object parameter, CultureInfo culture)
        {
            return TimeSpan.FromSeconds(value);
        }
    }
}
