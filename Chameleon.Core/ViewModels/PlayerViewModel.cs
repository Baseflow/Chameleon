using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Acr.UserDialogs;
using MediaManager;
using MediaManager.Library;
using MediaManager.Playback;
using MediaManager.Queue;
using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using Xamarin.Forms;

namespace Chameleon.Core.ViewModels
{
    public class PlayerViewModel : BaseViewModel<IMediaItem>
    {
        public IMediaManager MediaManager { get; }
        private readonly IUserDialogs _userDialogs;

        public PlayerViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService, IUserDialogs userDialogs, IMediaManager mediaManager) : base(logProvider, navigationService)
        {
            MediaManager = mediaManager ?? throw new ArgumentNullException(nameof(mediaManager));
            _userDialogs = userDialogs ?? throw new ArgumentNullException(nameof(userDialogs));
            MediaManager.MediaPlayer.PropertyChanged += MediaPlayer_PropertyChanged;
        }

        private void MediaPlayer_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if(e.PropertyName == nameof(MediaManager.MediaPlayer.VideoHeight) && VideoWidth > 0)
            {
                VideoHeight = VideoWidth / MediaManager.MediaPlayer.VideoAspectRatio;
            }
        }

        private IMediaItem _source;
        public IMediaItem Source
        {
            get => _source;
            set
            {
                if (SetProperty(ref _source, value))
                {
                    var metaData = new List<Metadata>();
                    foreach (var item in value.ToDictionary())
                    {
                        metaData.Add(new ViewModels.Metadata() { Key = item.Key, Value = item.Value?.ToString() });
                    }
                    Metadata = metaData;
                    RaisePropertyChanged(nameof(Metadata));
                }
            }
        }

        public IList<Metadata> Metadata { get; set; }

        public MvxObservableCollection<IPlaylist> Playlists { get; set; } = new MvxObservableCollection<IPlaylist>();

        private bool _showControls;
        public bool ShowControls
        {
            get => _showControls;
            set => SetProperty(ref _showControls, value);
        }

        private bool _dragStarted = false;
        public bool DragStarted
        {
            get => _dragStarted;
            set => SetProperty(ref _dragStarted, value);
        }

        private double _position = 0;
        public double Position
        {
            get => _position;
            set
            {
                SetProperty(ref _position, value);
            }
        }

        private double _duration = 0;
        public double Duration
        {
            get => _duration;
            set => SetProperty(ref _duration, value);
        }

        private TimeSpan _timeSpanPosition = TimeSpan.Zero;
        public TimeSpan TimeSpanPosition
        {
            get => _timeSpanPosition;
            set
            {
                SetProperty(ref _timeSpanPosition, value);
            }
        }

        private TimeSpan _timeSpanDuration = TimeSpan.Zero;
        public TimeSpan TimeSpanDuration
        {
            get => _timeSpanDuration;
            set => SetProperty(ref _timeSpanDuration, value);
        }

        private ImageSource _playPauseImage = ImageSource.FromFile("playback_controls_pause_button");
        public ImageSource PlayPauseImage
        {
            get => _playPauseImage;
            set => SetProperty(ref _playPauseImage, value);
        }

        private ImageSource _repeatImage = ImageSource.FromFile("playback_controls_repeat_off");
        public ImageSource RepeatImage
        {
            get => _repeatImage;
            set => SetProperty(ref _repeatImage, value);
        }

        private ImageSource _shuffleImage = ImageSource.FromFile("playback_controls_shuffle_off");
        public ImageSource ShuffleImage
        {
            get => _shuffleImage;
            set => SetProperty(ref _shuffleImage, value);
        }

        private ImageSource _favoriteImage = ImageSource.FromFile("playback_controls_favorite_off");
        public ImageSource FavoriteImage
        {
            get => _favoriteImage;
            set => SetProperty(ref _favoriteImage, value);
        }

        private bool _isFavorite;
        public bool IsFavorite
        {
            get => _isFavorite;
            set => SetProperty(ref _isFavorite, value);
        }

        private string _playlistName;
        public string PlaylistName
        {
            get => _playlistName;
            set
            {
                SetProperty(ref _playlistName, value);
            }
        }

        private double _videoHeight = 200;
        public double VideoHeight
        {
            get => _videoHeight;
            set => SetProperty(ref _videoHeight, value);
        }

        private double _videoWidth = 0;
        public double VideoWidth
        {
            get => _videoWidth;
            set => SetProperty(ref _videoWidth, value);
        }

        private IMvxAsyncCommand _dragCompletedCommand;
        public IMvxAsyncCommand DragCompletedCommand => _dragCompletedCommand ?? (_dragCompletedCommand = new MvxAsyncCommand(() =>
        {
            DragStarted = false;
            return MediaManager.SeekTo(TimeSpan.FromSeconds(Position));
        }));

        private IMvxCommand _dragStartedCommand;
        public IMvxCommand DragStartedCommand => _dragStartedCommand ?? (_dragStartedCommand = new MvxCommand(() => DragStarted = true));

        private IMvxAsyncCommand _previousCommand;
        public IMvxAsyncCommand PreviousCommand => _previousCommand ?? (_previousCommand = new MvxAsyncCommand(() => MediaManager.PlayPrevious()));

        private IMvxAsyncCommand _skipBackwardsCommand;
        public IMvxAsyncCommand SkipBackwardsCommand => _skipBackwardsCommand ?? (_skipBackwardsCommand = new MvxAsyncCommand(() => MediaManager.StepBackward()));

        private IMvxAsyncCommand _playpauseCommand;
        public IMvxAsyncCommand PlayPauseCommand => _playpauseCommand ?? (_playpauseCommand = new MvxAsyncCommand(PlayPause));

        private IMvxAsyncCommand _skipForwardCommand;
        public IMvxAsyncCommand SkipForwardCommand => _skipForwardCommand ?? (_skipForwardCommand = new MvxAsyncCommand(() => MediaManager.StepForward()));

        private IMvxAsyncCommand _nextCommand;
        public IMvxAsyncCommand NextCommand => _nextCommand ?? (_nextCommand = new MvxAsyncCommand(() => MediaManager.PlayNext()));

        private IMvxCommand _controlsCommand;
        public IMvxCommand ControlsCommand => _controlsCommand ?? (_controlsCommand = new MvxCommand(ShowHideControls));

        private IMvxAsyncCommand _queueCommand;
        public IMvxAsyncCommand QueueCommand => _queueCommand ?? (_queueCommand = new MvxAsyncCommand(
            () => NavigationService.Navigate<QueueViewModel>()));

        private IMvxCommand _repeatCommand;
        public IMvxCommand RepeatCommand => _repeatCommand ?? (_repeatCommand = new MvxCommand(Repeat));

        private IMvxCommand _shuffleCommand;
        public IMvxCommand ShuffleCommand => _shuffleCommand ?? (_shuffleCommand = new MvxCommand(Shuffle));

        private IMvxAsyncCommand _addToPlaylistCommand;
        public IMvxAsyncCommand AddToPlaylistCommand => _addToPlaylistCommand ?? (_addToPlaylistCommand = new MvxAsyncCommand(AddToPlaylist));

        private IMvxCommand _favoriteCommand;
        public IMvxCommand FavoriteCommand => _favoriteCommand ?? (_favoriteCommand = new MvxCommand(Favorite));

        public override void Prepare(IMediaItem parameter)
        {
            Source = parameter;
        }

        public override Task Initialize()
        {
            Playlists.Add(new Playlist { Title = "Favorites" });

            return base.Initialize();
        }

        public override void ViewAppearing()
        {
            base.ViewAppearing();

            var favorites = Playlists?.FirstOrDefault(x => x.Title == "Favorites");
            if (favorites.MediaItems.Contains(_source))
            {
                FavoriteImage = ImageSource.FromFile("playback_controls_favorite_on");
            }
            else
            {
                FavoriteImage = ImageSource.FromFile("playback_controls_favorite_off");
            }
        }

        public override void ViewAppeared()
        {
            base.ViewAppeared();
            TimeSpanPosition = MediaManager.Position;
            Position = MediaManager.Position.TotalSeconds;
            TimeSpanDuration = MediaManager.Duration;
            Duration = MediaManager.Duration.TotalSeconds;
            MediaManager.PositionChanged += MediaManager_PositionChanged;
        }

        public override void ViewDisappeared()
        {
            base.ViewDisappeared();
            MediaManager.PositionChanged -= MediaManager_PositionChanged;
        }

        private void MediaManager_PositionChanged(object sender, MediaManager.Playback.PositionChangedEventArgs e)
        {
            if (!DragStarted)
            {
                TimeSpanPosition = e.Position;
                Position = e.Position.TotalSeconds;
            }
            TimeSpanDuration = MediaManager.Duration;
            Duration = MediaManager.Duration.TotalSeconds;
        }

        private void ShowHideControls()
        {
            ShowControls = !ShowControls;
        }

        private async Task PlayPause()
        {
            if (MediaManager.IsPlaying())
                PlayPauseImage = ImageSource.FromFile("playback_controls_play_button");
            else
                PlayPauseImage = ImageSource.FromFile("playback_controls_pause_button");

            await MediaManager.PlayPause();
        }

        private void Repeat()
        {
            MediaManager.ToggleRepeat();
            switch (MediaManager.RepeatMode)
            {
                case RepeatMode.Off:
                    RepeatImage = ImageSource.FromFile("playback_controls_repeat_off");
                    break;
                case RepeatMode.One:
                    RepeatImage = ImageSource.FromFile("playback_controls_repeat_once_on");
                    break;
                case RepeatMode.All:
                    RepeatImage = ImageSource.FromFile("playback_controls_repeat_on");
                    break;
            }
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

        private void Favorite()
        {
            var favorites = Playlists?.FirstOrDefault(x => x.Title == "Favorites");
            if (favorites.MediaItems.Contains(_source))
            {
                favorites.MediaItems.Remove(_source);
                FavoriteImage = ImageSource.FromFile("playback_controls_favorite_off");
            }
            else
            {
                favorites.MediaItems.Add(_source);
                FavoriteImage = ImageSource.FromFile("playback_controls_favorite_on");
                _userDialogs.Toast(GetText("Favorite"));
            }
        }

        private async Task AddToPlaylist()
        {
            await NavigationService.Navigate<AddToPlaylistViewModel, IMediaItem>(Source);
        }
    }

    public class Metadata
    {
        public string Key { get; set; }
        public string Value { get; set; }
    }
}
