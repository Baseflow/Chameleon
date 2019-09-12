using System;
using System.Threading.Tasks;
using Acr.UserDialogs;
using MediaManager;
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
        private readonly IUserDialogs _userDialogs;
        private readonly IMediaManager _mediaManager;

        public AddToPlaylistViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService, IUserDialogs userDialogs, IMediaManager mediaManager) : base(logProvider, navigationService)
        {
            _userDialogs = userDialogs ?? throw new ArgumentNullException(nameof(userDialogs));
            _mediaManager = mediaManager;
        }

        private IMediaItem _mediaItem { get; set; }

        public MvxObservableCollection<IPlaylist> Playlists { get; set; } = new MvxObservableCollection<IPlaylist>();

        private IMvxAsyncCommand<IPlaylist> _addToPlaylistCommand;
        public IMvxAsyncCommand<IPlaylist> AddToPlaylistCommand => _addToPlaylistCommand ?? (_addToPlaylistCommand = new MvxAsyncCommand<IPlaylist>(AddToPlaylist));

        private IMvxAsyncCommand _addPlaylistCommand;
        public IMvxAsyncCommand AddPlaylistCommand => _addPlaylistCommand ?? (_addPlaylistCommand = new MvxAsyncCommand(AddPlaylist));

        private string _playlistName;
        public string PlaylistName
        {
            get => _playlistName;
            set
            {
                SetProperty(ref _playlistName, value);
            }
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

        public override void Prepare(IMediaItem parameter)
        {
            _mediaItem = parameter;
        }

        public override async Task Initialize()
        {
            Playlists.ReplaceWith(await _mediaManager.Library.GetAll<IPlaylist>());

            IPlaylist playlist = null;
            await GetSubtitle(playlist);
        }

        private async Task GetSubtitle(IPlaylist playlist)
        {
            var trackAmount = new FormattedString();
            trackAmount.Spans.Add(new Span { Text = playlist.MediaItems.Count.ToString() });
            trackAmount.Spans.Add(new Span { Text = " tracks" });
            TrackAmount = trackAmount;

            var playlistTime = new FormattedString();
            playlistTime.Spans.Add(new Span { Text = playlist.TotalTime.Hours.ToString() });
            playlistTime.Spans.Add(new Span { Text = " hours, " });
            playlistTime.Spans.Add(new Span { Text = playlist.TotalTime.Minutes.ToString() });
            playlistTime.Spans.Add(new Span { Text = " minutes" });
            PlaylistTime = playlistTime;
        }


        private async Task AddToPlaylist(IPlaylist arg)
        {
            arg.MediaItems.Add(_mediaItem);
            await _mediaManager.Library.AddOrUpdate<IPlaylist>(arg);
            _userDialogs.Toast(GetText("AddedToPlaylist"));

            await NavigationService.Close(this);
        }

        private async Task AddPlaylist()
        {
            if (!string.IsNullOrEmpty(PlaylistName))
            {
                var playlist = new Playlist() { Title = PlaylistName };
                Playlists.Add(playlist);
                await _mediaManager.Library.AddOrUpdate<IPlaylist>(playlist);
                PlaylistName = string.Empty;
            }
            else
                await _userDialogs.AlertAsync(GetText("InvalidName"));
        }
    }
}
