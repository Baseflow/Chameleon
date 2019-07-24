using System;
using System.Collections.Generic;
using System.Text;
using MediaManager.Media;
using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;

namespace Chameleon.Core.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {
        public HomeViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService) : base(logProvider, navigationService)
        {
        }

        private IMvxAsyncCommand _playerCommand;
        public IMvxAsyncCommand PlayerCommand => _playerCommand ?? (_playerCommand = new MvxAsyncCommand(
            () => NavigationService.Navigate<PlayerViewModel, IMediaItem>(new MediaItem("https://clips.vorwaerts-gmbh.de/big_buck_bunny.mp4"))));
    }
}
