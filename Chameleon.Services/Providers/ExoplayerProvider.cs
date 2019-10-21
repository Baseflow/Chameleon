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
    public class ExoplayerProvider : ProviderBase, IMediaItemProvider
    {
        private readonly IMediaManager _mediaManager;

        public ExoplayerProvider(IMediaManager mediaManager)
        {
            _mediaManager = mediaManager ?? throw new ArgumentNullException(nameof(mediaManager));
        }

        public override bool CanEdit => false;

        public Task<bool> AddOrUpdate(IMediaItem item)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Exists(string id)
        {
            throw new NotImplementedException();
        }

        public Task<IMediaItem> Get(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<IMediaItem>> GetAll()
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

        public Task<bool> Remove(IMediaItem item)
        {
            throw new NotImplementedException();
        }

        public Task<bool> RemoveAll()
        {
            throw new NotImplementedException();
        }
    }
}
