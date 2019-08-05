using AppKit;
using Chameleon.Core;
using Foundation;
using MvvmCross.Forms.Platforms.Mac.Core;

namespace Chameleon.Mac
{
    [Register("AppDelegate")]
    public class AppDelegate : MvxFormsApplicationDelegate<MvxFormsMacSetup<Core.App, FormsApp>, Core.App, FormsApp>
    {
        public AppDelegate()
        {
        }
    }
}
