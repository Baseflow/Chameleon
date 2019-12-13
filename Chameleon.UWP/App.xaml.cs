using Chameleon.Core;
using MediaManager;
using MvvmCross.Forms.Platforms.Uap.Core;
using MvvmCross.Forms.Platforms.Uap.Views;
using Xamarin.Forms;

namespace Chameleon.UWP
{
    sealed partial class App
    {
        public App()
        {
            FFImageLoading.Forms.Platform.CachedImageRenderer.Init();
            CrossMediaManager.Current.Init();
            InitializeComponent();
        }
    }

    public abstract class ChameleonApp : MvxWindowsApplication<MvxFormsWindowsSetup<Core.App, FormsApp>, Core.App, FormsApp, MainPage>
    {
    }
}
