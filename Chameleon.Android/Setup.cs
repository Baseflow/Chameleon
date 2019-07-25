using System;
using System.Net.Http;
using Acr.UserDialogs;
using Android.App;
using Chameleon.Core;
using MvvmCross;
using MvvmCross.Forms.Platforms.Android.Core;
using MvvmCross.IoC;
using MvvmCross.Platforms.Android;
using Xamarin.Android.Net;

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
