using Acr.UserDialogs;
using Chameleon.Core;
using MvvmCross;
using MvvmCross.Forms.Platforms.Android.Core;
using MvvmCross.Platforms.Android;

namespace Chameleon.Android
{
    public class Setup : MvxFormsAndroidSetup<Core.App, FormsApp>
    {
        protected override void InitializeIoC()
        {
            base.InitializeIoC();

            UserDialogs.Init(() => Mvx.IoCProvider.Resolve<IMvxAndroidCurrentTopActivity>().Activity);
            //Mvx.IoCProvider.RegisterSingleton<HttpMessageHandler>(new AndroidClientHandler());
        }
    }
}
