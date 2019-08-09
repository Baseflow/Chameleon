using AppKit;
using Chameleon.Core;
using Foundation;
using MediaManager;
using MvvmCross.Forms.Platforms.Mac.Core;

namespace Chameleon.Mac
{
    [Register("AppDelegate")]
    public class AppDelegate : MvxFormsApplicationDelegate<MvxFormsMacSetup<Core.App, FormsApp>, Core.App, FormsApp>
    {
        public AppDelegate()
        {
        }

        public override void DidFinishLaunching(NSNotification notification)
        {
            CrossMediaManager.Current.Init();
            base.DidFinishLaunching(notification);
        }
    }
}
