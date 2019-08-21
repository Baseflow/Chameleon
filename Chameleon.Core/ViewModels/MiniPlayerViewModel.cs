using System;
using MediaManager;
using MvvmCross.Logging;
using MvvmCross.Navigation;

namespace Chameleon.Core.ViewModels
{
    public class MiniPlayerViewModel : BaseViewModel
    {
        public IMediaManager MediaManager { get; }

        public MiniPlayerViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService, IMediaManager mediaManager)
            : base(logProvider, navigationService)
        {
            MediaManager = mediaManager ?? throw new ArgumentNullException(nameof(mediaManager));
        }   
    }
}
