using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MediaManager.Library;
using MediaManager.Media;

namespace Chameleon.Services.Providers
{
    public class RadioProvider : IMediaItemProvider
    {
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

        public Task<IEnumerable<IMediaItem>> GetAll()
        {
            throw new NotImplementedException();
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
