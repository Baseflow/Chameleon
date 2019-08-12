using Xamarin.Forms;

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
            // Handle when your app starts   
            /*AppCenter.Start($"android={AppSettings.AndroidAppcenterSecret}; uwp={AppSettings.UwpAppcenterSecret}; ios={AppSettings.IosAppcenterSecret}",
                  typeof(Analytics), typeof(Crashes), typeof(Distribute));*/
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
