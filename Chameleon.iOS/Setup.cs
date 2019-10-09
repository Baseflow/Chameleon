using Chameleon.Core;
using Chameleon.Core.Helpers;
using MvvmCross;
using MvvmCross.Forms.Platforms.Ios.Core;
using MvvmCross.IoC;

namespace Chameleon.iOS
{
    public class Setup : MvxFormsIosSetup<Core.App, FormsApp>
    {
        protected override void InitializeFirstChance()
        {
            base.InitializeFirstChance();
            Mvx.IoCProvider.LazyConstructAndRegisterSingleton<IThemeService, ThemeServiceBase>();
        }
    }
}
