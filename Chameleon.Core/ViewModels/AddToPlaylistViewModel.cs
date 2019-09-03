using System;
using System.Threading.Tasks;
using Acr.UserDialogs;
using Chameleon.Services.Services;
using MediaManager.Library;
using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace Chameleon.Core.ViewModels
{
    public class AddToPlaylistViewModel : BaseViewModel<IMediaItem>
    {
        private readonly IPlaylistService _playlistService;
        private readonly IUserDialogs _userDialogs;

        public AddToPlaylistViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService, IUserDialogs userDialogs, IPlaylistService playlistService) : base(logProvider, navigationService)
        {
            _playlistService = playlistService ?? throw new ArgumentNullException(nameof(playlistService));
            _userDialogs = userDialogs ?? throw new ArgumentNullException(nameof(userDialogs));
        }

        private IMediaItem _mediaItem { get; set; }

        public MvxObservableCollection<IPlaylist> Playlists { get; set; } = new MvxObservableCollection<IPlaylist>();

        private IMvxAsyncCommand<IPlaylist> _addToPlaylistCommand;
        public IMvxAsyncCommand<IPlaylist> AddToPlaylistCommand => _addToPlaylistCommand ?? (_addToPlaylistCommand = new MvxAsyncCommand<IPlaylist>(AddToPlaylist));

        private IMvxAsyncCommand _addPlaylistCommand;
        public IMvxAsyncCommand AddPlaylistCommand => _addPlaylistCommand ?? (_addPlaylistCommand = new MvxAsyncCommand(AddPlaylist));

        private string _playlistName;
        public string PlaylistName
        {
            get => _playlistName;
            set
            {
                SetProperty(ref _playlistName, value);
            }
        }

        public override async Task Initialize()
        {
            Playlists.ReplaceWith(await _playlistService.GetPlaylists());
        }

        private async Task AddToPlaylist(IPlaylist arg)
        {
            arg.MediaItems.Add(_mediaItem);
            await _playlistService.SavePlaylists(Playlists);
            _userDialogs.Toast(GetText("AddedToPlaylist"));

            await NavigationService.Close(this);
        }

        public override void Prepare(IMediaItem parameter)
        {
            _mediaItem = parameter;
        }

        private async Task AddPlaylist()
        {
            if (!string.IsNullOrEmpty(PlaylistName))
            {
                Playlists.Add(new Playlist() { Title = PlaylistName });
                await _playlistService.SavePlaylists(Playlists);
                PlaylistName = string.Empty;
            }
            else
                await _userDialogs.AlertAsync(GetText("InvalidName"));
        }
    }
}
