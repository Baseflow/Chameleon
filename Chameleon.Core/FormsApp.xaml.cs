using System;
using Chameleon.Services;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using Microsoft.AppCenter.Distribute;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Chameleon.Core
{
    public partial class FormsApp : Application
    {
        public FormsApp()
        {
            InitializeComponent();
        }

        protected override void OnStart()
        {
            
            /*AppCenter.Start($"android={AppSettings.AndroidAppcenterSecret}; uwp={AppSettings.UwpAppcenterSecret}; ios={AppSettings.IosAppcenterSecret}",
                  typeof(Analytics), typeof(Crashes), typeof(Distribute));*/

            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
