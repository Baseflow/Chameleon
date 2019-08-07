using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Chameleon.Services.Resources;
using MediaManager;
using MediaManager.Library;
using MediaManager.Media;
using MonkeyCache;
using MonkeyCache.LiteDB;

namespace Chameleon.Services.Services
{
    public class PlaylistService : IPlaylistService
    {
        private readonly IBarrel _barrel;

        public PlaylistService(IBarrel barrel)
        {
            _barrel = barrel ?? throw new ArgumentNullException(nameof(barrel));
        }

        public async Task<IList<IMediaItem>> GetPlaylist()
        {
            var json = ExoPlayerSamples.GetEmbeddedResourceString("media.exolist.json");
            var jsonList = ExoPlayerSamples.FromJson(json);
            IList<IMediaItem> items = new List<IMediaItem>();

            foreach (var item in jsonList)
            {
                foreach (var sample in item.Samples)
                {
                    if (!string.IsNullOrEmpty(sample.Uri))
                    {
                        //TODO: Fix this code in MediaManager
                        /*var mediaItem = await CrossMediaManager.Current.MediaExtractor.CreateMediaItem(sample.Uri);
                        mediaItem.Title = sample.Name;
                        mediaItem.Album = item.Name;
                        mediaItem.FileExtension = sample.Extension ?? "";*/

                        var mediaItem = new MediaItem(sample.Uri)
                        {
                            Title = sample.Name,
                            Album = item.Name,
                            FileExtension = sample.Extension ?? ""
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

            return items;
        }

        public Task<IList<IPlaylist>> GetPlaylists()
        {
            IList<IPlaylist> playlists = new List<IPlaylist>();
            //playlists.Add(new MediaManager.Media.Playlist() { Title = "ExoPlayer" });

            return Task.FromResult(playlists);
        }

        public Task SavePlaylist(IPlaylist playlist)
        {
            _barrel.Add<IPlaylist>(playlist.PlaylistId, playlist, TimeSpan.MaxValue);
            return Task.CompletedTask;
        }
    }
}
