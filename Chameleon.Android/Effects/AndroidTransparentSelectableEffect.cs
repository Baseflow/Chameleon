using System;
using Android.Util;
using Chameleon.Android.Effects;
using Chameleon.Core.Effects;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ResolutionGroupName("Chameleon")]
[assembly: ExportEffect(typeof(AndroidTransparentSelectableEffect), nameof(TransparentSelectableEffect))]
namespace Chameleon.Android.Effects
{
    public class AndroidTransparentSelectableEffect : PlatformEffect
    {
        protected override void OnAttached()
        {
            try
            {
                var value = new TypedValue();
                global::Android.App.Application.Context.Theme.ResolveAttribute(global::Android.Resource.Attribute.SelectableItemBackgroundBorderless, value, true);
                Control.SetBackgroundResource(value.ResourceId);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Cannot set property on attached control. Error: ", ex.Message);
            }
        }

        protected override void OnDetached()
        {
        }
    }
}
