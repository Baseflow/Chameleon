using System;
using System.Collections.Generic;
using System.Text;
using MediaManager.Library;
using MediaManager.Media;

namespace Chameleon.Services.Providers
{
    public class UrlSourcesProvider : BarrelCacheProvider<IMediaItem>, IMediaItemProvider
    {
        public UrlSourcesProvider(MonkeyCache.IBarrel barrel) : base(barrel)
        {
        }

        public override string CacheName => "url_sources";
    }
}
