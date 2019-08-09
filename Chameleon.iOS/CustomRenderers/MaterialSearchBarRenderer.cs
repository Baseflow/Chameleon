using System;
using Chameleon.iOS.CustomRenderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(SearchBar), typeof(MaterialSearchBarRenderer))]
namespace Chameleon.iOS.CustomRenderers
{
    public class MaterialSearchBarRenderer : SearchBarRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<SearchBar> e)
        {
            base.OnElementChanged(e);

            UISearchBar bar = Control;

            if (bar == null)
                return;

            bar.AutocapitalizationType = UITextAutocapitalizationType.None;
            bar.AutocorrectionType = UITextAutocorrectionType.No;
            bar.BarStyle = UIBarStyle.BlackTranslucent;
            bar.BarTintColor = UIColor.DarkGray;
            bar.KeyboardType = UIKeyboardType.ASCIICapable;
        }
    }
}
