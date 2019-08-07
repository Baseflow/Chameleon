using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediaManager.Library;
using MediaManager.Media;

namespace Chameleon.Services.Services
{
    public interface IPlaylistService
    {
        Task<IList<IPlaylist>> GetPlaylists();

        Task<IList<IMediaItem>> GetPlaylist();
    }
}
