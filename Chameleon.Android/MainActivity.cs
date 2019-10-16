using System;
using Android.App;
using Android.Content.PM;
using Android.Content.Res;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Chameleon.Core;
using Chameleon.Core.Helpers;
using Chameleon.Core.ViewModels;
using MediaManager;
using MvvmCross;
using MvvmCross.Forms.Platforms.Android.Views;
using MvvmCross.Navigation;
using Plugin.CurrentActivity;
using Xamarin.Forms;
using Intent = global::Android.Content.Intent;

namespace Chameleon.Android
{
    [Activity(
        Label = "@string/app_name",
        Icon = "@mipmap/icon",
        Theme = "@style/MainTheme.Launcher",
        MainLauncher = true,
        ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode)]
    [IntentFilter(new[] { Intent.ActionSend, Intent.ActionSendMultiple, Intent.ActionView }, Categories = new[] { Intent.CategoryDefault, Intent.CategoryBrowsable, Intent.CategoryAppMusic }, DataMimeTypes = new[] { "video/*", "audio/*" }, Label = "@string/app_name")]
    public class MainActivity : MvxFormsAppCompatActivity<Setup, Core.App, FormsApp>
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            Forms.SetFlags("CollectionView_Experimental");
            CrossCurrentActivity.Current.Init(this, savedInstanceState);

            TabLayoutResource = Android.Resource.Layout.Tabbar;
            ToolbarResource = Android.Resource.Layout.Toolbar;
            SetTheme(Android.Resource.Style.MainTheme);

            base.OnCreate(savedInstanceState);

            CrossCurrentActivity.Current.Init(this, savedInstanceState);
            CrossMediaManager.Current.Init(this);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
        }

        protected override void OnStart()
        {
            base.OnStart();
            UpdateTheme(Resources.Configuration);
        }

        protected override void OnResume()
        {
            base.OnResume();
            HandleIntent();
            UpdateTheme(Resources.Configuration);
        }

        private async void HandleIntent()
        {
            if (await CrossMediaManager.Android.PlayFromIntent(Intent))
            {
                await Mvx.IoCProvider.Resolve<IMvxNavigationService>().Navigate<PlayerViewModel>();
            }
        }

        public override void OnConfigurationChanged(Configuration newConfig)
        {
            base.OnConfigurationChanged(newConfig);
            UpdateTheme(newConfig);
        }

        private void UpdateTheme(Configuration newConfig)
        {
            if (Build.VERSION.SdkInt >= BuildVersionCodes.Froyo)
            {
                var uiModeFlags = newConfig.UiMode & UiMode.NightMask;
                switch (uiModeFlags)
                {
                    case UiMode.NightYes:
                        Mvx.IoCProvider.Resolve<IThemeService>().UpdateTheme(Core.Models.ThemeMode.Dark);
                        //AppCompatDelegate.DefaultNightMode = AppCompatDelegate.ModeNightYes;
                        break;
                    case UiMode.NightNo:
                        Mvx.IoCProvider.Resolve<IThemeService>().UpdateTheme(Core.Models.ThemeMode.Light);
                        break;
                    default:
                        throw new NotSupportedException($"UiMode {uiModeFlags} not supported");
                }
            }
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] global::Android.Content.PM.Permission[] grantResults)
        {
            Plugin.Permissions.PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}
