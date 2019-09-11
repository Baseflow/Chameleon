using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediaManager.Library;
using MediaManager.Media;
using MonkeyCache;

namespace Chameleon.Services.Providers
{
    public class PlaylistProvider : IPlaylistProvider
    {
        private readonly IBarrel _barrel;

        public PlaylistProvider(IBarrel barrel)
        {
            _barrel = barrel ?? throw new ArgumentNullException(nameof(barrel));
        }

        public async Task<bool> AddOrUpdate(IPlaylist item)
        {
            var items = (await GetAll())?.ToList();
            if (items == null)
                return false;

            var playlist = items?.FirstOrDefault(x => x.Id == item.Id);
            if (playlist == null)
                items.Add(item);
            else
            {
                var index = items.IndexOf(playlist);
                items.RemoveAt(index);
                items.Insert(index, item);
            }

            _barrel.Add("playlists", items, TimeSpan.MaxValue);
            return true;
        }

        public async Task<bool> Exists(string id)
        {
            var items = await GetAll();
            return items.Any(x => x.Id == id);
        }

        public async Task<IPlaylist> Get(string id)
        {
            var items = await GetAll();
            return items.FirstOrDefault(x => x.Id == id);
        }

        public Task<IEnumerable<IPlaylist>> GetAll()
        {
            try
            {
                if (!_barrel.Exists("playlists"))
                    _barrel.Add<IEnumerable<IPlaylist>>("playlists", new List<IPlaylist>(), TimeSpan.MaxValue);

                var items = _barrel.Get<IEnumerable<IPlaylist>>("playlists");
                return Task.FromResult(items);
            }
            catch
            { }
            return Task.FromResult<IEnumerable<IPlaylist>>(null);
        }

        public Task<bool> Remove(IPlaylist item)
        {
            _barrel.Empty(item.Id);
            return Task.FromResult(true);
        }

        public Task<bool> RemoveAll()
        {
            _barrel.EmptyAll();
            return Task.FromResult(true);
        }
    }
}
