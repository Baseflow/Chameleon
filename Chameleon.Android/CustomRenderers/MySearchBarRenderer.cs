using Android.Content;
using Android.Widget;
using Chameleon.Android.CustomRenderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(SearchBar), typeof(MySearchBarRenderer))]
namespace Chameleon.Android.CustomRenderers
{
    public class MySearchBarRenderer : SearchBarRenderer
    {
        public MySearchBarRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<SearchBar> e)
        {
            base.OnElementChanged(e);

            SearchView searchView = (base.Control as SearchView);

            int textViewId = searchView.Context.Resources.GetIdentifier("android:id/search_src_text", null, null);
            EditText textView = (searchView.FindViewById(textViewId) as EditText);
            if (textView != null)
            {
                int searchMagIcon = searchView.Context.Resources.GetIdentifier("android:id/search_mag_icon", null, null);
                ImageView magIcon = (ImageView)searchView.FindViewById(searchMagIcon);
                magIcon.SetColorFilter(global::Android.Graphics.Color.Rgb(255, 255, 255));
            }
        }
    }
}
