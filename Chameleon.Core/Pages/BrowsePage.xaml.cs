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
    [MvxTabbedPagePresentation(WrapInNavigationPage = true, Icon = "tab_bar_browse")]
    public partial class BrowsePage : MvxContentPage<BrowseViewModel>
    {
        public BrowsePage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            IconImageSource = ImageSource.FromFile("tab_bar_browse_active");
            base.OnAppearing();
        }
        protected override void OnDisappearing()
        {
            IconImageSource = ImageSource.FromFile("tab_bar_browse");
            base.OnDisappearing();
        }
    }
}
