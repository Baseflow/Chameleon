using System;
using System.Linq;
using System.Threading.Tasks;
using Acr.UserDialogs;
using Chameleon.Core.ViewModels;
using Chameleon.Services;
using MediaManager;
using MediaManager.Library;
using MvvmCross;
using MvvmCross.Commands;
using MvvmCross.Forms.Presenters;
using MvvmCross.Localization;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using MvvmCross.Views;

namespace Chameleon.Core.Helpers
{
    public static class NavigationHelper
    {
        private static IMediaManager _mediaManager => CrossMediaManager.Current;
        private static IUserDialogs _userDialogs => UserDialogs.Instance;
        private static IMvxNavigationService _navigationService => Mvx.IoCProvider.Resolve<IMvxNavigationService>();
        private static IMvxTextProvider _textProvider => Mvx.IoCProvider.Resolve<IMvxTextProvider>();
        private static IMvxFormsPagePresenter _formsPagePresenter => Mvx.IoCProvider.Resolve<IMvxFormsPagePresenter>();

        private static IMvxViewModel _topViewModel => (_formsPagePresenter.CurrentPageTree.LastOrDefault() as IMvxView)?.ViewModel;
        private static Type _topViewModelType => _topViewModel?.GetType();

        private static IMvxAsyncCommand<IContentItem> _optionsCommand;
        public static IMvxAsyncCommand<IContentItem> OptionsCommand => _optionsCommand ?? (_optionsCommand = new MvxAsyncCommand<IContentItem>(OpenOptions));

        public static string GetText(string viewModel, string key)
        {
            return _textProvider.GetText(AppSettings.TextProviderNamespace, viewModel, key);
        }

        public static string GetText(string key)
        {
            return GetText("Shared", key);
        }

        private static Task OpenOptions(IContentItem contentItem)
        {
            var config = new ActionSheetConfig();
            config.UseBottomSheet = true;

            config.Cancel = new ActionSheetOption(GetText("Cancel"));
            //config.Title = "";
            //config.Message = "";

            if (contentItem is IMediaItem mediaItem)
            {
                if (_topViewModel is QueueViewModel queueViewModel)
                {
                    config.Add(GetText("RemoveFromQueue"), () =>
                    {
                        _mediaManager.Queue.Remove(mediaItem);
                        _userDialogs.Toast(GetText("ItemRemovedFromQueue"));
                    }, "remove_from_queue");
                }
                else
                {
                    config.Add(GetText("AddToQueue"), () =>
                    {
                        _mediaManager.Queue.Add(mediaItem);
                        _userDialogs.Toast(GetText("ItemAddedToQueue"));
                    }, "add_to_queue");
                }
                if (_topViewModel is PlaylistViewModel playlistViewModel)
                {
                    config.Add(GetText("RemoveFromPlaylist"), async () =>
                    {
                        playlistViewModel.CurrentPlaylist?.MediaItems?.Remove(mediaItem);
                        await _mediaManager.Library.AddOrUpdate<IPlaylist>(playlistViewModel.CurrentPlaylist);
                        _userDialogs.Toast(GetText("ItemRemovedFromPlaylist"));
                    }, "remove_from_queue");
                }
                config.Add(GetText("AddToPlaylist"), async () =>
                {
                    await _navigationService.Navigate<AddToPlaylistViewModel, IMediaItem>(mediaItem);
                }, "add_to_playlist");

                //config.Add(GetText("ShowArtist"), () => _navigationService.Navigate<ArtistViewModel>());
                //config.Add(GetText("ShowAlbum"), () => _navigationService.Navigate<AlbumViewModel>());
                //config.Add(GetText("Share"), () => { });
            }
            else if (contentItem is IPlaylist playlist)
            {
                config.Add(GetText("RenamePlaylist"), async () =>
                {
                    await RenamePlaylist(playlist);
                }, "icon_contextual_rename");
                config.Add(GetText("DeletePlaylist"), async () =>
                {
                    await DeletePlaylist(playlist);
                }, "delete");
            }
            else if (contentItem is IArtist artist)
            {
                config.Add(GetText("Share"), () =>
                {

                }, "share");
            }
            else if (contentItem is IAlbum album)
            {
                config.Add(GetText("Share"), () =>
                {

                }, "share");
            }
            _userDialogs.ActionSheet(config);
            return Task.CompletedTask;
        }

        private static async Task DeletePlaylist(IPlaylist playlist)
        {
            if (await _userDialogs.ConfirmAsync(GetText("SureToDelete")))
            {
                await _mediaManager.Library.Remove<IPlaylist>(playlist);
                _userDialogs.Toast(GetText("PlaylistDeleted"));
            }
        }

        public static async Task RenamePlaylist(IPlaylist playlist)
        {
            var config = new PromptConfig();
            config.Message = GetText("EnterNewName");
            var result = await _userDialogs.PromptAsync(config);
            if (result.Ok && !string.IsNullOrEmpty(result.Value))
            {
                playlist.Title = result.Value;
                await _mediaManager.Library.AddOrUpdate<IPlaylist>(playlist);
                _userDialogs.Toast(GetText("RenameSuccessful"));
            }
        }
    }
}
