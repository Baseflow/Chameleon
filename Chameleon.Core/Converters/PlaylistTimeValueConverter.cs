using System;
using System.Globalization;
using MediaManager.Library;
using MvvmCross.Converters;

namespace Chameleon.Core.Converters
{
    public class PlaylistTimeValueConverter : MvxValueConverter<IPlaylist, string>
    {
        protected override string Convert(IPlaylist value, Type targetType, object parameter, CultureInfo culture)
        {
            return $"{value.TotalTime.Hours} hours " + $" { value.TotalTime.Minutes} minutes";
        }
    }
}

