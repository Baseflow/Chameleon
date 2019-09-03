using System;
using System.Linq;
using System.Threading.Tasks;
using Acr.UserDialogs;
using Chameleon.Services.Services;
using MediaManager;
using MediaManager.Library;
using MediaManager.Media;
using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using Xamarin.Forms;

namespace Chameleon.Core.ViewModels
{
    public class PlaylistViewModel : BaseViewModel<IPlaylist>
    {
        private readonly IUserDialogs _userDialogs;
        private readonly IPlaylistService _playlistService;
        private IMediaManager _mediaManager;

        public PlaylistViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService, IUserDialogs userDialogs, IMediaManager mediaManager, IPlaylistService playlistService)
            : base(logProvider, navigationService)
        {
            _userDialogs = userDialogs ?? throw new ArgumentNullException(nameof(userDialogs));
            _mediaManager = mediaManager ?? throw new ArgumentNullException(nameof(mediaManager));
            _playlistService = playlistService ?? throw new ArgumentNullException(nameof(playlistService));
        }

        private IMediaItem _selectedMediaItem;
        public IMediaItem SelectedMediaItem
        {
            get => _selectedMediaItem;
            set => SetProperty(ref _selectedMediaItem, value);
        }

        private IPlaylist _currentPlaylist;
        public IPlaylist CurrentPlaylist
        {
            get
            {
                if (string.IsNullOrWhiteSpace(SearchText))
                {
                    return _currentPlaylist;
                }
                else
                {
                    var searchedItems = _currentPlaylist.MediaItems.Where(x => x.Title.ToLower().Contains(SearchText.ToLower()) || x.Album.ToLower().Contains(SearchText.ToLower()));
                    var playlist = new Playlist();
                    foreach (var item in searchedItems)
                        playlist.MediaItems.Add(item);

                    return playlist;
                }
            }
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

        private IMvxAsyncCommand<IMediaItem> _playerCommand;
        public IMvxAsyncCommand<IMediaItem> PlayerCommand => _playerCommand ?? (_playerCommand = new MvxAsyncCommand<IMediaItem>(Play));

        private IMvxAsyncCommand _startPlaylistCommand;
        public IMvxAsyncCommand StartPlaylistCommand => _startPlaylistCommand ?? (_startPlaylistCommand = new MvxAsyncCommand(StartPlaylist));

        public override void Prepare(IPlaylist playlist)
        {

            CurrentPlaylist = playlist;

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

        public override void ViewAppearing()
        {
            _mediaManager.MediaItemChanged += MediaManager_MediaItemChanged;
            ActiveMediaItem = _mediaManager.Queue.Current;
            base.ViewAppearing();
        }

        public override void ViewDisappearing()
        {
            _mediaManager.MediaItemChanged -= MediaManager_MediaItemChanged;
            base.ViewDisappearing();
        }

        private void MediaManager_MediaItemChanged(object sender, MediaItemEventArgs e)
        {
            ActiveMediaItem = _mediaManager.Queue.Current;
        }

        private async Task Play(IMediaItem mediaItem)
        {
            await NavigationService.Navigate<PlayerViewModel, IMediaItem>(mediaItem);
            SelectedMediaItem = null;
        }

        private async Task StartPlaylist()
        {
            await _mediaManager.Play(CurrentPlaylist);
        }
    }
}
