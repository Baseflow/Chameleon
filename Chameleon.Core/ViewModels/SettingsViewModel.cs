using System;
using Acr.UserDialogs;
using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;

namespace Chameleon.Core.ViewModels
{
    public class SettingsViewModel : BaseViewModel
    {
        private readonly IUserDialogs _userDialogs;

        public SettingsViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService, IUserDialogs userDialogs) : base(logProvider, navigationService)
        {
            _userDialogs = userDialogs ?? throw new ArgumentNullException(nameof(userDialogs));
        }

        private IMvxAsyncCommand _githubCommand;
        public IMvxAsyncCommand GithubCommand => _githubCommand ?? (_githubCommand = new MvxAsyncCommand(() => Xamarin.Essentials.Browser.OpenAsync("https://github.com/Baseflow")));

        private IMvxAsyncCommand _baseflowCommand;
        public IMvxAsyncCommand BaseflowCommand => _baseflowCommand ?? (_baseflowCommand = new MvxAsyncCommand(() => Xamarin.Essentials.Browser.OpenAsync("https://baseflow.com/")));

        private IMvxAsyncCommand _openSourceCommand;
        public IMvxAsyncCommand OpenSourceCommand => _openSourceCommand ?? (_openSourceCommand = new MvxAsyncCommand(() => NavigationService.Navigate<OpenSourceViewModel>()));

        private IMvxAsyncCommand _playbackSettingsCommand;
        public IMvxAsyncCommand PlaybackSettingsCommand => _playbackSettingsCommand ?? (_playbackSettingsCommand = new MvxAsyncCommand(() => NavigationService.Navigate<SettingsPlaybackViewModel>()));

        private IMvxAsyncCommand _themingCommand;
        public IMvxAsyncCommand ThemingCommand => _themingCommand ?? (_themingCommand = new MvxAsyncCommand(() => NavigationService.Navigate<ThemingViewModel>()));
    }
}
