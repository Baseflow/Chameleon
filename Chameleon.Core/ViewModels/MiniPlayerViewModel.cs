using System;
using System.Threading.Tasks;
using MediaManager;
using MediaManager.Library;
using MediaManager.Playback;
using MediaManager.Queue;
using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using Xamarin.Forms;

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

        private ImageSource _playPauseImage;
        public ImageSource PlayPauseImage
        {
            get => _playPauseImage;
            set => SetProperty(ref _playPauseImage, value);
        }

        private ImageSource _shuffleImage = ImageSource.FromFile("playback_controls_shuffle_off");
        public ImageSource ShuffleImage
        {
            get => _shuffleImage;
            set => SetProperty(ref _shuffleImage, value);
        }

        private FormattedString _currentMediaItemText;
        public FormattedString CurrentMediaItemText
        {
            get
            {
                var currentMediaItemText = new FormattedString();
                if (MediaManager.MediaQueue.Current != null)
                {
                    currentMediaItemText.Spans.Add(new Span { Text = MediaManager.MediaQueue.Current.Title, FontAttributes = FontAttributes.Bold, FontSize = 12 });
                    currentMediaItemText.Spans.Add(new Span { Text = " • " });
                    currentMediaItemText.Spans.Add(new Span { Text = MediaManager.MediaQueue.Current.Album, FontSize = 12 });
                }
                else
                    currentMediaItemText.Spans.Add(new Span { Text = "CHAMELEON" });

                _currentMediaItemText = currentMediaItemText;
                return currentMediaItemText;
            }
            set => SetProperty(ref _currentMediaItemText, value);
        }

        private double _progress;
        public double Progress
        {
            get => _progress;
            set => SetProperty(ref _progress, value);
        }

        public IMediaItem CurrentlyPlaying
        {
            get
            {
                var currentlyPlaying = MediaManager.MediaQueue.Current;
                if (currentlyPlaying != null)
                    return currentlyPlaying;
                else
                    return new MediaItem();
            }
        }

        private IMvxAsyncCommand _playpauseCommand;
        public IMvxAsyncCommand PlayPauseCommand => _playpauseCommand ?? (_playpauseCommand = new MvxAsyncCommand(PlayPause));

        private IMvxCommand _shuffleCommand;
        public IMvxCommand ShuffleCommand => _shuffleCommand ?? (_shuffleCommand = new MvxCommand(Shuffle));

        private IMvxAsyncCommand _previousCommand;
        public IMvxAsyncCommand PreviousCommand => _previousCommand ?? (_previousCommand = new MvxAsyncCommand(PlayPrevious));

        private IMvxAsyncCommand _nextCommand;
        public IMvxAsyncCommand NextCommand => _nextCommand ?? (_nextCommand = new MvxAsyncCommand(PlayNext));

        private IMvxAsyncCommand _openPlayerCommand;
        public IMvxAsyncCommand OpenPlayerCommand => _openPlayerCommand ?? (_openPlayerCommand = new MvxAsyncCommand(OpenPlayer));

        public override Task Initialize()
        {
            return base.Initialize();
        }

        public override void ViewAppearing()
        {
            base.ViewAppearing();

            if (MediaManager.IsPlaying())
                PlayPauseImage = ImageSource.FromFile("playback_controls_pause_button");
            else
                PlayPauseImage = ImageSource.FromFile("playback_controls_play_button");

            RaisePropertyChanged(nameof(CurrentMediaItemText));
        }

        public override void ViewAppeared()
        {
            base.ViewAppeared();

            Progress = MediaManager.Position.TotalSeconds / MediaManager.Duration.TotalSeconds;
            MediaManager.PositionChanged += MediaManager_PositionChanged;
        }

        public override void ViewDisappeared()
        {
            base.ViewDisappeared();
            MediaManager.PositionChanged -= MediaManager_PositionChanged;
        }

        private void MediaManager_PositionChanged(object sender, PositionChangedEventArgs e)
        {
            Progress = e.Position.TotalSeconds / MediaManager.Duration.TotalSeconds;
        }

        private async Task PlayPause()
        {
            if (MediaManager.IsPlaying())
                PlayPauseImage = ImageSource.FromFile("playback_controls_play_button");
            else
                PlayPauseImage = ImageSource.FromFile("playback_controls_pause_button");

            await MediaManager.PlayPause();
        }

        private void Shuffle()
        {
            MediaManager.ToggleShuffle();
            switch (MediaManager.ShuffleMode)
            {
                case ShuffleMode.Off:
                    ShuffleImage = ImageSource.FromFile("playback_controls_shuffle_off");
                    break;
                case ShuffleMode.All:
                    ShuffleImage = ImageSource.FromFile("playback_controls_shuffle_on");
                    break;
            }
        }

        private async Task PlayPrevious()
        {
            await MediaManager.PlayPrevious();
            await RaisePropertyChanged(nameof(CurrentMediaItemText));
            PlayPauseImage = ImageSource.FromFile("playback_controls_pause_button");
        }

        private async Task PlayNext()
        {
            await MediaManager.PlayNext();
            await RaisePropertyChanged(nameof(CurrentMediaItemText));
            PlayPauseImage = ImageSource.FromFile("playback_controls_pause_button");
        }

        private async Task OpenPlayer()
        {
            await NavigationService.Navigate<PlayerViewModel>();
        }
    }
}
