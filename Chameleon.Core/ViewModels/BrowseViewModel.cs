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
        public IMvxAsyncCommand AddCommand => _addCommand ?? (_addCommand = new MvxAsyncCommand(async () => await NavigationService.Navigate<ProvidersOverviewViewModel>(), allowConcurrentExecutions: true));

        public bool IsArtistsVisible => string.IsNullOrEmpty(SearchText) && FavoriteArtists.Count > 0;

        public bool ShowEmptyView => RecentlyPlayedItems.Count == 0;

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
                    var searchedItems = _recentlyPlayedItems.Where(x => x.DisplayTitle.ToLower().Contains(SearchText.ToLower()) || x.DisplaySubtitle.ToLower().Contains(SearchText.ToLower()));
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

        private bool _isInitialized;
        public override async void ViewAppearing()
        {
            base.ViewAppearing();

            if (_isInitialized)
                await ReloadData().ConfigureAwait(false);

            _isInitialized = true;
        }

        public override async Task ReloadData(bool forceReload = false)
        {
            IsLoading = true;

            try
            {
                var mediaItems = await _mediaManager.Library.GetAll<IMediaItem>().ConfigureAwait(false);
                RecentlyPlayedItems.ReplaceWith(mediaItems.OrderBy(x => x.DisplayTitle).ToList());
            }
            catch (Exception)
            {
            }

            IsLoading = false;
            //await RaisePropertyChanged(nameof(HasRecent));
            //await RaisePropertyChanged(nameof(HasNoPlaylists));
        }
    }
}
