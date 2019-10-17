using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Chameleon.Services.Resources;
using MediaManager;
using MediaManager.Library;
using MonkeyCache;
using Chameleon.Services.Extensions;
using System.Linq;

namespace Chameleon.Services.Services
{
    public class BrowseService : IBrowseService
    {
        private readonly IMediaManager _mediaManager;
        private readonly IBarrel _barrel;

        public BrowseService(IBarrel barrel, IMediaManager mediaManager)
        {
            _mediaManager = mediaManager ?? throw new ArgumentNullException(nameof(mediaManager));
            _barrel = barrel ?? throw new ArgumentNullException(nameof(barrel));
        }

        private const string RecentMediaKey = "recentMedia";

        public IList<IMediaItem> RecentMedia
        {
            get => _barrel.GetOrCreate(RecentMediaKey, new List<IMediaItem>());
            set => _barrel.Add(RecentMediaKey, value, TimeSpan.MaxValue);
        }

        public void AddToRecentMedia(IMediaItem mediaItem)
        {
            var recentMedia = RecentMedia;

            var oldItem = recentMedia.FirstOrDefault(x => x.Id == mediaItem.Id);
            if (oldItem != null)
                recentMedia.Remove(oldItem);

            recentMedia.Insert(0, mediaItem);
            if (recentMedia.Count > 10)
                recentMedia.RemoveAt(RecentMedia.Count);
            RecentMedia = recentMedia;
        }

        public Task<IList<IArtist>> GetFavoriteArtists()
        {
            return null;
        }

        public async Task<IList<IMediaItem>> GetMedia()
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
                        IMediaItem mediaItem = new MediaItem(sample.Uri)
                        {
                            Title = sample.Name,
                            Album = item.Name,
                            FileExtension = sample.Extension ?? "",
                            IsMetadataExtracted = true
                        };
                        mediaItem = await CrossMediaManager.Current.Extractor.UpdateMediaItem(mediaItem).ConfigureAwait(false);
                        items.Add(mediaItem);
                    }
                }
            }

            return items;
        }
    }
}
