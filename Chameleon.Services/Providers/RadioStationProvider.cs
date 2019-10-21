using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Chameleon.Services.Services;
using MediaManager;
using MediaManager.Library;
using MediaManager.Media;
using Refit;

namespace Chameleon.Services.Providers
{
    public class RadioStationProvider : ProviderBase, IMediaItemProvider
    {
        private readonly IMediaManager _mediaManager;

        public RadioStationProvider(IMediaManager mediaManager)
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
            var radioApi = RestService.For<IRadioSourceService>("http://www.radio-browser.info/webservice/json");
            var sources = await radioApi.GetRadioSourceByCountry("netherlands").ConfigureAwait(false);

            IList<IMediaItem> items = new List<IMediaItem>();
            foreach (var source in sources)
            {
                IMediaItem mediaItem = new MediaItem(source.Url)
                {
                    ImageUri = source.Favicon,
                    Title = source.Name,
                    Album = "Radio",
                    IsMetadataExtracted = true
                };
                //mediaItem = await _mediaManager.Extractor.UpdateMediaItem(mediaItem).ConfigureAwait(false);
                items.Add(mediaItem);
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
