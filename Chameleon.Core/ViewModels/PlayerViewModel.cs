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
        private readonly IMediaManager _mediaManager;

        public PlayerViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService, IMediaManager mediaManager) : base(logProvider, navigationService)
        {
            _mediaManager = mediaManager ?? throw new ArgumentNullException(nameof(mediaManager));
            _mediaManager.PositionChanged += MediaManager_PositionChanged;
        }

        private void MediaManager_PositionChanged(object sender, MediaManager.Playback.PositionChangedEventArgs e)
        {
            Position = e.Position.TotalSeconds;
            Duration = _mediaManager.Duration.TotalSeconds;
        }

        private IMediaItem _source;
        public IMediaItem Source
        {
            get => _source;
            set => SetProperty(ref _source, value);
        }

        private bool _showControls;
        public bool ShowControls
        {
            get => _showControls;
            set => SetProperty(ref _showControls, value);
        }

        private double _position;
        public double Position
        {
            get => _position;
            set => SetProperty(ref _position, value);
        }

        private double _Duration;
        public double Duration
        {
            get => _Duration;
            set => SetProperty(ref _Duration, value);
        }

        private ImageSource _playPauseImage = ImageSource.FromFile("playback_controls_pause_button");
        public ImageSource PlayPauseImage
        {
            get => _playPauseImage;
            set => SetProperty(ref _playPauseImage, value);
        }

        private IMvxAsyncCommand _dragCompletedCommand;
        public IMvxAsyncCommand DragCompletedCommand => _dragCompletedCommand ?? (_dragCompletedCommand = new MvxAsyncCommand(() => _mediaManager.SeekTo(TimeSpan.FromSeconds(Position))));

        private IMvxAsyncCommand _previousCommand;
        public IMvxAsyncCommand PreviousCommand => _previousCommand ?? (_previousCommand = new MvxAsyncCommand(() => _mediaManager.PlayPrevious()));

        private IMvxAsyncCommand _skipBackwardsCommand;
        public IMvxAsyncCommand SkipBackwardsCommand => _skipBackwardsCommand ?? (_skipBackwardsCommand = new MvxAsyncCommand(() => _mediaManager.StepBackward()));

        private IMvxAsyncCommand _playpauseCommand;
        public IMvxAsyncCommand PlayPauseCommand => _playpauseCommand ?? (_playpauseCommand = new MvxAsyncCommand(PlayPause));

        private IMvxAsyncCommand _skipForwardCommand;
        public IMvxAsyncCommand SkipForwardCommand => _skipForwardCommand ?? (_skipForwardCommand = new MvxAsyncCommand(() => _mediaManager.StepForward()));

        private IMvxAsyncCommand _nextCommand;
        public IMvxAsyncCommand NextCommand => _nextCommand ?? (_nextCommand = new MvxAsyncCommand(() => _mediaManager.PlayNext()));

        private IMvxCommand _controlsCommand;
        public IMvxCommand ControlsCommand => _controlsCommand ?? (_controlsCommand = new MvxCommand(ShowHideControls));

        private IMvxAsyncCommand _queueCommand;
        public IMvxAsyncCommand QueueCommand => _queueCommand ?? (_queueCommand = new MvxAsyncCommand(
            () => NavigationService.Navigate<QueueViewModel>()));

        public override void Prepare(IMediaItem parameter)
        {
            Source = parameter;
        }

        public override async Task Initialize()
        {
            if (Source == null)
                Source = await _mediaManager.Play("https://clips.vorwaerts-gmbh.de/big_buck_bunny.mp4");
        }

        private void ShowHideControls()
        {
            ShowControls = !ShowControls;
        }

        private async Task PlayPause()
        {
            await _mediaManager.PlayPause();

            if (_mediaManager.IsPlaying())
                PlayPauseImage = ImageSource.FromFile("playback_controls_play_button");
            else
                PlayPauseImage = ImageSource.FromFile("playback_controls_pause_button");
        }
    }
}
