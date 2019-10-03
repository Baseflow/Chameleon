using System;
using System.Globalization;
using MvvmCross.Converters;

namespace Chameleon.Core.Helpers
{
    public class TimeSpanToStringConverter : MvxValueConverter<TimeSpan, string>
    {
        protected override string Convert(TimeSpan value, Type targetType, object parameter, CultureInfo culture)
        {
            return value.ToString(@"mm\:ss");
        }
    }
}
