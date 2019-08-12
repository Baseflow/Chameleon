using System;
using System.Collections.Generic;
using System.Linq;
using Chameleon.Core;
using Foundation;
using MediaManager;
using MvvmCross.Forms.Platforms.Ios.Core;
using UIKit;
using Xamarin.Forms;

namespace Chameleon.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : MvxFormsApplicationDelegate<Setup, Core.App, FormsApp>
    {
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            Forms.SetFlags("CollectionView_Experimental");

            FFImageLoading.Forms.Platform.CachedImageRenderer.Init();
            CrossMediaManager.Current.Init();

            return base.FinishedLaunching(app, options);
        }
    }
}
