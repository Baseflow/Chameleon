using System;
using Chameleon.iOS;
using CoreGraphics;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(Frame), typeof(MaterialFrameRenderer))]
namespace Chameleon.iOS
{
    /// <summary>
    /// Renderer to update all frames with better shadows matching material design standards
    /// </summary>
    
    public class MaterialFrameRenderer : FrameRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Frame> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement != null)
            {
                if (Element.HasShadow)
                {
                    // Update shadow to match better material design standards of elevation
                    Layer.ShadowRadius = 2.0f;
                    Layer.ShadowColor = UIColor.Gray.CGColor;
                    Layer.ShadowOffset = new CGSize(2, 2);
                    Layer.ShadowOpacity = 0.80f;
                    Layer.ShadowPath = UIBezierPath.FromRect(Layer.Bounds).CGPath;
                    Layer.MasksToBounds = false;
                }
            }
        }
    }
}
