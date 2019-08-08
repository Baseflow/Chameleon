using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using MvvmCross.Forms.Platforms.Android.Views;
using Chameleon.Core;
using MediaManager;
using Xamarin.Forms;
using Plugin.CurrentActivity;
using Intent = global::Android.Content.Intent;
using Uri = global::Android.Net.Uri;
using MvvmCross;
using MvvmCross.Navigation;
using Chameleon.Core.ViewModels;
using Android.Provider;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Chameleon.Android
{
    [Activity(
        Label = "@string/app_name",
        Icon = "@mipmap/icon",
        Theme = "@style/MainTheme.Launcher",
        MainLauncher = true,
        ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    [IntentFilter(new[] { Intent.ActionSend, Intent.ActionSendMultiple }, Categories = new[] { Intent.CategoryDefault }, DataMimeTypes = new[] { "video/*", "audio/*" }, Label = "@string/app_name")]
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

            CrossMediaManager.Current.Init(this);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);

            HandleIntent();
        }

        private async void HandleIntent()
        {
            var receivedIntent = Intent;
            var receivedAction = receivedIntent.Action;
            var receivedType = receivedIntent.Type;

            if (receivedAction == Intent.ActionSend)
            {
                string path = "";

                if (receivedType.StartsWith("video/") || receivedType.StartsWith("audio/"))
                {
                    var receiveUri = receivedIntent.GetParcelableExtra(Intent.ExtraStream) as Uri;
                    path = receiveUri.ToString();
                }
                if (!string.IsNullOrEmpty(path))
                {
                    await CrossMediaManager.Current.Play(path);
                    await Mvx.IoCProvider.Resolve<IMvxNavigationService>().Navigate<PlayerViewModel>();
                }
            }
            else if(receivedAction == Intent.ActionSendMultiple)
            {
                IEnumerable<string> mediaUrls = null;

                if (receivedType.StartsWith("video/") || receivedType.StartsWith("audio/"))
                {
                    var receiveUris = receivedIntent.GetParcelableArrayListExtra(Intent.ExtraStream);
                    mediaUrls = receiveUris.Cast<Uri>().Select(x => x.ToString());
                }
                if (mediaUrls != null)
                {
                    await CrossMediaManager.Current.Play(mediaUrls);
                    await Mvx.IoCProvider.Resolve<IMvxNavigationService>().Navigate<PlayerViewModel>();
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
