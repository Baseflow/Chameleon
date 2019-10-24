using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Chameleon.Services.Extensions;
using Chameleon.Services.Services;
using MediaManager;
using MediaManager.Library;
using MediaManager.Media;
using Refit;

namespace Chameleon.Services.Providers
{
    public class RadioStationProvider : BarrelCacheProvider<IMediaItem>, IMediaItemProvider
    {
        private readonly IMediaManager _mediaManager;

        public RadioStationProvider(MonkeyCache.IBarrel barrel, IMediaManager mediaManager) : base(barrel)
        {
            _mediaManager = mediaManager ?? throw new ArgumentNullException(nameof(mediaManager));
        }

        public override bool CanEdit => false;

        public override string CacheName => "radio_stations";

        public override async Task<IEnumerable<IMediaItem>> GetAll()
        {
            var radioApi = RestService.For<IRadioSourceService>("http://www.radio-browser.info/webservice/json");
            var sources = await radioApi.GetRadioSourceByCountry("netherlands").ConfigureAwait(false);

            IList<IMediaItem> items = new List<IMediaItem>();
            foreach (var source in sources)
            {
                IMediaItem mediaItem = new MediaItem(source.Url)
                {
                    ImageUri = source.Favicon.IsValidUrl() ? source.Favicon : "cover_art_placeholder.png",
                    Title = source.Name,
                    Album = "Radio",
                    IsMetadataExtracted = true
                };
                //mediaItem = await _mediaManager.Extractor.UpdateMediaItem(mediaItem).ConfigureAwait(false);
                items.Add(mediaItem);
            }
            return items;
        }
    }
}
