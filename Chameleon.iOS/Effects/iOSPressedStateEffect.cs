 using System;
using System.ComponentModel;
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
                Control.BackgroundColor = backgroundColor = UIColor.Black;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Cannot set property on attached control. Error: ", ex.Message);
            }
        }

        protected override void OnDetached()
        {
        }

        protected override void OnElementPropertyChanged(PropertyChangedEventArgs args)
        {
            base.OnElementPropertyChanged(args);

            try
            {
                if (args.PropertyName == "IsFocused")
                {
                    if (Control.BackgroundColor == backgroundColor)
                    {
                        Control.BackgroundColor = UIColor.Black;
                    }
                    else
                    {
                        Control.BackgroundColor = backgroundColor;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Cannot set property on attached control. Error: ", ex.Message);
            }
        }
    }
}

//var cell = base.GetCell(item, reusableCell, tv);
//        var view = item as CustomViewCell;
//        cell.SelectedBackgroundView = new UIView
//            {
//                BackgroundColor = view.SelectedItemBackgroundColor.ToUIColor(),
//            };
//            return cell;

