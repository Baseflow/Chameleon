using System.Collections.Generic;
using System.Reflection;
using Acr.UserDialogs;
using Chameleon.Core;
using Chameleon.Core.Helpers;
using MvvmCross;
using MvvmCross.Forms.Platforms.Android.Core;
using MvvmCross.IoC;
using MvvmCross.Platforms.Android;

namespace Chameleon.Android
{
    public class Setup : MvxFormsAndroidSetup<Core.App, FormsApp>
    {
        protected override void InitializeFirstChance()
        {
            base.InitializeFirstChance();
            UserDialogs.Init(() => Mvx.IoCProvider.Resolve<IMvxAndroidCurrentTopActivity>().Activity);
            ActionSheetConfig.DefaultAndroidStyleId = Android.Resource.Style.MainTheme_BottomSheet;
        }

        protected override void InitializeLastChance()
        {
            base.InitializeLastChance();
            Mvx.IoCProvider.LazyConstructAndRegisterSingleton<IThemeService, ThemeService>();
        }

        protected override IEnumerable<Assembly> AndroidViewAssemblies =>
            new List<Assembly>(base.AndroidViewAssemblies)
            {
                typeof(global::Android.Support.Design.Widget.BottomNavigationView).Assembly
            };
    }
}
