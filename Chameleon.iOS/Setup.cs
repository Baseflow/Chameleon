using System.Net.Http;
using Chameleon.Core;
using MvvmCross;
using MvvmCross.Forms.Platforms.Ios.Core;
using MvvmCross.IoC;

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
