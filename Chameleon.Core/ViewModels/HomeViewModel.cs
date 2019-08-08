using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Acr.UserDialogs;
using Chameleon.Services.Services;
using MediaManager;
using MediaManager.Library;
using MediaManager.Media;
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

        public HomeViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService, IUserDialogs userDialogs, IPlaylistService playlistService) : base(logProvider, navigationService)
        {
            _userDialogs = userDialogs ?? throw new ArgumentNullException(nameof(userDialogs));
            _playlistService = playlistService ?? throw new ArgumentNullException(nameof(playlistService));
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

        private IMvxAsyncCommand _playerCommand;
        public IMvxAsyncCommand PlayerCommand => _playerCommand ?? (_playerCommand = new MvxAsyncCommand(PlaySelectedMediaItem));

        private IMvxAsyncCommand _openUrlCommand;
        public IMvxAsyncCommand OpenUrlCommand => _openUrlCommand ?? (_openUrlCommand = new MvxAsyncCommand(OpenUrl));

        private IMvxAsyncCommand _openFileCommand;
        public IMvxAsyncCommand OpenFileCommand => _openFileCommand ?? (_openFileCommand = new MvxAsyncCommand(OpenFile));

        private IMvxAsyncCommand _openPlaylistCommand;
        public IMvxAsyncCommand OpenPlaylistCommand => _openPlaylistCommand ?? (_openPlaylistCommand = new MvxAsyncCommand(OpenPlaylist));
        
        public override async Task Initialize()
        {
            RecentlyPlayedItems.ReplaceWith(await _playlistService.GetPlaylist());
            Playlists.ReplaceWith(await _playlistService.GetPlaylists());
        }

        private async Task OpenUrl()
        {
            var result = await _userDialogs.PromptAsync("Enter url", inputType: InputType.Url);

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

        private async Task PlaySelectedMediaItem()
        {
            if (_selectedMediaItem != null)
            {
                await NavigationService.Navigate<PlayerViewModel, IMediaItem>(SelectedMediaItem);
                SelectedMediaItem = null;
            }
            return;
        }

        private async Task OpenPlaylist()
        {
            if (_selectedPlaylist != null)
            {
                await NavigationService.Navigate<PlaylistViewModel, IPlaylist>(SelectedPlaylist);
                SelectedPlaylist = null;
            }
            return;
        }
    }
}
