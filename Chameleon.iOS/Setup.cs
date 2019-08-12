using Chameleon.Core;
using MvvmCross.Forms.Platforms.Ios.Core;

namespace Chameleon.iOS
{
    public class Setup : MvxFormsIosSetup<Core.App, FormsApp>
    {
        protected override void InitializeIoC()
        {
            base.InitializeIoC();

            //Mvx.IoCProvider.RegisterSingleton<HttpMessageHandler>(new NSUrlSessionHandler());
        }
    }
}
