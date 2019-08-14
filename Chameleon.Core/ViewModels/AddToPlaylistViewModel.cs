using System;
using System.Threading.Tasks;
using Chameleon.Services.Services;
using MediaManager.Library;
using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using Xamarin.Forms;

namespace Chameleon.Core.ViewModels
{
    public class AddToPlaylistViewModel : BaseViewModel<IMediaItem>
    {
        private readonly IPlaylistService _playlistService;

        public AddToPlaylistViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService, IPlaylistService playlistService) : base(logProvider, navigationService)
        {
            _playlistService = playlistService ?? throw new ArgumentNullException(nameof(playlistService));
        }

        private IMediaItem _mediaItem { get; set; }

        public MvxObservableCollection<IPlaylist> Playlists { get; set; } = new MvxObservableCollection<IPlaylist>();

        private IMvxAsyncCommand<IPlaylist> _addToPlaylistCommand;
        public IMvxAsyncCommand<IPlaylist> AddToPlaylistCommand => _addToPlaylistCommand ?? (_addToPlaylistCommand = new MvxAsyncCommand<IPlaylist>(AddToPlaylist));

        private IMvxAsyncCommand<IPlaylist> _addNewPlaylistCommand;
        public IMvxAsyncCommand<IPlaylist> AddNewPlaylistCommand => _addNewPlaylistCommand ?? (_addNewPlaylistCommand = new MvxAsyncCommand<IPlaylist>(AddNewPlaylist));

        private IMediaItem _selectedItem;
        public IMediaItem SelectedItem
        {
            get => _selectedItem;
            set => SetProperty(ref _selectedItem, value);
        }

        private string _addNewPlaylistName;
        public string AddNewPlaylistName
        {
            get => _addNewPlaylistName;
            set
            {
                SetProperty(ref _addNewPlaylistName, value);
            }
        }

        public override async Task Initialize()
        {
            Playlists.ReplaceWith(await _playlistService.GetPlaylists());
        }

        private async Task AddToPlaylist(IPlaylist arg)
        {
            arg.Add(_mediaItem);

            //TODO: Save playlist
            //await _playlistService.Save();

            await NavigationService.Close(this);
        }


        private async Task AddNewPlaylist(IPlaylist arg)
        {
            arg.Add((MediaManager.Library.IMediaItem)Playlists);

            //TODO: Save playlist
            //await _playlistService.Save();

            await NavigationService.Close(this);
        }

        public override void Prepare(IMediaItem parameter)
        {
            _mediaItem = parameter;
        }

     
    }
}
