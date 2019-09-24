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

namespace Chameleon.Core.ViewModels
{
    public class BrowseViewModel : BaseViewModel
    {
        private readonly IUserDialogs _userDialogs;
        private readonly IMediaManager _mediaManager;
        private readonly IBrowseService _browseService;

        public BrowseViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService, IUserDialogs userDialogs, IMediaManager mediaManager, IBrowseService browseService) : base(logProvider, navigationService)
        {
            _userDialogs = userDialogs ?? throw new ArgumentNullException(nameof(userDialogs));
            _mediaManager = mediaManager ?? throw new ArgumentNullException(nameof(mediaManager));
            _browseService = browseService ?? throw new ArgumentNullException(nameof(browseService));
        }

        private IMvxAsyncCommand _addCommand;
        public IMvxAsyncCommand AddCommand => _addCommand ?? (_addCommand = new MvxAsyncCommand(() => NavigationService.Navigate<ProvidersViewModel>()));


        private IMediaItem _selectedMediaItem;
        public IMediaItem SelectedMediaItem
        {
            get => _selectedMediaItem;
            set => SetProperty(ref _selectedMediaItem, value);
        }

        public bool IsArtistsVisible => string.IsNullOrEmpty(SearchText) && FavoriteArtists.Count > 0;

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

        public MvxObservableCollection<IArtist> FavoriteArtists { get; set; } = new MvxObservableCollection<IArtist>();

        private IMvxAsyncCommand<IArtist> _openArtistCommand;
        public IMvxAsyncCommand<IArtist> OpenArtistCommand => _openArtistCommand ?? (_openArtistCommand = new MvxAsyncCommand<IArtist>(OpenArtist));

        private async Task OpenArtist(IArtist arg)
        {
            //TODO:
        }

        private IMvxAsyncCommand<IMediaItem> _playerCommand;
        public IMvxAsyncCommand<IMediaItem> PlayerCommand => _playerCommand ?? (_playerCommand = new MvxAsyncCommand<IMediaItem>(Play));

        private string _searchText;
        public string SearchText
        {
            get => _searchText;
            set
            {
                SetProperty(ref _searchText, value);
                RaisePropertyChanged(nameof(RecentlyPlayedItems));
                RaisePropertyChanged(nameof(IsArtistsVisible));
            }
        }
        public override async Task Initialize()
        {

            IsLoading = true;

            try
            {
                RecentlyPlayedItems.ReplaceWith(await _browseService.GetMedia());
                //var browseService = await _browseService.GetMedia();
                //if (browseService != null)
                //    RecentlyPlayedItems.ReplaceWith(browseService);

            }
            catch (Exception)
            {
            }

            IsLoading = false;
        }
     

        private async Task Play(IMediaItem mediaItem)
        {
            await NavigationService.Navigate<PlayerViewModel, IMediaItem>(mediaItem);
            SelectedMediaItem = null;
        }
    }
}
