using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Acr.UserDialogs;
using MediaManager;
using MediaManager.Library;
using MvvmCross.Commands;

namespace Chameleon.Core.Helpers
{
    public static class NavigationHelper
    {
        private static IMediaManager mediaManager => CrossMediaManager.Current;

        private static IMvxAsyncCommand<IContentItem> _optionsCommand;
        public static IMvxAsyncCommand<IContentItem> OptionsCommand => _optionsCommand ?? (_optionsCommand = new MvxAsyncCommand<IContentItem>(OpenOptions));

        private static Task OpenOptions(IContentItem contentItem)
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
                config.Add("Show artist", () => { });
                UserDialogs.Instance.ActionSheet(config);
            }
            else if(contentItem is IPlaylist playlist)
            {
                var config = new ActionSheetConfig();
                config.UseBottomSheet = true;
                config.Add("Delete playlist", () => {

                });
                UserDialogs.Instance.ActionSheet(config);
            }
            else if (contentItem is IArtist artist)
            {
                var config = new ActionSheetConfig();
                config.UseBottomSheet = true;
                config.Add("Delete artist", () => {

                });
                UserDialogs.Instance.ActionSheet(config);
            }
            else if (contentItem is IAlbum album)
            {
                var config = new ActionSheetConfig();
                config.UseBottomSheet = true;
                config.Add("Delete album", () => {

                });
                UserDialogs.Instance.ActionSheet(config);
            }
            return Task.CompletedTask;
        }
    }
}
