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
            MediaItems.AddRange(_mediaManager.MediaQueue);
        }

        private MvxObservableCollection<IMediaItem> _mediaItems;
        public MvxObservableCollection<IMediaItem> MediaItems { get; set; } = new MvxObservableCollection<IMediaItem>();

        private IMvxAsyncCommand<IMediaItem> _playCommand;
        public IMvxAsyncCommand<IMediaItem> PlayCommand => _playCommand ?? (_playCommand = new MvxAsyncCommand<IMediaItem>(Play));

        private async Task Play(IMediaItem arg)
        {
            await NavigationService.Navigate<PlayerViewModel, IMediaItem>(arg);
        }

        public override async Task Initialize()
        {
            MediaItems.ReplaceWith(await _playlistService.GetPlaylist());
        }
    }
}
