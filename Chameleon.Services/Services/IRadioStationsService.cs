using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediaManager.Library;

namespace Chameleon.Services.Services
{
    public interface IRadioStationsService
    {
        Task<IList<IMediaItem>> GetRadioStations();

        Task<IList<IMediaItem>> GetRecentRadioStation();
    }
}
