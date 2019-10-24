using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Chameleon.Services.Resources;
using MediaManager;
using MediaManager.Library;
using MediaManager.Media;

namespace Chameleon.Services.Providers
{
    public class ExoplayerProvider : BarrelCacheProvider<IMediaItem>, IMediaItemProvider
    {
        private readonly IMediaManager _mediaManager;

        public ExoplayerProvider(MonkeyCache.IBarrel barrel, IMediaManager mediaManager) : base(barrel)
        {
            _mediaManager = mediaManager ?? throw new ArgumentNullException(nameof(mediaManager));
        }

        public override bool CanEdit => false;

        public override string CacheName => "exoplayer";

        public override async Task<IEnumerable<IMediaItem>> GetAll()
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
                            ImageUri = "cover_art_placeholder.png",
                            IsMetadataExtracted = true
                        };
                        mediaItem = await _mediaManager.Extractor.UpdateMediaItem(mediaItem).ConfigureAwait(false);
                        items.Add(mediaItem);
                    }
                }
            }

            return items;
        }
    }
}
