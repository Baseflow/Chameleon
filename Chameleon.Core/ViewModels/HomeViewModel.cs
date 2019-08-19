using System;
using System.Threading.Tasks;
using Acr.UserDialogs;
using Chameleon.Services.Services;
using MediaManager;
using MediaManager.Library;
using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using Plugin.Media;

namespace Chameleon.Core.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {
        private readonly IUserDialogs _userDialogs;
        private readonly IPlaylistService _playlistService;
        private readonly IBrowseService _browseService;

        public HomeViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService, IUserDialogs userDialogs, IPlaylistService playlistService, IBrowseService browseService) : base(logProvider, navigationService)
        {
            _userDialogs = userDialogs ?? throw new ArgumentNullException(nameof(userDialogs));
            _playlistService = playlistService ?? throw new ArgumentNullException(nameof(playlistService));
            _browseService = browseService ?? throw new ArgumentNullException(nameof(browseService));
        }

        private bool _isInitialized;

        public string AddPlaylistLabel
        {
            get
            {
                var text = GetText("AddPlaylist");
                return text;
            }
        }

        private MvxObservableCollection<IPlaylist> _playlists = new MvxObservableCollection<IPlaylist>();
        public MvxObservableCollection<IPlaylist> Playlists
        {
            get => _playlists;
            set => SetProperty(ref _playlists, value);
        }

        private MvxObservableCollection<IMediaItem> _recentlyPlayedItems = new MvxObservableCollection<IMediaItem>();
        public MvxObservableCollection<IMediaItem> RecentlyPlayedItems
        {
            get => _recentlyPlayedItems;
            set => SetProperty(ref _recentlyPlayedItems, value);
        }

        private IMediaItem _selectedMediaItem;
        public IMediaItem SelectedMediaItem
        {
            get => _selectedMediaItem;
            set => SetProperty(ref _selectedMediaItem, value);
        }

        private IPlaylist _selectedPlaylist;
        public IPlaylist SelectedPlaylist
        {
            get => _selectedPlaylist;
            set => SetProperty(ref _selectedPlaylist, value);
        }

        public bool HasRecent => !IsLoading && RecentlyPlayedItems.Count > 0;
        public bool HasNoPlaylists => !IsLoading && Playlists.Count == 0;

        private IMvxAsyncCommand<IMediaItem> _playerCommand;
        public IMvxAsyncCommand<IMediaItem> PlayerCommand => _playerCommand ?? (_playerCommand = new MvxAsyncCommand<IMediaItem>(Play));

        private IMvxAsyncCommand _addPlaylistCommand;
        public IMvxAsyncCommand AddPlaylistCommand => _addPlaylistCommand ?? (_addPlaylistCommand = new MvxAsyncCommand(AddPlaylist));

        private IMvxAsyncCommand _openUrlCommand;
        public IMvxAsyncCommand OpenUrlCommand => _openUrlCommand ?? (_openUrlCommand = new MvxAsyncCommand(OpenUrl));

        private IMvxAsyncCommand _openFileCommand;
        public IMvxAsyncCommand OpenFileCommand => _openFileCommand ?? (_openFileCommand = new MvxAsyncCommand(OpenFile));

        private IMvxAsyncCommand<IPlaylist> _openPlaylistCommand;
        public IMvxAsyncCommand<IPlaylist> OpenPlaylistCommand => _openPlaylistCommand ?? (_openPlaylistCommand = new MvxAsyncCommand<IPlaylist>(OpenPlaylist));

        private IMvxAsyncCommand _openPlaylistOverviewCommand;
        public IMvxAsyncCommand OpenPlaylistOverviewCommand => _openPlaylistOverviewCommand ?? (_openPlaylistOverviewCommand = new MvxAsyncCommand(() => NavigationService.Navigate<PlaylistOverviewViewModel>()));

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

            //var recentMedia = await _browseService.GetRecentMedia().ConfigureAwait(false);
            //if (recentMedia != null)
            //    RecentlyPlayedItems.ReplaceWith(recentMedia);

            var playlists = await _playlistService.GetPlaylists().ConfigureAwait(false);
            if (playlists != null)
                Playlists.ReplaceWith(playlists);

            RaisePropertyChanged(nameof(HasRecent));
            RaisePropertyChanged(nameof(HasNoPlaylists));
            IsLoading = false;
        }

        private async Task OpenUrl()
        {
            var result = await _userDialogs.PromptAsync(GetText("EnterUrl"), inputType: InputType.Url);

            //TODO: Check if the url is valid
            if (!string.IsNullOrWhiteSpace(result.Value))
            {
                var mediaItem = await CrossMediaManager.Current.Play(result.Value);
                await NavigationService.Navigate<PlayerViewModel, IMediaItem>(mediaItem);
            }
        }

        private async Task OpenFile()
        {
            if (await CrossMedia.Current.Initialize())
            {
                var videoMediaFile = await CrossMedia.Current.PickVideoAsync();

                if (videoMediaFile != null)
                {
                    var mediaItem = await CrossMediaManager.Current.Play(videoMediaFile.Path);
                    await NavigationService.Navigate<PlayerViewModel, IMediaItem>(mediaItem);
                }
            }
        }

        private async Task Play(IMediaItem mediaItem)
        {
            await NavigationService.Navigate<PlayerViewModel, IMediaItem>(mediaItem);
            SelectedMediaItem = null;
        }

        private async Task OpenPlaylist(IPlaylist playlist)
        {
            await NavigationService.Navigate<PlaylistViewModel, IPlaylist>(playlist);
            SelectedPlaylist = null;
        }

        private async Task AddPlaylist()
        {
            var config = new PromptConfig();
            config.Message = GetText("EnterNewName");
            var result = await _userDialogs.PromptAsync(config);
            if (result.Ok && !string.IsNullOrEmpty(result.Value))
            {
                Playlists.Add(new Playlist() { Title = result.Value });
                await _playlistService.SavePlaylists(Playlists);
            }

            await ReloadData();
        }
    }
}
