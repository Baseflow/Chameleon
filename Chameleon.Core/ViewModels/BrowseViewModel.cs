using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Acr.UserDialogs;
using Chameleon.Services.Services;
using MediaManager;
using MediaManager.Media;
using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace Chameleon.Core.ViewModels
{
    public class BrowseViewModel : BaseViewModel
    {
        private readonly IUserDialogs _userDialogs;
        private readonly IMediaManager _mediaManager;
        private readonly IPlaylistService _playlistService;

        public BrowseViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService, IUserDialogs userDialogs, IMediaManager mediaManager, IPlaylistService playlistService) : base(logProvider, navigationService)
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

        public bool IsVisible
        {
            get
            {
                return string.IsNullOrEmpty(SearchText);
            }
        }

        private MvxObservableCollection<IMediaItem> _recentlyPlayedItems = new MvxObservableCollection<IMediaItem>();
        public MvxObservableCollection<IMediaItem> RecentlyPlayedItems
        {
            get
            {
                if (string.IsNullOrWhiteSpace(SearchText))
                {
                    return _recentlyPlayedItems;
                }
                else
                {
                    var searchedItems = _recentlyPlayedItems.Where(x => x.Title.ToLower().Contains(SearchText.ToLower()) || x.Album.ToLower().Contains(SearchText.ToLower()));
                    return new MvxObservableCollection<IMediaItem>(searchedItems);
                }
            }
            set => SetProperty(ref _recentlyPlayedItems, value);
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
                RaisePropertyChanged(nameof(RecentlyPlayedItems));
                RaisePropertyChanged(nameof(IsVisible));
            }
        }

        public override async Task Initialize()
        {
            RecentlyPlayedItems.ReplaceWith(await _playlistService.GetPlaylist());
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
