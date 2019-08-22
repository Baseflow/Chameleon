using System;
using System.Threading.Tasks;
using MediaManager;
using MvvmCross.Commands;
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

        public override Task Initialize()
        {
            return base.Initialize();
        }

        private IMvxAsyncCommand _playpauseCommand;
        public IMvxAsyncCommand PlayPauseCommand => _playpauseCommand ?? (_playpauseCommand = new MvxAsyncCommand(PlayPause));

        private Task PlayPause()
        {
            throw new NotImplementedException();
        }
    }
}
