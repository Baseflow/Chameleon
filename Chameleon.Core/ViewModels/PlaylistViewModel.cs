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
    public class PlaylistViewModel : BaseViewModel<IPlaylist>
    {
        private readonly IUserDialogs _userDialogs;
        private readonly IMediaManager _mediaManager;
        private readonly IPlaylistService _playlistService;

        public PlaylistViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService, IUserDialogs userDialogs, IMediaManager mediaManager, IPlaylistService playlistService) : base(logProvider, navigationService)
        {
            _userDialogs = userDialogs ?? throw new ArgumentNullException(nameof(userDialogs));
            _mediaManager = mediaManager ?? throw new ArgumentNullException(nameof(mediaManager));
            _playlistService = playlistService ?? throw new ArgumentNullException(nameof(playlistService));
        }

        public MvxObservableCollection<IMediaItem> MediaItems { get; set; } = new MvxObservableCollection<IMediaItem>();

        private IMvxAsyncCommand<IMediaItem> _playCommand;
        public IMvxAsyncCommand<IMediaItem> PlayCommand => _playCommand ?? (_playCommand = new MvxAsyncCommand<IMediaItem>(Play));

        private IMediaItem _selectedItem;
        public IMediaItem SelectedItem
        {
            get => _selectedItem;
            set => SetProperty(ref _selectedItem, value);
        }

        public override async Task Initialize()
        {
            MediaItems.ReplaceWith(await _playlistService.GetPlaylist());
        }

        private async Task Play(IMediaItem arg)
        {
            await NavigationService.Navigate<PlayerViewModel, IMediaItem>(SelectedItem);
        }

        public override void Prepare(IPlaylist parameter)
        {

        }
    }
}
