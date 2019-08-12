using System.Collections.Generic;
using System.Threading.Tasks;
using MediaManager.Library;

namespace Chameleon.Services.Services
{
    public interface IBrowseService
    {
        Task<IList<IMediaItem>> GetMedia();

        Task<IList<IMediaItem>> GetRecentMedia();

        Task<IList<IArtist>> GetFavoriteArtists();
    }
}
