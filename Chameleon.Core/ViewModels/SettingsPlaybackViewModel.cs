using System;
using Acr.UserDialogs;
using MediaManager;
using MediaManager.Library;
using MediaManager.Volume;
using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;

namespace Chameleon.Core.ViewModels
{
    public class SettingsPlaybackViewModel : BaseViewModel
    {
        private readonly IUserDialogs _userDialogs;
        public IMediaManager MediaManager { get; }

        public SettingsPlaybackViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService, IUserDialogs userDialogs, IMediaManager mediaManager) : base(logProvider, navigationService)
        {
            _userDialogs = userDialogs ?? throw new ArgumentNullException(nameof(userDialogs));
            MediaManager = mediaManager ?? throw new ArgumentNullException(nameof(mediaManager));
        }
    }
}
