using System;
using System.Collections.Generic;
using System.Text;
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

        public QueueViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService, IUserDialogs userDialogs, IMediaManager mediaManager) : base(logProvider, navigationService)
        {
            _userDialogs = userDialogs;
            _mediaManager = mediaManager;
            MediaItems.AddRange(_mediaManager.MediaQueue);
        }

        public MvxObservableCollection<IMediaItem> MediaItems { get; set; } = new MvxObservableCollection<IMediaItem>();

        private IMvxAsyncCommand<IMediaItem> _playCommand;
        public IMvxAsyncCommand<IMediaItem> PlayCommand => _playCommand ?? (_playCommand = new MvxAsyncCommand<IMediaItem>(Play));

        private async Task Play(IMediaItem arg)
        {
            await NavigationService.Navigate<PlayerViewModel, IMediaItem>(arg);
        }
    }
}
