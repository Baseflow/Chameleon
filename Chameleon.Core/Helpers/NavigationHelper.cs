using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Acr.UserDialogs;
using MediaManager;
using MediaManager.Library;
using MvvmCross;
using MvvmCross.Commands;
using MvvmCross.Navigation;

namespace Chameleon.Core.Helpers
{
    public static class NavigationHelper
    {
        private static IMediaManager mediaManager => CrossMediaManager.Current;
        private static IUserDialogs userDialogs => UserDialogs.Instance;

        private static IMvxAsyncCommand<IContentItem> _optionsCommand;
        public static IMvxAsyncCommand<IContentItem> OptionsCommand => _optionsCommand ?? (_optionsCommand = new MvxAsyncCommand<IContentItem>(OpenOptions));

        private static IMvxNavigationService navigationService => Mvx.IoCProvider.Resolve<IMvxNavigationService>();

        private static async Task OpenOptions(IContentItem contentItem)
        {
            if(contentItem is IMediaItem mediaItem)
            {
                var config = new ActionSheetConfig();
                config.UseBottomSheet = true;
                //config.Title = "";
                //config.Message = "";
                config.Add("Add to playlist", () => {

                });
                config.Add("Add to queue", () => mediaManager.MediaQueue.Add(mediaItem));
                //config.Add("Show artist", () => { });
                userDialogs.ActionSheet(config);
            }
            else if(contentItem is IPlaylist playlist)
            {
                var config = new ActionSheetConfig();
                config.UseBottomSheet = true;
                config.Add("Rename playlist", async () => {
                    await RenamePlaylist();
                });
                config.Add("Delete playlist", () => {

                });
                userDialogs.ActionSheet(config);
            }
            else if (contentItem is IArtist artist)
            {
                var config = new ActionSheetConfig();
                config.UseBottomSheet = true;
                config.Add("Share", () => {

                });
                userDialogs.ActionSheet(config);
            }
            else if (contentItem is IAlbum album)
            {
                var config = new ActionSheetConfig();
                config.UseBottomSheet = true;
                config.Add("Share", () => {

                });
                userDialogs.ActionSheet(config);
            }
        }

        public static async Task RenamePlaylist()
        {
            var config = new PromptConfig();
            config.Message = "Enter the name of your new playlist";
            var result = await userDialogs.PromptAsync(config);
            if (result.Ok && !string.IsNullOrEmpty(result.Value))
            {
                //TODO: Save
            }
        }
    }
}
