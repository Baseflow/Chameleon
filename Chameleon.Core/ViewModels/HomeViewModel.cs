using System;
using System.Threading.Tasks;
using Acr.UserDialogs;
using Chameleon.Services.Services;
using MediaManager;
using MediaManager.Library;
using MvvmCross;
using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using Plugin.FilePicker;
using Plugin.FilePicker.Abstractions;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;

namespace Chameleon.Core.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {
        private readonly IUserDialogs _userDialogs;
        private readonly IBrowseService _browseService;

        public IMediaManager MediaManager { get; }

        public HomeViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService, IUserDialogs userDialogs, IBrowseService browseService, IMediaManager mediaManager) : base(logProvider, navigationService)
        {
            _userDialogs = userDialogs ?? throw new ArgumentNullException(nameof(userDialogs));
            _browseService = browseService ?? throw new ArgumentNullException(nameof(browseService));

            MediaManager = mediaManager ?? throw new ArgumentNullException(nameof(mediaManager));
        }

        private bool _isInitialized;

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
        /*
        private IMediaItem _selectedMediaItem;
        public IMediaItem SelectedMediaItem
        {
            get => _selectedMediaItem;
            set => SetProperty(ref _selectedMediaItem, value);
        }*/

        private IPlaylist _selectedPlaylist;
        public IPlaylist SelectedPlaylist
        {
            get => _selectedPlaylist;
            set => SetProperty(ref _selectedPlaylist, value);
        }

        public bool HasRecent => !IsLoading && RecentlyPlayedItems.Count > 0;
        public bool HasNoPlaylists => !IsLoading && Playlists.Count == 0;

        private IMvxAsyncCommand _openPlayerCommand;
        public IMvxAsyncCommand OpenPlayerCommand => _openPlayerCommand ?? (_openPlayerCommand = new MvxAsyncCommand(() => NavigationService.Navigate<PlayerViewModel>()));

        private IMvxAsyncCommand _addPlaylistCommand;
        public IMvxAsyncCommand AddPlaylistCommand => _addPlaylistCommand ?? (_addPlaylistCommand = new MvxAsyncCommand(AddPlaylist));

        private IMvxAsyncCommand _openUrlCommand;
        public IMvxAsyncCommand OpenUrlCommand => _openUrlCommand ?? (_openUrlCommand = new MvxAsyncCommand(OpenUrl));

        private IMvxAsyncCommand _openVideoPickerCommand;
        public IMvxAsyncCommand OpenVideoPickerCommand => _openVideoPickerCommand ?? (_openVideoPickerCommand = new MvxAsyncCommand(OpenVideoPicker));

        private IMvxAsyncCommand _openFilePickerCommand;
        public IMvxAsyncCommand OpenFilePickerCommand => _openFilePickerCommand ?? (_openFilePickerCommand = new MvxAsyncCommand(OpenFilePicker));

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
            
            try
            {
                RecentlyPlayedItems.ReplaceWith(_browseService.RecentMedia);

                var playlists = await MediaManager.Library.GetAll<IPlaylist>().ConfigureAwait(false);
                if (playlists != null)
                    Playlists.ReplaceWith(playlists);
            }
            catch (Exception ex)
            { }

            IsLoading = false;
            await RaisePropertyChanged(nameof(HasRecent));
            await RaisePropertyChanged(nameof(HasNoPlaylists));
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

        private async Task OpenVideoPicker()
        {
            if (await CrossMedia.Current.Initialize() && CrossMedia.Current.IsPickVideoSupported)
            {
                string path = null;
                try
                {
                    var videoMediaFile = await CrossMedia.Current.PickVideoAsync();
                    if (videoMediaFile != null)
                    {
                        path = videoMediaFile.Path;
                    }
                }
                catch (MediaPermissionException ex)
                {
                    await _userDialogs.AlertAsync(GetText("EnablePermissions"));
                }
                if (!string.IsNullOrEmpty(path))
                {
                    var mediaItem = await CrossMediaManager.Current.Play(path);
                    await NavigationService.Navigate<PlayerViewModel, IMediaItem>(mediaItem);
                }
            }
            else
                await _userDialogs.AlertAsync(GetText("EnablePermissions"));
        }

        private async Task OpenFilePicker()
        {
            try
            {
                var status = await CrossPermissions.Current.RequestPermissionAsync<StoragePermission>();
                if(status != PermissionStatus.Granted)
                {
                    await _userDialogs.AlertAsync(GetText("EnablePermissions"));
                    return;
                }

                var fileData = await CrossFilePicker.Current.PickFile();
                if (fileData == null)
                    return; // user canceled file picking

                var mediaItem = await CrossMediaManager.Current.Play(fileData.FilePath);
                await NavigationService.Navigate<PlayerViewModel, IMediaItem>(mediaItem);
            }
            catch (Exception ex)
            {
                System.Console.WriteLine("Exception choosing file: " + ex.ToString());
            }
        }
        /*
        private async Task Play(IMediaItem mediaItem)
        {
            await NavigationService.Navigate<PlayerViewModel, IMediaItem>(mediaItem);
            SelectedMediaItem = null;
        }*/

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
                var playlist = new Playlist() { Title = result.Value };
                Playlists.Add(playlist);
                await MediaManager.Library.AddOrUpdate<IPlaylist>(playlist);
            }

            await ReloadData();
        }
    }
}
