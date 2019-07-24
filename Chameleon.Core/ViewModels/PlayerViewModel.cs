using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using MediaManager;
using MediaManager.Media;
using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using Xamarin.Forms;

namespace Chameleon.Core.ViewModels
{
    public class PlayerViewModel : BaseViewModel<IMediaItem>
    {
        public PlayerViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService) : base(logProvider, navigationService)
        {
        }

        private IMediaItem _source;
        public IMediaItem Source
        {
            get => _source;
            set => SetProperty(ref _source, value);
        }

        private bool _showProgress;
        public bool ShowProgress
        {
            get => _showProgress;
            set => SetProperty(ref _showProgress, value);
        }

        private IMvxAsyncCommand _previousCommand;
        public IMvxAsyncCommand PreviousCommand => _previousCommand ?? (_previousCommand = new MvxAsyncCommand(() => CrossMediaManager.Current.PlayPrevious()));

        private IMvxAsyncCommand _skipBackwardsCommand;
        public IMvxAsyncCommand SkipBackwardsCommand => _skipBackwardsCommand ?? (_skipBackwardsCommand = new MvxAsyncCommand(() => CrossMediaManager.Current.StepBackward()));

        private IMvxAsyncCommand _playCommand;
        public IMvxAsyncCommand PlayCommand => _playCommand ?? (_playCommand = new MvxAsyncCommand(() => CrossMediaManager.Current.PlayPause()));

        private IMvxAsyncCommand _skipForwardCommand;
        public IMvxAsyncCommand SkipForwardCommand => _skipForwardCommand ?? (_skipForwardCommand = new MvxAsyncCommand(() => CrossMediaManager.Current.StepForward()));

        private IMvxAsyncCommand _nextCommand;
        public IMvxAsyncCommand NextCommand => _nextCommand ?? (_nextCommand = new MvxAsyncCommand(() => CrossMediaManager.Current.PlayNext()));


        public override void Prepare(IMediaItem parameter)
        {
            Source = parameter;
        }
        
        private IMvxAsyncCommand _onTapCommand;

        public MvxAsyncCommand OnTapCommand
        {
            get {
                return (MvvmCross.Commands.MvxAsyncCommand)_onTapCommand;
            }
        }

        void OnTapped(object s)
        {
            Debug.WriteLine("parameter: " + s);
        }

    }
}