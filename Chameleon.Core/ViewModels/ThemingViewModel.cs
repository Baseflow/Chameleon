using MvvmCross.Logging;
using MvvmCross.Navigation;
using MediaManager.Queue;
using MvvmCross.Commands;
using Xamarin.Forms;
using System;
using System.Collections.Generic;
using Chameleon.Core.Resources;
using System.Linq;

namespace Chameleon.Core.ViewModels
{
    public class ThemingViewModel : BaseViewModel
    {
        public ThemingViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService) : base(logProvider, navigationService)
        {
        }

        private ImageSource _themeDarkImage = ImageSource.FromFile("theme_dark");
        public ImageSource ThemeDarkImage
        {
            get => _themeDarkImage;
            set => SetProperty(ref _themeDarkImage, value);
        }

        private ImageSource _themeLightImage = ImageSource.FromFile("theme_light");
        public ImageSource ThemeLightImage
        {
            get => _themeLightImage;
            set => SetProperty(ref _themeLightImage, value);
        }

        private IMvxCommand _themeDarkCommand;
        public IMvxCommand ThemeDarkCommand => _themeDarkCommand ?? (_themeDarkCommand = new MvxCommand(ThemeDark));

        private IMvxCommand _themeLightCommand;
        public IMvxCommand ThemeLigthCommand => _themeLightCommand ?? (_themeLightCommand = new MvxCommand(ThemeLight));

        private void ThemeDark()
        {
            Application.Current.Resources.Clear();
            var style = new Styles();
            style.MergedDictionaries.Add(new DarkColors());

            Application.Current.Resources = style;

            ThemeDarkImage = ImageSource.FromFile("theme_dark_on");
            ThemeLightImage = ImageSource.FromFile("theme_light");
        }

        private void ThemeLight()
        {
            Application.Current.Resources.Clear();
            var style = new Styles();
            style.MergedDictionaries.Add(new LightColors());

            Application.Current.Resources = style;

            ThemeLightImage = ImageSource.FromFile("theme_light_on");

            ThemeDarkImage = ImageSource.FromFile("theme_dark");
        }



        //public override void ViewAppearing()
        //{
        //    base.ViewAppearing();

        //    ResourceDictionary Source = MergedDictionaries("Resources/LightTheme.xaml");

        //    Application.Current.Resources;

        //    Application.Current.Resources

                

        //    var AppRequestedTheme = Application.Current.Resources.MergedDictionaries;
        //    if (AppRequestedTheme == DarkColors)
        //        resources = new Xamarin.Forms.ResourceDictionary.DarkThemeResources();
        //    if (ThemeDark())
        //        if (true)
        //        {
        //            FavoriteImage = ImageSource.FromFile("playback_controls_favorite_off");
        //        }
        //        else
        //        {
        //            FavoriteImage = ImageSource.FromFile("playback_controls_favorite_on");
        //        }
        //}
    }
}
