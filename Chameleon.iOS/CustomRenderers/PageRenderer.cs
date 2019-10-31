using System;
using Chameleon.Core.Helpers;
using MvvmCross;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(ContentPage), typeof(Chameleon.iOS.CustomRenderers.PageRenderer))]
namespace Chameleon.iOS.CustomRenderers
{
    public class PageRenderer : Xamarin.Forms.Platform.iOS.PageRenderer
    {
        protected override void OnElementChanged(VisualElementChangedEventArgs e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null || Element == null)
            {
                return;
            }

            try
            {
                SetAppTheme();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"\t\t\tERROR: {ex.Message}");
            }
        }

        public override void TraitCollectionDidChange(UITraitCollection previousTraitCollection)
        {
            base.TraitCollectionDidChange(previousTraitCollection);

            if (TraitCollection?.UserInterfaceStyle != previousTraitCollection?.UserInterfaceStyle)
            {
                SetAppTheme();
            }
        }

        private void SetAppTheme()
        {
            Mvx.IoCProvider.TryResolve<IThemeService>(out var themeService);
            if (themeService != null)
            {
                if (TraitCollection.UserInterfaceStyle == UIUserInterfaceStyle.Dark)
                {
                    themeService.UpdateTheme(Core.Models.ThemeMode.Dark);
                }
                else
                {
                    themeService.UpdateTheme(Core.Models.ThemeMode.Light);
                }
            }
        }
    }
}
