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
using Plugin.Media;

namespace Chameleon.Core.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {
        private readonly IUserDialogs _userDialogs;

        public HomeViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService, IUserDialogs userDialogs) : base(logProvider, navigationService)
        {
            _userDialogs = userDialogs ?? throw new ArgumentNullException(nameof(userDialogs));
        }

        //Temporary command for queue
        private IMvxAsyncCommand _queueCommand;
        public IMvxAsyncCommand QueueCommand => _queueCommand ?? (_queueCommand = new MvxAsyncCommand(
            () => NavigationService.Navigate<QueueViewModel>()));

        private IMvxAsyncCommand _playerCommand;
        public IMvxAsyncCommand PlayerCommand => _playerCommand ?? (_playerCommand = new MvxAsyncCommand(
            () => NavigationService.Navigate<PlayerViewModel>()));

        private IMvxAsyncCommand _openUrlCommand;
        public IMvxAsyncCommand OpenUrlCommand => _openUrlCommand ?? (_openUrlCommand = new MvxAsyncCommand(OpenUrl));

        private IMvxAsyncCommand _openFileCommand;
        public IMvxAsyncCommand OpenFileCommand => _openFileCommand ?? (_openFileCommand = new MvxAsyncCommand(OpenFile));

        private async Task OpenUrl()
        {
            var result = await _userDialogs.PromptAsync("Enter url", inputType: InputType.Url);

            //TODO: Check if the url is valid
            if (!string.IsNullOrWhiteSpace(result.Value))
            {
                var mediaItem = await CrossMediaManager.Current.Play(result.Value);
                await NavigationService.Navigate<PlayerViewModel, IMediaItem>(mediaItem);
            }
        }

        private async Task OpenFile()
        {
            if (await CrossMedia.Current.Initialize())
            {
                var videoMediaFile = await CrossMedia.Current.PickVideoAsync();
                
                if (videoMediaFile != null)
                {
                    var mediaItem = await CrossMediaManager.Current.Play(videoMediaFile.Path);
                    await NavigationService.Navigate<PlayerViewModel, IMediaItem>(mediaItem);
                }
            }
        }
    }
}
