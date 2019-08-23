using System.ComponentModel;
using Chameleon.Core.ViewModels;
using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
using MvvmCross.Presenters;
using MvvmCross.Presenters.Attributes;
using MvvmCross.ViewModels;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Chameleon.Core.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class PlayerPage : MvxContentPage<PlayerViewModel>, IMvxOverridePresentationAttribute
    {
        //PlayerPortraitView PortraitView = new PlayerPortraitView();
        //PlayerLandscapeView LandscapeView = new PlayerLandscapeView();

        public PlayerPage()
        {
            InitializeComponent();
            //UpdateContent(DeviceDisplay.MainDisplayInfo.Orientation);
        }

        public MvxBasePresentationAttribute PresentationAttribute(MvxViewModelRequest request)
        {
            //TODO: Detect when the player starts from another app
            //if(Application.Current?.MainPage?.Navigation?.NavigationStack?.Count > 0)
                return new MvxModalPresentationAttribute() { WrapInNavigationPage = true };
            //else
            //    return new MvxContentPagePresentationAttribute() { WrapInNavigationPage = false };
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            //DeviceDisplay.MainDisplayInfoChanged += DeviceDisplay_MainDisplayInfoChanged;
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            //DeviceDisplay.MainDisplayInfoChanged -= DeviceDisplay_MainDisplayInfoChanged;
        }

        private void DeviceDisplay_MainDisplayInfoChanged(object sender, DisplayInfoChangedEventArgs e)
        {
            var orientation = e.DisplayInfo.Orientation;
            UpdateContent(orientation);
        }

        private void UpdateContent(DisplayOrientation orientation)
        {
            //if (orientation == DisplayOrientation.Portrait && Content != PortraitView)
            //    Content = PortraitView;
            //else if (orientation == DisplayOrientation.Landscape && Content != LandscapeView)
            //    Content = LandscapeView;
        }
    }
}
