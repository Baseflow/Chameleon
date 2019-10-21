using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediaManager.Library;
using MediaManager.Media;
using MonkeyCache;

namespace Chameleon.Services.Providers
{
    public class PlaylistProvider : BarrelCacheProvider<IPlaylist>, IPlaylistProvider
    {
        public PlaylistProvider(IBarrel barrel) : base(barrel)
        {
        }

        public override string CacheName => "playlists";
    }
}
