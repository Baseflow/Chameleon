using System;
using System.Globalization;
using MediaManager.Library;
using MvvmCross.Converters;

namespace Chameleon.Core.Converters
{
    public class PlaylistToSubtitleValueConverter : MvxValueConverter<IPlaylist, string>
    {
        protected override string Convert(IPlaylist value, Type targetType, object parameter, CultureInfo culture)
        {
            string time;
            if (value.TotalTime.Hours > 2)
            {
                time = $"{value.TotalTime.Hours} hours " + $" { value.TotalTime.Minutes} minutes";
            }
            else if (value.TotalTime.Hours > 1)
            {
                time = $"{value.TotalTime.Hours} hour " + $" { value.TotalTime.Minutes} minutes";
            }
            else
            {
                time = $" { value.TotalTime.Minutes} minutes";
            }

            return $"{value.MediaItems.Count} tracks, {time}";
        }
    }
}

