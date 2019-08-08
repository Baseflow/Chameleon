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
    public class OpenSourceViewModel : BaseViewModel
    {
        private readonly IUserDialogs _userDialogs;

        public OpenSourceViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService, IUserDialogs userDialogs) : base(logProvider, navigationService)
        {
            _userDialogs = userDialogs ?? throw new ArgumentNullException(nameof(userDialogs));
        }

        private IMvxAsyncCommand _mediaManagerCommand;
        public IMvxAsyncCommand MediaManagerCommand => _mediaManagerCommand ?? (_mediaManagerCommand = new MvxAsyncCommand(() => Xamarin.Essentials.Browser.OpenAsync("https://github.com/martijn00/XamarinMediaManager")));

        private IMvxAsyncCommand _xamarinFormsCommand;
        public IMvxAsyncCommand XamarinFormsCommand => _xamarinFormsCommand ?? (_xamarinFormsCommand = new MvxAsyncCommand(() => Xamarin.Essentials.Browser.OpenAsync("https://docs.microsoft.com/en-us/xamarin/xamarin-forms/")));
    }
}
