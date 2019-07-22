using System;
using System.Net.Http;
using Android.App;
using Chameleon.Core;
using MvvmCross;
using MvvmCross.Forms.Platforms.Android.Core;
using MvvmCross.IoC;
using Xamarin.Android.Net;

namespace Chameleon.Android
{
    public class Setup : MvxFormsAndroidSetup<Core.App, FormsApp>
    {
        protected override void InitializeIoC()
        {
            base.InitializeIoC();

            //Mvx.IoCProvider.RegisterSingleton<HttpMessageHandler>(new AndroidClientHandler());
        }
    }
}
