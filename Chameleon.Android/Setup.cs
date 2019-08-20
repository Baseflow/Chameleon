using System.Collections.Generic;
using System.Reflection;
using Acr.UserDialogs;
using Chameleon.Core;
using Chameleon.Services;
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
            ActionSheetConfig.DefaultAndroidStyleId = Resource.Style.MainTheme_Alert;
            //Mvx.IoCProvider.RegisterSingleton<HttpMessageHandler>(new AndroidClientHandler());
        }

        protected override IEnumerable<Assembly> AndroidViewAssemblies =>
            new List<Assembly>(base.AndroidViewAssemblies)
            {
                typeof(global::Android.Support.Design.Widget.BottomNavigationView).Assembly
            };
    }
}
