using MediaManager;
using MediaManager.Library;
using Xamarin.Forms;

namespace Chameleon.Core.Templates
{
    public class MediaItemTemplateSelector : DataTemplateSelector
    {
        public DataTemplate ActiveTemplate { get; set; }
        public DataTemplate InActiveTemplate { get; set; }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            var currentItem = CrossMediaManager.Current.MediaQueue?.Current?.MediaUri;
            var mediaItem = ((IMediaItem)item)?.MediaUri;
            var template = currentItem == mediaItem && currentItem != null ? ActiveTemplate : InActiveTemplate;
            return template;
        }
    }
}
