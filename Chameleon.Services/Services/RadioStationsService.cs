using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Chameleon.Services.Resources;
using MediaManager.Library;

namespace Chameleon.Services.Services
{
    public class RadioStationsService : IRadioStationsService
    {
        public Task<IList<IMediaItem>> GetRadioStations()
        {
            var json = RadioStations.GetEmbeddedResourceString("RadioStationsList.json");
            var jsonList = RadioStations.FromJson(json);
            IList<IMediaItem> items = new List<IMediaItem>();
            
            foreach (var item in jsonList)
            {
                foreach (var radioStation in item.RadioStationsList)
                {
                    if (!string.IsNullOrEmpty(radioStation.Uri))
                    {
                        var mediaItem = new MediaItem(radioStation.Uri)
                        {
                            Title = radioStation.Name,
                            FileExtension = radioStation.Extension ?? ""
                        };
                        if (mediaItem.FileExtension == "mpd" || mediaItem.MediaUri.EndsWith(".mpd"))
                            mediaItem.MediaType = MediaType.Dash;
                        else if (mediaItem.FileExtension == "ism" || mediaItem.MediaUri.EndsWith(".ism"))
                            mediaItem.MediaType = MediaType.SmoothStreaming;
                        else if (mediaItem.FileExtension == "m3u8" || mediaItem.MediaUri.EndsWith(".m3u8"))
                            mediaItem.MediaType = MediaType.Hls;

                        items.Add(mediaItem);
                    }
                }
            }

            return Task.FromResult(items);
        }

        public Task<IList<IMediaItem>> GetRecentRadioStation()
        {
            throw new NotImplementedException();
        }
    }
}
