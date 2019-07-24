using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Acr.UserDialogs;
using MediaManager.Media;
using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;

namespace Chameleon.Core.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {
        private readonly IUserDialogs _userDialogs;

        public HomeViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService, IUserDialogs userDialogs) : base(logProvider, navigationService)
        {
            _userDialogs = userDialogs;
        }

        private IMvxAsyncCommand _playerCommand;
        public IMvxAsyncCommand PlayerCommand => _playerCommand ?? (_playerCommand = new MvxAsyncCommand(
            () => NavigationService.Navigate<PlayerViewModel, IMediaItem>(new MediaItem("https://clips.vorwaerts-gmbh.de/big_buck_bunny.mp4"))));

        private IMvxAsyncCommand _openUrlCommand;
        public IMvxAsyncCommand OpenUrlCommand => _openUrlCommand ?? (_openUrlCommand = new MvxAsyncCommand(OpenUrl));

        private IMvxAsyncCommand _openFileCommand;
        public IMvxAsyncCommand OpenFileCommand => _openFileCommand ?? (_openFileCommand = new MvxAsyncCommand(OpenFile));

        private async Task OpenUrl()
        {
            var result = await _userDialogs.PromptAsync("Enter url", inputType: InputType.Url);
            //TODO: Check if the url is valid
            await NavigationService.Navigate<PlayerViewModel, IMediaItem>(new MediaItem(result.Value));
        }

        private async Task OpenFile()
        {
            //TODO: Open gallery
        }
    }
}
