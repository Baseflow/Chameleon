using Chameleon.Core;
using Foundation;
using MediaManager;
using MvvmCross.Forms.Platforms.Mac.Core;
using Xamarin.Forms;

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
            Forms.SetFlags("CollectionView_Experimental");
            CrossMediaManager.Current.Init();
            base.DidFinishLaunching(notification);
        }
    }
}
