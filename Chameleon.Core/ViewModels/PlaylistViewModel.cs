using System;
using System.Linq;
using System.Threading.Tasks;
using Acr.UserDialogs;
using Chameleon.Services.Services;
using MediaManager;
using MediaManager.Library;
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
        private readonly IMediaManager _mediaManager;
        private readonly IPlaylistService _playlistService;

        public PlaylistViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService, IUserDialogs userDialogs, IMediaManager mediaManager, IPlaylistService playlistService) : base(logProvider, navigationService)
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

        private IPlaylist _playlist;
        public IPlaylist Playlist
        {
            get => _playlist;
            set => SetProperty(ref _playlist, value);
        }

        public bool IsVisible
        {
            get
            {
                return string.IsNullOrEmpty(SearchText);
            }
        }

        private ImageSource _playPauseImage = ImageSource.FromFile("playback_controls_pause_button");
        public ImageSource PlayPauseImage
        {
            get => _playPauseImage;
            set => SetProperty(ref _playPauseImage, value);
        }

        private MvxObservableCollection<IMediaItem> _playlistItems = new MvxObservableCollection<IMediaItem>();
        public MvxObservableCollection<IMediaItem> PlaylistItems
        {
            get
            {
                if (string.IsNullOrWhiteSpace(SearchText))
                {
                    return _playlistItems;
                }
                else
                {
                    var searchedItems = _playlistItems.Where(x => x.Title.ToLower().Contains(SearchText.ToLower()) || x.Album.ToLower().Contains(SearchText.ToLower()));
                    return new MvxObservableCollection<IMediaItem>(searchedItems);
                }
            }
            set => SetProperty(ref _playlistItems, value);
        }

        private IMvxAsyncCommand _playerCommand;
        public IMvxAsyncCommand PlayerCommand => _playerCommand ?? (_playerCommand = new MvxAsyncCommand(PlayWhenSelected));

        private string _searchText;
        public string SearchText
        {
            get => _searchText;
            set
            {
                SetProperty(ref _searchText, value);
                RaisePropertyChanged(nameof(PlaylistItems));
                RaisePropertyChanged(nameof(IsVisible));
            }
        }

        public override void Prepare(IPlaylist playlist)
        {
            Playlist = playlist;
        }

        private async Task PlayWhenSelected()
        {
            if (_selectedMediaItem != null)
            {
                await NavigationService.Navigate<PlayerViewModel, IMediaItem>(SelectedMediaItem);
                SelectedMediaItem = null;
            }
            return;
        }
    }
}
