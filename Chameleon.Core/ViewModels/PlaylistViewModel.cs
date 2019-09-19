using System;
using System.Linq;
using System.Threading.Tasks;
using Acr.UserDialogs;
using Chameleon.Core.Helpers;
using MediaManager;
using MediaManager.Library;
using MediaManager.Media;
using MediaManager.Playback;
using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using Xamarin.Forms;

namespace Chameleon.Core.ViewModels
{
    public class PlaylistViewModel : BaseViewModel<IPlaylist>
    {
        private readonly IUserDialogs _userDialogs;
        private IMediaManager _mediaManager;
        public IMediaManager MediaManager { get; }


        public PlaylistViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService, IUserDialogs userDialogs, IMediaManager mediaManager)
            : base(logProvider, navigationService)
        {
            _userDialogs = userDialogs ?? throw new ArgumentNullException(nameof(userDialogs));
            _mediaManager = mediaManager ?? throw new ArgumentNullException(nameof(mediaManager));
            MediaManager = mediaManager ?? throw new ArgumentNullException(nameof(mediaManager));

        }

        public MvxObservableCollection<IMediaItem> MediaItems { get; set; } = new MvxObservableCollection<IMediaItem>();
        
        private IMediaItem _selectedMediaItem;
        public IMediaItem SelectedMediaItem
        {
            get => _selectedMediaItem;
            set => SetProperty(ref _selectedMediaItem, value);
        }

        private IPlaylist _currentPlaylistSource;
        public IPlaylist CurrentPlaylistSource
        {
            get
            {
                if (string.IsNullOrWhiteSpace(SearchText))
                {
                    return _currentPlaylistSource;
                }
                else
                {
                    var searchedItems = _currentPlaylistSource?.MediaItems?.Where(x => x.Title.ToLower().Contains(SearchText.ToLower()) || x.Album.ToLower().Contains(SearchText.ToLower()));
                    var playlist = new Playlist();
                    foreach (var item in searchedItems)
                        playlist.MediaItems.Add(item);

                    return playlist;
                }
            }
            set => SetProperty(ref _currentPlaylistSource, value);
        }

        private IPlaylist _currentPlaylist;
        public IPlaylist CurrentPlaylist
        {
            get => _currentPlaylist;
            set => SetProperty(ref _currentPlaylist, value);
        }

        public bool IsVisible
        {
            get
            {
                return string.IsNullOrEmpty(SearchText);
            }
        }

        private IMediaItem _activeMediaItem;
        public IMediaItem ActiveMediaItem
        {
            get => _activeMediaItem;
            set => SetProperty(ref _activeMediaItem, value);
        }

        private FormattedString _trackAmount;
        public FormattedString TrackAmount
        {
            get => _trackAmount;
            set => SetProperty(ref _trackAmount, value);
        }

        private FormattedString _playlistTime;
        public FormattedString PlaylistTime
        {
            get => _playlistTime;
            set => SetProperty(ref _playlistTime, value);
        }

        private string _searchText;
        public string SearchText
        {
            get => _searchText;
            set
            {
                SetProperty(ref _searchText, value);
                RaisePropertyChanged(nameof(CurrentPlaylist));
                RaisePropertyChanged(nameof(IsVisible));
            }
        }

        private double _progress = 0;
        public double Progress
        {
            get => _progress;
            set => SetProperty(ref _progress, value);
        }

        private string _timeLeft = "0";
        public string TimeLeft
        {
            get => _timeLeft;
            set => SetProperty(ref _timeLeft, value);
        }

        private IMvxAsyncCommand<IMediaItem> _playerCommand;
        public IMvxAsyncCommand<IMediaItem> PlayerCommand => _playerCommand ?? (_playerCommand = new MvxAsyncCommand<IMediaItem>(Play));

        private IMvxAsyncCommand _startPlaylistCommand;
        public IMvxAsyncCommand StartPlaylistCommand => _startPlaylistCommand ?? (_startPlaylistCommand = new MvxAsyncCommand(StartPlaylist));

        public override void Prepare(IPlaylist playlist)
        {
            CurrentPlaylist = playlist;
            CurrentPlaylistSource = playlist;

            var trackAmount = new FormattedString();
            trackAmount.Spans.Add(new Span { Text = CurrentPlaylist.MediaItems.Count.ToString(), FontAttributes = FontAttributes.Bold, FontSize = 12 });
            trackAmount.Spans.Add(new Span { Text = " tracks" });
            TrackAmount = trackAmount;

            var playlistTime = new FormattedString();
            playlistTime.Spans.Add(new Span { Text = CurrentPlaylist.TotalTime.Hours.ToString(), FontAttributes = FontAttributes.Bold, FontSize = 12 });
            playlistTime.Spans.Add(new Span { Text = " hours, " });
            playlistTime.Spans.Add(new Span { Text = CurrentPlaylist.TotalTime.Minutes.ToString(), FontAttributes = FontAttributes.Bold, FontSize = 12 });
            playlistTime.Spans.Add(new Span { Text = " minutes" });
            PlaylistTime = playlistTime;
        }

        public override async Task Initialize()
        {
            IsLoading = true;

            try
            {
                var mediaItem = await _mediaManager.Library.GetAll<MediaItem>();
                if (mediaItem != null)
                    MediaItems.ReplaceWith(mediaItem);
            }
            catch (Exception)
            {
            }

            IsLoading = false;
        }

        public override void ViewAppearing()
        {
            _mediaManager.MediaItemChanged += MediaManager_MediaItemChanged;
            ActiveMediaItem = _mediaManager.Queue.Current;
            CurrentPlaylistSource = null;
            CurrentPlaylistSource = CurrentPlaylist;
            base.ViewAppearing();
        }

        public override void ViewAppeared()
        {
            base.ViewAppeared();

            Progress = MediaManager.Position.TotalSeconds / MediaManager.Duration.TotalSeconds;
            MediaManager.PositionChanged += MediaManager_PositionChanged;
            TimeLeft = $"-{(MediaManager.Position - MediaManager.Duration).ToString(@"mm\:ss")}";
        }

        public override void ViewDisappeared()
        {
            base.ViewDisappeared();
            MediaManager.PositionChanged -= MediaManager_PositionChanged;
        }

        public override void ViewDisappearing()
        {
            _mediaManager.MediaItemChanged -= MediaManager_MediaItemChanged;
            base.ViewDisappearing();
        }

        private void MediaManager_MediaItemChanged(object sender, MediaItemEventArgs e)
        {
            ActiveMediaItem = _mediaManager.Queue.Current;
            CurrentPlaylistSource = null;
            CurrentPlaylistSource = CurrentPlaylist;

            TimeLeft = $"-{(MediaManager.Position - MediaManager.Duration).ToString(@"mm\:ss")}";

            ReloadData();
        }

        private void MediaManager_PositionChanged(object sender, PositionChangedEventArgs e)
        {
            Progress = e.Position.TotalSeconds / MediaManager.Duration.TotalSeconds;

            TimeLeft = $"-{(e.Position - MediaManager.Duration).ToString(@"mm\:ss")}";
        }

        private async Task Play(IMediaItem mediaItem)
        {
            await NavigationService.Navigate<PlayerViewModel, IMediaItem>(mediaItem);
            SelectedMediaItem = null;
        }

        private async Task StartPlaylist()
        {
            await _mediaManager.Play(CurrentPlaylist.MediaItems);
            ActiveMediaItem = _mediaManager.Queue.Current;
            CurrentPlaylistSource = null;
            CurrentPlaylistSource = CurrentPlaylist;
        }
    }
}
