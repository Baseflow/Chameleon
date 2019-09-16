using System;
using System.Globalization;
using MediaManager.Library;
using MvvmCross.Converters;

namespace Chameleon.Core.Converters
{
    public class TrackAmountValueConverter : MvxValueConverter<IPlaylist, string>
    {
        protected override string Convert(IPlaylist value, Type targetType, object parameter, CultureInfo culture)
        {
            return $"{value.MediaItems.Count} tracks";
        }
    }
}

