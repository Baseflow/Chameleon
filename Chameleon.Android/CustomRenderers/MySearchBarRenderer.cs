using Android.Content;
using Android.Widget;
using Chameleon.Droid.CustomRenderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(SearchBar), typeof(MySearchBarRenderer))]
namespace Chameleon.Droid.CustomRenderers
{
    public class MySearchBarRenderer : SearchBarRenderer
    {
        public MySearchBarRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<SearchBar> e)
        {
            base.OnElementChanged(e);

            var searchView = Control as SearchView;

            var textViewId = searchView.Context.Resources.GetIdentifier("android:id/search_src_text", null, null);
            var textView = (searchView.FindViewById(textViewId) as EditText);
            if (textView != null)
            {
                var searchMagIcon = searchView.Context.Resources.GetIdentifier("android:id/search_mag_icon", null, null);
                var magIcon = (ImageView)searchView.FindViewById(searchMagIcon);
                magIcon.SetColorFilter(Android.Graphics.Color.Rgb(255, 255, 255));
            }
        }
    }
}
