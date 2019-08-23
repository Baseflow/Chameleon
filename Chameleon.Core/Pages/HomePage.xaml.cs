using System.ComponentModel;
using Chameleon.Core.ViewModels;
using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
using Xamarin.Forms;

namespace Chameleon.Core.Pages
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    [MvxTabbedPagePresentation(WrapInNavigationPage = true, Icon = "tab_bar_icon_home")]
    public partial class HomePage : MvxContentPage<HomeViewModel>
    {
        public HomePage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            IconImageSource = ImageSource.FromFile("tab_bar_icon_home_active");
            base.OnAppearing();
        }
        protected override void OnDisappearing()
        {
            IconImageSource = ImageSource.FromFile("tab_bar_icon_home");
            base.OnDisappearing();
        }
    }
}
