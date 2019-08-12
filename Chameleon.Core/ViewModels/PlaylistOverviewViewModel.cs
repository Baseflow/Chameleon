using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Acr.UserDialogs;
using Chameleon.Services.Services;
using MediaManager;
using MediaManager.Library;
using MediaManager.Media;
using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace Chameleon.Core.ViewModels
{
    public class PlaylistOverviewViewModel : BaseViewModel
    {
        private readonly IUserDialogs _userDialogs;
        private readonly IMediaManager _mediaManager;
        private readonly IPlaylistService _playlistService;

        public PlaylistOverviewViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService, IUserDialogs userDialogs, IMediaManager mediaManager, IPlaylistService playlistService) : base(logProvider, navigationService)
        {
            _userDialogs = userDialogs ?? throw new ArgumentNullException(nameof(userDialogs));
            _mediaManager = mediaManager ?? throw new ArgumentNullException(nameof(mediaManager));
            _playlistService = playlistService ?? throw new ArgumentNullException(nameof(playlistService));
        }

        public MvxObservableCollection<IPlaylist> Playlists { get; set; } = new MvxObservableCollection<IPlaylist>();

        private IMvxAsyncCommand<IPlaylist> _openPlaylistCommand;
        public IMvxAsyncCommand<IPlaylist> OpenPlaylistCommand => _openPlaylistCommand ?? (_openPlaylistCommand = new MvxAsyncCommand<IPlaylist>(OpenPlaylist));

        private IMvxCommand _addPlaylistCommand;
        public IMvxCommand AddPlaylistCommand => _addPlaylistCommand ?? (_addPlaylistCommand = new MvxCommand(() => Playlists.Add(new Playlist() { Title = "test" })));

        private IPlaylist _selectedItem;
        public IPlaylist SelectedItem
        {
            get => _selectedItem;
            set => SetProperty(ref _selectedItem, value);
        }

        public override async Task Initialize()
        {
            Playlists.ReplaceWith(await _playlistService.GetPlaylists());
        }

        private async Task OpenPlaylist(IPlaylist arg)
        {
            await NavigationService.Navigate<PlaylistViewModel, IPlaylist>(SelectedItem);
        }
    }
}
