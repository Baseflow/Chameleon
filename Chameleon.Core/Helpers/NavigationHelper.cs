using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Acr.UserDialogs;
using Chameleon.Core.ViewModels;
using Chameleon.Services;
using MediaManager;
using MediaManager.Library;
using MvvmCross;
using MvvmCross.Commands;
using MvvmCross.Localization;
using MvvmCross.Navigation;

namespace Chameleon.Core.Helpers
{
    public static class NavigationHelper
    {
        private static IMediaManager _mediaManager => CrossMediaManager.Current;
        private static IUserDialogs _userDialogs => UserDialogs.Instance;
        private static IMvxNavigationService _navigationService => Mvx.IoCProvider.Resolve<IMvxNavigationService>();
        private static IMvxTextProvider _textProvider => Mvx.IoCProvider.Resolve<IMvxTextProvider>();

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

        private static async Task OpenOptions(IContentItem contentItem)
        {
            var config = new ActionSheetConfig();
            config.UseBottomSheet = true;
            config.Destructive = new ActionSheetOption(GetText("Cancel"));
            //config.Title = "";
            //config.Message = "";

            if (contentItem is IMediaItem mediaItem)
            {
                config.Add(GetText("AddToPlaylist"), async () => {
                    await _navigationService.Navigate<AddToPlaylistViewModel, IMediaItem>(mediaItem);
                });
                config.Add(GetText("AddToQueue"), () => _mediaManager.MediaQueue.Add(mediaItem));
                //config.Add(GetText("ShowArtist"), () => { });
                //config.Add(GetText("ShowAlbum"), () => { });
                
            }
            else if(contentItem is IPlaylist playlist)
            {
                config.Add(GetText("RenamePlaylist"), async () => {
                    await RenamePlaylist(playlist);
                });
                config.Add(GetText("DeletePlaylist"), async () => {
                    await DeletePlaylist(playlist);
                });
            }
            else if (contentItem is IArtist artist)
            {
                config.Add(GetText("Share"), () => {

                });
            }
            else if (contentItem is IAlbum album)
            {
                config.Add(GetText("Share"), () => {

                });
            }
            _userDialogs.ActionSheet(config);
        }

        private static async Task DeletePlaylist(IPlaylist playlist)
        {
            if(await _userDialogs.ConfirmAsync(GetText("SureToDelete")))
            {

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
                //TODO: Save
            }
        }
    }
}
