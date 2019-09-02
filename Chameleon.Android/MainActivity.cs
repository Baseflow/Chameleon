
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Chameleon.Core;
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
        ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
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

            CrossMediaManager.Current.Init(this);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
        }

        protected override void OnResume()
        {
            base.OnResume();
            HandleIntent();
        }

        private async void HandleIntent()
        {
            //TODO: Use next mediamanager update
            //if (await CrossMediaManager.Android.PlayFromIntent(Intent))
            if (await HandleIntent(Intent))
            {
                await Mvx.IoCProvider.Resolve<IMvxNavigationService>().Navigate<PlayerViewModel>();
            }
        }

        public async Task<bool> HandleIntent(Intent intent)
        {
            if (intent == null)
                return false;

            var action = intent.Action;
            var type = intent.Type;

            if (action == Intent.ActionView)
            {
                string path = "";

                if (type.StartsWith("video/") || type.StartsWith("audio/"))
                {
                    path = intent.DataString;
                }
                if (!string.IsNullOrEmpty(path))
                {
                    await CrossMediaManager.Current.Play(path);
                    return true;
                }
            }
            else if (action == Intent.ActionSend)
            {
                string path = "";

                if (type.StartsWith("video/") || type.StartsWith("audio/"))
                {
                    var receivedUri = intent.GetParcelableExtra(Intent.ExtraStream) as global::Android.Net.Uri;
                    path = receivedUri?.ToString();
                }
                if (!string.IsNullOrEmpty(path))
                {
                    await CrossMediaManager.Current.Play(path);
                    return true;
                }
            }
            else if (action == Intent.ActionSendMultiple)
            {
                IEnumerable<string> mediaUrls = null;

                if (type.StartsWith("video/") || type.StartsWith("audio/"))
                {
                    var receivedUris = intent.GetParcelableArrayListExtra(Intent.ExtraStream);
                    mediaUrls = receivedUris.Cast<global::Android.Net.Uri>().Select(x => x?.ToString());
                }
                if (mediaUrls != null)
                {
                    await CrossMediaManager.Current.Play(mediaUrls);
                    return true;
                }
            }
            return false;
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] global::Android.Content.PM.Permission[] grantResults)
        {
            Plugin.Permissions.PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}
