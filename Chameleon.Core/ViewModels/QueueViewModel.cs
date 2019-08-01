using System;
using System.Collections.Generic;
using System.Text;
using Chameleon.Services.Services;
using System.Threading.Tasks;
using Acr.UserDialogs;
using MediaManager;
using MediaManager.Media;
using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace Chameleon.Core.ViewModels
{
    public class QueueViewModel : BaseViewModel
    {
        private readonly IUserDialogs _userDialogs;
        private readonly IMediaManager _mediaManager;
        private readonly IPlaylistService _playlistService;

        public QueueViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService,
                                IUserDialogs userDialogs, IMediaManager mediaManager, IPlaylistService playlistService)
            : base(logProvider, navigationService)
        {
            _userDialogs = userDialogs ?? throw new ArgumentNullException(nameof(userDialogs));
            _mediaManager = mediaManager ?? throw new ArgumentNullException(nameof(mediaManager));
            _playlistService = playlistService ?? throw new ArgumentNullException(nameof(playlistService));
        }

        //private MvxObservableCollection<IMediaItem> _mediaItems;
        public MvxObservableCollection<IMediaItem> MediaItems { get; set; } = new MvxObservableCollection<IMediaItem>();

        private IMediaItem _selectedMediaItem;
        public IMediaItem SelectedMediaItem
        {
            get => _selectedMediaItem;
            set => SetProperty(ref _selectedMediaItem, value);
        }

        private IMvxAsyncCommand _openPlayerCommand;
        public IMvxAsyncCommand OpenPlayerCommand => _openPlayerCommand ?? (_openPlayerCommand = new MvxAsyncCommand(Play));

        private IMvxAsyncCommand _closeCommand;
        public IMvxAsyncCommand CloseCommand => _closeCommand ?? (_closeCommand = new MvxAsyncCommand(Close));

        private async Task Close()
        {
            await NavigationService.Close(this);
        }

        private async Task Play()
        {
            await NavigationService.Navigate<PlayerViewModel, IMediaItem>(SelectedMediaItem);
        }

        public override async Task Initialize()
        {
            MediaItems.ReplaceWith(await _playlistService.GetPlaylist());
        }
    }
}
