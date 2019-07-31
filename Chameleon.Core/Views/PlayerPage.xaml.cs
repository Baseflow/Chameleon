using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chameleon.Core.ViewModels;
using MvvmCross.Forms.Views;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Chameleon.Core.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class PlayerPage : MvxContentPage<PlayerViewModel>
    {
        //PlayerPortraitView PortraitView = new PlayerPortraitView();
        //PlayerLandscapeView LandscapeView = new PlayerLandscapeView();

        public PlayerPage()
        {
            InitializeComponent();
            //UpdateContent(DeviceDisplay.MainDisplayInfo.Orientation);
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
            if (orientation == DisplayOrientation.Portrait && Content != PortraitView)
                Content = PortraitView;
            else if (orientation == DisplayOrientation.Landscape && Content != LandscapeView)
                Content = LandscapeView;
        }
    }
}
