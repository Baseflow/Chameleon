using System;
using System.ComponentModel;
using Chameleon.Core.ViewModels;
using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
using Xamarin.Forms;

namespace Chameleon.Core.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    [MvxTabbedPagePresentation(WrapInNavigationPage = true, Icon = "tab_bar_settings")]
    public partial class SettingsPage : MvxContentPage<SettingsViewModel>
    {
        public SettingsPage()
        {
            InitializeComponent();
        }

        private async void ViewCell_TappedOpenSource(object sender, EventArgs e)
        {
            await ViewModel.OpenSourceCommand.ExecuteAsync();
        }

        private async void ViewCell_TappedPlaybackSettings(object sender, EventArgs e)
        {
            await ViewModel.PlaybackSettingsCommand.ExecuteAsync();
        }

        protected override void OnAppearing()
        {
            IconImageSource = ImageSource.FromFile("tab_bar_settings_active.png");
            base.OnAppearing();
        }
        protected override void OnDisappearing()
        {
            IconImageSource = ImageSource.FromFile("tab_bar_settings.png");
            base.OnDisappearing();
        }

        private async void ViewCell_TappedTheming(object sender, EventArgs e)
        {
            await ViewModel.ThemingCommand.ExecuteAsync();
        }
    }
}
