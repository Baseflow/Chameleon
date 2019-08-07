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
    public class AddToPlaylistViewModel : BaseViewModel<IMediaItem>
    {
        private readonly IPlaylistService _playlistService;

        public AddToPlaylistViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService, IPlaylistService playlistService) : base(logProvider, navigationService)
        {
            _playlistService = playlistService ?? throw new ArgumentNullException(nameof(playlistService));
        }

        private IMediaItem _mediaItem { get; set; }

        public MvxObservableCollection<IPlaylist> Playlists { get; set; } = new MvxObservableCollection<IPlaylist>();

        private IMvxAsyncCommand<IPlaylist> _addToPlaylistCommand;
        public IMvxAsyncCommand<IPlaylist> AddToPlaylistCommand => _addToPlaylistCommand ?? (_addToPlaylistCommand = new MvxAsyncCommand<IPlaylist>(AddToPlaylist));

        private IMediaItem _selectedItem;
        public IMediaItem SelectedItem
        {
            get => _selectedItem;
            set => SetProperty(ref _selectedItem, value);
        }

        public override async Task Initialize()
        {
            Playlists.ReplaceWith(await _playlistService.GetPlaylists());
        }

        private async Task AddToPlaylist(IPlaylist arg)
        {
            arg.Add(_mediaItem);

            //TODO: Save playlist
            //await _playlistService.Save();

            await NavigationService.Close(this);
        }

        public override void Prepare(IMediaItem parameter)
        {
            _mediaItem = parameter;
        }
    }
}
