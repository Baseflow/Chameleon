using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Acr.UserDialogs;
using Chameleon.Services.Services;
using MediaManager;
using MediaManager.Media;
using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

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
        public IMvxAsyncCommand GithubCommand => _githubCommand ?? (_githubCommand = new MvxAsyncCommand(() => Xamarin.Essentials.Browser.OpenAsync("https://github.com/BaseflowIT")));

        private IMvxAsyncCommand _baseflowCommand;
        public IMvxAsyncCommand BaseflowCommand => _baseflowCommand ?? (_baseflowCommand = new MvxAsyncCommand(() => Xamarin.Essentials.Browser.OpenAsync("https://baseflow.com/")));
    }
}
