using System;
using System.Collections.Generic;
using System.Text;
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

        public Task<bool> AddOrUpdate(IPlaylist item)
        {
            _barrel.Add(item.Id, item, TimeSpan.MaxValue);
            return Task.FromResult(true);
        }

        public Task<IPlaylist> Get(string id)
        {
            var item = _barrel.Get<IPlaylist>(id);
            return Task.FromResult(item);
        }

        public Task<IEnumerable<IPlaylist>> GetAll()
        {
            throw new NotImplementedException();
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
