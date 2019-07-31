using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Acr.UserDialogs;
using Chameleon.Services.Media;
using Chameleon.Services.Services;
using MediaManager;
using MediaManager.Media;
using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace Chameleon.Core.ViewModels
{
    public class BrowseViewModel : BaseViewModel
    {
        private readonly IUserDialogs _userDialogs;
        private readonly IMediaManager _mediaManager;
        private readonly IPlaylistService _playlistService;

        public BrowseViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService, IUserDialogs userDialogs, IMediaManager mediaManager, IPlaylistService playlistService) : base(logProvider, navigationService)
        {
            _userDialogs = userDialogs ?? throw new ArgumentNullException(nameof(userDialogs));
            _mediaManager = mediaManager ?? throw new ArgumentNullException(nameof(mediaManager));
            _playlistService = playlistService ?? throw new ArgumentNullException(nameof(playlistService));
        }

        public MvxObservableCollection<IPlaylist> Playlists { get; set; } = new MvxObservableCollection<IPlaylist>();
        public MvxObservableCollection<IMediaItem> FavoriteArtists { get; set; } = new MvxObservableCollection<IMediaItem>();
        public MvxObservableCollection<IMediaItem> RecentlyPlayedItems { get; set; } = new MvxObservableCollection<IMediaItem>();

        private IMvxAsyncCommand<IPlaylist> _openPlaylistCommand;
        public IMvxAsyncCommand<IPlaylist> OpenPlaylistCommand => _openPlaylistCommand ?? (_openPlaylistCommand = new MvxAsyncCommand<IPlaylist>(OpenPlaylist));

        public override async Task Initialize()
        {
            FavoriteArtists.ReplaceWith(await _playlistService.GetPlaylist());
            RecentlyPlayedItems.ReplaceWith(await _playlistService.GetPlaylist());
        }

        private async Task OpenPlaylist(IPlaylist arg)
        {
            await NavigationService.Navigate<PlaylistViewModel, IPlaylist>(arg);
        }
    }
}
