using System;
using System.Linq;
using Android.Util;
using Chameleon.Droid.Effects;
using Chameleon.Core.Effects;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ResolutionGroupName("Chameleon")]
[assembly: ExportEffect(typeof(AndroidTransparentSelectableEffect), nameof(TransparentSelectableEffect))]
namespace Chameleon.Droid.Effects
{
    public class AndroidTransparentSelectableEffect : PlatformEffect
    {
        protected override void OnAttached()
        {
            try
            {
                var effect = (TransparentSelectableEffect)Element.Effects.FirstOrDefault(e => e is TransparentSelectableEffect);
                int resid = 0;
                if (effect != null && effect.Borderless)
                    resid = Android.Resource.Attribute.SelectableItemBackgroundBorderless;
                else
                    resid = Android.Resource.Attribute.SelectableItemBackground;

                var value = new TypedValue();
                Android.App.Application.Context.Theme.ResolveAttribute(resid, value, true);

                if (Control != null)
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
