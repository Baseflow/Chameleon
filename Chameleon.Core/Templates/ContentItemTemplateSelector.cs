using MediaManager;
using MediaManager.Library;
using Xamarin.Forms;

namespace Chameleon.Core.Templates
{
    public class ContentItemTemplateSelector : DataTemplateSelector
    {
        IMediaManager MediaManager => CrossMediaManager.Current;

        public DataTemplate CurrentMediaItemTemplate { get; set; } = new DataTemplate(() => new CurrentMediaItemTemplate());
        public DataTemplate MediaItemTemplate { get; set; } = new DataTemplate(() => new MediaItemTemplate());
        public DataTemplate HorizontalMediaItemTemplate { get; set; } = new DataTemplate(() => new HorizontalMediaItemTemplate());
        public DataTemplate PlaylistTemplate { get; set; } = new DataTemplate(() => new PlaylistItemTemplate());
        public DataTemplate HorizontalPlaylistTemplate { get; set; } = new DataTemplate(() => new HorizontalMediaItemTemplate());

        public virtual bool Horizontal => false;

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            switch (item)
            {
                case IMediaItem mediaItem:
                    if(Horizontal)
                        return HorizontalMediaItemTemplate;
                    else if (ReferenceEquals(mediaItem, MediaManager.Queue.Current))
                        return CurrentMediaItemTemplate;
                    else
                        return MediaItemTemplate;
                case IPlaylist playlist:
                    if (Horizontal)
                        return HorizontalPlaylistTemplate;
                    else
                        return PlaylistTemplate;
                default:
                    break;
            }
            return new DataTemplate();
        }
    }
}
