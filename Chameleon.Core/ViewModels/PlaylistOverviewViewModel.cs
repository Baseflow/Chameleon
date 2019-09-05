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

namespace Chameleon.Core.ViewModels
{
    public class PlaylistOverviewViewModel : BaseViewModel
    {
        private readonly IUserDialogs _userDialogs;
        private readonly IMediaManager _mediaManager;

        public PlaylistOverviewViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService, IUserDialogs userDialogs, IMediaManager mediaManager) : base(logProvider, navigationService)
        {
            _userDialogs = userDialogs ?? throw new ArgumentNullException(nameof(userDialogs));
            _mediaManager = mediaManager ?? throw new ArgumentNullException(nameof(mediaManager));
        }

        public MvxObservableCollection<IPlaylist> Playlists { get; set; } = new MvxObservableCollection<IPlaylist>();

        private IMvxAsyncCommand<IPlaylist> _openPlaylistCommand;
        public IMvxAsyncCommand<IPlaylist> OpenPlaylistCommand => _openPlaylistCommand ?? (_openPlaylistCommand = new MvxAsyncCommand<IPlaylist>(OpenPlaylist));

        private IMvxAsyncCommand _addPlaylistCommand;
        public IMvxAsyncCommand AddPlaylistCommand => _addPlaylistCommand ?? (_addPlaylistCommand = new MvxAsyncCommand(AddPlaylist));

        private async Task AddPlaylist()
        {
            var config = new PromptConfig();
            config.Message = GetText("EnterNewName");
            var result = await _userDialogs.PromptAsync(config);
            if (result.Ok && !string.IsNullOrEmpty(result.Value))
            {
                var playlist = new Playlist() { Title = result.Value };
                Playlists.Add(playlist);
                await _mediaManager.Library.AddOrUpdate<IPlaylist>(playlist);
            }
        }

        private IPlaylist _selectedItem;
        public IPlaylist SelectedItem
        {
            get => _selectedItem;
            set => SetProperty(ref _selectedItem, value);
        }

        public override async Task Initialize()
        {
            IsLoading = true;

            try
            {
                Playlists.ReplaceWith(await _mediaManager.Library.GetAll<IPlaylist>());
            }
            catch (Exception ex)
            {
                throw ex;
            }

            IsLoading = false;
        }

        private async Task OpenPlaylist(IPlaylist arg)
        {
            await NavigationService.Navigate<PlaylistViewModel, IPlaylist>(arg);
            SelectedItem = null;
        }
    }
}
