using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chameleon.Services.Extensions;
using MediaManager.Library;
using MediaManager.Media;
using MonkeyCache;

namespace Chameleon.Services.Providers
{
    public abstract class BarrelCacheProvider<TContentItem> : ProviderBase, ILibraryProvider<TContentItem> where TContentItem : IContentItem
    {
        private readonly IBarrel _barrel;

        public BarrelCacheProvider(IBarrel barrel)
        {
            _barrel = barrel ?? throw new ArgumentNullException(nameof(barrel));
        }

        public abstract string CacheName { get; }

        public string EnabledCacheName => CacheName + "_enabled";
        public override bool Enabled
        {
            get => _barrel.GetOrCreate(EnabledCacheName, false);
            set
            {
                _barrel.Add(EnabledCacheName, value, TimeSpan.MaxValue);
                base.Enabled = value;
            }
        }

        public async Task<bool> AddOrUpdate(TContentItem item)
        {
            if (item == null)
                return false;

            var items = (await GetAll().ConfigureAwait(false))?.ToList();
            if (items == null)
                return false;

            var contentItem = items.FirstOrDefault(x => x.Id == item.Id);
            if (contentItem == null)
                items.Add(item);
            else
            {
                var index = items.IndexOf(contentItem);
                items.RemoveAt(index);
                items.Insert(index, item);
            }

            _barrel.Add(CacheName, items, TimeSpan.MaxValue);
            return true;
        }

        public async Task<bool> Exists(string id)
        {
            var items = await GetAll().ConfigureAwait(false);
            return items?.Any(x => x.Id == id) == true;
        }

        public async Task<TContentItem> Get(string id)
        {
            var items = await GetAll().ConfigureAwait(false);
            return items.FirstOrDefault(x => x.Id == id);
        }

        public virtual Task<IEnumerable<TContentItem>> GetAll()
        {
            try
            {
                var items = _barrel.GetOrCreate<IEnumerable<TContentItem>>(CacheName, new List<TContentItem>());
                return Task.FromResult(items);
            }
            catch
            { }
            return Task.FromResult<IEnumerable<TContentItem>>(null);
        }

        public async Task<bool> Remove(TContentItem item)
        {
            var items = (await GetAll().ConfigureAwait(false))?.ToList();
            var contentItem = items.FirstOrDefault(x => x.Id == item.Id);
            if (items?.Remove(contentItem) == true)
            {
                _barrel.Add(CacheName, items, TimeSpan.MaxValue);
                return true;
            }
            return false;
        }

        public Task<bool> RemoveAll()
        {
            _barrel.EmptyAll();
            return Task.FromResult(true);
        }
    }
}
