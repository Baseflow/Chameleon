using System;
using Chameleon.Core.Effects;
using Chameleon.iOS.Effects;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly:ResolutionGroupName ("Chameleon")]
[assembly: ExportEffect(typeof(iOSPressedStateEffect), nameof(PressedStateEffect))]

namespace Chameleon.iOS.Effects
{
    public class iOSPressedStateEffect : PlatformEffect
    {
        UIColor backgroundColor;

        protected override void OnAttached()
        { 
            try
            {
                Control.BackgroundColor = backgroundColor;
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