using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using MediaManager;
using MediaManager.Media;
using MediaManager.Playback;
using MediaManager.Queue;
using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using Xamarin.Forms;

namespace Chameleon.Core.ViewModels
{
    public class PlayerViewModel : BaseViewModel<IMediaItem>
    {
        public IMediaManager MediaManager { get; }

        public PlayerViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService, IMediaManager mediaManager) : base(logProvider, navigationService)
        {
            MediaManager = mediaManager ?? throw new ArgumentNullException(nameof(mediaManager));
            MediaManager.PositionChanged += MediaManager_PositionChanged;
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

        private IMediaItem _source;
        public IMediaItem Source
        {
            get => _source;
            set
            {
                if (SetProperty(ref _source, value))
                {
                    var metaData = new List<Metadata>();
                    metaData.Add(new ViewModels.Metadata() { Key = "Author", Value = _source.Advertisement });
                    metaData.Add(new ViewModels.Metadata() { Key = "Album", Value = _source.Album });
                    metaData.Add(new ViewModels.Metadata() { Key = "AlbumArt", Object = _source.AlbumArt });
                    metaData.Add(new ViewModels.Metadata() { Key = "Album Artist", Value = _source.AlbumArtist });
                    metaData.Add(new ViewModels.Metadata() { Key = "Album Art Uri", Value = _source.AlbumArtUri });
                    metaData.Add(new ViewModels.Metadata() { Key = "Art", Object = _source.Art});
                    metaData.Add(new ViewModels.Metadata() { Key = "Artist", Value = _source.Artist });
                    metaData.Add(new ViewModels.Metadata() { Key = "Art Uri", Value = _source.ArtUri });
                    metaData.Add(new ViewModels.Metadata() { Key = "Author", Value = _source.Author });
                    metaData.Add(new ViewModels.Metadata() { Key = "BtFolderType", BtFolderType = _source.BtFolderType });
                    metaData.Add(new ViewModels.Metadata() { Key = "Compilation", Value = _source.Compilation });
                    metaData.Add(new ViewModels.Metadata() { Key = "Composer", Value = _source.Composer });
                    metaData.Add(new ViewModels.Metadata() { Key = "Date", Value = _source.Date });
                    metaData.Add(new ViewModels.Metadata() { Key = "Disc Number", IntValue = _source.DiscNumber });
                    metaData.Add(new ViewModels.Metadata() { Key = "Description", Value = _source.DisplayDescription });
                    metaData.Add(new ViewModels.Metadata() { Key = "Display Icon", Object = _source.DisplayIcon });
                    metaData.Add(new ViewModels.Metadata() { Key = "Display Icon Uri", Value = _source.DisplayIconUri });
                    metaData.Add(new ViewModels.Metadata() { Key = "Display Subtitle", Value = _source.DisplaySubtitle });
                    metaData.Add(new ViewModels.Metadata() { Key = "Display Tilte", Value = _source.DisplayTitle });
                    metaData.Add(new ViewModels.Metadata() { Key = "Download Status", DownloadStatus = _source.DownloadStatus });
                    metaData.Add(new ViewModels.Metadata() { Key = "Duration", TimeSpan = _source.Duration });
                    metaData.Add(new ViewModels.Metadata() { Key = "Extras", Object = _source.Extras });
                    metaData.Add(new ViewModels.Metadata() { Key = "File Extension", Value = _source.FileExtension });
                    metaData.Add(new ViewModels.Metadata() { Key = "Genre", Value = _source.Genre });
                    metaData.Add(new ViewModels.Metadata() { Key = "Is Metadata Extracted", Boolean = _source.IsMetadataExtracted});
                    metaData.Add(new ViewModels.Metadata() { Key = "MediaId", Value = _source.MediaId });
                    metaData.Add(new ViewModels.Metadata() { Key = "MediaLocation", MediaLocation = _source.MediaLocation });
                    metaData.Add(new ViewModels.Metadata() { Key = "Media Type", MediaType = _source.MediaType });
                    metaData.Add(new ViewModels.Metadata() { Key = "Media Uri", Value = _source.MediaUri });
                    metaData.Add(new ViewModels.Metadata() { Key = "Num Tracks", IntValue = _source.NumTracks });
                    metaData.Add(new ViewModels.Metadata() { Key = "Rating", Object = _source.Rating });
                    metaData.Add(new ViewModels.Metadata() { Key = "Title", Value = _source.Title });
                    metaData.Add(new ViewModels.Metadata() { Key = "Track Number", IntValue = _source.TrackNumber });
                    metaData.Add(new ViewModels.Metadata() { Key = "User Rating", Object = _source.UserRating });
                    metaData.Add(new ViewModels.Metadata() { Key = "Writer", Value = _source.Writer });
                    metaData.Add(new ViewModels.Metadata() { Key = "Year", IntValue = _source.Year });
                    Metadata = metaData;
                    RaisePropertyChanged(nameof(Metadata));
                }
            }
        }

        public IList<Metadata> Metadata { get; set; }

        private bool _showControls;
        public bool ShowControls
        {
            get => _showControls;
            set => SetProperty(ref _showControls, value);
        }

        private bool _dragStarted;
        public bool DragStarted
        {
            get => _dragStarted;
            set => SetProperty(ref _dragStarted, value);
        }

        private double _position;
        public double Position
        {
            get => _position;
            set
            {
                SetProperty(ref _position, value);
            }
        }

        private double _duration;
        public double Duration
        {
            get => _duration;
            set => SetProperty(ref _duration, value);
        }

        private TimeSpan _timeSpanPosition;
        public TimeSpan TimeSpanPosition
        {
            get => _timeSpanPosition;
            set
            {
                SetProperty(ref _timeSpanPosition, value);
            }
        }

        private TimeSpan _timeSpanDuration;
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
        public IMvxAsyncCommand AddToPlaylistCommand => _addToPlaylistCommand ?? (_addToPlaylistCommand = new MvxAsyncCommand(() => MediaManager.PlayNext()));

        private IMvxAsyncCommand _favoriteCommand;
        public IMvxAsyncCommand FavoriteCommand => _favoriteCommand ?? (_favoriteCommand = new MvxAsyncCommand(() => MediaManager.PlayNext()));

        public override void Prepare(IMediaItem parameter)
        {
            Source = parameter;
        }

        private void ShowHideControls()
        {
            ShowControls = !ShowControls;
        }

        private async Task PlayPause()
        {
            await MediaManager.PlayPause();

            if (MediaManager.IsPlaying())
                PlayPauseImage = ImageSource.FromFile("playback_controls_play_button");
            else
                PlayPauseImage = ImageSource.FromFile("playback_controls_pause_button");
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
    }

    public class Metadata
    {
        public string Key { get; set; }
        public string Value { get; set; }
        public int IntValue { get; set; }
        public bool Boolean { get; set; }
        public TimeSpan TimeSpan { get; set; }
        public MediaType MediaType { get; set; }
        public Object Object { get; set; }
        public BtFolderType BtFolderType { get; set; }
        public DownloadStatus DownloadStatus { get; set; }
        public MediaLocation MediaLocation { get; set; }
    }
}
